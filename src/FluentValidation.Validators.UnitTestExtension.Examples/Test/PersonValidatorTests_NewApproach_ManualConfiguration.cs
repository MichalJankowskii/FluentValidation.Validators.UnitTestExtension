namespace FluentValidation.Validators.UnitTestExtension.Examples.Test
{
	using System;
	using Core;
	using Production;
	using ValidatorVerifiers;
	using Xunit;

	public class PersonValidatorTests_NewApproach_ManualConfiguration
	{
		// Act
		readonly PersonValidator personValidator = new PersonValidator();

		[Fact]
		public void Given_When_PersonValidatorConstructing_Then_8PropertiesShouldHaveRules()
		{
			// Assert
			this.personValidator.ShouldHaveRulesCount(8);
		}

		[Fact]
		public void Given_When_PersonValidatorConstructing_Then_RulesForFirstNameAreConfiguredCorrectly()
		{
			// Assert
			this.personValidator.ShouldHaveRules(x => x.FirstName,
				new IValidatorVerifier[]
				{
					new TypeValidatorVerifier<NotNullValidator<Person, string>>(),
					new TypeValidatorVerifier<NotEmptyValidator<Person, string>>(),
					new LengthValidatorVerifier<LengthValidator<Person>>(0, 20)
				});
		}

		[Fact]
		public void Given_When_PersonValidatorConstructing_Then_RulesForLastNameAreConfiguredCorrectly()
		{
			// Assert
			this.personValidator.ShouldHaveRules(x => x.LastName,
				new IValidatorVerifier[]
				{
					new TypeValidatorVerifier<NotNullValidator<Person, string>>(),
					new TypeValidatorVerifier<NotEmptyValidator<Person, string>>(),
					new LengthValidatorVerifier<LengthValidator<Person>>(0, 20)
				});
		}

		[Fact]
		public void Given_When_PersonValidatorConstructing_Then_RulesForHeightAreConfiguredCorrectly()
		{
			// Assert
			this.personValidator.ShouldHaveRules(x => x.HeightInCentimeters,
				new IValidatorVerifier[]
				{
					new AbstractComparisonValidatorVerifier<GreaterThanValidator<Person, int>, Person, int>(0),
					new AbstractComparisonValidatorVerifier<LessThanOrEqualValidator<Person, int>, Person, int>(250),
				});
		}

		[Fact]
		public void Given_When_PersonValidatorConstructing_Then_RulesForEmailAreConfiguredCorrectly()
		{
			// Assert
			this.personValidator.ShouldHaveRules(x => x.Email,
				new IValidatorVerifier[]
				{
					new RegularExpressionValidatorVerifier<RegularExpressionValidator<Person>>("^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$")
				});
		}

		[Fact]
		public void Given_When_PersonValidatorConstructing_Then_RulesForWeightAreConfiguredCorrectly()
		{
			// Assert
			this.personValidator.ShouldHaveRules(x => x.Weight,
				new IValidatorVerifier[]
				{
					new ScalePrecisionValidatorVerifier<ScalePrecisionValidator<Person>, Person>(2, 4)
				});
		}

		[Fact]
		public void Given_When_PersonValidatorConstructing_Then_RulesForFavouriteDayAreConfiguredCorrectly()
		{
			// Assert
			this.personValidator.ShouldHaveRules(x => x.FavouriteDay,
				new IValidatorVerifier[]
				{
					new EnumValidatorVerifier<EnumValidator<Person, DayOfWeek>, Person, DayOfWeek>(typeof(DayOfWeek))
				});
		}

		[Fact]
		public void Given_When_PersonValidatorConstructing_Then_RulesForHeightInMetersValidatorAreConfiguredCorrectly()
		{
			// Assert
			this.personValidator.ShouldHaveRules(x => x.HeightInMeters,
				new IValidatorVerifier[]
				{
					new BetweenValidatorVerifier<InclusiveBetweenValidator<Person, double>>(0.0, 2.5)
				});
		}

        [Fact]
        public void Given_When_PersonValidatorConstructing_Then_RulesForAddressValidatorAreConfiguredCorrectly()
        {
            // Assert
            this.personValidator.ShouldHaveRules(x => x.Address,
                new IValidatorVerifier[]
                {
                    new ChildValidatorVerifier<AddressValidator, Person, Address>()
                });
        }
	}
}
