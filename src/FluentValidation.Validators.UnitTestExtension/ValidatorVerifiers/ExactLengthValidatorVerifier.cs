namespace FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers
{
    public class ExactLengthValidatorVerifier<T> : LengthValidatorVerifier<ExactLengthValidator<T>>
    {
        public ExactLengthValidatorVerifier(int length) : base(length, length)
        {
        }
    }
}