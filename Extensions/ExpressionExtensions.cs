using System;
using System.Linq.Expressions;
using System.Reflection;

namespace WeAreGeekers.ModelMapper.Extensions
{

    /// <summary>
    /// Internal static class with extension methods of expression object
    /// </summary>
    internal static class ExpressionExtensions
    {

        /// <summary>
        /// Get the PropertyInfo from expression 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        internal static PropertyInfo GetPropertyInfo(this Expression expression)
        {
            if (expression is LambdaExpression lambdaExpression)
            {
                return lambdaExpression.Body.GetPropertyInfo();
            }

            if (expression is UnaryExpression unaryExpression)
            {
                return unaryExpression.Operand.GetPropertyInfo();
            }

            if (expression is MemberExpression memberExpression)
            {
                if (memberExpression.Member != null)
                {
                    switch (memberExpression.Member.MemberType)
                    {
                        case MemberTypes.Property:
                            return (PropertyInfo)memberExpression.Member;
                        
                        default:
                            throw new NotImplementedException("The expression type 'MemberExpression' with property 'MemberType' equals to '" + memberExpression.Member.MemberType + "' is not supported yet in the method 'GetSqlComponentAttribute'!");
                    }
                }
                else
                {
                    throw new NotImplementedException("The expression type 'MemberExpression' with property 'Member' equals to 'null' is not supported yet in the method 'GetSqlComponentAttribute'!");
                }
            }

            // Not find Expression type, return null
            return null;
        }

    }

}
