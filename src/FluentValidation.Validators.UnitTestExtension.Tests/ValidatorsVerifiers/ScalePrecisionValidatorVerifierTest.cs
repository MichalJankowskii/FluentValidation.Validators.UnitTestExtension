namespace FluentValidation.Validators.UnitTestExtension.Tests.ValidatorsVerifiers
{
    using Helpers;
    using Helpers.Fakes;
    using ValidatorVerifiers;
    using Xunit;
    using Xunit.Sdk;

    public class ScalePrecisionValidatorVerifierTest
    {
        [Fact]
        public void Given_DifferentValidatorType_When_Verifying_Then_ValidationMustFail()
        {
            // Arrange
            var otherValidator = new FakePropertyValidator();
            var verifier = new ScalePrecisionValidatorVerifier<ScalePrecisionValidator>(1, 1);

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(otherValidator), "(wrong type)");
        }

        [Fact]
        public void Given_CorrectValidatorWithDifferentScaleValue_When_Verifying_Then_ValidationFail()
        {
            // Arrange
            var scalePrecisionValidator = new ScalePrecisionValidator(1, 3);
            var verifier = new ScalePrecisionValidatorVerifier<ScalePrecisionValidator>(2, 3);

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(scalePrecisionValidator),
                "(Scale property)");
        }

        [Fact]
        public void Given_CorrectValidatorWithDifferentPrecisionValue_When_Verifying_Then_ValidationFail()
        {
            // Arrange
            var scalePrecisionValidator = new ScalePrecisionValidator(1, 3);
            var verifier = new ScalePrecisionValidatorVerifier<ScalePrecisionValidator>(1, 2);

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(scalePrecisionValidator),
                "(Precision property)");
        }

        [Fact]
        public void Given_CorrectValidatorWithDifferentIgnoreTrailingZerosValue_When_Verifying_Then_ValidationFail()
        {
            // Arrange
            var scalePrecisionValidator = new ScalePrecisionValidator(1, 2) {IgnoreTrailingZeros = true};
            var verifier =
                new ScalePrecisionValidatorVerifier<ScalePrecisionValidator>(1, 2) {IgnoreTrailingZeros = false};

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(scalePrecisionValidator),
                "(IgnoreTrailingZeros property)");
        }

        [Fact]
        public void Given_CorrectValidator_When_Verifying_Then_ValidationPass()
        {
            // Arrange
            var scalePrecisionValidator = new ScalePrecisionValidator(1, 2);
            var verifier = new ScalePrecisionValidatorVerifier<ScalePrecisionValidator>(1, 2);

            // Act & Assert
            AssertExtension.NotThrows(() => verifier.Verify(scalePrecisionValidator));
        }

        [Fact]
        public void Given_CorrectValidatorWithIgnoreTrailingZeros_When_Verifying_Then_ValidationPass()
        {
            // Arrange
            var scalePrecisionValidator = new ScalePrecisionValidator(1, 2) {IgnoreTrailingZeros = true};
            var verifier =
                new ScalePrecisionValidatorVerifier<ScalePrecisionValidator>(1, 2) {IgnoreTrailingZeros = true};

            // Act & Assert
            AssertExtension.NotThrows(() => verifier.Verify(scalePrecisionValidator));
        }
    }
}
