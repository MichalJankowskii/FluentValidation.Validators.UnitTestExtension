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

namespace FluentValidation.Validators.UnitTestExtension.Composer
{
    using System.Collections.Generic;
    using Core;
    using ValidatorVerifiers;

    // TODO: Add class which will build tests from build in rules
    public class BaseVerifiersSetComposer
    {
        private readonly List<IValidatorVerifier> verifiers;

        private BaseVerifiersSetComposer()
        {
            this.verifiers = new List<IValidatorVerifier>();
        }

        public static BaseVerifiersSetComposer Build()
        {
            return new BaseVerifiersSetComposer();
        }

        public BaseVerifiersSetComposer AddPropertyValidatorVerifier<T>() where T : IPropertyValidator
        {
            this.verifiers.Add(new TypeValidatorVerifier<T>());
            return this;
        }

        public BaseVerifiersSetComposer AddPropertyValidatorVerifier<T>(object valueToCompare) where T : IComparisonValidator
        {
            this.verifiers.Add(new ComparisonValidatorVerifier<T>(valueToCompare));
            return this;
        }

        public BaseVerifiersSetComposer AddPropertyValidatorVerifier<T>(int min, int max) where T : ILengthValidator
        {
            this.verifiers.Add(new LengthValidatorVerifier<T>(min, max));
            return this;
        }

        public BaseVerifiersSetComposer AddChildValidatorVerifier<T>()
        {
            this.verifiers.Add(new ChildValidatorVerifier<T>());
            return this;
        }

        public BaseVerifiersSetComposer AddChildCollectionValidatorVerifier<T>()
        {
            this.verifiers.Add(new ChildCollectionValidatorVerifier<T>());
            return this;
        }

        public BaseVerifiersSetComposer AddVerifier(IValidatorVerifier ruleVerifier)
        {
            this.verifiers.Add(ruleVerifier);
            return this;
        }

        public BaseVerifiersSetComposer AddCustomVerifier()
        {
            this.verifiers.Add(new CustomVerifier());
            return this;
        }

        public IValidatorVerifier[] Create()
        {
            return this.verifiers.ToArray();
        }
    }
}
