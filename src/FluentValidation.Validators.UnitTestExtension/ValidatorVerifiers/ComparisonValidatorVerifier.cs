using FluentAssertions;

namespace FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers
{
    public class ComparisonValidatorVerifier<T> : TypeValidatorVerifier<T> where T : IComparisonValidator
    {
        private readonly object valueToCompare;

        public ComparisonValidatorVerifier(object valueToCompare)
        {
            this.valueToCompare = valueToCompare;
        }

        public override void Verify<TValidator>(TValidator validator)
        {
            base.Verify(validator);
            // TODO: Implement all verification of all properties in IComparisonValidator
            ((IComparisonValidator)validator).ValueToCompare.ShouldBeEquivalentTo(this.valueToCompare, "(ValueToCompare property)");
        }
    }
}
