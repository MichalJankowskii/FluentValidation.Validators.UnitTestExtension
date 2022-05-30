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
	        var person = new Person
	        {
		        FirstName = null
	        };
	        this.validator.TestValidate(person).ShouldHaveValidationErrorFor(x => x.FirstName);
        }

        [Fact]
        public void Given_FirstNameIsEmpty_When_Validating_Then_Error()
        {
	        var person = new Person
	        {
		        FirstName = string.Empty
	        };
	        this.validator.TestValidate(person).ShouldHaveValidationErrorFor(x => x.FirstName);
        }

        [Fact]
        public void Given_FirstNameIsToLong_When_Validating_Then_Error()
        {
	        var person = new Person
	        {
		        FirstName = "Long_Test_More_Than_20_Characters"
	        };
	        this.validator.TestValidate(person).ShouldHaveValidationErrorFor(x => x.FirstName);
        }

        [Fact]
        public void Given_CorrectFirstName_When_Validating_Then_NoError()
        {
	        var person = new Person
	        {
		        FirstName = "John"
	        };
	        this.validator.TestValidate(person).ShouldNotHaveValidationErrorFor(x => x.FirstName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("Long_Test_More_Than_20_Characters")]
        public void Given_NotCorrectLastName_When_Validating_Then_Error(string notAcceptedText)
        {
	        var person = new Person
	        {
		        LastName = notAcceptedText
	        };
	        this.validator.TestValidate(person).ShouldHaveValidationErrorFor(x => x.FirstName);
        }

        [Fact]
        public void Given_CorrectLastName_When_Validating_Then_NoError()
        {
	        var person = new Person
	        {
		        LastName = "Doe"
	        };
	        this.validator.TestValidate(person).ShouldNotHaveValidationErrorFor(x => x.LastName);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        [InlineData(260)]
        public void Given_NotCorrectHeight_When_Validating_Then_Error(int height)
        {
	        var person = new Person
	        {
		        HeightInCentimeters = height
	        };
	        this.validator.TestValidate(person).ShouldHaveValidationErrorFor(x => x.HeightInCentimeters);
        }

        [Fact]
        public void Given_CorrectHeight_When_Validating_Then_NoError()
        {
	        var person = new Person
	        {
		        HeightInCentimeters = 150
	        };
	        this.validator.TestValidate(person).ShouldNotHaveValidationErrorFor(x => x.HeightInCentimeters);
        }

	    [Theory]
	    [InlineData("test")]
	    [InlineData("test@")]
	    [InlineData("@test")]
	    public void Given_NotCorrectEmail_When_Validating_Then_Error(string email)
	    {
		    var person = new Person
		    {
			    Email = email
		    };
		    this.validator.TestValidate(person).ShouldHaveValidationErrorFor(x => x.Email);
	    }

	    [Fact]
	    public void Given_CorrectEmail_When_Validating_Then_NoError()
	    {
		    var person = new Person
		    {
			    Email = "test@test.pl"
		    };
		    this.validator.TestValidate(person).ShouldNotHaveValidationErrorFor(x => x.Email);
	    }
	}
}
