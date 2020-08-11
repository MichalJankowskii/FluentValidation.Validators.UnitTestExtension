namespace FluentValidation.Validators.UnitTestExtension.Internal.Extensions
{
    using System.Linq.Expressions;

    internal static class ExpressionComparisonExtensions
    {
        /// <summary>
        /// Compares members of two expressions recursively.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="otherExpression"></param>
        /// <returns> Whether the two expressions are equal in terms of their members.</returns>
        public static bool CompareMembersRecursively(this Expression expression, Expression otherExpression)
        {
            if (expression.NodeType == otherExpression.NodeType)
            {
                switch (expression.NodeType)
                {
                    case ExpressionType.Parameter:
                        return true;
                    case ExpressionType.Lambda:
                        return CompareMembersRecursively(((LambdaExpression) expression).Body,
                            ((LambdaExpression) otherExpression).Body);
                    case ExpressionType.MemberAccess:
                        var memberExpression = expression as MemberExpression;
                        var otherMemberExpression = otherExpression as MemberExpression;

                        return memberExpression != null && otherMemberExpression != null &&
                               memberExpression.Member == otherMemberExpression.Member &&
                               CompareMembersRecursively(memberExpression.Expression, otherMemberExpression.Expression);
                }
            }

            return false;
        }
        
    }
}