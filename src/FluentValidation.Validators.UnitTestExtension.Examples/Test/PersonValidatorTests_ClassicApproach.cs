namespace FluentValidation.Validators.UnitTestExtension.Examples.Test
{
    using Production;
    using TestHelper;
    using Xunit;

    public class PersonValidatorTests_ClassicApproach
    {
        private readonly PersonValidator validator = new PersonValidator();

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
