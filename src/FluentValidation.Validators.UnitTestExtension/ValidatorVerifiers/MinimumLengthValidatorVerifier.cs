namespace FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers
{
    public class MinimumLengthValidatorVerifier<T> : LengthValidatorVerifier<MinimumLengthValidator<T>>
    {
        public MinimumLengthValidatorVerifier(int min) : base(min, -1)
        {
        }
    }
}