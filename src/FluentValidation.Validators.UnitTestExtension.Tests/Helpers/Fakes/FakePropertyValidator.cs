namespace FluentValidation.Validators.UnitTestExtension.Tests.Helpers.Fakes
{
    using System;

    public class FakePropertyValidator : IPropertyValidator
    {
        public string Name => throw new NotImplementedException();

        public string GetDefaultMessageTemplate(string errorCode)
        {
            throw new NotImplementedException();
        }
    }
}
