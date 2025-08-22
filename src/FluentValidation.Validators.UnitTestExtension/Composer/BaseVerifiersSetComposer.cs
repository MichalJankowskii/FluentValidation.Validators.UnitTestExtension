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
        /// <typeparam name="TPropertyValidator">The type of property validator that configuration will be checked.</typeparam>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddPropertyValidatorVerifier<TPropertyValidator>() where TPropertyValidator : IPropertyValidator
        {
            this.verifiers.Add(new TypeValidatorVerifier<TPropertyValidator>());
            return this;
        }

        /// <summary>
        /// Adds the property equal/not-equal validator verifier.
        /// </summary>
        /// <typeparam name="TEqualValidator">The type of comparison validator that configuration will be checked.</typeparam>
        /// <typeparam name="T">The type of the object that property will be validated</typeparam>
        /// <typeparam name="TProperty">Tye type of the property that will be validated</typeparam>
        /// <param name="valueToCompare">The value to compare.</param>
        /// <param name="comparison">The comparison type.</param>
        /// <param name="memberToCompare">The member being compared.</param>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddEqualValidatorVerifier<TEqualValidator, T, TProperty>(object valueToCompare, Comparison? comparison = null, MemberInfo memberToCompare = null) where TEqualValidator : PropertyValidator<T, TProperty> where TProperty : IComparable<TProperty>, IComparable
        {
            this.verifiers.Add(new EqualValidatorVerifier<TEqualValidator, T, TProperty>(valueToCompare, comparison, memberToCompare));
            return this;
        }

        /// <summary>
        /// Adds the property comparison validator verifier.
        /// </summary>
        /// <typeparam name="TComparisonValidator">The type of comparison validator that configuration will be checked.</typeparam>
        /// <typeparam name="T">The type of the object that property will be validated</typeparam>
        /// <typeparam name="TProperty">Tye type of the property that will be validated</typeparam>
        /// <param name="valueToCompare">The value to compare.</param>
        /// <param name="comparison">The comparison type.</param>
        /// <param name="memberToCompare">The member being compared.</param>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddComparisonValidatorVerifier<TComparisonValidator, T, TProperty>(object valueToCompare, Comparison? comparison = null, MemberInfo memberToCompare = null) where TComparisonValidator : AbstractComparisonValidator<T, TProperty> where TProperty : IComparable<TProperty>, IComparable
        {
            this.verifiers.Add(new ComparisonValidatorVerifier<TComparisonValidator, T, TProperty>(valueToCompare, comparison, memberToCompare));
            return this;
        }

        /// <summary>
        /// Adds the property length validator verifier.
        /// </summary>
        /// <typeparam name="TLengthValidator">The type of length validator that configuration will be checked.</typeparam>
        /// <param name="min">The minimum length.</param>
        /// <param name="max">The maximum length.</param>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddPropertyValidatorVerifier<TLengthValidator>(int min, int max) where TLengthValidator : ILengthValidator
        {
            this.verifiers.Add(new LengthValidatorVerifier<TLengthValidator>(min, max));
            return this;
        }

        /// <summary>
        /// Adds the property between validator verifier.
        /// </summary>
        /// <typeparam name="TBetweenValidator">The type of between validator that configuration will be checked.</typeparam>
        /// <param name="from">The from.</param>
        /// <param name="to">The to.</param>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddBetweenValidatorVerifier<TBetweenValidator>(IComparable from, IComparable to) where TBetweenValidator : IBetweenValidator
        {
            this.verifiers.Add(new BetweenValidatorVerifier<TBetweenValidator>(from, to));
            return this;
        }

        /// <summary>
        /// Adds the property scale precision validator verifier.
        /// </summary>
        /// <typeparam name="TPrecisionScaleValidator">The type of PrecisionScaleValidator that configuration will be checked.</typeparam>
        /// <typeparam name="T">The type of the object that property will be validated</typeparam>
        /// <param name="scale">The scale.</param>
        /// <param name="precision">The precision.</param>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddPrecisionScaleValidatorVerifier<TPrecisionScaleValidator, T>(int scale, int precision, bool ignoreTrailingZeros) where TPrecisionScaleValidator : PrecisionScaleValidator<T>
        {
            this.verifiers.Add(new PrecisionScaleValidatorVerifier<TPrecisionScaleValidator, T>(scale, precision, ignoreTrailingZeros));
            return this;
        }

        /// <summary>
        /// Adds the property enum validator verifier.
        /// </summary>
        /// <typeparam name="TEnumValidator">The type of EnumValidator that configuration will be checked.</typeparam>
        /// <typeparam name="T">The type of the object that property will be validated</typeparam>
        /// <typeparam name="TProperty">Tye type of the property that will be validated</typeparam>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddEnumValidatorVerifier<TEnumValidator, T, TProperty>() where TEnumValidator : EnumValidator<T, TProperty>
        {
            this.verifiers.Add(new EnumValidatorVerifier<TEnumValidator, T, TProperty>());
            return this;
        }

        /// <summary>
        /// Adds the property enum validator verifier.
        /// </summary>
        /// <typeparam name="T">The type of the object that property will be validated</typeparam>
        /// <typeparam name="TProperty">Tye type of the property that will be validated</typeparam>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddEnumValidatorVerifier<T, TProperty>()
        {
            return this.AddEnumValidatorVerifier<EnumValidator<T, TProperty>, T, TProperty>();
        }

        /// <summary>
        /// Adds the property regular expression validator verifier.
        /// </summary>
        /// <typeparam name="TRegularExpressionValidator">The type of regular expression validator that configuration will be checked.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddPropertyValidatorVerifier<TRegularExpressionValidator>(string expression) where TRegularExpressionValidator : IRegularExpressionValidator
        {
            this.verifiers.Add(new RegularExpressionValidatorVerifier<TRegularExpressionValidator>(expression));
            return this;
        }

        /// <summary>
        /// Adds the property regular expression validator verifier.
        /// </summary>
        /// <typeparam name="TRegularExpressionValidator">The type of regular expression validator that configuration will be checked.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddPropertyValidatorVerifier<TRegularExpressionValidator>(string expression, RegexOptions options) where TRegularExpressionValidator : IRegularExpressionValidator
        {
            this.verifiers.Add(new RegularExpressionValidatorVerifier<TRegularExpressionValidator>(expression, options));
            return this;
        }

        /// <summary>
        /// Adds the property regular expression validator verifier.
        /// </summary>
        /// <typeparam name="TRegularExpressionValidator">The type of regular expression validator that configuration will be checked.</typeparam>
        /// <param name="regex">The regular expression.</param>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddPropertyValidatorVerifier<TRegularExpressionValidator>(Regex regex) where TRegularExpressionValidator : IRegularExpressionValidator
        {
            this.verifiers.Add(new RegularExpressionValidatorVerifier<TRegularExpressionValidator>(regex));
            return this;
        }

        /// <summary>
        /// Adds the child validator verifier.
        /// </summary>
        /// <typeparam name="TPropertyValidator">The type of child validator.</typeparam>
        /// <typeparam name="T">The type of the object that property will be validated</typeparam>
        /// <typeparam name="TProperty">Tye type of the property that will be validated</typeparam>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddChildValidatorVerifier<TPropertyValidator, T, TProperty>()
        {
            this.verifiers.Add(new ChildValidatorVerifier<TPropertyValidator, T, TProperty>());
            return this;
        }

        /// <summary>
        /// Adds the exact length validator verifier.
        /// </summary>
        /// <param name="length">The exact length.</param>
        /// <typeparam name="T">The type of the object that property will be validated</typeparam>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddExactLengthValidatorVerifier<T>(int length)
        {
            this.verifiers.Add(new ExactLengthValidatorVerifier<T>(length));
            return this;
        }

        /// <summary>
        /// Adds the minimum length validator verifier.
        /// </summary>
        /// <param name="min">The minimum length.</param>
        /// <typeparam name="T">The type of the object that property will be validated</typeparam>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddMinimumLengthValidatorVerifier<T>(int min)
        {
            this.verifiers.Add(new MinimumLengthValidatorVerifier<T>(min));
            return this;
        }

        /// <summary>
        /// Adds the maximum length validator verifier.
        /// </summary>
        /// <param name="max">The maximum length.</param>
        /// <typeparam name="T">The type of the object that property will be validated</typeparam>
        /// <returns></returns>
        public BaseVerifiersSetComposer AddMaximumLengthValidatorVerifier<T>(int max)
        {
            this.verifiers.Add(new MaximumLengthValidatorVerifier<T>(max));
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
