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

namespace FluentValidation.Validators.UnitTestExtension.Examples.Test
{
    using Production;
    using TestHelper;
    using Xunit;

    public class PersonValidatorTests_ClassicApproach
    {
        readonly PersonValidator validator = new PersonValidator();

        [Fact]
        public void Given_FirstNameIsNull_When_Validating_Then_Error()
        {
	        this.validator.ShouldHaveValidationErrorFor(person => person.FirstName, null as string);
        }

        [Fact]
        public void Given_FirstNameIsEmpty_When_Validating_Then_Error()
        {
	        this.validator.ShouldHaveValidationErrorFor(person => person.FirstName, string.Empty);
        }

        [Fact]
        public void Given_FirstNameIsToLong_When_Validating_Then_Error()
        {
	        this.validator.ShouldHaveValidationErrorFor(person => person.FirstName, "Long_Test_More_Than_20_Characters");
        }

        [Fact]
        public void Given_CorrectFirstName_When_Validating_Then_NoError()
        {
	        this.validator.ShouldNotHaveValidationErrorFor(person => person.FirstName, "John");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("Long_Test_More_Than_20_Characters")]
        public void Given_NotCorrectLastName_When_Validating_Then_Error(string notAcceptedText)
        {
	        this.validator.ShouldHaveValidationErrorFor(person => person.LastName, notAcceptedText);
        }

        [Fact]
        public void Given_CorrectLastName_When_Validating_Then_NoError()
        {
	        this.validator.ShouldNotHaveValidationErrorFor(person => person.LastName, "John");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        [InlineData(260)]
        public void Given_NotCorrectHeight_When_Validating_Then_Error(int height)
        {
	        this.validator.ShouldHaveValidationErrorFor(person => person.HeightInCentimeters, height);
        }

        [Fact]
        public void Given_CorrectHeight_When_Validating_Then_NoError()
        {
	        this.validator.ShouldNotHaveValidationErrorFor(person => person.HeightInCentimeters, 150);
        }

	    [Theory]
	    [InlineData("test")]
	    [InlineData("test@")]
	    [InlineData("@test")]
	    public void Given_NotCorrectEmail_When_Validating_Then_Error(string email)
	    {
		    this.validator.ShouldHaveValidationErrorFor(person => person.Email, email);
	    }

	    [Fact]
	    public void Given_CorrectEmail_When_Validating_Then_NoError()
	    {
		    this.validator.ShouldNotHaveValidationErrorFor(person => person.Email, "test@test.pl");
	    }
	}
}
