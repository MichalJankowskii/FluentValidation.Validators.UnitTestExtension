namespace FluentValidation.Validators.UnitTestExtension.Tests.Helpers.Fakes
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using FluentValidation.Results;

    public class FakeValidator<TProperty> : IValidator<TProperty>
    {
        public CascadeMode CascadeMode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool CanValidateInstancesOfType(Type type)
        {
            throw new NotImplementedException();
        }

        public IValidatorDescriptor CreateDescriptor()
        {
            throw new NotImplementedException();
        }

        public ValidationResult Validate(TProperty instance)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Validate(IValidationContext context)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> ValidateAsync(TProperty instance, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> ValidateAsync(IValidationContext context, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
