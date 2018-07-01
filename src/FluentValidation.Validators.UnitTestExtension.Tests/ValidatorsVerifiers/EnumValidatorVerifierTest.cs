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
            var verifier = new EnumValidatorVerifier<EnumValidator>(typeof(FakeEnum));

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(otherValidator), "(wrong type)");
        }

        [Fact]
        public void Given_CorrectValidatorWithDifferentEnumType_When_Verifying_Then_ValidationFail()
        {
            // Arrange
            var enumValidator = new EnumValidator(typeof(MyEnum));
            var verifier = new EnumValidatorVerifier<EnumValidator>(typeof(FakeEnum));

            // Act & Assert
            AssertExtension.Throws<XunitException>(() => verifier.Verify(enumValidator), "(EnumType field)");
        }

        [Fact]
        public void Given_CorrectValidator_When_Verifying_Then_ValidationPass()
        {
            // Arrange
            var enumValidator = new EnumValidator(typeof(MyEnum));
            var verifier = new EnumValidatorVerifier<EnumValidator>(typeof(MyEnum));

            // Act & Assert
            AssertExtension.NotThrows(() => verifier.Verify(enumValidator));
        }

        public enum MyEnum
        {
        }
    }
}
