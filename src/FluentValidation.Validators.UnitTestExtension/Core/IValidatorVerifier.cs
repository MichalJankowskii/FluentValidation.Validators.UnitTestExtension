namespace FluentValidation.Validators.UnitTestExtension.Core
{
    public interface IValidatorVerifier
    {
        /// <summary>
        /// Verifies the specified validator.
        /// </summary>
        /// <typeparam name="T">The type of the object being validated.</typeparam>
        /// <param name="validator">The validator that will be examined.</param>
        void Verify<T>(T validator);
    }
}
