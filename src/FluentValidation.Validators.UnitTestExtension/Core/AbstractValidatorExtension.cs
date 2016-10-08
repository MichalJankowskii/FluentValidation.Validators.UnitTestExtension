using System;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using FluentValidation.Internal;

namespace FluentValidation.Validators.UnitTestExtension.Core
{
    public static class AbstractValidatorExtension
    {
        public static void ShouldHaveRules<TRequest, TProperty>(
            this AbstractValidator<TRequest> validator,
            Expression<Func<TRequest, TProperty>> expression,
            params IValidatorVerifier[] validatorRuleVerifieres)
        {
            var validators = validator.Select(x => (PropertyRule)x).Where(r => r.Member == expression.GetMember()).SelectMany(x => x.Validators).ToList();

            validators.Should().HaveCount(validatorRuleVerifieres.Length);

            for (var i = 0; i < validatorRuleVerifieres.Length; i++)
            {
                validatorRuleVerifieres[i].Verify(validators[i]);
            }
        }

        public static void ShouldHaveRulesCount<T>(this AbstractValidator<T> validator, int rulesNumber)
        {
            validator.Count().ShouldBeEquivalentTo(rulesNumber);
        }
    }
}
