namespace FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers
{
	using FluentAssertions;

	public class ScalePrecisionValidatorVerifier<T, K> : TypeValidatorVerifier<T> where T : ScalePrecisionValidator<K>
	{
		private readonly int scale;
		private readonly int precision;

		public ScalePrecisionValidatorVerifier(int scale, int precision)
		{
			this.scale = scale;
			this.precision = precision;
		}

		public bool IgnoreTrailingZeros { get; set; }

		public override void Verify<TValidator>(TValidator validator)
		{
			base.Verify(validator);
			var scalePrecisionValidator = validator as ScalePrecisionValidator<K>;
			scalePrecisionValidator.Precision.Should().Be(this.precision, "(Precision property)");
			scalePrecisionValidator.Scale.Should().Be(this.scale, "(Scale property)");
			scalePrecisionValidator.IgnoreTrailingZeros.Should().Be(this.IgnoreTrailingZeros, "(IgnoreTrailingZeros property)");
		}
	}
}
