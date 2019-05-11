namespace FluentValidation.Validators.UnitTestExtension.Tests.Helpers.Fakes
{
    using UnitTestExtension.Core;

    public class FakeValidatorVerifier : IValidatorVerifier
    {
        public void Verify<T>(T validator)
        {
            throw new System.NotImplementedException();
        }
    }
}
