using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Resources;
using FluentValidation.Results;

namespace FluentValidation.Validators.UnitTestExtension.Tests.Helpers
{
    class FakePropertyValidator : IPropertyValidator
    {
        public IEnumerable<ValidationFailure> Validate(PropertyValidatorContext context)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ValidationFailure>> ValidateAsync(PropertyValidatorContext context, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public bool IsAsync { get; }
        public ICollection<Func<object, object, object>> CustomMessageFormatArguments { get; }
        public Func<object, object> CustomStateProvider { get; set; }
        public IStringSource ErrorMessageSource { get; set; }
        public IStringSource ErrorCodeSource { get; set; }
    }
}
