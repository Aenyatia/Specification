using FluentAssertions;
using Specification.Tests.Shared;
using System;
using Xunit;

namespace Specification.Tests
{
	public class OrSpecificationTests
	{
		[Fact]
		public void Or_AnySpecificationOrNull_ShouldThrowException()
		{
			Specification<Car> any = new CarsWithProductionYearBeforeSpecification(2001);

			Func<Specification<Car>> func = () => any.Or(null);

			func.Should().Throw<NullReferenceException>();
		}

		[Fact]
		public void Or_AllFalseOrAnySpecification_ShouldReturnAnySpecification()
		{
			Specification<Car> allFalse = Specification<Car>.AllFalse;
			Specification<Car> any = new CarsWithProductionYearBeforeSpecification(2001);

			Func<Specification<Car>> func = () => allFalse.Or(any);

			func.Invoke().Should().BeSameAs(any);
		}

		[Fact]
		public void Or_AnySpecificationOrAllFalse_ShouldReturnAnySpecification()
		{
			Specification<Car> any = new CarsWithProductionYearBeforeSpecification(2001);
			Specification<Car> allFalse = Specification<Car>.AllFalse;

			Func<Specification<Car>> func = () => any.Or(allFalse);

			func.Invoke().Should().BeSameAs(any);
		}

		[Fact]
		public void Or_AllTrueOrAnySpecification_ShouldReturnAllTrue()
		{
			Specification<Car> allTrue = Specification<Car>.AllTrue;
			Specification<Car> any = new CarsWithProductionYearBeforeSpecification(2001);

			Func<Specification<Car>> func = () => allTrue.Or(any);

			func.Invoke().Should().BeSameAs(allTrue);
		}

		[Fact]
		public void Or_AnySpecificationOrAllTrue_ShouldReturnAllTrue()
		{
			Specification<Car> any = new CarsWithProductionYearBeforeSpecification(2001);
			Specification<Car> allTrue = Specification<Car>.AllTrue;

			Func<Specification<Car>> func = () => any.Or(allTrue);

			func.Invoke().Should().BeSameAs(allTrue);
		}

		[Fact]
		public void Or_AnySpecificationOrAnySpecification_ShouldReturnOrSpecification()
		{
			Specification<Car> left = new CarsWithProductionYearBeforeSpecification(2001);
			Specification<Car> right = new CarsWithModelContainsSpecification("6");

			Func<Specification<Car>> func = () => left.Or(right);

			func.Invoke().Should().BeOfType<OrSpecification<Car>>();
		}
	}
}
