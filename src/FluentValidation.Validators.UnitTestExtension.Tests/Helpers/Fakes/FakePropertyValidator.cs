namespace FluentValidation.Validators.UnitTestExtension.Tests.Helpers.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Results;

    public class FakePropertyValidator : IPropertyValidator
    {
        public string Name => throw new NotImplementedException();

        public string GetDefaultMessageTemplate(string errorCode)
        {
            throw new NotImplementedException();
        }
    }
}
