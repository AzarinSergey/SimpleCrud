using Gis.Model.Entity;
using Moedi.Cqrs.Handler;
using Moedi.Data.Core.Access;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gis.Domain.Query
{
    public class GetDistanceQuery : QueryHandler<double>
    {
        public const double EarthRadius = 3958.7559;

        private readonly string _airportCodeA;
        private readonly string _airportCodeB;

        public GetDistanceQuery(string airportCodeA, string airportCodeB)
        {
            _airportCodeA = airportCodeA;
            _airportCodeB = airportCodeB;
        }

        public override Task<double> Query(IQueryRepositoryFactory f, CancellationToken token)
        {
            var coords = f.GetRepository<Airport>().Query()
                .Where(x => x.Iata == _airportCodeA || x.Iata == _airportCodeB)
                .ToList();

            if (coords.Count != 2)
            {
                return Task.FromResult(0d);
            }

            return Task.FromResult(Calculate(coords[0], coords[1]));
        }

        /// <summary>
        ///http://osiktakan.ru/geo_koor.htm
        ///градусы в радианы: 1*pi/180 = 0,01745 рад
        ///средний радиус Земли в км 6371, что равно 3958,7559 миль (ошибка 0.5%)
        ///L = d·R,
        ///cos(d) = sin(latА)·sin(latB) + cos(latА)·cos(latB)·cos(lonB − lonA),
        ///L = arccos( sin(latА)·sin(latB) + cos(latА)·cos(latB)·cos(lonB − lonA) ) * R
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private double Calculate(Airport a, Airport b)
        {
            var pi = Math.PI / 180d;
            var l1Rad = a.Lat * pi;
            var l2Rad = b.Lat * pi;
            var ln1Rad = a.Lon * pi;
            var ln2Rad = b.Lon * pi;

            var delta = ln1Rad - ln2Rad;

            var sinL1Rad = Math.Sin(l1Rad);
            var sinL2Rad = Math.Sin(l2Rad);

            var cosL1Rad = Math.Cos(l1Rad);
            var cosL2Rad = Math.Cos(l2Rad);

            var deltaCos = Math.Cos(delta);

            var angleRadCos = sinL1Rad * sinL2Rad +
                              cosL1Rad * cosL2Rad * deltaCos;

            var angleRad = Math.Acos(angleRadCos);

            return EarthRadius * angleRad;
        }
    }
}