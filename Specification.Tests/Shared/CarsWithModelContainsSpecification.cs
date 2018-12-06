using System;
using System.Linq.Expressions;

namespace Specification.Tests.Shared
{
	public class CarsWithModelContainsSpecification : Specification<Car>
	{
		private readonly string _model;

		public CarsWithModelContainsSpecification(string model)
		{
			_model = model;
		}

		public override Expression<Func<Car, bool>> ToExpression()
		{
			return c => c.Model.Contains(_model);
		}
	}
}
