namespace FluentValidation.Validators.UnitTestExtension.Tests.ValidatorsVerifiers
{
    using Helpers;
    using ValidatorVerifiers;
    using Xunit;

    public class CustomVerifierTest
    {
        [Fact]
        public void Given_AnyValidator_When_Verifying_Then_ValidationMustPass()
        {
            // Arrange
            var lengthValidator = new LengthValidator(1, 10);
            var verifier = new PlaceholderVerifier();

            // Act & Assert
            AssertExtension.NotThrows(() => verifier.Verify(lengthValidator));
        }
    }
}
