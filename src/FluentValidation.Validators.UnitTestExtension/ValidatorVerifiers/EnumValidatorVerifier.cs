namespace FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers
{
    using System;
    using System.Reflection;
    using FluentAssertions;

    public class EnumValidatorVerifier<TEnumValidator, T, TProperty> : TypeValidatorVerifier<TEnumValidator> where TEnumValidator : EnumValidator<T, TProperty>
    {
	    private readonly Type enumType;

	    public EnumValidatorVerifier()
	    {
	        enumType = typeof(TProperty);
	    }

        public override void Verify<TValidator>(TValidator validator)
		{
			base.Verify(validator);
			var enumValidator = validator as EnumValidator<T, TProperty>;
		    typeof(EnumValidator<T, TProperty>).GetField("_enumType", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(enumValidator).Should().Be(enumType, "(EnumType field)");
		}
	}
}
