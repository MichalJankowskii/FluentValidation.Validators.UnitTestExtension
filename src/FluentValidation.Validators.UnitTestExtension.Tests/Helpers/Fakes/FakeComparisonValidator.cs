namespace FluentValidation.Validators.UnitTestExtension.Tests.Helpers.Fakes
{
    using System.Reflection;

    public class FakeComparisonValidator<T, TProperty> : PropertyValidator<T, TProperty>, IComparisonValidator
    {
        public Comparison Comparison { get; set; }
        public MemberInfo MemberToCompare { get; set; }
        public object ValueToCompare { get; set; }
        public override bool IsValid(ValidationContext<T> context, TProperty value)
        {
            throw new System.NotImplementedException();
        }

        public override string Name { get; }
    }
}
