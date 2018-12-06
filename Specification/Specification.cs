using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Specification.Tests")]
namespace Specification
{
	public abstract class Specification<T>
	{
		public static readonly Specification<T> AllTrue = new AllTrueSpecification<T>();
		public static readonly Specification<T> AllFalse = new AllFalseSpecification<T>();

		public abstract Expression<Func<T, bool>> ToExpression();

		public bool IsSatisfiedBy(T entity)
		{
			Func<T, bool> predicate = ToExpression().Compile();

			return predicate(entity);
		}

		public Specification<T> And(Specification<T> right)
		{
			if (ReferenceEquals(right, null))
				throw new NullReferenceException();

			if (ReferenceEquals(this, AllFalse) || ReferenceEquals(right, AllFalse))
				return AllFalse;

			if (ReferenceEquals(this, AllTrue))
				return right;

			if (ReferenceEquals(right, AllTrue))
				return this;

			return new AndSpecification<T>(this, right);
		}

		public Specification<T> Or(Specification<T> right)
		{
			if (ReferenceEquals(right, null))
				throw new NullReferenceException();

			if (ReferenceEquals(this, AllFalse))
				return right;

			if (ReferenceEquals(right, AllFalse))
				return this;

			if (ReferenceEquals(this, AllTrue) || ReferenceEquals(right, AllTrue))
				return AllTrue;

			return new OrSpecification<T>(this, right);
		}

		public Specification<T> Not()
		{
			if (ReferenceEquals(this, AllFalse))
				return AllTrue;

			if (ReferenceEquals(this, AllTrue))
				return AllFalse;

			return new NotSpecification<T>(this);
		}
	}
}
