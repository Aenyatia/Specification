using System.Collections.Generic;
using System.Linq;

namespace Specification.Tests.Shared
{
	public class CarRepository
	{
		private static readonly IEnumerable<Car> Cars = new HashSet<Car>
		{
			new Car
			{
				Id = 1,
				Brand = "BMW",
				Model = "E65",
				ProductionYear = 2001
			},
			new Car
			{
				Id = 2,
				Brand = "BMW",
				Model = "G32",
				ProductionYear = 2017
			},
			new Car
			{
				Id = 3,
				Brand = "Audi",
				Model = "A6",
				ProductionYear = 2004
			},
			new Car
			{
				Id = 4,
				Brand = "Audi",
				Model = "R8",
				ProductionYear = 2015
			},
			new Car
			{
				Id = 5,
				Brand = "Toyota",
				Model = "AE86",
				ProductionYear = 1983
			},
			new Car
			{
				Id = 6,
				Brand = "Toyota",
				Model = "Premio",
				ProductionYear = 2001
			},
			new Car
			{
				Id = 7,
				Brand = "Toyota",
				Model = "Auris",
				ProductionYear = 2013
			}
		};

		public IEnumerable<Car> Get(Specification<Car> specification)
		{
			return Cars.AsQueryable()
				.Where(specification.ToExpression())
				.ToList();
		}
	}
}
