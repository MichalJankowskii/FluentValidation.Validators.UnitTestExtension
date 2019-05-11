namespace FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Exceptions;
    using FluentAssertions;

    public class ComparisonValidatorVerifier<T> : TypeValidatorVerifier<T> where T : IComparisonValidator
    {
        private static readonly Dictionary<Type, Comparison> ComparisonValidatorSetUp = new Dictionary<Type, Comparison>()
        {
            {typeof(EqualValidator), Comparison.Equal},
            {typeof(NotEqualValidator), Comparison.NotEqual},
            {typeof(LessThanValidator), Comparison.LessThan},
            {typeof(LessThanOrEqualValidator), Comparison.LessThanOrEqual},
            {typeof(GreaterThanValidator), Comparison.GreaterThan},
            {typeof(GreaterThanOrEqualValidator), Comparison.GreaterThanOrEqual}
        };

        private readonly object valueToCompare;

        private readonly Comparison? comparison;

        private readonly MemberInfo memberToCompare;

        public ComparisonValidatorVerifier(object valueToCompare, Comparison? comparison = null, MemberInfo memberToCompare = null)
        {
            this.valueToCompare = valueToCompare;
            this.comparison = comparison;
            this.memberToCompare = memberToCompare;

            if (this.comparison == null)
            {
                if (ComparisonValidatorSetUp.ContainsKey(typeof(T)))
                {
                    this.comparison = ComparisonValidatorSetUp[typeof(T)];
                }
                else
                {
                    throw new ComparisonNotProvidedException();
                }
            }
        }

        public override void Verify<TValidator>(TValidator validator)
        {
            base.Verify(validator);
            ((IComparisonValidator)validator).ValueToCompare.Should().BeEquivalentTo(this.valueToCompare, "(ValueToCompare property)");
            ((IComparisonValidator)validator).Comparison.Should().BeEquivalentTo(this.comparison, "(Comparison property)");
            if (this.memberToCompare != null)
            {
                ((IComparisonValidator) validator).MemberToCompare.Should().BeEquivalentTo(this.memberToCompare, "(MemberToCompare property)");
            }
        }
    }
}
