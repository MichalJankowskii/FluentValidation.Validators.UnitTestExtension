using FluentValidation.Validators.UnitTestExtension.Tests.Helpers;
using FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers;
using Xunit;
using Xunit.Sdk;

namespace FluentValidation.Validators.UnitTestExtension.Tests.ValidatorsVerifiers
{
    public class TypeValidatorVerifierTest
    {
        [Fact]
        public void Given_DifferentValidatorType_When_Verifying_Then_ValidationFail()
        {
            // Arrange
            var otherValidator = new OtherFakePropertyValidator();
            var verifier = new TypeValidatorVerifier<FakePropertyValidator>();

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(otherValidator), "(wrong type)");
        }

        [Fact]
        public void Given_SameValidatorTypeThanExpected_When_Verifying_Then_ValidationPass()
        {
            // Arrange
            var fakePropertyValidator = new FakePropertyValidator();
            var verifier = new TypeValidatorVerifier<FakePropertyValidator>();

            // Act & Assert
            AssertExtension.NotThrows(() => verifier.Verify(fakePropertyValidator));
        }

        private class OtherFakePropertyValidator : FakePropertyValidator
        {
        }
    }
}
