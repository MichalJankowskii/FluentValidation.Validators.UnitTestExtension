namespace FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers
{
    using Core;
    using FluentAssertions;

    // TODO: Maybe we should also check inner validator
    public class ChildValidatorVerifier<T, TProperty> : IValidatorVerifier
    {
        public void Verify<TChildValidatorAdaptor>(TChildValidatorAdaptor validator)
        {
            validator.Should().BeOfType<ChildValidatorAdaptor<T, TProperty>>("(wrong type)");

            (validator as ChildValidatorAdaptor<T, TProperty>).ValidatorType.Should().Be(typeof(T), "(ValidatorType property)");
        }
    }
}
