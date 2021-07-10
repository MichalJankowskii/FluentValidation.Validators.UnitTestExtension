namespace FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers
{
    using FluentAssertions;

    public class ExactLengthValidatorVerifier<T> : LengthValidatorVerifier<ExactLengthValidator<T>>
    {
        public ExactLengthValidatorVerifier(int length) : base(length, length)
        {
        }
    }
}