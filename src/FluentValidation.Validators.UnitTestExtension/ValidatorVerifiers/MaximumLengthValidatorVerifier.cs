namespace FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers
{
    using FluentAssertions;

    public class MaximumLengthValidatorVerifier : LengthValidatorVerifier<MaximumLengthValidator>
    {
        public MaximumLengthValidatorVerifier(int max) : base(0, max)
        {
        }
    }
}