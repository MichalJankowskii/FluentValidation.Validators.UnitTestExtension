namespace FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers
{
    using FluentAssertions;

    public class LengthValidatorVerifier<T> : TypeValidatorVerifier<T> where T : ILengthValidator
    {
        protected readonly int min;

        protected readonly int max;

        public LengthValidatorVerifier(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

        public override void Verify<TValidator>(TValidator validator)
        {
            base.Verify(validator);
            var lengthValidator = (ILengthValidator)validator;
            lengthValidator.Min.Should().Be(this.min, "(Min property)");
            lengthValidator.Max.Should().Be(this.max, "(Max property)");
        }
    }
}