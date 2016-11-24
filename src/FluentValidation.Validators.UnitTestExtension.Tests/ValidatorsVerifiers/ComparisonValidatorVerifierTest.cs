#region License
// MIT License
// 
// Copyright(c) 2016 Michał Jankowski (http://www.jankowskimichal.pl)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// 
// The latest version of this file can be found at https://github.com/MichalJankowskii/FluentValidation.Validators.UnitTestExtension
#endregion

namespace FluentValidation.Validators.UnitTestExtension.Tests.ValidatorsVerifiers
{
    using Helpers;
    using Helpers.Fakes;
    using ValidatorVerifiers;
    using Xunit;
    using Xunit.Sdk;

    public class ComparisonValidatorVerifierTest
    {
        [Fact]
        public void Given_DifferentValidatorType_When_Verifying_Then_ValidationMustFail()
        {
            // Arrange
            var otherValidator = new FakePropertyValidator();
            var lengthValidatorVerifier = new ComparisonValidatorVerifier<FakeComparisonValidator>(10);

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => lengthValidatorVerifier.Verify(otherValidator), "(wrong type)");
        }

        [Fact]
        public void Given_CorrectValidatorWithDifferentValueToCompare_When_Verifying_Then_ValidationFail()
        {
            // Arrange
            var comparisonValidator = new FakeComparisonValidator {ValueToCompare = 15};
            var verifier = new ComparisonValidatorVerifier<FakeComparisonValidator>(10);

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(comparisonValidator), "(ValueToCompare property)");
        }

        [Fact]
        public void Given_CorrectValidatorWithSameValueToCompare_When_Verifying_Then_ValidationPass()
        {
            // Arrange
            var comparisonValidator = new FakeComparisonValidator { ValueToCompare = 15 };
            var verifier = new ComparisonValidatorVerifier<FakeComparisonValidator>(15);

            // Act & Assert
            AssertExtension.NotThrows(() => verifier.Verify(comparisonValidator));
        }
    }
}
