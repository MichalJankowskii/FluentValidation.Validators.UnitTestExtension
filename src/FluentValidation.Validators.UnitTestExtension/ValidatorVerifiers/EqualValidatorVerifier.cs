namespace FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Exceptions;
    using FluentAssertions;

    public class EqualValidatorVerifier<TEqualValidator, T, TProperty> : TypeValidatorVerifier<TEqualValidator> where TEqualValidator : PropertyValidator<T, TProperty>
    {
        private static readonly Dictionary<Type, Comparison> ComparisonValidatorSetUp = new Dictionary<Type, Comparison>()
        {
            {typeof(EqualValidator<T, TProperty>), Comparison.Equal},
            {typeof(NotEqualValidator<T, TProperty>), Comparison.NotEqual},
        };

        private readonly object valueToCompare;

        private readonly Comparison? comparison;

        private readonly MemberInfo memberToCompare;

        public EqualValidatorVerifier(object valueToCompare, Comparison? comparison = null, MemberInfo memberToCompare = null)
        {
            this.valueToCompare = valueToCompare;
            this.comparison = comparison;
            this.memberToCompare = memberToCompare;

            if (this.comparison == null)
            {
                if (ComparisonValidatorSetUp.ContainsKey(typeof(TEqualValidator)))
                {
                    this.comparison = ComparisonValidatorSetUp[typeof(TEqualValidator)];
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
            ((IComparisonValidator)validator).Comparison.Should().Be(this.comparison, "(Comparison property)");
            if (this.memberToCompare != null)
            {
                ((IComparisonValidator) validator).MemberToCompare.Should().BeEquivalentTo(this.memberToCompare, "(MemberToCompare property)");
            }
        }
    }
}
