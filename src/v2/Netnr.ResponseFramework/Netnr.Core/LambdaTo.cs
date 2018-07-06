using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Netnr.Core
{
    public class LambdaTo
    {
        #region lambda
        /// <summary>
        /// p=>p.field==1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="ParameterName"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Equal<T>(string field, object value, string ParameterName = "p")
        {
            ParameterExpression param = Expression.Parameter(typeof(T), ParameterName);
            Expression left = Expression.Property(param, typeof(T).GetProperty(field));
            Expression right = Expression.Constant(value, value.GetType());
            if (left.Type.IsGenericType)
            {
                left = Expression.Convert(left, value.GetType());
            }
            Expression result = Expression.Equal(left, right);

            var finalExpression = Expression.Lambda<Func<T, bool>>(result, new ParameterExpression[] { param });
            return finalExpression;
        }

        /// <summary>
        /// p=>p.field==1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="values"></param>
        /// <param name="ParameterName"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Contains<T>(string field, List<object> values, string ParameterName = "p")
        {
            ParameterExpression param = Expression.Parameter(typeof(T), ParameterName);
            Expression left = Expression.Property(param, typeof(T).GetProperty(field));
            Expression expression = null;
            foreach (var val in values)
            {
                Expression right = Expression.Constant(val, val.GetType());
                right = Expression.Convert(right, val.GetType());

                Expression result = Expression.Equal(left, right);

                expression = expression == null ? result : Expression.Or(result, expression);
            }
            var finalExpression = Expression.Lambda<Func<T, bool>>(expression, new ParameterExpression[] { param });
            return finalExpression;
        }
        #endregion
    }
}
