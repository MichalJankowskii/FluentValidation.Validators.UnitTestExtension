namespace FluentValidation.Validators.UnitTestExtension.Tests.ValidatorsVerifiers
{
    using Helpers;
    using Helpers.Fakes;
    using ValidatorVerifiers;
    using Xunit;
    using Xunit.Sdk;

    public class BetweenValidatorVerifierTest
    {
        [Fact]
        public void Given_DifferentValidatorType_When_Verifying_Then_ValidationMustFail()
        {
            // Arrange
            var otherValidator = new FakePropertyValidator();
            var verifier = new BetweenValidatorVerifier<FakeBetweenValidator>(1, 10);

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(otherValidator), "(wrong type)");
        }

        [Fact]
        public void Given_CorrectValidatorWithDifferentFromValue_When_Verifying_Then_ValidationFail()
        {
            // Arrange
            var betweenValidator = new FakeBetweenValidator { From = 10, To = 10 };
            var verifier = new BetweenValidatorVerifier<FakeBetweenValidator>(1, 10);

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(betweenValidator),
                "(From property)");
        }

        [Fact]
        public void Given_CorrectValidatorWithDifferentToValue_When_Verifying_Then_ValidationFail()
        {
            // Arrange
            var betweenValidator = new FakeBetweenValidator { From = 10, To = 10 };
            var verifier = new BetweenValidatorVerifier<FakeBetweenValidator>(10, 20);

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(betweenValidator),
                "(To property)");
        }

        [Fact]
        public void Given_CorrectValidator_When_Verifying_Then_ValidationPass()
        {
            // Arrange
            var betweenValidator = new FakeBetweenValidator { From = 1, To = 10 };
            var verifier = new BetweenValidatorVerifier<FakeBetweenValidator>(1, 10);

            // Act & Assert
            AssertExtension.NotThrows(() => verifier.Verify(betweenValidator));
        }
    }
}
