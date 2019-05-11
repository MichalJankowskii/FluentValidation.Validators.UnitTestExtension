namespace FluentValidation.Validators.UnitTestExtension.Examples.Production
{
    using System;

    public class PersonValidator : AbstractValidator<Person>
    {
        private readonly string regexString = "^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$";

        public PersonValidator()
        {
            this.RuleFor(person => person.FirstName).NotNull().NotEmpty().Length(0, 20);
            this.RuleFor(person => person.LastName).NotNull().NotEmpty().Length(0, 20);
            this.RuleFor(person => person.HeightInCentimeters).GreaterThan(0).LessThanOrEqualTo(250);
            this.RuleFor(person => person.Email).SetValidator(new RegularExpressionValidator(this.regexString));
            this.RuleFor(person => person.Weight).SetValidator(new ScalePrecisionValidator(2, 4));
            this.RuleFor(person => person.FavouriteDay).SetValidator(new EnumValidator(typeof(DayOfWeek)));
            this.RuleFor(person => person.HeightInMeters).InclusiveBetween(0.0, 2.5);
        }
    }
}
