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
    using System.Reflection;
    using System.Text.RegularExpressions;
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

        /// <summary>
        /// Builds this instance of SetComposer.
        /// </summary>
        /// <returns></returns>
        public static BaseVerifiersSetComposer Build()
        {
            return new BaseVerifiersSetComposer();
        }

        /// <summary>
        /// Adds the property validator verifier.
        /// </summary>
        /// <typeparam name="T">The type of property validator that configuration will be checked.</typeparam>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddPropertyValidatorVerifier<T>() where T : IPropertyValidator
        {
            this.verifiers.Add(new TypeValidatorVerifier<T>());
            return this;
        }

        /// <summary>
        /// Adds the property comparison validator verifier.
        /// </summary>
        /// <typeparam name="T">The type of comparison validator that configuration will be checked.</typeparam>
        /// <param name="valueToCompare">The value to compare.</param>
        /// <param name="comparison">The comparison type.</param>
        /// <param name="memberToCompare">The member being compared.</param>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddPropertyValidatorVerifier<T>(object valueToCompare, Comparison? comparison = null, MemberInfo memberToCompare = null) where T : IComparisonValidator
        {
            this.verifiers.Add(new ComparisonValidatorVerifier<T>(valueToCompare, comparison, memberToCompare));
            return this;
        }

        /// <summary>
        /// Adds the property length validator verifier.
        /// </summary>
        /// <typeparam name="T">The type of lenght validator that configuration will be checked.</typeparam>
        /// <param name="min">The minimum length.</param>
        /// <param name="max">The maximum length.</param>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddPropertyValidatorVerifier<T>(int min, int max) where T : ILengthValidator
        {
            this.verifiers.Add(new LengthValidatorVerifier<T>(min, max));
            return this;
        }

	    /// <summary>
	    /// Adds the property regular expression validator verifier.
	    /// </summary>
	    /// <typeparam name="T">The type of regular expression validator that configuration will be checked.</typeparam>
	    /// <param name="expression">The expression.</param>
	    /// <returns></returns>
		public BaseVerifiersSetComposer AddPropertyValidatorVerifier<T>(string expression) where T : IRegularExpressionValidator
	    {
		    this.verifiers.Add(new RegularExpressionValidatorVerifier<T>(expression));
		    return this;
	    }

	    /// <summary>
	    /// Adds the property regular expression validator verifier.
	    /// </summary>
	    /// <typeparam name="T">The type of regular expression validator that configuration will be checked.</typeparam>
	    /// <param name="regex">The regular expression.</param>
	    /// <returns></returns>
		public BaseVerifiersSetComposer AddPropertyValidatorVerifier<T>(Regex regex) where T : IRegularExpressionValidator
	    {
		    this.verifiers.Add(new RegularExpressionValidatorVerifier<T>(regex));
		    return this;
	    }

		/// <summary>
		/// Adds the child validator verifier.
		/// </summary>
		/// <typeparam name="T">The type of child validator.</typeparam>
		/// <returns></returns>
		public BaseVerifiersSetComposer AddChildValidatorVerifier<T>()
        {
            this.verifiers.Add(new ChildValidatorVerifier<T>());
            return this;
        }

        /// <summary>
        /// Adds the child collection validator verifier.
        /// </summary>
        /// <typeparam name="T">The type of child validator.</typeparam>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddChildCollectionValidatorVerifier<T>()
        {
            this.verifiers.Add(new ChildCollectionValidatorVerifier<T>());
            return this;
        }

        /// <summary>
        /// Adds own implemented verifier.
        /// </summary>
        /// <param name="ruleVerifier">The rule verifier.</param>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddVerifier(IValidatorVerifier ruleVerifier)
        {
            this.verifiers.Add(ruleVerifier);
            return this;
        }

        /// <summary>
        /// Adds the placeholder for verifer that will be checked in separte test.
        /// </summary>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddPlaceholderVerifier()
        {
            this.verifiers.Add(new PlaceholderVerifier());
            return this;
        }

        /// <summary>
        /// Generate array of verifiers.
        /// </summary>
        /// <returns></returns>
        public IValidatorVerifier[] Create()
        {
            return this.verifiers.ToArray();
        }
    }
}