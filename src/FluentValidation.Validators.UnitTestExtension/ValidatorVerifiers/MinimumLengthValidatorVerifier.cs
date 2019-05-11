namespace FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers
{
    using FluentAssertions;

    public class MinimumLengthValidatorVerifier : LengthValidatorVerifier<MinimumLengthValidator>
    {
        public MinimumLengthValidatorVerifier(int min) : base(min, -1)
        {
        }
    }
}