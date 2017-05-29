namespace FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers
{
    using FluentAssertions;

    public class ScalePrecisionValidatorVerifier<T> : TypeValidatorVerifier<T> where T : ScalePrecisionValidator
    {
	    private readonly int scale;
	    private readonly int precision;

	    public ScalePrecisionValidatorVerifier(int scale, int precision)
	    {
	        this.scale = scale;
	        this.precision = precision;
	    }

        public bool? IgnoreTrailingZeros { get; set; }

        public override void Verify<TValidator>(TValidator validator)
		{
			base.Verify(validator);
			var scalePrecisionValidator = validator as ScalePrecisionValidator;
			scalePrecisionValidator.Precision.ShouldBeEquivalentTo(this.precision, "(Precision property)");
			scalePrecisionValidator.Scale.ShouldBeEquivalentTo(this.scale, "(Scale property)");
            scalePrecisionValidator.IgnoreTrailingZeros.ShouldBeEquivalentTo(this.IgnoreTrailingZeros, "(IgnoreTrailingZeros property)");
		}
	}
}
