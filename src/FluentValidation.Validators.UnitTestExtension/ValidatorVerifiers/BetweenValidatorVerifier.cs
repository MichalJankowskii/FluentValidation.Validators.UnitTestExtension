namespace FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers
{
    using System;
    using FluentAssertions;

    public class BetweenValidatorVerifier<T> : TypeValidatorVerifier<T> where T : IBetweenValidator
    {
        private readonly IComparable from;

        private readonly IComparable to;

        public BetweenValidatorVerifier(IComparable from, IComparable to)
        {
            this.from = from;
            this.to = to;
        }

        public override void Verify<TValidator>(TValidator validator)
        {
            base.Verify(validator);
            var betweenValidator = (IBetweenValidator)validator;
            betweenValidator.From.Should().Be(this.from, "(From property)");
            betweenValidator.To.Should().Be(this.to, "(To property)");
        }
    }
}
