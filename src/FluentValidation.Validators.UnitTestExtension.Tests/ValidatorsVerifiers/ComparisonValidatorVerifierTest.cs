using System.Reflection;
using FluentValidation.Validators.UnitTestExtension.Tests.Helpers;
using FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers;
using Xunit;
using Xunit.Sdk;

namespace FluentValidation.Validators.UnitTestExtension.Tests.ValidatorsVerifiers
{
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

        private class FakeComparisonValidator : FakePropertyValidator, IComparisonValidator
        {
            public Comparison Comparison { get; set; }
            public MemberInfo MemberToCompare { get; set; }
            public object ValueToCompare { get; set; }
        }
    }
}
