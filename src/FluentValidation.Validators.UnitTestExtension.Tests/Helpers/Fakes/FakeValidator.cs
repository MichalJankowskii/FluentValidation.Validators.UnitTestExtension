namespace FluentValidation.Validators.UnitTestExtension.Tests.Helpers.Fakes
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Results;

    public class FakeValidator : IValidator
    {
        public ValidationResult Validate(object instance)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> ValidateAsync(object instance, CancellationToken cancellation = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public ValidationResult Validate(ValidationContext context)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> ValidateAsync(ValidationContext context, CancellationToken cancellation = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IValidatorDescriptor CreateDescriptor()
        {
            throw new NotImplementedException();
        }

        public bool CanValidateInstancesOfType(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
