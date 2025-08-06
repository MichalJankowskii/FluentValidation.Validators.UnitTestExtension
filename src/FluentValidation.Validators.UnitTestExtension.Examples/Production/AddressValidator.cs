namespace FluentValidation.Validators.UnitTestExtension.Examples.Production
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(address => address.City).NotNull().NotEmpty().Length(0, 40);
            RuleFor(address => address.Country).NotNull().NotEmpty().Length(0, 40);
        }
    }
}
