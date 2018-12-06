using FluentAssertions;
using Specification.Tests.Shared;
using Xunit;

namespace Specification.Tests
{
	public class SpecificationTests
	{
		[Fact]
		public void Get_CarProductionBeforeYear_ReturnListOfCarsThatMeetTheCondition()
		{
			var carProductionBefore = new CarsWithProductionYearBeforeSpecification(2001);
			var carRepository = new CarRepository();

			var result = carRepository.Get(carProductionBefore);

			result.Should().HaveCount(1);
		}

		[Fact]
		public void Get_CarModelContainsString_ReturnListOfCarsThatMeetTheCondition()
		{
			var containString = new CarsWithModelContainsSpecification("E");
			var carRepository = new CarRepository();

			var result = carRepository.Get(containString);

			result.Should().HaveCount(2);
		}

		[Fact]
		public void Get_WhereArgumentIsAndSpecification_ReturnIntersectOfTheTwoConditions()
		{
			var spec1 = new CarsWithProductionYearBeforeSpecification(2001);
			var spec2 = new CarsWithModelContainsSpecification("E");
			var cars = new CarRepository();

			var result = cars.Get(spec1.And(spec2));

			result.Should().HaveCount(1);
		}

		[Fact]
		public void Get_WhereArgumentIsOrSpecification_ReturnUnionOfTheTwoConditions()
		{
			var spec1 = new CarsWithProductionYearBeforeSpecification(2001);
			var spec2 = new CarsWithModelContainsSpecification("E");
			var cars = new CarRepository();

			var result = cars.Get(spec1.Or(spec2));

			result.Should().HaveCount(2);
		}

		[Fact]
		public void Get_WhereArgumentIsNotSpecification_ReturnComplementOfTheCondition()
		{
			var spec1 = new CarsWithProductionYearBeforeSpecification(2001);
			var cars = new CarRepository();

			var result = cars.Get(spec1.Not());

			result.Should().HaveCount(6);
		}

		[Fact]
		public void IsSatisfiedBy_WhenInvoke_ShouldReturnExpressionResult()
		{
			var spec1 = new CarsWithProductionYearBeforeSpecification(2001);
			var spec2 = new CarsWithModelContainsSpecification("E");
			var car = new Car
			{
				Id = 1,
				Brand = "Toyota",
				Model = "Corolla-E",
				ProductionYear = 2001
			};

			var result = spec1.And(spec2).IsSatisfiedBy(car);

			result.Should().BeFalse();
		}
	}
}
