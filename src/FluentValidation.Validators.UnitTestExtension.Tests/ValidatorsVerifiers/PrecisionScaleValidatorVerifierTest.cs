namespace FluentValidation.Validators.UnitTestExtension.Tests.ValidatorsVerifiers
{
    using Helpers;
    using Helpers.Fakes;
    using ValidatorVerifiers;
    using Xunit;
    using Xunit.Sdk;

    public class PrecisionScaleValidatorVerifierTest
    {
        [Fact]
        public void Given_DifferentValidatorType_When_Verifying_Then_ValidationMustFail()
        {
            // Arrange
            var otherValidator = new FakePropertyValidator();
            var verifier = new PrecisionScaleValidatorVerifier<PrecisionScaleValidator<object>, object>(1, 1, false);

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(otherValidator), "(wrong type)");
        }

        [Fact]
        public void Given_CorrectValidatorWithDifferentScaleValue_When_Verifying_Then_ValidationFail()
        {
            // Arrange
            var scalePrecisionValidator = new PrecisionScaleValidator<object>(3, 1, false);
            var verifier = new PrecisionScaleValidatorVerifier<PrecisionScaleValidator<object>, object>(3, 2, false);

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(scalePrecisionValidator), "(Scale property)");
        }

        [Fact]
        public void Given_CorrectValidatorWithDifferentPrecisionValue_When_Verifying_Then_ValidationFail()
        {
            // Arrange
            var scalePrecisionValidator = new PrecisionScaleValidator<object>(3, 1, false);
            var verifier = new PrecisionScaleValidatorVerifier<PrecisionScaleValidator<object>, object>(2, 1, false);

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(scalePrecisionValidator),
                "(Precision property)");
        }

        [Fact]
        public void Given_CorrectValidatorWithDifferentIgnoreTrailingZerosValue_When_Verifying_Then_ValidationFail()
        {
            // Arrange
            var scalePrecisionValidator = new PrecisionScaleValidator<object>(2, 1, true);
            var verifier =
                new PrecisionScaleValidatorVerifier<PrecisionScaleValidator<object>, object>(2, 1, false);

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(scalePrecisionValidator), "(IgnoreTrailingZeros property)");
        }

        [Fact]
        public void Given_CorrectValidator_When_Verifying_Then_ValidationPass()
        {
            // Arrange
            var scalePrecisionValidator = new PrecisionScaleValidator<object>(2, 1, false);
            var verifier = new PrecisionScaleValidatorVerifier<PrecisionScaleValidator<object>, object>(2, 1, false);

            // Act & Assert
            AssertExtension.NotThrows(() => verifier.Verify(scalePrecisionValidator));
        }

        [Fact]
        public void Given_CorrectValidatorWithIgnoreTrailingZeros_When_Verifying_Then_ValidationPass()
        {
            // Arrange
            var scalePrecisionValidator = new PrecisionScaleValidator<object>(2, 1, true);
            var verifier =
                new PrecisionScaleValidatorVerifier<PrecisionScaleValidator<object>, object>(2, 1, true);

            // Act & Assert
            AssertExtension.NotThrows(() => verifier.Verify(scalePrecisionValidator));
        }
    }
}
