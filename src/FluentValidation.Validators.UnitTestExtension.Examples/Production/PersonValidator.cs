namespace FluentValidation.Validators.UnitTestExtension.Examples.Production
{
    using System;

    public class PersonValidator : AbstractValidator<Person>
    {
        private readonly string regexString = "^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$";

        public PersonValidator()
        {
            RuleFor(person => person.FirstName).NotNull().NotEmpty().Length(0, 20);
            RuleFor(person => person.LastName).NotNull().NotEmpty().Length(0, 20);
            RuleFor(person => person.HeightInCentimeters).GreaterThan(0).LessThanOrEqualTo(250);
            RuleFor(person => person.Email).SetValidator(new RegularExpressionValidator<Person>(regexString));
            RuleFor(person => person.Weight).SetValidator(new PrecisionScaleValidator<Person>(4, 2, false));
            RuleFor(person => person.FavouriteDay).SetValidator(new EnumValidator<Person, DayOfWeek>());
            RuleFor(person => person.HeightInMeters).InclusiveBetween(0.0, 2.5);
            RuleFor(person => person.Address).SetValidator(new AddressValidator());
        }
    }
}
