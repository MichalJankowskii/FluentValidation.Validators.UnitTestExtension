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
