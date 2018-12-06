using System;
using System.Linq.Expressions;

namespace Specification.Tests.Shared
{
	public class CarsWithProductionYearBeforeSpecification : Specification<Car>
	{
		private readonly int _year;

		public CarsWithProductionYearBeforeSpecification(int year)
		{
			_year = year;
		}

		public override Expression<Func<Car, bool>> ToExpression()
		{
			return c => c.ProductionYear < _year;
		}
	}
}
