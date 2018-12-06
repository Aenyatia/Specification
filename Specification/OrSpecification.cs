using System;
using System.Linq.Expressions;

namespace Specification
{
	internal sealed class OrSpecification<T> : Specification<T>
	{
		private readonly Specification<T> _left;
		private readonly Specification<T> _right;

		public OrSpecification(Specification<T> left, Specification<T> right)
		{
			_left = left;
			_right = right;
		}

		public override Expression<Func<T, bool>> ToExpression()
		{
			Expression<Func<T, bool>> left = _left.ToExpression();
			Expression<Func<T, bool>> right = _right.ToExpression();

			InvocationExpression invocationExpression = Expression.Invoke(right, left.Parameters);

			return Expression.Lambda<Func<T, bool>>(Expression.OrElse(left.Body, invocationExpression), left.Parameters);
		}
	}
}
