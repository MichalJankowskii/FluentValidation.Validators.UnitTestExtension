namespace FluentValidation.Validators.UnitTestExtension.Tests.Composer.Core
{
	using FluentAssertions;
	using Helpers;
	using UnitTestExtension.Composer;
	using UnitTestExtension.Core;
	using ValidatorVerifiers;
	using Xunit;
	using Xunit.Sdk;

	public class AbstractValidatorExtensionTest
	{
		[Fact]
		public void Given_AbstractValidatorWithoutRules_When_CheckingIfRuleCountIs0_Then_ValidationPass()
		{
			// Arrange
			var validation = new FakeValidator<int>();

			//Act & Assert
			AssertExtension.NotThrows(() => validation.ShouldHaveRulesCount(0));
		}

		[Fact]
		public void Given_AbstractValidatorWith1Rule_When_CheckingIfRuleCountIs1_Then_ValidationPass()
		{
			// Arrange
			var validation = new FakeValidator<int>();
			validation.Include(new FakeValidator<int>());

			//Act & Assert
			AssertExtension.NotThrows(() => validation.ShouldHaveRulesCount(1));
		}

		[Fact]
		public void Given_AbstractValidatorWith1Rule_When_CheckingIfRuleCountIs2_Then_ValidationFail()
		{
			// Arrange
			var validation = new FakeValidator<int>();
			validation.Include(new FakeValidator<int>());

			//Act & Assert
			AssertExtension.Throws<XunitException>(() => validation.ShouldHaveRulesCount(2),
				"(number of rules for object)");
		}

		[Fact]
		public void Given_ValidatorWhichIsNotValidatingProperty_When_ValidatingWith1NeededValidator_Then_ValidationFail()
		{
			// Arrange
			var customerValidator = new CustomerValidator();

			// Act & assert
			AssertExtension.Throws<XunitException>(
				() => customerValidator.ShouldHaveRules(x => x.Surname, new FakeValidatorVerifier()),
				"(number of rules for property)");
		}

		[Fact]
		public void Given_ValidatorWhichIsValidatingProperty_When_ValidatingWith2NeededValidator_Then_ValidationFail()
		{
			// Arrange
			var customerValidator = new CustomerValidator();

			// Act & assert
			AssertExtension.Throws<XunitException>(
				() =>
					customerValidator.ShouldHaveRules(x => x.Name, new FakeValidatorVerifier(),
						new FakeValidatorVerifier()), "(number of rules for property)");
		}

		[Fact]
		public void Given_ValidatorWhichIsValidatingProperty_When_ValidatingWith0NeededValidator_Then_ValidationFail()
		{
			// Arrange
			var customerValidator = new CustomerValidator();

			// Act & assert
			AssertExtension.Throws<XunitException>(() => customerValidator.ShouldHaveRules(x => x.Name),
				"(number of rules for property)");
		}

		[Fact]
		public void
			Given_ValidatorWhichIsValidatingProperty_When_ValidatingWith1NeededFailingValidator_Then_ValidationFail()
		{
			// Arrange
			var customerValidator = new CustomerValidator();

			// Act & assert
			Assert.Throws<XunitException>(
				() => customerValidator.ShouldHaveRules(x => x.Name, new FakeValidatorVerifier(true)));
		}

		[Fact]
		public void
			Given_ValidatorWhichIsValidatingProperty_When_ValidatingWithNeededPassingValidator_Then_ValidationPass()
		{
			// Arrange
			var customerValidator = new CustomerValidator();

			// Act & assert
			AssertExtension.NotThrows(() => customerValidator.ShouldHaveRules(x => x.Name, new FakeValidatorVerifier()));
		}

		[Fact]
		public void
			Given_DelegatingValidatorWhichIsValidatingProperty_When_ValidatingWithNeededPassingValidator_Then_ValidationPass()
		{
			// Arrange
			var customerValidator = new CustomerValidator();

			// Act & assert
			AssertExtension.NotThrows(() => customerValidator.ShouldHaveRules(x => x.Email, new FakeValidatorVerifier()));
		}

		[Fact]
		public void
			Given_ValidatorWhichIsValidatingProperty_When_ValidatingModelWithSimilarNullableFields_Then_ValidationPass()
		{
			// Arrange
			var modelWithNullableFieldsValidator = new ModelWithNullableFieldsValidator();

			// Act & assert
			AssertExtension.NotThrows(() => modelWithNullableFieldsValidator.ShouldHaveRules(x => x.NullableInt.Value,
                new ComparisonValidatorVerifier<GreaterThanValidator>(0)));
		}

		private class FakeValidator<T> : AbstractValidator<T>
		{
		}

		private class ModelWithNullableFields
		{
			public int? NullableInt { get; set; }
			public int? AnotherNullableInt { get; set; }
		}

		private class ModelWithNullableFieldsValidator : AbstractValidator<ModelWithNullableFields>
		{
			public ModelWithNullableFieldsValidator()
			{
				this.RuleFor(model => model.NullableInt.Value).GreaterThan(0);
				this.RuleFor(model => model.AnotherNullableInt.Value).GreaterThan(0);
			}
		}
		
		private class Customer
		{
			public string Name { get; set; }
			public string Surname { get; set; }
			public string Email { get; set; }
		}

		private class CustomerValidator : AbstractValidator<Customer>
		{
			public CustomerValidator()
			{
				this.RuleFor(cust => cust.Name).NotEmpty();
				this.RuleFor(cust => cust.Email).NotEmpty().When(x => true);
			}
		}

		private class FakeValidatorVerifier : IValidatorVerifier
		{
			private readonly bool shouldFailValidation;

			public FakeValidatorVerifier(bool shouldFailValidation = false)
			{
				this.shouldFailValidation = shouldFailValidation;
			}

			public void Verify<T>(T validator)
			{
				this.shouldFailValidation.Should().BeFalse();
			}
		}
	}
}
