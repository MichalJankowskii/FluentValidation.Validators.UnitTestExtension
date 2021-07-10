namespace FluentValidation.Validators.UnitTestExtension.Tests.Helpers.Fakes
{
    using System;

    public class FakeBetweenValidator : FakePropertyValidator, IBetweenValidator
    {
        public object From { get; set; }
        public object To { get; set; }
    }
}
