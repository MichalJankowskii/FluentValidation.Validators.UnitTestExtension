namespace FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers
{
    using FluentAssertions;

    public class MinimumLengthValidatorVerifier<T> : LengthValidatorVerifier<MinimumLengthValidator<T>>
    {
        public MinimumLengthValidatorVerifier(int min) : base(min, -1)
        {
        }
    }
}