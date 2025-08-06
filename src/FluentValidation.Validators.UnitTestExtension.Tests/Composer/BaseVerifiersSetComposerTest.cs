﻿namespace FluentValidation.Validators.UnitTestExtension.Tests.Composer
{
    using System.Linq;
    using System.Text.RegularExpressions;
    using Helpers;
    using Helpers.Fakes;
    using UnitTestExtension.Composer;
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
            Assert.Equal([typeof(TypeValidatorVerifier<FakePropertyValidator>)], rules.Select(x => x.GetType()).ToArray());
        }

        [Fact]
        public void Given_Composer_When_AddingPropertyValidatorVerifierWithObjectToCompare_Then_CorrectRuleSet()
        {
            // Arrange
            var composer = BaseVerifiersSetComposer.Build();
            var fakeComparisonValidator = new EqualValidator<object, int>(10, new Int32EqualityComparer());

            // Act
            var rules = composer.AddEqualValidatorVerifier<EqualValidator<object, int>, object, int>(10).Create();

            // Assert
            Assert.Equal([typeof(EqualValidatorVerifier<EqualValidator<object, int>, object, int>)], rules.Select(x => x.GetType()).ToArray());
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
            Assert.Equal([typeof(LengthValidatorVerifier<FakeLengthValidator>)], rules.Select(x => x.GetType()).ToArray());
            AssertExtension.NotThrows(() => rules[0].Verify(fakeLengthValidator));
        }

        [Fact]
        public void Given_Composer_When_AddingPropertyValidatorVerifierWithExpression_Then_CorrectRuleSet()
        {
            // Arrange
            var composer = BaseVerifiersSetComposer.Build();
            var regularExpressionValidator = new FakeRegularExpressionValidator { Expression = "regex" };

            // Act
            var rules = composer.AddPropertyValidatorVerifier<FakeRegularExpressionValidator>("regex").Create();

            // Assert
            Assert.Equal([typeof(RegularExpressionValidatorVerifier<FakeRegularExpressionValidator>)], rules.Select(x => x.GetType()).ToArray());
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
            Assert.Equal([typeof(RegularExpressionValidatorVerifier<FakeRegularExpressionValidator>)], rules.Select(x => x.GetType()).ToArray());
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
            Assert.Equal([typeof(RegularExpressionValidatorVerifier<FakeRegularExpressionValidator>)], rules.Select(x => x.GetType()).ToArray());
            AssertExtension.NotThrows(() => rules[0].Verify(regularExpressionValidator));
        }

        [Fact]
        public void Given_Composer_When_AddBetweenValidatorVerifierGeneric_Then_CorrectRuleSet()
        {
            // Arrange
            var composer = BaseVerifiersSetComposer.Build();
            var betweenValidator = new FakeBetweenValidator { From = 1, To = 2 };

            // Act
            var rules = composer.AddBetweenValidatorVerifier<FakeBetweenValidator>(1, 2).Create();

            // Assert
            Assert.Equal([typeof(BetweenValidatorVerifier<FakeBetweenValidator>)], rules.Select(x => x.GetType()).ToArray());
            AssertExtension.NotThrows(() => rules[0].Verify(betweenValidator));
        }

        [Fact]
        public void Given_Composer_When_AddPrecisionScaleValidatorVerifierGeneric_Then_CorrectRuleSet()
        {
            // Arrange
            var composer = BaseVerifiersSetComposer.Build();
            var scalePrecisionValidator = new PrecisionScaleValidator<object>(2, 1, false);

            // Act
            var rules = composer.AddPrecisionScaleValidatorVerifier<PrecisionScaleValidator<object>, object>(2, 1, false).Create();

            // Assert
            Assert.Equal([typeof(PrecisionScaleValidatorVerifier<PrecisionScaleValidator<object>, object>)], rules.Select(x => x.GetType()).ToArray());
            AssertExtension.NotThrows(() => rules[0].Verify(scalePrecisionValidator));
        }

        [Fact]
        public void Given_Composer_When_AddEnumValidatorVerifierGeneric_Then_CorrectRuleSet()
        {
            // Arrange
            var composer = BaseVerifiersSetComposer.Build();
            var enumValidator = new EnumValidator<object, FakeEnum>();

            // Act
            var rules = composer.AddEnumValidatorVerifier<EnumValidator<object, FakeEnum>, object, FakeEnum>().Create();

            // Assert
            Assert.Equal([typeof(EnumValidatorVerifier<EnumValidator<object, FakeEnum>, object, FakeEnum>)], rules.Select(x => x.GetType()).ToArray());
            AssertExtension.NotThrows(() => rules[0].Verify(enumValidator));
        }

        [Fact]
        public void Given_Composer_When_AddEnumValidatorVerifierNotGeneric_Then_CorrectRuleSet()
        {
            // Arrange
            var composer = BaseVerifiersSetComposer.Build();
            var enumValidator = new EnumValidator<object, FakeEnum>();

            // Act
            var rules = composer.AddEnumValidatorVerifier<object, FakeEnum>().Create();

            // Assert
            Assert.Equal([typeof(EnumValidatorVerifier<EnumValidator<object, FakeEnum>, object, FakeEnum>)], rules.Select(x => x.GetType()).ToArray());
            AssertExtension.NotThrows(() => rules[0].Verify(enumValidator));
        }

        [Fact]
        public void Given_Composer_When_AddingChildValidatorVerifier_Then_CorrectRuleSet()
        {
            // Arrange
            var composer = BaseVerifiersSetComposer.Build();

            // Act
            var rules = composer.AddChildValidatorVerifier<string, double, int>().Create();

            // Assert
            Assert.Equal([typeof(ChildValidatorVerifier<string, double, int>)], rules.Select(x => x.GetType()).ToArray());
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
            Assert.Equal([fakeValidatorVerifier], rules.ToArray());
        }

        [Fact]
        public void Given_Composer_When_AddingPlaceholderVerifier_Then_CorrectRuleSet()
        {
            // Arrange
            var composer = BaseVerifiersSetComposer.Build();

            // Act
            var rules = composer.AddPlaceholderVerifier().Create();

            // Assert
            Assert.Equal([typeof(PlaceholderVerifier)], rules.Select(x => x.GetType()).ToArray());
        }

        [Fact]
        public void Given_Composer_When_ExactLengthValidatorVerifierGeneric_Then_CorrectRuleSet()
        {
            // Arrange
            var composer = BaseVerifiersSetComposer.Build();
            var exactLengthValidator = new ExactLengthValidator<object>(10);

            // Act
            var rules = composer.AddExactLengthValidatorVerifier<object>(10).Create();

            // Assert
            Assert.Equal([typeof(ExactLengthValidatorVerifier<object>)], rules.Select(x => x.GetType()).ToArray());
            AssertExtension.NotThrows(() => rules[0].Verify(exactLengthValidator));
        }

        [Fact]
        public void Given_Composer_When_AddMaximumLengthVerifierGeneric_Then_CorrectRuleSet()
        {
            // Arrange
            var composer = BaseVerifiersSetComposer.Build();
            var maximumLengthValidator = new MaximumLengthValidator<object>(10);

            // Act
            var rules = composer.AddMaximumLengthValidatorVerifier<object>(10).Create();

            // Assert
            Assert.Equal([typeof(MaximumLengthValidatorVerifier<object>)], rules.Select(x => x.GetType()).ToArray());
            AssertExtension.NotThrows(() => rules[0].Verify(maximumLengthValidator));
        }
        [Fact]
        public void Given_Composer_When_AddMinimumLenghtValidatorVerifierGeneric_Then_CorrectRuleSet()
        {
            // Arrange
            var composer = BaseVerifiersSetComposer.Build();
            var exactLengthValidator = new MinimumLengthValidator<object>(10);

            // Act
            var rules = composer.AddMinimumLengthValidatorVerifier<object>(10).Create();

            // Assert
            Assert.Equal([typeof(MinimumLengthValidatorVerifier<object>)], rules.Select(x => x.GetType()).ToArray());
            AssertExtension.NotThrows(() => rules[0].Verify(exactLengthValidator));
        }
    }
}
