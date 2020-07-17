namespace FluentValidation.Validators.UnitTestExtension.Tests.ValidatorsVerifiers
{
    using Helpers;
    using Helpers.Fakes;
    using ValidatorVerifiers;
    using Xunit;
    using Xunit.Sdk;

    public class ChildValidatorVerifierTest
    {
        [Fact]
        public void Given_DifferentTypeValidator_When_Verifying_Then_ValidationMustFail()
        {
            // Arrange
            var otherValidator = new FakePropertyValidator();
            var verifier = new ChildValidatorVerifier<FakePropertyValidator, int>();

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(otherValidator), "(wrong type)");
        }

        [Fact]
        public void Given_CorrectValidatorWithDifferentChildValidatorType_When_Verifying_Then_ValidationFail()
        {
            // Arrange
            var childValidatorAdaptor = new ChildValidatorAdaptor<FakeValidator<int>, int>(new OtherFakeValidator<int>(), typeof(OtherFakeValidator<int>));
            var verifier = new ChildValidatorVerifier<FakeValidator<int>, int>();

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(childValidatorAdaptor), "(ValidatorType property)");
        }

        [Fact]
        public void Given_CorrectValidatorWithSameValidatorType_When_Verifying_Then_ValidationPass()
        {
            // Arrange
            var childValidatorAdaptor = new ChildValidatorAdaptor<FakeValidator<int>, int>(new FakeValidator<int>(), typeof(FakeValidator<int>));
            var verifier = new ChildValidatorVerifier<FakeValidator<int>, int>();

            // Act & Assert
            AssertExtension.NotThrows(() => verifier.Verify(childValidatorAdaptor));
        }
    }
}
