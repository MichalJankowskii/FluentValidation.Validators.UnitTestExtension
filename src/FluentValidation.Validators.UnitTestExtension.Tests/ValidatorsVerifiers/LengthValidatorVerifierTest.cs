using FluentValidation.Validators.UnitTestExtension.Tests.Helpers;
using FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers;
using Xunit;
using Xunit.Sdk;

namespace FluentValidation.Validators.UnitTestExtension.Tests.ValidatorsVerifiers
{
    public class LengthValidatorVerifierTest
    {
        [Fact]
        public void Given_DifferentValidatorType_When_Verifying_Then_ValidationMustFail()
        {
            // Arrange
            var otherValidator = new FakePropertyValidator();
            var verifier = new LengthValidatorVerifier<FakeLengthValidator>(1, 10);

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(otherValidator), "(wrong type)");
        }

        [Fact]
        public void Given_CorrectValidatorWithDifferentMinValue_When_Verifying_Then_ValidationFail()
        {
            // Arrange
            var lengthValidator = new FakeLengthValidator {Min = 10, Max = 10};
            var verifier = new LengthValidatorVerifier<FakeLengthValidator>(1, 10);

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(lengthValidator),
                "(Min property)");
        }

        [Fact]
        public void Given_CorrectValidatorWithDifferentMaxValue_When_Verifying_Then_ValidationFail()
        {
            // Arrange
            var lengthValidator = new FakeLengthValidator {Min = 1, Max = 100};
            var verifier = new LengthValidatorVerifier<FakeLengthValidator>(1, 10);

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(lengthValidator),
                "(Max property)");
        }

        [Fact]
        public void Given_CorrectValidator_When_Verifying_Then_ValidationPass()
        {
            // Arrange
            var lengthValidator = new FakeLengthValidator {Min = 1, Max = 10};
            var verifier = new LengthValidatorVerifier<FakeLengthValidator>(1, 10);

            // Act & Assert
            AssertExtension.NotThrows(() => verifier.Verify(lengthValidator));
        }

        private class FakeLengthValidator : FakePropertyValidator, ILengthValidator
        {
            public int Min { get; set; }
            public int Max { get; set; }
        }
    }
}
