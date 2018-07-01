namespace FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers
{
    using System;
    using System.Reflection;
    using FluentAssertions;

    public class EnumValidatorVerifier<T> : TypeValidatorVerifier<T> where T : EnumValidator
    {
	    private readonly Type enumType;

	    public EnumValidatorVerifier(Type enumType)
	    {
	        this.enumType = enumType;
	    }

        public override void Verify<TValidator>(TValidator validator)
		{
			base.Verify(validator);
			var enumValidator = validator as EnumValidator;
		    typeof(EnumValidator).GetField("enumType", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(enumValidator).Should().Be(this.enumType, "(EnumType field)");
		}
	}
}
