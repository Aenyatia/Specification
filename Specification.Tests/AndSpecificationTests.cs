using FluentAssertions;
using Specification.Tests.Shared;
using System;
using Xunit;

namespace Specification.Tests
{
	public class AndSpecificationTests
	{
		[Fact]
		public void And_AnySpecificationAndNull_ShouldThrowException()
		{
			Specification<Car> any = new CarsWithProductionYearBeforeSpecification(2001);

			Func<Specification<Car>> func = () => any.And(null);

			func.Should().Throw<NullReferenceException>();
		}

		[Fact]
		public void And_AllFalseAndAnySpecification_ShouldReturnAllFalse()
		{
			Specification<Car> allFalse = Specification<Car>.AllFalse;
			Specification<Car> any = new CarsWithProductionYearBeforeSpecification(2001);

			Func<Specification<Car>> func = () => allFalse.And(any);

			func.Invoke().Should().BeSameAs(allFalse);
		}

		[Fact]
		public void And_AnySpecificationAndAllFalse_ShouldReturnAllFalse()
		{
			Specification<Car> any = new CarsWithProductionYearBeforeSpecification(2001);
			Specification<Car> allFalse = Specification<Car>.AllFalse;

			Func<Specification<Car>> func = () => any.And(allFalse);

			func.Invoke().Should().BeSameAs(allFalse);
		}

		[Fact]
		public void And_AllTrueAndAnySpecification_ShouldReturnAnySpecification()
		{
			Specification<Car> allTrue = Specification<Car>.AllTrue;
			Specification<Car> any = new CarsWithProductionYearBeforeSpecification(2001);

			Func<Specification<Car>> func = () => allTrue.And(any);

			func.Invoke().Should().BeSameAs(any);
		}

		[Fact]
		public void And_AnySpecificationAndAllTrue_ShouldReturnAnySpecification()
		{
			Specification<Car> any = new CarsWithProductionYearBeforeSpecification(2001);
			Specification<Car> allTrue = Specification<Car>.AllTrue;

			Func<Specification<Car>> func = () => any.And(allTrue);

			func.Invoke().Should().BeSameAs(any);
		}

		[Fact]
		public void And_AnySpecificationAndAnySpecification_ShouldReturnAndSpecification()
		{
			Specification<Car> left = new CarsWithProductionYearBeforeSpecification(2001);
			Specification<Car> right = new CarsWithModelContainsSpecification("6");

			Func<Specification<Car>> func = () => left.And(right);

			func.Invoke().Should().BeOfType<AndSpecification<Car>>();
		}
	}
}
