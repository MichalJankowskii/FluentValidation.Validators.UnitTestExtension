namespace FluentValidation.Validators.UnitTestExtension.Composer
{
    using System;
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
        /// <typeparam name="T">The type of length validator that configuration will be checked.</typeparam>
        /// <param name="min">The minimum length.</param>
        /// <param name="max">The maximum length.</param>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddPropertyValidatorVerifier<T>(int min, int max) where T : ILengthValidator
        {
            this.verifiers.Add(new LengthValidatorVerifier<T>(min, max));
            return this;
        }

        /// <summary>
        /// Adds the property between validator verifier.
        /// </summary>
        /// <typeparam name="T">The type of between validator that configuration will be checked.</typeparam>
        /// <param name="from">The from.</param>
        /// <param name="to">The to.</param>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddBetweenValidatorVerifier<T>(IComparable from, IComparable to) where T : IBetweenValidator
        {
            this.verifiers.Add(new BetweenValidatorVerifier<T>(from, to));
            return this;
        }

        /// <summary>
        /// Adds the property scale precision validator verifier.
        /// </summary>
        /// <typeparam name="T">The type of ScalePrecisionValidator that configuration will be checked.</typeparam>
        /// <param name="scale">The scale.</param>
        /// <param name="precision">The precision.</param>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddScalePrecisionValidatorVerifier<T>(int scale, int precision) where T : ScalePrecisionValidator
        {
            this.verifiers.Add(new ScalePrecisionValidatorVerifier<T>(scale, precision));
            return this;
        }

        /// <summary>
        /// Adds the property scale precision validator verifier.
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <param name="precision">The precision.</param>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddScalePrecisionValidatorVerifier(int scale, int precision)
        {
            this.verifiers.Add(new ScalePrecisionValidatorVerifier<ScalePrecisionValidator>(scale, precision));
            return this;
        }

        /// <summary>
        /// Adds the property enum validator verifier.
        /// </summary>
        /// <typeparam name="T">The type of EnumValidator that configuration will be checked.</typeparam>
        /// <param name="enumType">The enumType.</param>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddEnumValidatorVerifier<T>(Type enumType) where T : EnumValidator
        {
            this.verifiers.Add(new EnumValidatorVerifier<T>(enumType));
            return this;
        }

        /// <summary>
        /// Adds the property enum validator verifier.
        /// </summary>
        /// 
        /// <param name="enumType">The enumType.</param>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddEnumValidatorVerifier(Type enumType)
        { 
            this.verifiers.Add(new EnumValidatorVerifier<EnumValidator>(enumType));
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
        /// <param name="expression">The expression.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddPropertyValidatorVerifier<T>(string expression, RegexOptions options) where T : IRegularExpressionValidator
        {
            this.verifiers.Add(new RegularExpressionValidatorVerifier<T>(expression, options));
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
        /// Adds the exact length validator verifier.
        /// </summary>
        /// <param name="length">The exact length.</param>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddExactLengthValidatorVerifier(int length)
        {
            this.verifiers.Add(new ExactLengthValidatorVerifier(length));
            return this;
        }

        /// <summary>
        /// Adds the minimum length validator verifier.
        /// </summary>
        /// <param name="min">The minimum length.</param>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddMinimumLengthValidatorVerifier(int min)
        {
            this.verifiers.Add(new MinimumLengthValidatorVerifier(min));
            return this;
        }

        /// <summary>
        /// Adds the maximum length validator verifier.
        /// </summary>
        /// <param name="max">The maximum length.</param>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddMaximumLengthValidatorVerifier(int max)
        {
            this.verifiers.Add(new MaximumLengthValidatorVerifier(max));
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
        /// Adds the placeholder for verifier that will be checked in separate test.
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