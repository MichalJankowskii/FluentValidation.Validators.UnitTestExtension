#region License
// MIT License
// 
// Copyright(c) 2016 Michał Jankowski (http://www.jankowskimichal.pl)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// 
// The latest version of this file can be found at https://github.com/MichalJankowskii/FluentValidation.Validators.UnitTestExtension
#endregion

namespace FluentValidation.Validators.UnitTestExtension.Tests.Composer
{
    using System.Linq;
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
        public void Given_Composer_When_AddingCollectionValidatorVerifier_Then_CorrectRuleSet()
        {
            // Arrange
            var composer = BaseVerifiersSetComposer.Build();

            // Act
            var rules = composer.AddChildCollectionValidatorVerifier<int>().Create();

            // Assert
            Assert.Equal(new[] { typeof(ChildCollectionValidatorVerifier<int>) }, rules.Select(x => x.GetType()).ToArray());
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
        public void Given_Composer_When_AddingCustomVerifier_Then_CorrectRuleSet()
        {
            // Arrange
            var composer = BaseVerifiersSetComposer.Build();

            // Act
            var rules = composer.AddCustomVerifier().Create();

            // Assert

            Assert.Equal(new[] { typeof(CustomVerifier) }, rules.Select(x => x.GetType()).ToArray());
        }
    }
}
