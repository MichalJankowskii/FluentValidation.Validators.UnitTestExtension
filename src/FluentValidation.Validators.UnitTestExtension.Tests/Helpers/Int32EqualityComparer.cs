namespace FluentValidation.Validators.UnitTestExtension.Tests.Helpers
{
    using System.Collections.Generic;

    internal class Int32EqualityComparer : IEqualityComparer<int>
    {
        bool IEqualityComparer<int>.Equals(int x, int y)
        {
            return x.Equals(y);
        }

        int IEqualityComparer<int>.GetHashCode(int obj)
        {
            return obj.GetHashCode();
        }
    }
}