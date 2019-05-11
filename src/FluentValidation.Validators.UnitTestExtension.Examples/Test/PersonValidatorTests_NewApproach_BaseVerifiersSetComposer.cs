namespace FluentValidation.Validators.UnitTestExtension.Examples.Test
{
	using System;
	using Composer;
	using Core;
	using Production;
	using Xunit;

	public class PersonValidatorTests_NewApproach_BaseVerifiersSetComposer
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
				BaseVerifiersSetComposer.Build()
					.AddPropertyValidatorVerifier<NotNullValidator>()
					.AddPropertyValidatorVerifier<NotEmptyValidator>()
					.AddPropertyValidatorVerifier<LengthValidator>(0, 20)
					.Create());
		}

		[Fact]
		public void Given_When_PersonValidatorConstructing_Then_RulesForLastNameAreConfiguredCorrectly()
		{
			// Assert
			this.personValidator.ShouldHaveRules(x => x.LastName,
				BaseVerifiersSetComposer.Build()
					.AddPropertyValidatorVerifier<NotNullValidator>()
					.AddPropertyValidatorVerifier<NotEmptyValidator>()
					.AddPropertyValidatorVerifier<LengthValidator>(0, 20)
					.Create());
		}

		[Fact]
		public void Given_When_PersonValidatorConstructing_Then_RulesForHeightAreConfiguredCorrectly()
		{
			// Assert
			this.personValidator.ShouldHaveRules(x => x.HeightInCentimeters,
				BaseVerifiersSetComposer.Build()
					.AddPropertyValidatorVerifier<GreaterThanValidator>(0)
					.AddPropertyValidatorVerifier<LessThanOrEqualValidator>(250)
					.Create());
		}

		[Fact]
		public void Given_When_PersonValidatorConstructing_Then_RulesForEmailAreConfiguredCorrectly()
		{
			// Assert
			this.personValidator.ShouldHaveRules(x => x.Email,
				BaseVerifiersSetComposer.Build()
					.AddPropertyValidatorVerifier<RegularExpressionValidator>("^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$")
					.Create());
		}

		[Fact]
		public void Given_When_PersonValidatorConstructing_Then_RulesForWeightUsingGenericAreConfiguredCorrectly()
		{
			// Assert
			this.personValidator.ShouldHaveRules(x => x.Weight,
				BaseVerifiersSetComposer.Build()
					.AddScalePrecisionValidatorVerifier<ScalePrecisionValidator>(2, 4)
					.Create());
		}

		[Fact]
		public void Given_When_PersonValidatorConstructing_Then_RulesForWeightAreConfiguredCorrectly()
		{
			// Assert
			this.personValidator.ShouldHaveRules(x => x.Weight,
				BaseVerifiersSetComposer.Build()
					.AddScalePrecisionValidatorVerifier(2, 4)
					.Create());
		}

		[Fact]
		public void Given_When_PersonValidatorConstructing_Then_RulesForFavouriteDayUsingGenericAreConfiguredCorrectly()
		{
			// Assert
			this.personValidator.ShouldHaveRules(x => x.FavouriteDay,
				BaseVerifiersSetComposer.Build()
					.AddEnumValidatorVerifier<EnumValidator>(typeof(DayOfWeek))
					.Create());
		}

		[Fact]
		public void Given_When_PersonValidatorConstructing_Then_RulesForFavouriteDayAreConfiguredCorrectly()
		{
			// Assert
			this.personValidator.ShouldHaveRules(x => x.FavouriteDay,
				BaseVerifiersSetComposer.Build()
					.AddEnumValidatorVerifier(typeof(DayOfWeek))
					.Create());
		}

		[Fact]
		public void Given_When_PersonValidatorConstructing_Then_RulesForHeightInMetersValidatorAreConfiguredCorrectly()
		{
			// Assert
			this.personValidator.ShouldHaveRules(x => x.HeightInMeters,
				BaseVerifiersSetComposer.Build()
					.AddBetweenValidatorVerifier<InclusiveBetweenValidator>(0.0, 2.5)
					.Create());
		}
	}
}
