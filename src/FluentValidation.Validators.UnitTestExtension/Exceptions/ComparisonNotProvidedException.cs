namespace FluentValidation.Validators.UnitTestExtension.Exceptions
{
    using System;

    public class ComparisonNotProvidedException : ArgumentException
    {
        public ComparisonNotProvidedException() : base("Comparison must be provided for not build-in IComparisonValidator.")
        {            
        }
    }
}
