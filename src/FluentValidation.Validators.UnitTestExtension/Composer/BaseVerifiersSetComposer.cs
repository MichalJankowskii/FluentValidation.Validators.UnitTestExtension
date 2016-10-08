using System.Collections.Generic;
using FluentValidation.Validators.UnitTestExtension.Core;
using FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers;

namespace FluentValidation.Validators.UnitTestExtension.Composer
{
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
