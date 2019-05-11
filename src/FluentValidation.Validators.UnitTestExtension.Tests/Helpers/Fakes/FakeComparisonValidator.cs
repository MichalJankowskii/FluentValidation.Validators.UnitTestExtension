namespace FluentValidation.Validators.UnitTestExtension.Tests.Helpers.Fakes
{
    using System.Reflection;

    public class FakeComparisonValidator : FakePropertyValidator, IComparisonValidator
    {
        public Comparison Comparison { get; set; }
        public MemberInfo MemberToCompare { get; set; }
        public object ValueToCompare { get; set; }
    }
}
