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
        public void Given_When_PersonValidatorConstructing_Then_8PropertiesShouldHaveRules()
        {
            // Assert
            personValidator.ShouldHaveRulesCount(8);
        }

        [Fact]
        public void Given_When_PersonValidatorConstructing_Then_RulesForFirstNameAreConfiguredCorrectly()
        {
            // Assert
            personValidator.ShouldHaveRules(x => x.FirstName,
                BaseVerifiersSetComposer.Build()
                    .AddPropertyValidatorVerifier<NotNullValidator<Person, string>>()
                    .AddPropertyValidatorVerifier<NotEmptyValidator<Person, string>>()
                    .AddPropertyValidatorVerifier<LengthValidator<Person>>(0, 20)
                    .Create());
        }

        [Fact]
        public void Given_When_PersonValidatorConstructing_Then_RulesForLastNameAreConfiguredCorrectly()
        {
            // Assert
            personValidator.ShouldHaveRules(x => x.LastName,
                BaseVerifiersSetComposer.Build()
                    .AddPropertyValidatorVerifier<NotNullValidator<Person, string>>()
                    .AddPropertyValidatorVerifier<NotEmptyValidator<Person, string>>()
                    .AddPropertyValidatorVerifier<LengthValidator<Person>>(0, 20)
                    .Create());
        }

        [Fact]
        public void Given_When_PersonValidatorConstructing_Then_RulesForHeightAreConfiguredCorrectly()
        {
            // Assert
            personValidator.ShouldHaveRules(x => x.HeightInCentimeters,
                BaseVerifiersSetComposer.Build()
                    .AddComparisonValidatorVerifier<GreaterThanValidator<Person, int>, Person, int>(0)
                    .AddComparisonValidatorVerifier<LessThanOrEqualValidator<Person, int>, Person, int>(250)
                    .Create());
        }

        [Fact]
        public void Given_When_PersonValidatorConstructing_Then_RulesForEmailAreConfiguredCorrectly()
        {
            // Assert
            personValidator.ShouldHaveRules(x => x.Email,
                BaseVerifiersSetComposer.Build()
                    .AddPropertyValidatorVerifier<RegularExpressionValidator<Person>>("^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$")
                    .Create());
        }

        [Fact]
        public void Given_When_PersonValidatorConstructing_Then_RulesForWeightUsingGenericAreConfiguredCorrectly()
        {
            // Assert
            personValidator.ShouldHaveRules(x => x.Weight,
                BaseVerifiersSetComposer.Build()
                    .AddPrecisionScaleValidatorVerifier<PrecisionScaleValidator<Person>, Person>(4, 2, false)
                    .Create());
        }

        [Fact]
        public void Given_When_PersonValidatorConstructing_Then_RulesForFavouriteDayUsingGenericAreConfiguredCorrectly()
        {
            // Assert
            personValidator.ShouldHaveRules(x => x.FavouriteDay,
                BaseVerifiersSetComposer.Build()
                    .AddEnumValidatorVerifier<EnumValidator<Person, DayOfWeek>, Person, DayOfWeek>()
                    .Create());
        }

        [Fact]
        public void Given_When_PersonValidatorConstructing_Then_RulesForFavouriteDayAreConfiguredCorrectly()
        {
            // Assert
            personValidator.ShouldHaveRules(x => x.FavouriteDay,
                BaseVerifiersSetComposer.Build()
                    .AddEnumValidatorVerifier<Person, DayOfWeek>()
                    .Create());
        }

        [Fact]
        public void Given_When_PersonValidatorConstructing_Then_RulesForHeightInMetersValidatorAreConfiguredCorrectly()
        {
            // Assert
            personValidator.ShouldHaveRules(x => x.HeightInMeters,
                BaseVerifiersSetComposer.Build()
                    .AddBetweenValidatorVerifier<InclusiveBetweenValidator<Person, double>>(0.0, 2.5)
                    .Create());
        }

        [Fact]
        public void Given_When_PersonValidatorConstructing_Then_RulesForAddressValidatorAreConfiguredCorrectly()
        {
            // Assert
            personValidator.ShouldHaveRules(x => x.Address,
                BaseVerifiersSetComposer.Build()
                    .AddChildValidatorVerifier<AddressValidator, Person, Address>()
                    .Create());
        }
    }
}
