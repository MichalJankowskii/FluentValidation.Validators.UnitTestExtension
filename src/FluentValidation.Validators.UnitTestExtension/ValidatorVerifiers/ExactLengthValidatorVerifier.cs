namespace FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers
{
    using FluentAssertions;

    public class ExactLengthValidatorVerifier : LengthValidatorVerifier<ExactLengthValidator>
    {
        public ExactLengthValidatorVerifier(int lenght) : base(lenght, lenght)
        {
        }
    }
}