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
		public void Given_When_PersonValidatorConstructing_Then_7PropertiesShouldHaveRules()
		{
			// Assert
			this.personValidator.ShouldHaveRulesCount(7);
		}

		[Fact]
		public void Given_When_PersonValidatorConstructing_Then_RulesForFirstNameAreConfiguredCorrectly()
		{
			// Assert
			this.personValidator.ShouldHaveRules(x => x.FirstName,
				new IValidatorVerifier[]
				{
					new TypeValidatorVerifier<NotNullValidator>(),
					new TypeValidatorVerifier<NotEmptyValidator>(),
					new LengthValidatorVerifier<LengthValidator>(0, 20)
				});
		}

		[Fact]
		public void Given_When_PersonValidatorConstructing_Then_RulesForLastNameAreConfiguredCorrectly()
		{
			// Assert
			this.personValidator.ShouldHaveRules(x => x.LastName,
				new IValidatorVerifier[]
				{
					new TypeValidatorVerifier<NotNullValidator>(),
					new TypeValidatorVerifier<NotEmptyValidator>(),
					new LengthValidatorVerifier<LengthValidator>(0, 20)
				});
		}

		[Fact]
		public void Given_When_PersonValidatorConstructing_Then_RulesForHeightAreConfiguredCorrectly()
		{
			// Assert
			this.personValidator.ShouldHaveRules(x => x.HeightInCentimeters,
				new IValidatorVerifier[]
				{
					new ComparisonValidatorVerifier<GreaterThanValidator>(0),
					new ComparisonValidatorVerifier<LessThanOrEqualValidator>(250),
				});
		}

		[Fact]
		public void Given_When_PersonValidatorConstructing_Then_RulesForEmailAreConfiguredCorrectly()
		{
			// Assert
			this.personValidator.ShouldHaveRules(x => x.Email,
				new IValidatorVerifier[]
				{
					new RegularExpressionValidatorVerifier<RegularExpressionValidator>("^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$")
				});
		}

		[Fact]
		public void Given_When_PersonValidatorConstructing_Then_RulesForWeightAreConfiguredCorrectly()
		{
			// Assert
			this.personValidator.ShouldHaveRules(x => x.Weight,
				new IValidatorVerifier[]
				{
					new ScalePrecisionValidatorVerifier<ScalePrecisionValidator>(2, 4)
				});
		}

		[Fact]
		public void Given_When_PersonValidatorConstructing_Then_RulesForFavouriteDayAreConfiguredCorrectly()
		{
			// Assert
			this.personValidator.ShouldHaveRules(x => x.FavouriteDay,
				new IValidatorVerifier[]
				{
					new EnumValidatorVerifier<EnumValidator>(typeof(DayOfWeek))
				});
		}

		[Fact]
		public void Given_When_PersonValidatorConstructing_Then_RulesForHeightInMetersValidatorAreConfiguredCorrectly()
		{
			// Assert
			this.personValidator.ShouldHaveRules(x => x.HeightInMeters,
				new IValidatorVerifier[]
				{
					new BetweenValidatorVerifier<InclusiveBetweenValidator>(0.0, 2.5)
				});
		}
	}
}
