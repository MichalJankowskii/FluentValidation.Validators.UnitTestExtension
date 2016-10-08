using FluentAssertions;
using FluentValidation.Validators.UnitTestExtension.Core;

namespace FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers
{
    // TODO: Maybe we should also check inner validator
    public class ChildCollectionValidatorVerifier<T> : IValidatorVerifier
    {
        public void Verify<TChildValidatorAdaptor>(TChildValidatorAdaptor validator)
        {
            validator.Should().BeOfType<ChildCollectionValidatorAdaptor>("(wrong type)");

            (validator as ChildCollectionValidatorAdaptor).ChildValidatorType.Should().Be(typeof(T), "(ChildValidatorType property)");
        }
    }
}
