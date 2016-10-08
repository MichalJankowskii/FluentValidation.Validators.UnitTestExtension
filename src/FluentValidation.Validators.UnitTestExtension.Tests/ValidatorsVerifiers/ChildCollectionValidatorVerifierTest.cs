using FluentValidation.Validators.UnitTestExtension.Tests.Helpers;
using FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers;
using Xunit;
using Xunit.Sdk;

namespace FluentValidation.Validators.UnitTestExtension.Tests.ValidatorsVerifiers
{
    public class ChildCollectionValidatorVerifierTest
    {
        [Fact]
        public void Given_DifferentTypeValidator_When_VerifyingType_Then_ValidationMustFail()
        {
            // Arrange
            var otherValidator = new FakePropertyValidator();
            var verifier = new ChildCollectionValidatorVerifier<FakePropertyValidator>();

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(otherValidator), "(wrong type)");
        }

        [Fact]
        public void Given_CorrectValidatorWithDifferentChildValidatorType_When_Verifying_Then_ValidationFail()
        {
            // Arrange
            var childCollectionValidatorAdaptor = new ChildCollectionValidatorAdaptor(new OtherFakeValidator());
            var verifier = new ChildCollectionValidatorVerifier<FakeValidator>();

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(childCollectionValidatorAdaptor), "(ChildValidatorType property)");
        }

        [Fact]
        public void Given_CorrectValidatorWithSameValidatorType_When_Verifying_Then_ValidationPass()
        {
            // Arrange
            var childCollectionValidatorAdaptor = new ChildCollectionValidatorAdaptor(new FakeValidator());
            var verifier = new ChildCollectionValidatorVerifier<FakeValidator>();

            // Act & Assert
            AssertExtension.NotThrows(() => verifier.Verify(childCollectionValidatorAdaptor));
        }

        private class OtherFakeValidator : FakeValidator
        {
        }
    }
}
