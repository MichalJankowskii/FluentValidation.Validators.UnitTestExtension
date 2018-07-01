namespace FluentValidation.Validators.UnitTestExtension.Tests.Helpers.Fakes
{
    using System;

    public class FakeBetweenValidator : FakePropertyValidator, IBetweenValidator
    {
        public IComparable From { get; set; }
        public IComparable To { get; set; }
    }
}
