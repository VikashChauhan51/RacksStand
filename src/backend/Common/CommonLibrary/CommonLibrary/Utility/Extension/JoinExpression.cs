using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Extension
{
    public static class JoinExpression
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            ParameterExpression p = expr1.Parameters[0];

            Expression body = Expression.AndAlso(expr1.Body, expr2.WithParametersOf(expr1).Body);
            return Expression.Lambda<Func<T, bool>>(body, p);
        }
        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expr)
        {
            return Expression.Lambda<Func<T, bool>>(Expression.Not(expr.Body), expr.Parameters[0]);
        }
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, expr2.WithParametersOf(expr1).Body), expr1.Parameters);
        }
        public static Expression<Func<T1, T3>> Combine<T1, T2, T3>(this Expression<Func<T1, T2>> expr1, Expression<Func<T2, T3>> expr2)
        {
            var param = Expression.Parameter(typeof(T1), "param");
            var body = Expression.Invoke(expr2, Expression.Invoke(expr1, param));
            return Expression.Lambda<Func<T1, T3>>(body, param);
        }

        private static Expression<Func<TResult>> WithParametersOf<T, TResult>(this Expression<Func<T, TResult>> left, Expression<Func<T, TResult>> right)
        {
            return new ReplaceParameterVisitor<Func<TResult>>(left.Parameters[0], right.Parameters[0]).Visit(left);
        }
    }

    internal class ReplaceParameterVisitor<TResult> : ExpressionVisitor
    {
        private readonly ParameterExpression parameter;
        private readonly Expression replacement;

        public ReplaceParameterVisitor(ParameterExpression parameter, Expression replacement)
        {
            this.parameter = parameter;
            this.replacement = replacement;
        }

        public Expression<TResult> Visit<T>(Expression<T> node)
        {
            var parameters = node.Parameters.Where(p => p != parameter);
            return Expression.Lambda<TResult>(Visit(node.Body), parameters);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node == parameter ? replacement : base.VisitParameter(node);
        }
    }

}
