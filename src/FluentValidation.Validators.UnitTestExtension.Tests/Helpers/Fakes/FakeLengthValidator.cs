namespace FluentValidation.Validators.UnitTestExtension.Tests.Helpers.Fakes
{
    public class FakeLengthValidator : FakePropertyValidator, ILengthValidator
    {
        public int Min { get; set; }
        public int Max { get; set; }
    }
}
