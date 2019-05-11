namespace FluentValidation.Validators.UnitTestExtension.Tests.Helpers
{
    using System;
    using Xunit;

    public static class AssertExtension
    {
        public static void NotThrows(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception has been thrown", ex);
            }
        }

        public static void Throws<T>(Action action, string subString) where T: Exception
        {
            if (string.IsNullOrEmpty(subString))
            {
                throw new ArgumentException("subString");
            }

            Exception exception = Assert.Throws<T>(action);
            Assert.True(exception.Message.Contains(subString), "Not found substring: " + subString + " in: " + exception.Message);
        }
    }
}
