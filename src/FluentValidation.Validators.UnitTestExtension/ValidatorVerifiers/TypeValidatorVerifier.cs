namespace FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers
{
    using Core;
    using FluentAssertions;

    public class TypeValidatorVerifier<T> : IValidatorVerifier where T : IPropertyValidator
    {
        public virtual void Verify<TValidator>(TValidator validator)
        {
            validator.Should().BeOfType<T>("(wrong type)");
        }
    }
}
