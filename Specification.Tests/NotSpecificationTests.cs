using FluentAssertions;
using Specification.Tests.Shared;
using System;
using Xunit;

namespace Specification.Tests
{
	public class NotSpecificationTests
	{
		[Fact]
		public void Not_AllTrueNegate_ShouldReturnAllFalse()
		{
			Specification<Car> allTrue = Specification<Car>.AllTrue;

			Func<Specification<Car>> func = () => allTrue.Not();

			func.Invoke().Should().BeSameAs(Specification<Car>.AllFalse);
		}

		[Fact]
		public void Not_AllFalseNegate_ShouldReturnAllTrue()
		{
			Specification<Car> allFalse = Specification<Car>.AllFalse;

			Func<Specification<Car>> func = () => allFalse.Not();

			func.Invoke().Should().Be(Specification<Car>.AllTrue);
		}

		[Fact]
		public void Not_AnySpecificationNegate_ShouldReturnNotSpecification()
		{
			Specification<Car> any = new CarsWithProductionYearBeforeSpecification(2001);

			Func<Specification<Car>> func = () => any.Not();

			func.Invoke().Should().BeOfType<NotSpecification<Car>>();
		}
	}
}
