$fluentAssertionConfigurations = Get-Content "$PSScriptRoot/sourceConfiguration/Ver_FluentAssertion.txt"
$fluentValidatorConfigurations = Get-Content "$PSScriptRoot/sourceConfiguration/Ver_FluentValidator.txt"
$fluentValidatorExtConfigurations = Get-Content "$PSScriptRoot/sourceConfiguration/Ver_FluentValidator_ext.txt"
$nugetFile = "$PSScriptRoot/sourceConfiguration/packages.config"
$soultionFile = "$PSScriptRoot/sourceConfiguration/FluentValidation.Validators.UnitTestExtension.NuGet.Tests.csproj"
$targetFolder = "$PSScriptRoot/testConfigurations"

$configurations = New-Object System.Collections.ArrayList

function SaveNewNuGetConfiguration($initialConf, $fluentValidatorExtVersion, $fluentAssertionVersion, $fluentValidatorVersion)
{
  [xml] $xml = Get-Content $initialConf
  $target = $xml.SelectSingleNode('//packages/package[@id="FluentValidation.Validators.UnitTestExtension"]')
  $target.SetAttribute("version", "$fluentValidatorExtVersion")
  $target = $xml.SelectSingleNode('//packages/package[@id="FluentAssertions"]')
  $target.SetAttribute("version", "$fluentAssertionVersion")
  $target = $xml.SelectSingleNode('//packages/package[@id="FluentValidation"]')
  $target.SetAttribute("version", "$fluentValidatorVersion")

  $key = GenerateConfigurationName $fluentValidatorExtVersion $fluentAssertionVersion $fluentValidatorVersion
  $xml.Save("$PSScriptRoot/testConfigurations/packages_"+$key+".config")
  return $key
}

function SaveNewCsProjConfiguration($initialConf, $fluentValidatorExtVersion, $fluentAssertionVersion, $fluentValidatorVersion)
{
    $proj = [xml](Get-Content $initialConf)
    AddReference $proj "..\packages\FluentAssertions.$fluentAssertionVersion\lib\net45\FluentAssertions.dll" "FluentAssertions"
    AddReference $proj "..\packages\FluentAssertions.$fluentAssertionVersion\lib\net45\FluentAssertions.Core.dll" "FluentAssertions.Core"
    AddReference $proj "..\packages\FluentValidation.$fluentValidatorVersion\lib\net45\FluentValidation.dll" "FluentValidation"
    AddReference $proj "..\packages\FluentValidation.Validators.UnitTestExtension.$fluentValidatorExtVersion\lib\net452\FluentValidation.Validators.UnitTestExtension.dll" "FluentValidation.Validators.UnitTestExtension"
    $key = GenerateConfigurationName $fluentValidatorExtVersion $fluentAssertionVersion $fluentValidatorVersion
    $proj.Save("$PSScriptRoot/testConfigurations/proj_"+$key+".csproj")
}

function GenerateConfigurationName($fluentValidatorExtVersion, $fluentAssertionVersion, $fluentValidatorVersion)
{
  return $fluentValidatorExtVersion+"_FA"+$fluentAssertionVersion+"_FV"+$fluentValidatorVersion
}

function AddReference($proj, [String]$dllRef, [String]$refName) 
{
  [System.Console]::WriteLine("")
  [System.Console]::WriteLine("AddReference {0} on {1}", $refName, $path)

  # Create the following hierarchy
  #  <Reference Include='{0}'>
  #     <HintPath>{1}</HintPath>
  #  </Reference>
  # where (0) is $refName and {1} is $dllRef

  $xmlns = "http://schemas.microsoft.com/developer/msbuild/2003"
  $itemGroup = $proj.CreateElement("ItemGroup", $xmlns);
  $proj.Project.AppendChild($itemGroup);

  $referenceNode = $proj.CreateElement("Reference", $xmlns);
  $referenceNode.SetAttribute("Include", $refName);
  $itemGroup.AppendChild($referenceNode)

  $hintPath = $proj.CreateElement("HintPath", $xmlns);
  $hintPath.InnerXml = $dllRef
  $referenceNode.AppendChild($hintPath)
}

foreach($fluentValidatorExtConf in $fluentValidatorExtConfigurations)
{
  foreach($fluentAssertionConf in $fluentAssertionConfigurations)
  {
    $conf = SaveNewNuGetConfiguration $nugetFile $fluentValidatorExtConf $fluentAssertionConf ($fluentValidatorConfigurations  | Select-Object -first 1)
    SaveNewCsProjConfiguration $soultionFile $fluentValidatorExtConf $fluentAssertionConf ($fluentValidatorConfigurations  | Select-Object -first 1)
    $configurations.Add($conf)  
  }

    foreach($fluentValidatorConf in $fluentValidatorConfigurations)
  {
    $conf = SaveNewNuGetConfiguration $nugetFile $fluentValidatorExtConf ($fluentAssertionConfigurations  | Select-Object -first 1) $fluentValidatorConf
    SaveNewCsProjConfiguration $soultionFile $fluentValidatorExtConf ($fluentAssertionConfigurations  | Select-Object -first 1) $fluentValidatorConf
    $configurations.Add($conf)  
  }
}

$stream = [System.IO.StreamWriter] "$PSScriptRoot/../.travis.yml"
$stream.WriteLine("language: csharp")
$stream.WriteLine("env:")
foreach($conf in $configurations)
{
  $stream.WriteLine("  - CONF=$conf")
}

$stream.WriteLine("script:")
$stream.WriteLine('  - "cp ./src/testConfigurations/packages_$CONF.config ./src/FluentValidation.Validators.UnitTestExtension.NuGet.Tests/packages.config"')
$stream.WriteLine('  - "cp ./src/testConfigurations/proj_$CONF.csproj ./src/FluentValidation.Validators.UnitTestExtension.NuGet.Tests/FluentValidation.Validators.UnitTestExtension.NuGet.Tests.csproj"')
$stream.WriteLine("  - sudo apt-get install -y gtk-sharp2")
$stream.WriteLine("  - nuget restore ./src/FluentValidation.Validators.UnitTestExtension.NuGet.Tests.sln")
$stream.WriteLine("  - xbuild /p:Configuration=Release ./src/FluentValidation.Validators.UnitTestExtension.NuGet.Tests.sln")
$stream.WriteLine("  - mono ./src/packages/xunit.runner.console.*/tools/xunit.console.exe ./src/FluentValidation.Validators.UnitTestExtension.NuGet.Tests/bin/Release/FluentValidation.Validators.UnitTestExtension.NuGet.Tests.dll")
$stream.close()

