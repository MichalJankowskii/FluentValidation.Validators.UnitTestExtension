using FluentValidation.Validators.UnitTestExtension.Tests.Helpers;
using FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers;
using Xunit;
using Xunit.Sdk;

namespace FluentValidation.Validators.UnitTestExtension.Tests.ValidatorsVerifiers
{
    public class ChildValidatorVerifierTest
    {
        [Fact]
        public void Given_DifferentTypeValidator_When_Verifying_Then_ValidationMustFail()
        {
            // Arrange
            var otherValidator = new FakePropertyValidator();
            var verifier = new ChildValidatorVerifier<FakePropertyValidator>();

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(otherValidator), "(wrong type)");
        }

        [Fact]
        public void Given_CorrectValidatorWithDifferentChildValidatorType_When_Verifying_Then_ValidationFail()
        {
            // Arrange
            var childValidatorAdaptor = new ChildValidatorAdaptor(new OtherFakeValidator());
            var verifier = new ChildValidatorVerifier<FakeValidator>();

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(childValidatorAdaptor), "(ValidatorType property)");
        }

        [Fact]
        public void Given_CorrectValidatorWithSameValidatorType_When_Verifying_Then_ValidationPass()
        {
            // Arrange
            var childValidatorAdaptor = new ChildValidatorAdaptor(new FakeValidator());
            var verifier = new ChildValidatorVerifier<FakeValidator>();

            // Act & Assert
            AssertExtension.NotThrows(() => verifier.Verify(childValidatorAdaptor));
        }

        private class OtherFakeValidator : FakeValidator
        {
        }
    }
}
