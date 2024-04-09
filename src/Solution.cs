public class Solution
{
    // 2.1
    public void GetRealEstateByPriceAndDistrict(double minPrice, double maxPrice, string districtName)
    {
        using (var context = new RealEstateContext())
        {
            var realEstates = from re in context.RealEstateObjects
                              join d in context.Districts on re.DistrictId equals d.DistrictId
                              where re.Price >= minPrice && re.Price <= maxPrice && d.DistrictName == districtName
                              orderby re.Price descending
                              select new
                              {
                                  re.Address,
                                  re.Area,
                                  re.Floor
                              };

            foreach (var realEstate in realEstates)
            {
                Console.WriteLine($"Адрес: {realEstate.Address}, Площадь: {realEstate.Area}, Этаж: {realEstate.Floor}");
            }
        }
    }

    // 2.2
    public void GetRealtorsSellingTwoRoomObjects()
    {
        using (var context = new RealEstateContext())
        {
            var realtors = from re in context.RealEstateObjects
                           join sale in context.Sales on re.RealEstateObjectId equals sale.RealEstateObjectId
                           join realtor in context.Realtors on sale.RealtorId equals realtor.RealtorId
                           where re.RoomCount == 2
                           select new
                           {
                               realtor.LastName,
                               realtor.FirstName,
                               realtor.MiddleName
                           };

            foreach (var realtor in realtors.Distinct())
            {
                Console.WriteLine($"{realtor.LastName} {realtor.FirstName} {realtor.MiddleName}");
            }
        }
    }

    // 2.3
    public void GetTotalPriceOfTwoRoomObjectsByDistrict(string districtName)
    {
        using (var context = new RealEstateContext())
        {
            var totalPrice = (from re in context.RealEstateObjects
                              join sale in context.Sales on re.RealEstateObjectId equals sale.RealEstateObjectId
                              join district in context.Districts on re.DistrictId equals district.DistrictId
                              where re.RoomCount == 2 && district.DistrictName == districtName
                              select sale.Price).Sum();

            Console.WriteLine($"Общая стоимость двухкомнатных объектов недвижимости в районе {districtName}: {totalPrice}");
        }
    }

    // 2.4
    public void GetMaxAndMinPricesSoldByRealtor(string realtorLastName)
    {
        using (var context = new RealEstateContext())
        {
            var prices = (from sale in context.Sales
                          join re in context.RealEstateObjects on sale.RealEstateObjectId equals re.RealEstateObjectId
                          join realtor in context.Realtors on sale.RealtorId equals realtor.RealtorId
                          where realtor.LastName == realtorLastName
                          select sale.Price).ToList();

            if (prices.Any())
            {
                Console.WriteLine($"Максимальная стоимость: {prices.Max()}");
                Console.WriteLine($"Минимальная стоимость: {prices.Min()}");
            }
            else
            {
                Console.WriteLine($"Риэлтор {realtorLastName} не продал недвижимость.");
            }
        }
    }

    // 2.5
    public void GetAverageSafetyScoreOfApartmentsSoldByRealtor(string realtorLastName)
    {
        using (var context = new RealEstateContext())
        {
            var averageScore = (from sale in context.Sales
                                join re in context.RealEstateObjects on sale.RealEstateObjectId equals re.RealEstateObjectId
                                join realtor in context.Realtors on sale.RealtorId equals realtor.RealtorId
                                join evaluation in context.Evaluations on re.RealEstateObjectId equals evaluation.RealEstateObjectId
                                join criterion in context.EvaluationCriteria on evaluation.CriterionId equals criterion.EvaluationCriterionId
                                join type in context.Types on re.TypeId equals type.TypeId
                                where realtor.LastName == realtorLastName && type.TypeName == "Апартаменты" && criterion.CriterionName == "Безопасность"
                                select evaluation.Score).Average();

            Console.WriteLine($"Средняя оценка безопасности апартаментов, проданных риэлтором {realtorLastName}: {averageScore}");
        }
    }

    // 2.6
    public void GetRealEstateCountOnSecondFloorByDistrict()
    {
        using (var context = new RealEstateContext())
        {
            var realEstateCounts = from re in context.RealEstateObjects
                                   join district in context.Districts on re.DistrictId equals district.DistrictId
                                   where re.Floor == 2
                                   group district by district.DistrictName into g
                                   select new
                                   {
                                       DistrictName = g.Key,
                                       RealEstateCount = g.Count()
                                   };

            foreach (var item in realEstateCounts)
            {
                Console.WriteLine($"Район: {item.DistrictName}, Количество объектов недвижимости на 2 этаже: {item.RealEstateCount}");
            }
        }
    }

    // 2.7
    public void GetSoldApartmentsCountByRealtor()
    {
        using (var context = new RealEstateContext())
        {
            var soldApartmentsCounts = from sale in context.Sales
                                       join re in context.RealEstateObjects on sale.RealEstateObjectId equals re.RealEstateObjectId
                                       join realtor in context.Realtors on sale.RealtorId equals realtor.RealtorId
                                       join type in context.Types on re.TypeId equals type.TypeId
                                       where type.TypeName == "Квартира"
                                       group realtor by new { realtor.LastName, realtor.FirstName, realtor.MiddleName } into g
                                       select new
                                       {
                                           RealtorName = $"{g.Key.LastName} {g.Key.FirstName} {g.Key.MiddleName}",
                                           ApartmentsSoldCount = g.Count()
                                       };

            foreach (var item in soldApartmentsCounts)
            {
                Console.WriteLine($"Риэлтор: {item.RealtorName}, Количество проданных квартир: {item.ApartmentsSoldCount}");
            }
        }
    }

    // 2.8
    public void GetTopThreeMostExpensiveRealEstateByDistrict()
    {
        using (var context = new RealEstateContext())
        {
            var topThreeRealEstatesByDistrict = context.Districts.Select(district => new
            {
                DistrictName = district.DistrictName,
                RealEstates = context.RealEstateObjects.Where(re => re.DistrictId == district.DistrictId)
                                                        .OrderByDescending(re => re.Price)
                                                        .OrderBy(re => re.Floor)
                                                        .Take(3)
                                                        .Select(re => new
                                                        {
                                                            Address = re.Address,
                                                            Price = re.Price,
                                                            Floor = re.Floor
                                                        })
                                                        .ToList()
            });

            foreach (var district in topThreeRealEstatesByDistrict)
            {
                Console.WriteLine($"Район: {district.DistrictName}");
                foreach (var realEstate in district.RealEstates)
                {
                    Console.WriteLine($"Адрес: {realEstate.Address}, Стоимость: {realEstate.Price}, Этаж: {realEstate.Floor}");
                }
            }
        }
    }

    // 2.9
    public void GetYearsWithMoreThanTwoSalesByRealtor(string realtorFullName)
    {
        using (var context = new RealEstateContext())
        {
            var realtorFullNameParts = realtorFullName.Split(" ");
            var yearsWithMoreThanTwoSales = context.Sales
                .Join(context.Realtors, sale => sale.RealtorId, realtor => realtor.RealtorId, (sale, realtor) => new { sale, realtor })
                .Where(x => x.realtor.LastName == realtorFullNameParts[0])
                .Where(x => x.realtor.FirstName == realtorFullNameParts[1])
                .Where(x => x.realtor.MiddleName == realtorFullNameParts[2])
                .GroupBy(x => x.sale.SaleDate.Year)
                .Where(g => g.Count() > 2)
                .Select(g => g.Key);
            

            foreach (var year in yearsWithMoreThanTwoSales)
            {
                Console.WriteLine($"Риэлтор {realtorFullName} продал больше двух объектов недвижимости в {year} году.");
            }
        }
    }

    // 2.10
    public void GetYearsWithTwoToThreeRealEstates()
    {
        using (var context = new RealEstateContext())
        {
            var yearsWithTwoToThreeRealEstates = context.RealEstateObjects
                .GroupBy(re => re.AnnouncementDate.Year)
                .Where(g => g.Count() >= 2 && g.Count() <= 3)
                .Select(g => g.Key);

            foreach (var year in yearsWithTwoToThreeRealEstates)
            {
                Console.WriteLine($"В {year} году было размещено от двух до трех объектов недвижимости.");
            }
        }
    }

    // 2.11
    public void GetRealEstatesWithPriceDifferenceLessThanTwentyPercent()
    {
        using (var context = new RealEstateContext())
        {
            var realEstates = context.RealEstateObjects
                .Join(context.Sales, re => re.RealEstateObjectId, sale => sale.RealEstateObjectId, (re, sale) => new { re, sale })
                .Where(x => Math.Abs(x.re.Price - x.sale.Price) / x.re.Price <= 0.2)
                .Join(context.Districts, x => x.re.DistrictId, district => district.DistrictId, (x, district) => new { x, district })
                .Select(x => new
                {
                    x.x.re.Address,
                    x.district.DistrictName
                });

            foreach (var realEstate in realEstates)
            {
                Console.WriteLine($"Адрес: {realEstate.Address}, Район: {realEstate.DistrictName}");
            }
        }
    }

    // 2.12
    public void GetApartmentsWithPricePerSquareMeterBelowAverageByDistrict()
    {
        using (var context = new RealEstateContext())
        {
            var averagePricePerSquareMeterByDistrict = context.RealEstateObjects
            .Where(re => re.TypeId == 1)
            .Join(context.Districts, x => x.DistrictId, district => district.DistrictId, (x, district) => new { x, district })
            .GroupBy(re => re.district.DistrictName)
            .Select(g => new
            {
                DistrictName = g.Key,
                AveragePricePerSquareMeter = g.Average(re => re.x.Price / re.x.Area)
            })
            .ToDictionary(x => x.DistrictName, x => x.AveragePricePerSquareMeter);

            var apartments = context.RealEstateObjects
            .Where(re => re.TypeId == 1)
            .Join(context.Districts, x => x.DistrictId, district => district.DistrictId, (x, district) => new { x, district })
            .Select(re => new
            {
                Address = re.x.Address,
                PricePerSquareMeter = re.x.Price / re.x.Area,
                DistrictName = re.district.DistrictName
            })
            .AsEnumerable()
            .Where(re => re.PricePerSquareMeter < averagePricePerSquareMeterByDistrict[re.DistrictName]);

            foreach (var apartment in apartments)
            {
                Console.WriteLine($"Адрес: {apartment.Address}, Стоимость 1м^2: {apartment.PricePerSquareMeter}, Район: {apartment.DistrictName}");
            }
        }
    }

    // 2.13
    public void GetRealtorsWithNoSalesThisYear()
    {
        int currentYear = DateTime.Now.Year;

        using (var context = new RealEstateContext())
        {
            var realtorsWithNoSales = context.Realtors
                .Where(r => !context.Sales.Any(s => s.RealtorId == r.RealtorId && s.SaleDate.Year == currentYear))
                .Select(r => new
                {
                    r.LastName,
                    r.FirstName,
                    r.MiddleName
                });

            foreach (var realtor in realtorsWithNoSales)
            {
                Console.WriteLine($"Риэлтор: {realtor.LastName} {realtor.FirstName} {realtor.MiddleName}");
            }
        }
    }

    // 2.14
    public void GetSalesInfoByDistrictForPreviousAndCurrentYears()
    {
        int currentYear = DateTime.Now.Year;
        int previousYear = currentYear - 1;

        using (var context = new RealEstateContext())
        {
            var salesInfoByDistrict = context.Sales
                .Where(s => s.SaleDate.Year == currentYear || s.SaleDate.Year == previousYear)
                .Join(context.RealEstateObjects, sale => sale.RealEstateObjectId, re => re.RealEstateObjectId, (sale, re) => new { sale, re })
                .Join(context.Districts, x => x.re.DistrictId, district => district.DistrictId, (x, district) => new { x.sale, district.DistrictName })
                .GroupBy(x => new { x.DistrictName, Year = x.sale.SaleDate.Year })
                .Select(g => new
                {
                    g.Key.DistrictName,
                    g.Key.Year,
                    SaleCount = g.Count()
                })
                .GroupBy(x => x.DistrictName)
                .ToList();

            foreach (var districtGroup in salesInfoByDistrict)
            {
                var currentYearSales = districtGroup.FirstOrDefault(x => x.Year == currentYear)?.SaleCount ?? 0;
                var previousYearSales = districtGroup.FirstOrDefault(x => x.Year == previousYear)?.SaleCount ?? 0;
                var percentChange = previousYearSales != 0 ? ((currentYearSales - previousYearSales) / (double)previousYearSales) * 100 : 0;

                Console.WriteLine($"Название района: {districtGroup.Key}");
                Console.WriteLine($"{previousYear}: {previousYearSales} продаж");
                Console.WriteLine($"{currentYear}: {currentYearSales} продаж");
                Console.WriteLine($"Процент изменения: {percentChange}%");
                Console.WriteLine();
            }
        }
    }

    // 2.15
    public void GetAverageEvaluationByCriterionForRealEstate(int realEstateId)
    {
        using (var context = new RealEstateContext())
        {
            var averageEvaluations = context.Evaluations
                .Where(e => e.RealEstateObjectId == realEstateId)
                .Join(context.EvaluationCriteria, evaluation => evaluation.CriterionId, evaluationcriterion => evaluationcriterion.EvaluationCriterionId, (evaluation, evaluationcriterion) => new { evaluation, evaluationcriterion })
                .GroupBy(e => e.evaluationcriterion.CriterionName)
                .Select(g => new
                {
                    CriterionName = g.Key,
                    AverageEvaluation = g.Average(e => e.evaluation.Score)
                })
                .ToList();

            foreach (var averageEvaluation in averageEvaluations)
            {
                string equivalentText = GetEquivalentTextForEvaluation(averageEvaluation.AverageEvaluation);
                Console.WriteLine($"{averageEvaluation.CriterionName}, Средняя оценка: {averageEvaluation.AverageEvaluation}, Эквивалентный текст: {equivalentText}");
            }
        }
    }

    private string GetEquivalentTextForEvaluation(double evaluationValue)
    {
        if (evaluationValue >= 90)
        {
            return "превосходно";
        }
        else if (evaluationValue >= 80)
        {
            return "очень хорошо";
        }
        else if (evaluationValue >= 70)
        {
            return "хорошо";
        }
        else if (evaluationValue >= 60)
        {
            return "удовлетворительно";
        }
        else
        {
            return "неудовлетворительно";
        }
    }
}