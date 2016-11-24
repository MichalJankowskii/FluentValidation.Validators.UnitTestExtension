#region License
// MIT License
// 
// Copyright(c) 2016 Michał Jankowski (http://www.jankowskimichal.pl)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// 
// The latest version of this file can be found at https://github.com/MichalJankowskii/FluentValidation.Validators.UnitTestExtension
#endregion

namespace FluentValidation.Validators.UnitTestExtension.Core
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using FluentAssertions;
    using Internal;

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
            var validators = validator.Select(x => (PropertyRule)x).Where(r => r.Member == expression.GetMember()).SelectMany(x => x.Validators).ToList();

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
            validator.Count().ShouldBeEquivalentTo(expectedRulesNumber, "(number of rules for object)");
        }
    }
}
