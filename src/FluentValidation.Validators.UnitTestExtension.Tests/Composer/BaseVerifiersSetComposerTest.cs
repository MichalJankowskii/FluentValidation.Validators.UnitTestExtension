namespace FluentValidation.Validators.UnitTestExtension.Tests.Composer
{
    using System.Linq;
    using System.Text.RegularExpressions;
    using Helpers;
    using Helpers.Fakes;
    using UnitTestExtension.Composer;
    using UnitTestExtension.Core;
    using ValidatorVerifiers;
    using Xunit;

    public class BaseVerifiersSetComposerTest
    {
        [Fact]
        public void Given_When_BuildNewRuleSet_Then_NoVerifiersInList()
        {
            // Act
            var composer = BaseVerifiersSetComposer.Build();

            // Assert
            Assert.Empty(composer.Create());
        }

        [Fact]
        public void Given_Composer_When_AddingPropertyValidatorVerifier_Then_CorrectRuleSet()
        {
            // Arrange
            var composer = BaseVerifiersSetComposer.Build();

            // Act
            var rules = composer.AddPropertyValidatorVerifier<FakePropertyValidator>().Create();

            // Assert
            Assert.Equal(new[] { typeof(TypeValidatorVerifier<FakePropertyValidator>) }, rules.Select(x => x.GetType()).ToArray());
        }

        [Fact]
        public void Given_Composer_When_AddingPropertyValidatorVerifierWithObjectToCompare_Then_CorrectRuleSet()
        {
            // Arrange
            var composer = BaseVerifiersSetComposer.Build();
            var fakeComparisonValidator = new FakeComparisonValidator {ValueToCompare = 10, Comparison = Comparison.Equal};

            // Act
            var rules = composer.AddPropertyValidatorVerifier<FakeComparisonValidator>(10, Comparison.Equal).Create();

            // Assert
            Assert.Equal(new[] {typeof(ComparisonValidatorVerifier<FakeComparisonValidator>) }, rules.Select(x => x.GetType()).ToArray());
            AssertExtension.NotThrows(() => rules[0].Verify(fakeComparisonValidator));
        }

        [Fact]
        public void Given_Composer_When_AddingPropertyValidatorVerifierWithRange_Then_CorrectRuleSet()
        {
            // Arrange
            var composer = BaseVerifiersSetComposer.Build();
            var fakeLengthValidator = new FakeLengthValidator { Min = 1, Max = 10 };

            // Act
            var rules = composer.AddPropertyValidatorVerifier<FakeLengthValidator>(1, 10).Create();

            // Assert
            Assert.Equal(new[] { typeof(LengthValidatorVerifier<FakeLengthValidator>) }, rules.Select(x => x.GetType()).ToArray());
            AssertExtension.NotThrows(() => rules[0].Verify(fakeLengthValidator));
        }

	    [Fact]
	    public void Given_Composer_When_AddingPropertyValidatorVerifierWithExpression_Then_CorrectRuleSet()
	    {
		    // Arrange
		    var composer = BaseVerifiersSetComposer.Build();
		    var regularExpressionValidator = new FakeRegularExpressionValidator { Expression = "regex"};

		    // Act
		    var rules = composer.AddPropertyValidatorVerifier<FakeRegularExpressionValidator>("regex").Create();

		    // Assert
		    Assert.Equal(new[] { typeof(RegularExpressionValidatorVerifier<FakeRegularExpressionValidator>) }, rules.Select(x => x.GetType()).ToArray());
		    AssertExtension.NotThrows(() => rules[0].Verify(regularExpressionValidator));
	    }

        [Fact]
        public void Given_Composer_When_AddingPropertyValidatorVerifierWithExpressionAndRegexOptions_Then_CorrectRuleSet()
        {
            // Arrange
            var composer = BaseVerifiersSetComposer.Build();
            var regularExpressionValidator = new FakeRegularExpressionValidator { Expression = "regex" };

            // Act
            var rules = composer.AddPropertyValidatorVerifier<FakeRegularExpressionValidator>("regex", RegexOptions.None).Create();

            // Assert
            Assert.Equal(new[] { typeof(RegularExpressionValidatorVerifier<FakeRegularExpressionValidator>) }, rules.Select(x => x.GetType()).ToArray());
            AssertExtension.NotThrows(() => rules[0].Verify(regularExpressionValidator));
        }

        [Fact]
	    public void Given_Composer_When_AddingPropertyValidatorVerifierWithRegex_Then_CorrectRuleSet()
	    {
		    // Arrange
		    var composer = BaseVerifiersSetComposer.Build();
		    var regularExpressionValidator = new FakeRegularExpressionValidator { Expression = "regex" };

		    // Act
		    var rules = composer.AddPropertyValidatorVerifier<FakeRegularExpressionValidator>(new Regex("regex")).Create();

		    // Assert
		    Assert.Equal(new[] { typeof(RegularExpressionValidatorVerifier<FakeRegularExpressionValidator>) }, rules.Select(x => x.GetType()).ToArray());
		    AssertExtension.NotThrows(() => rules[0].Verify(regularExpressionValidator));
	    }

        [Fact]
        public void Given_Composer_When_AddBetweenValidatorVerifierGeneric_Then_CorrectRuleSet()
        {
            // Arrange
            var composer = BaseVerifiersSetComposer.Build();
            var betweenValidator = new FakeBetweenValidator {From = 1, To = 2};

            // Act
            var rules = composer.AddBetweenValidatorVerifier<FakeBetweenValidator>(1, 2).Create();

            // Assert
            Assert.Equal(new[] {typeof(BetweenValidatorVerifier<FakeBetweenValidator>)}, rules.Select(x => x.GetType()).ToArray());
            AssertExtension.NotThrows(() => rules[0].Verify(betweenValidator));
        }

        [Fact]
        public void Given_Composer_When_AddScalePrecisionValidatorVerifierGeneric_Then_CorrectRuleSet()
        {
            // Arrange
            var composer = BaseVerifiersSetComposer.Build();
            var scalePrecisionValidator = new ScalePrecisionValidator(1, 2);

            // Act
            var rules = composer.AddScalePrecisionValidatorVerifier<ScalePrecisionValidator>(1, 2).Create();

            // Assert
            Assert.Equal(new[] { typeof(ScalePrecisionValidatorVerifier<ScalePrecisionValidator>) }, rules.Select(x => x.GetType()).ToArray());
            AssertExtension.NotThrows(() => rules[0].Verify(scalePrecisionValidator));
        }

        [Fact]
        public void Given_Composer_When_AddScalePrecisionValidatorVerifierNotGeneric_Then_CorrectRuleSet()
        {
            // Arrange
            var composer = BaseVerifiersSetComposer.Build();
            var scalePrecisionValidator = new ScalePrecisionValidator(1, 2);

            // Act
            var rules = composer.AddScalePrecisionValidatorVerifier(1, 2).Create();

            // Assert
            Assert.Equal(new[] { typeof(ScalePrecisionValidatorVerifier<ScalePrecisionValidator>) }, rules.Select(x => x.GetType()).ToArray());
            AssertExtension.NotThrows(() => rules[0].Verify(scalePrecisionValidator));
        }

        [Fact]
        public void Given_Composer_When_AddEnumValidatorVerifierGeneric_Then_CorrectRuleSet()
        {
            // Arrange
            var composer = BaseVerifiersSetComposer.Build();
            var enumValidator = new EnumValidator(typeof(FakeEnum));

            // Act
            var rules = composer.AddEnumValidatorVerifier<EnumValidator>(typeof(FakeEnum)).Create();

            // Assert
            Assert.Equal(new[] { typeof(EnumValidatorVerifier<EnumValidator>) }, rules.Select(x => x.GetType()).ToArray());
            AssertExtension.NotThrows(() => rules[0].Verify(enumValidator));
        }

        [Fact]
        public void Given_Composer_When_AddEnumValidatorVerifierNotGeneric_Then_CorrectRuleSet()
        {
            // Arrange
            var composer = BaseVerifiersSetComposer.Build();
            var enumValidator = new EnumValidator(typeof(FakeEnum));

            // Act
            var rules = composer.AddEnumValidatorVerifier(typeof(FakeEnum)).Create();

            // Assert
            Assert.Equal(new[] { typeof(EnumValidatorVerifier<EnumValidator>) }, rules.Select(x => x.GetType()).ToArray());
            AssertExtension.NotThrows(() => rules[0].Verify(enumValidator));
        }

        [Fact]
        public void Given_Composer_When_AddingChildValidatorVerifier_Then_CorrectRuleSet()
        {
            // Arrange
            var composer = BaseVerifiersSetComposer.Build();

            // Act
            var rules = composer.AddChildValidatorVerifier<int>().Create();

            // Assert
            Assert.Equal(new[] { typeof(ChildValidatorVerifier<int>) }, rules.Select(x => x.GetType()).ToArray());
        }

        [Fact]
        public void Given_Composer_When_AddingAnyValidatorVerifier_Then_CorrectRuleSet()
        {
            // Arrange
            var composer = BaseVerifiersSetComposer.Build();
            var fakeValidatorVerifier = new FakeValidatorVerifier();

            // Act
            var rules = composer.AddVerifier(fakeValidatorVerifier).Create();

            // Assert
            Assert.Equal(new IValidatorVerifier[] { fakeValidatorVerifier }, rules.ToArray());
        }

        [Fact]
        public void Given_Composer_When_AddingPlaceholderVerifier_Then_CorrectRuleSet()
        {
            // Arrange
            var composer = BaseVerifiersSetComposer.Build();

            // Act
            var rules = composer.AddPlaceholderVerifier().Create();

            // Assert
            Assert.Equal(new[] { typeof(PlaceholderVerifier) }, rules.Select(x => x.GetType()).ToArray());
        }

        [Fact]
        public void Given_Composer_When_ExactLengthValidatorVerifierGeneric_Then_CorrectRuleSet()
        {
            // Arrange
            var composer = BaseVerifiersSetComposer.Build();
            var exactLengthValidator = new ExactLengthValidator(10);

            // Act
            var rules = composer.AddExactLengthValidatorVerifier(10).Create();

            // Assert
            Assert.Equal(new[] { typeof(ExactLengthValidatorVerifier) }, rules.Select(x => x.GetType()).ToArray());
            AssertExtension.NotThrows(() => rules[0].Verify(exactLengthValidator));
        }

        [Fact]
        public void Given_Composer_When_AddMaximumLengthVerifierGeneric_Then_CorrectRuleSet()
        {
            // Arrange
            var composer = BaseVerifiersSetComposer.Build();
            var maximumLengthValidator = new MaximumLengthValidator(10);

            // Act
            var rules = composer.AddMaximumLengthValidatorVerifier(10).Create();

            // Assert
            Assert.Equal(new[] { typeof(MaximumLengthValidatorVerifier) }, rules.Select(x => x.GetType()).ToArray());
            AssertExtension.NotThrows(() => rules[0].Verify(maximumLengthValidator));
        }
        [Fact]
        public void Given_Composer_When_AddMinimumLenghtValidatorVerifierGeneric_Then_CorrectRuleSet()
        {
            // Arrange
            var composer = BaseVerifiersSetComposer.Build();
            var exactLengthValidator = new MinimumLengthValidator(10);

            // Act
            var rules = composer.AddMinimumLengthValidatorVerifier(10).Create();

            // Assert
            Assert.Equal(new[] { typeof(MinimumLengthValidatorVerifier) }, rules.Select(x => x.GetType()).ToArray());
            AssertExtension.NotThrows(() => rules[0].Verify(exactLengthValidator));
        }

    }
}
