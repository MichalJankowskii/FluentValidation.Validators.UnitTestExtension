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

namespace FluentValidation.Validators.UnitTestExtension.Tests.Core
{
    using FluentAssertions;
    using Helpers;
    using UnitTestExtension.Core;
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

        private class FakeValidator<T> : AbstractValidator<T>
        {
        }

        private class Customer
        {
            public string Name { get; set; }
            public string Surname { get; set; }
        }

        private class CustomerValidator : AbstractValidator<Customer>
        {
            public CustomerValidator()
            {
	            this.RuleFor(cust => cust.Name).NotEmpty();
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
