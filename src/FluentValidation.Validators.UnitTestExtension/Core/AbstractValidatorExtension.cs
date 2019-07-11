namespace FluentValidation.Validators.UnitTestExtension.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using FluentAssertions;
    using FluentValidation.Internal;
    using Internal;
    using Internal.Extensions;

    public static class AbstractValidatorExtension
    {
        /// <summary>
        /// Asserts that a validator has correct definition of validators connected to provided property.
        /// </summary>
        /// <typeparam name="T">The type of the object being validated.</typeparam>
        /// <typeparam name="TProperty">The type of property being validated.</typeparam>
        /// <param name="validator">The validator that will be examined.</param>
        /// <param name="expression">The expression representing the property to validate.</param>
        /// <param name="validatorVerifieres">Array of validator verifieres.</param>
        public static void ShouldHaveRules<T, TProperty>(
            this AbstractValidator<T> validator,
            Expression<Func<T, TProperty>> expression,
            params IValidatorVerifier[] validatorVerifieres)
        {

            var validators = new List<IPropertyValidator>();

            validator.Select(x => (PropertyRule)x).Where(r => r.Expression.CompareMembersRecursively(expression)).SelectMany(x => x.Validators).ToList().ForEach(
                propertyValidator =>
                {
                    if (propertyValidator is IDelegatingValidator delegatingValidator)
                    {
                        validators.Add(delegatingValidator.InnerValidator);
                    }
                    else
                    {
                        validators.Add(propertyValidator);
                    }
                });

            validators.Should().HaveCount(validatorVerifieres.Length, "(number of rules for property)");

            for (var i = 0; i < validatorVerifieres.Length; i++)
            {
                validatorVerifieres[i].Verify(validators[i]);
            }
        }

        /// <summary>
        /// Asserts that a validator has correct number of rules.
        /// </summary>
        /// <typeparam name="T">The type of the object being validated.</typeparam>
        /// <param name="validator">The validator that will be examined.</param>
        /// <param name="expectedRulesNumber">The expected number of rules in the collection.</param>
        public static void ShouldHaveRulesCount<T>(this AbstractValidator<T> validator, int expectedRulesNumber)
        {
            validator.Count().Should().Be(expectedRulesNumber, "(number of rules for object)");
        }
    }
}
