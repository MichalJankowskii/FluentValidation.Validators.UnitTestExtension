namespace FluentValidation.Validators.UnitTestExtension.Core
{
    public interface IValidatorVerifier
    {
        void Verify<T>(T validator);
    }
}
