namespace FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers
{
	using System.Text.RegularExpressions;
	using FluentAssertions;

	public class RegularExpressionValidatorVerifier<T> : TypeValidatorVerifier<T> where T : IRegularExpressionValidator
	{
		private readonly string expression;

		public RegularExpressionValidatorVerifier(string expression)
		{
			this.expression = expression;
		}

		public RegularExpressionValidatorVerifier(Regex regex) : this(regex.ToString())
		{
		}

		public RegularExpressionValidatorVerifier(string expression, RegexOptions options) : this(expression)
		{
		}

		public override void Verify<TValidator>(TValidator validator)
		{
			base.Verify(validator);
			var regularExpressionValidator = (IRegularExpressionValidator)validator;
			regularExpressionValidator.Expression.Should().BeEquivalentTo(this.expression, "(Expression property)");
		}
	}
}
