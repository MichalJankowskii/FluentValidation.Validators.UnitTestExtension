using FluentAssertions;
using FluentValidation.Validators.UnitTestExtension.Core;

namespace FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers
{
    public class TypeValidatorVerifier<T> : IValidatorVerifier where T : IPropertyValidator
    {
        public virtual void Verify<TValidator>(TValidator validator)
        {
            validator.Should().BeOfType<T>("(wrong type)");
        }
    }
}
