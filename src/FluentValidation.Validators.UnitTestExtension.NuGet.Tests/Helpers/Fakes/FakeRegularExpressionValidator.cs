namespace FluentValidation.Validators.UnitTestExtension.Tests.Helpers.Fakes
{
	public class FakeRegularExpressionValidator : FakePropertyValidator, IRegularExpressionValidator
	{
		public string Expression { get; set; }
	}
}
