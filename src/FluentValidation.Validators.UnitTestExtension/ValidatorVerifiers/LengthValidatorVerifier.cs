using FluentAssertions;

namespace FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers
{
    public class LengthValidatorVerifier<T> : TypeValidatorVerifier<T> where T : ILengthValidator
    {
        private readonly int min;

        private readonly int max;

        public LengthValidatorVerifier(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

        public override void Verify<TValidator>(TValidator validator)
        {
            base.Verify(validator);
            var lengthValidator = (ILengthValidator)validator;
            lengthValidator.Min.ShouldBeEquivalentTo(this.min, "(Min property)");
            lengthValidator.Max.ShouldBeEquivalentTo(this.max, "(Max property)");
        }
    }
}