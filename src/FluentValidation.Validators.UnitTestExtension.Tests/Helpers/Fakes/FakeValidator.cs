namespace FluentValidation.Validators.UnitTestExtension.Tests.Helpers.Fakes
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using FluentValidation.Results;

    public class FakeValidator : IValidator<int>
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

        public ValidationResult Validate(int instance)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Validate(IValidationContext context)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> ValidateAsync(int instance, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> ValidateAsync(IValidationContext context, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
