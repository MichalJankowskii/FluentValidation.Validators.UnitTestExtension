# FluentValidation.Validators.UnitTestExtension
[![Build Status](https://travis-ci.org/MichalJankowskii/FluentValidation.Validators.UnitTestExtension.svg?branch=master)](https://travis-ci.org/MichalJankowskii/FluentValidation.Validators.UnitTestExtension)
[![Downloads](http://nuget-stats-api.jankowskimichal.pl/api/badges/FluentValidation.Validators.UnitTestExtension/totalDownloads)](http://nuget-stats-api.jankowskimichal.pl/api/badges/FluentValidation.Validators.UnitTestExtension/totalDownloads)

[Full documentation](https://github.com/MichalJankowskii/FluentValidation.Validators.UnitTestExtension/wiki)
##Project description
Main purpose of this small library is to extend and simplify possibilities of testing code that is using [FluentValidation](https://github.com/JeremySkinner/FluentValidation) package.
##Installation - NuGet Packages
```
Install-Package FluentValidation.Validators.UnitTestExtension
```

##Overview
This library allows you to focus on testing your implementation. You will be able to write test that will run faster, will be readable and will test only your implementation.

Please look at the following example of `PersonValidator`:
```csharp
public class PersonValidator : AbstractValidator<Person>
{
	public PersonValidator()
    {
    	RuleFor(person => person.Name).NotNull();
   	}
}
```

By using this library you can write unit test like this:
```csharp
public class PersonValidatorTests
{
	[Fact]
	public void When_PersonValidatorConstructing_Then_RulesAreConfiguredCorrectly()
	{
		var personValidator = new PersonValidator();

		personValidator.ShouldHaveRules(x => x.Name,
			BaseVerifiersSetComposer.Build()
				.AddPropertyValidatorVerifier<NotNullValidator>()
				.Create());
	}
}
```

According to [FluentValidation wiki](https://github.com/JeremySkinner/FluentValidation/wiki/g.-Testing) same code should be tested in the following way:
```csharp
public class PersonValidatorTests
{
	private PersonValidator validator;

	public void PersonValidatorTester()
	{
		validator = new PersonValidator();
	}

	[Fact]
	public void Should_have_error_when_Name_is_null() 
    {
		validator.ShouldHaveValidationErrorFor(person => person.Name, null as string); 
	}

	[Fact]
    public void Should_not_have_error_when_name_is_specified()
	{
		validator.ShouldNotHaveValidationErrorFor(person => person.Name, "Jeremy");
	}
}
```

As you can see you need to test each scenario that can fail validation with [FluentValidation](https://github.com/JeremySkinner/FluentValidation) approach. With that approach you are writing rather integration test than real unit test. Your tests will be slower and you will need to write more code.


By using this framework you will be:
- more effective in writing unit tests
- you will be testing only your code
- your test will be more robustness
- moreover you will be able to check order of validators
- and many more.

Please see [documentation](https://github.com/MichalJankowskii/FluentValidation.Validators.UnitTestExtension/wiki).
