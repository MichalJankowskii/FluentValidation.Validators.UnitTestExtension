using FluentAssertions;
using FluentValidation.Validators.UnitTestExtension.Core;

namespace FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers
{
    // TODO: Maybe we should also check inner validator
    public class ChildValidatorVerifier<T> : IValidatorVerifier
    {
        public void Verify<TChildValidatorAdaptor>(TChildValidatorAdaptor validator)
        {
            validator.Should().BeOfType<ChildValidatorAdaptor>("(wrong type)");

            (validator as ChildValidatorAdaptor).ValidatorType.Should().Be(typeof(T), "(ValidatorType property)");
        }
    }
}
