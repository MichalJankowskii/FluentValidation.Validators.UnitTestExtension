namespace FluentValidation.Validators.UnitTestExtension.Examples.Production
{
    using System;

    public class Person
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int HeightInCentimeters { get; set; }

        public string Email { get; set; }

        public decimal Weight { get; set; }

        public DayOfWeek FavouriteDay { get; set; }

        public double HeightInMeters { get; set; }
    }
}
