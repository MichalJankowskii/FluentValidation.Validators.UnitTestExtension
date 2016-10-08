using FluentAssertions;
using FluentValidation.Results;

namespace FluentValidation.Validators.UnitTestExtension.Core
{
    public static class ValidationResultExtension
    {
        public static void IsInvalid(this ValidationResult validationResult, string propertyName, string errorCode)
        {
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.PropertyName == propertyName);
            validationResult.Errors.Should().ContainSingle(e => e.ErrorCode == errorCode);
        }
    }
}