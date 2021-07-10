namespace FluentValidation.Validators.UnitTestExtension.Tests.ValidatorsVerifiers
{
    using Helpers;
    using Helpers.Fakes;
    using ValidatorVerifiers;
    using Xunit;
    using Xunit.Sdk;

    public class MaximumLengthValidatorVerifierTest
    {
        [Fact]
        public void Given_DifferentValidatorType_When_Verifying_Then_ValidationMustFail()
        {
            // Arrange
            var otherValidator = new FakePropertyValidator();
            var verifier = new MaximumLengthValidatorVerifier<object>(10);

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(otherValidator), "(wrong type)");
        }

        [Fact]
        public void Given_CorrectValidatorWithDifferentValue_When_Verifying_Then_ValidationFail()
        {
            // Arrange
            var exactValidatorVerifier = new MaximumLengthValidator<object>(10);
            var verifier = new MaximumLengthValidatorVerifier<object>(1);

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(exactValidatorVerifier), "(Max property)");
        }

        [Fact]
        public void Given_CorrectValidator_When_Verifying_Then_ValidationPass()
        {
            // Arrange
            var exactValidatorVerifier = new MaximumLengthValidator<object>(10);
            var verifier = new MaximumLengthValidatorVerifier<object>(10);

            // Act & Assert
            AssertExtension.NotThrows(() => verifier.Verify(exactValidatorVerifier));
        }
    }
}
