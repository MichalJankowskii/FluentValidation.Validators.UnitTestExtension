namespace FluentValidation.Validators.UnitTestExtension.Tests.ValidatorsVerifiers
{
    using Helpers;
    using Helpers.Fakes;
    using ValidatorVerifiers;
    using Xunit;
    using Xunit.Sdk;

    public class EnumValidatorVerifierTest
    {
        [Fact]
        public void Given_DifferentValidatorType_When_Verifying_Then_ValidationMustFail()
        {
            // Arrange
            var otherValidator = new FakePropertyValidator();
            var verifier = new EnumValidatorVerifier<EnumValidator<object, FakeEnum>, object, FakeEnum>();

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(otherValidator), "(wrong type)");
        }

        [Fact]
        public void Given_CorrectValidator_When_Verifying_Then_ValidationPass()
        {
            // Arrange
            var enumValidator = new EnumValidator<object, MyEnum>();
            var verifier = new EnumValidatorVerifier<EnumValidator<object, MyEnum>, object, MyEnum>();

            // Act & Assert
            AssertExtension.NotThrows(() => verifier.Verify(enumValidator));
        }

        public enum MyEnum
        {
        }
    }
}
