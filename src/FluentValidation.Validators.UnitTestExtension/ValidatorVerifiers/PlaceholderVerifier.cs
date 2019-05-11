namespace FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers
{
    using Core;

    /// <summary>
    /// This verifier is uses as a placeholder and it means that separate test should be written
    /// </summary>
    /// <seealso cref="FluentValidation.Validators.UnitTestExtension.Core.IValidatorVerifier" />
    public class PlaceholderVerifier : IValidatorVerifier
    {
        public void Verify<T>(T validator)
        {
        }
    }
}
