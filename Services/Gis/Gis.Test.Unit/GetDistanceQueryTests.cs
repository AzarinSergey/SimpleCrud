using Gis.Domain.Query;
using Gis.Model.Entity;
using Moedi.Data.Core.Access;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Gis.Test.Unit
{
    public class GetDistanceQueryTests
    {
        private Airport _a;
        private Airport _b;
        private GetDistanceQuery _query;
        private IQueryRepository<Airport> _repository;
        private IQueryRepositoryFactory _factory;

        [SetUp]
        public void Setup()
        {
            _a = new Airport
            {
                Iata = "Tura",
                Lat = 64.28,
                Lon = 100.22,
                Name = "Tura"
            };

            _b = new Airport
            {
                Iata = "New-York",
                Lat = 40.71,
                Lon = -74.01,
                Name = "New-York"
            };

            _query = new GetDistanceQuery(_a.Iata, _b.Iata);

            _factory = Substitute.For<IQueryRepositoryFactory>();
            _repository = Substitute.For<IQueryRepository<Airport>>();
            _factory.GetRepository<Airport>().ReturnsForAnyArgs(_repository);
        }

        /// <summary>
        /// http://osiktakan.ru/geo_koor.htm
        /// вычислениe расстояния между Турой и Нью-Йорком (США)
        /// 8335 километров
        /// </summary>
        [Test]
        public void ShouldCalculateRightValue()
        {
            var data = new List<Airport> {_a, _b}.AsQueryable();
            _repository.Query().ReturnsForAnyArgs(data);

            var result = _query.Query(_factory, CancellationToken.None)
                .GetAwaiter().GetResult();

            Assert.IsTrue(ApproximatelyEqual(8335/1.609, result));
        }

        /// <summary>
        /// https://www.antipodesmap.com/
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        [TestCase(40.698470, -73.951442, -40.698470, 106.048558)]
        [TestCase(90.000000, -90.000000, -90.000000, 90.000000)]
        [TestCase(-85.000000, -180.000000, 85.000000, 0.000000)]
        [TestCase(0.000000, 180.000000, 0.000000, 0.000000)]
        public void DistanceBetweenAntipodesShouldBeAbout20000Km(double lat1, double lon1, double lat2, double lon2)
        {
            _a.Lat = lat1;
            _a.Lon = lon1;

            _b.Lat = lat2;
            _b.Lon = lon2;

            var data = new List<Airport> { _a, _b }.AsQueryable();
            _repository.Query().ReturnsForAnyArgs(data);

            var result = _query.Query(_factory, CancellationToken.None)
                .GetAwaiter().GetResult();

            Assert.IsTrue(ApproximatelyEqual(20000 / 1.609, result));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(3)]
        public void ShouldReturnZero(int count)
        {
            var list = new List<Airport>(count);
            for (int i = 0; i < 3; i++)
            {
                list.Add(_a);
            }

            var data = list.AsQueryable();
            _repository.Query().ReturnsForAnyArgs(data);

            var result = _query.Query(_factory, CancellationToken.None)
                .GetAwaiter().GetResult();

            Assert.AreEqual(0, result);
        }

        private bool ApproximatelyEqual(double expected, double fact)
        {
            //полпроцента - погрешность точности вычислений - взято "из воздуха"
            var allowedErrorPercent = 0.5;

            var deviation = allowedErrorPercent * expected / 100;
            var up = expected + deviation;
            var down = expected - deviation;

            return fact >= down && fact <= up;
        }
    }
}