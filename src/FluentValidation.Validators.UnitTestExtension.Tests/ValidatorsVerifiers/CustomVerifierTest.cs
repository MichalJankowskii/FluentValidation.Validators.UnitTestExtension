using FluentValidation.Validators.UnitTestExtension.Tests.Helpers;
using FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers;
using Xunit;

namespace FluentValidation.Validators.UnitTestExtension.Tests.ValidatorsVerifiers
{
    public class CustomVerifierTest
    {
        [Fact]
        public void Given_AnyValidator_When_Verifying_Then_ValidationMustPass()
        {
            // Arrange
            var lengthValidator = new LengthValidator(1, 10);
            var verifier = new CustomVerifier();

            // Act & Assert
            AssertExtension.NotThrows(() => verifier.Verify(lengthValidator));
        }
    }
}
