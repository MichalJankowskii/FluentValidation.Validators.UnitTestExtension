namespace FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers
{
    using FluentAssertions;

    public class MaximumLengthValidatorVerifier<T> : LengthValidatorVerifier<MaximumLengthValidator<T>>
    {
        public MaximumLengthValidatorVerifier(int max) : base(0, max)
        {
        }
    }
}