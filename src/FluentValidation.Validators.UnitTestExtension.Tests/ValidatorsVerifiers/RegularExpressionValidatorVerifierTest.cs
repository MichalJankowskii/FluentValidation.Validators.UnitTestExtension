namespace FluentValidation.Validators.UnitTestExtension.Tests.ValidatorsVerifiers
{
	using System.Text.RegularExpressions;
	using Helpers;
	using Helpers.Fakes;
	using ValidatorVerifiers;
	using Xunit;
	using Xunit.Sdk;

	public class RegularExpressionValidatorVerifierTest
	{
		[Fact]
		public void Given_DifferentValidatorType_When_Verifying_Then_ValidationMustFail()
		{
			// Arrange
			var otherValidator = new FakePropertyValidator();
			var verifier = new RegularExpressionValidatorVerifier<RegularExpressionValidator>("regex");

			// Act & Assert
			AssertExtension.Throws<XunitException>(() => verifier.Verify(otherValidator), "(wrong type)");
		}

		[Fact]
		public void Given_CorrectValidatorWithDifferentRegex_When_Verifying_Then_ValidationFail()
		{
			// Arrange
			var regularExpressionValidator = new FakeRegularExpressionValidator {Expression = "regex"};
			var verifier = new RegularExpressionValidatorVerifier<FakeRegularExpressionValidator>(new Regex("otherRegex"));

			// Act & Assert
			AssertExtension.Throws<XunitException>(() => verifier.Verify(regularExpressionValidator),
				"(Expression property)");
		}

		[Fact]
		public void Given_CorrectValidatorWithDifferentExpression_When_Verifying_Then_ValidationFail()
		{
			// Arrange
			var regularExpressionValidator = new FakeRegularExpressionValidator { Expression = "regex" };
			var verifier = new RegularExpressionValidatorVerifier<FakeRegularExpressionValidator>("otherRegex");

			// Act & Assert
			AssertExtension.Throws<XunitException>(() => verifier.Verify(regularExpressionValidator),
				"(Expression property)");
		}

		[Fact]
		public void Given_CorrectValidatorWithRegex_When_Verifying_Then_ValidationPass()
		{
			// Arrange
			var regularExpressionValidator = new FakeRegularExpressionValidator { Expression = "regex" };
			var verifier = new RegularExpressionValidatorVerifier<FakeRegularExpressionValidator>(new Regex("regex"));

			// Act & Assert
			AssertExtension.NotThrows(() => verifier.Verify(regularExpressionValidator));
		}

		[Fact]
		public void Given_CorrectValidatorWithExpression_When_Verifying_Then_ValidationPass()
		{
			// Arrange
			var regularExpressionValidator = new FakeRegularExpressionValidator { Expression = "regex" };
			var verifier = new RegularExpressionValidatorVerifier<FakeRegularExpressionValidator>("regex");

			// Act & Assert
			AssertExtension.NotThrows(() => verifier.Verify(regularExpressionValidator));
		}

	    [Fact]
	    public void Given_CorrectValidatorWithExpressionAndRegexOptions_When_Verifying_Then_ValidationPass()
	    {
	        // Arrange
	        var regularExpressionValidator = new FakeRegularExpressionValidator { Expression = "regex" };
	        var verifier = new RegularExpressionValidatorVerifier<FakeRegularExpressionValidator>("regex", RegexOptions.None);

	        // Act & Assert
	        AssertExtension.NotThrows(() => verifier.Verify(regularExpressionValidator));
	    }
    }
}
