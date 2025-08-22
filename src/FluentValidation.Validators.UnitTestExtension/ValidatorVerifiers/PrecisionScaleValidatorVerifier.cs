namespace FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers
{
    using FluentAssertions;

    public class PrecisionScaleValidatorVerifier<T, K> : TypeValidatorVerifier<T> where T : PrecisionScaleValidator<K>
    {
        private readonly int scale;
        private readonly int precision;
        private readonly bool ignoreTrailingZeros;

        public PrecisionScaleValidatorVerifier(int precision, int scale, bool ignoreTrailingZeros)
        {
            this.scale = scale;
            this.precision = precision;
            this.ignoreTrailingZeros = ignoreTrailingZeros;
        }

        public override void Verify<TValidator>(TValidator validator)
        {
            base.Verify(validator);
            var scalePrecisionValidator = validator as PrecisionScaleValidator<K>;
            scalePrecisionValidator.Precision.Should().Be(this.precision, "(Precision property)");
            scalePrecisionValidator.Scale.Should().Be(this.scale, "(Scale property)");
            scalePrecisionValidator.IgnoreTrailingZeros.Should().Be(this.ignoreTrailingZeros, "(IgnoreTrailingZeros property)");
        }
    }
}
