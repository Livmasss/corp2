using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

// Определение контекста данных
public class RealEstateContext : DbContext
{
    public DbSet<Type> Types { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<BuildingMaterial> BuildingMaterials { get; set; }
    public DbSet<RealEstateObject> RealEstateObjects { get; set; }
    public DbSet<EvaluationCriterion> EvaluationCriteria { get; set; }
    public DbSet<Evaluation> Evaluations { get; set; }
    public DbSet<Realtor> Realtors { get; set; }
    public DbSet<Sale> Sales { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost; Database=corp2; Username=postgres; Password=postgres");
    }
}



class Program
{
    static void Main(string[] args)
    {
        using (var context = new RealEstateContext())
        {
            // Очищаем базу данных перед заполнением
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Пример заполнения базы данных тестовыми данными
            var types = new List<Type>
            {
                new Type { TypeName = "Квартира" },
                new Type { TypeName = "Дом" },
                new Type { TypeName = "Апартаменты" }
            };
            context.Types.AddRange(types);

            var districts = new List<District>
            {
                new District { DistrictName = "Кировский" },
                new District { DistrictName = "Ленинский" },
                new District { DistrictName = "Октябрьский" }
            };
            context.Districts.AddRange(districts);

            var materials = new List<BuildingMaterial>
            {
                new BuildingMaterial { MaterialName = "Кирпич" },
                new BuildingMaterial { MaterialName = "Панель" },
                new BuildingMaterial { MaterialName = "Дерево" }
            };
            context.BuildingMaterials.AddRange(materials);

            var realEstates = new List<RealEstateObject>
            {
                new RealEstateObject {
                    DistrictId = 1,
                    Address = "ул. Ленина, д. 10",
                    Floor = 3,
                    RoomCount = 2,
                    TypeId = 1,
                    Status = 0,
                    Price = 1500000,
                    Description = "Прекрасная квартира в центре города",
                    MaterialId = 1,
                    Area = 80,
                    AnnouncementDate = DateTime.UtcNow.AddDays(-30)
                },
                new RealEstateObject {
                    DistrictId = 2,
                    Address = "ул. Пушкина, д. 5",
                    Floor = 5,
                    RoomCount = 3,
                    TypeId = 1,
                    Status = 1,
                    Price = 2000000,
                    Description = "Просторная квартира с видом на парк",
                    MaterialId = 2,
                    Area = 100,
                    AnnouncementDate = DateTime.UtcNow.AddDays(-20)
                },
                new RealEstateObject {
                    DistrictId = 2,
                    Address = "ул. Пушкина, д. 3",
                    Floor = 7,
                    RoomCount = 1,
                    TypeId = 3,
                    Status = 0,
                    Price = 1400000,
                    Description = "Уютная квартира с видом на озеро",
                    MaterialId = 2,
                    Area = 50,
                    AnnouncementDate = DateTime.UtcNow.AddDays(-25)
                },
                new RealEstateObject {
                    DistrictId = 3,
                    Address = "ул. Виноградная, д. 81",
                    Floor = 2,
                    RoomCount = 2,
                    TypeId = 2,
                    Status = 0,
                    Price = 3700000,
                    Description = "Шикарный дом на рандомной улице",
                    MaterialId = 3,
                    Area = 240,
                    AnnouncementDate = DateTime.UtcNow.AddDays(-45)
                },
                new RealEstateObject {
                    DistrictId = 1,
                    Address = "ул. Волгодонская, д. 31",
                    Floor = 2,
                    RoomCount = 2,
                    TypeId = 1,
                    Status = 0,
                    Price = 1700000,
                    Description = "Солидная двушка со всеми удобствами",
                    MaterialId = 2,
                    Area = 85,
                    AnnouncementDate = DateTime.UtcNow.AddDays(-35)
                },
                new RealEstateObject {
                    DistrictId = 1,
                    Address = "ул. Волгодонская, д. 29",
                    Floor = 5,
                    RoomCount = 3,
                    TypeId = 1,
                    Status = 1,
                    Price = 2900000,
                    Description = "Непродаваемая трёшка, зато со всеми удобствами",
                    MaterialId = 2,
                    Area = 140,
                    AnnouncementDate = DateTime.UtcNow.AddDays(-120)
                },
                new RealEstateObject {
                    DistrictId = 3,
                    Address = "ул. Яблоневая, д. 1",
                    Floor = 4,
                    RoomCount = 1,
                    TypeId = 1,
                    Status = 0,
                    Price = 2200000,
                    Description = "Огромная однушка, кому она вообще нужна?",
                    MaterialId = 1,
                    Area = 90,
                    AnnouncementDate = DateTime.UtcNow.AddDays(-310)
                },
                new RealEstateObject {
                    DistrictId = 1,
                    Address = "ул. Ленина, д. 3",
                    Floor = 1,
                    RoomCount = 2,
                    TypeId = 1,
                    Status = 0,
                    Price = 1900000,
                    Description = "Солидная квартира в центре на первом этаже",
                    MaterialId = 2,
                    Area = 85,
                    AnnouncementDate = DateTime.UtcNow.AddDays(-210)
                },
            };
            context.RealEstateObjects.AddRange(realEstates);

            var realtors = new List<Realtor>
            {
                new Realtor {
                    LastName = "Иванов",
                    FirstName = "Иван",
                    MiddleName = "Иванович",
                    ContactPhone = "+74234124400"
                },
                new Realtor {
                    LastName = "Петров",
                    FirstName = "Петр",
                    MiddleName = "Петрович",
                    ContactPhone = "+79516163636"
                },
                new Realtor {
                    LastName = "Куйбышев",
                    FirstName = "Тимофей",
                    MiddleName = "Викторович",
                    ContactPhone = "+78005553535"
                },
            };
            context.Realtors.AddRange(realtors);

            var evaluations = new List<Evaluation>
            {
                new Evaluation {
                    RealEstateObjectId = 1,
                    EvaluationDate = DateTime.UtcNow.AddDays(-5),
                    CriterionId = 1,
                    Score = 95 
                },
                new Evaluation {
                    RealEstateObjectId = 1,
                    EvaluationDate = DateTime.UtcNow.AddDays(-7),
                    CriterionId = 2,
                    Score = 90 
                },
                new Evaluation {
                    RealEstateObjectId = 1,
                    EvaluationDate = DateTime.UtcNow.AddDays(-7),
                    CriterionId = 3,
                    Score = 85 
                },
                new Evaluation {
                    RealEstateObjectId = 1,
                    EvaluationDate = DateTime.UtcNow.AddDays(-7),
                    CriterionId = 1,
                    Score = 90 
                },
                new Evaluation {
                    RealEstateObjectId = 2,
                    EvaluationDate = DateTime.UtcNow.AddDays(-4),
                    CriterionId = 3,
                    Score = 75 
                },
                new Evaluation {
                    RealEstateObjectId = 3,
                    EvaluationDate = DateTime.UtcNow.AddDays(-9),
                    CriterionId = 1,
                    Score = 85 
                },
                new Evaluation {
                    RealEstateObjectId = 3,
                    EvaluationDate = DateTime.UtcNow.AddDays(-3),
                    CriterionId = 1,
                    Score = 90 
                },
            };
            context.Evaluations.AddRange(evaluations);

            var criteria = new List<EvaluationCriterion>
            {
                new EvaluationCriterion { CriterionName = "Безопасность" },
                new EvaluationCriterion { CriterionName = "Транспортная доступность" },
                new EvaluationCriterion { CriterionName = "Комфорт" }
            };
            context.EvaluationCriteria.AddRange(criteria);

            var sales = new List<Sale>
            {
                new Sale {
                    RealEstateObjectId = 1,
                    SaleDate = DateTime.UtcNow.AddDays(-10),
                    RealtorId = 1,
                    Price = 1450000 
                },
                new Sale {
                    RealEstateObjectId = 3,
                    SaleDate = DateTime.UtcNow.AddDays(-12),
                    RealtorId = 1,
                    Price = 1380000 
                },
                new Sale {
                    RealEstateObjectId = 4,
                    SaleDate = DateTime.UtcNow.AddDays(-25),
                    RealtorId = 1,
                    Price = 2900000 
                },
                new Sale {
                    RealEstateObjectId = 5,
                    SaleDate = DateTime.UtcNow.AddDays(-27),
                    RealtorId = 2,
                    Price = 1700000 
                },
                new Sale {
                    RealEstateObjectId = 7,
                    SaleDate = DateTime.UtcNow.AddDays(-115),
                    RealtorId = 3,
                    Price = 1900000 
                },
                new Sale {
                    RealEstateObjectId = 8,
                    SaleDate = DateTime.UtcNow.AddDays(-175),
                    RealtorId = 3,
                    Price = 1850000 
                },
            };
            context.Sales.AddRange(sales);

            context.SaveChanges();
        }

        // вызова метода для выполнения запроса 
        var program = new Program();
        //program.GetRealEstateByPriceAndDistrict(1000000, 2000000, "Ленинский"); // 2.1
        //program.GetRealtorsSellingTwoRoomObjects(); // 2.2
        //program.GetTotalPriceOfTwoRoomObjectsByDistrict("Кировский"); // 2.3
        //program.GetMaxAndMinPricesSoldByRealtor("Иванов"); // 2.4
        //program.GetAverageSafetyScoreOfApartmentsSoldByRealtor("Иванов"); // 2.5
        //program.GetRealEstateCountOnSecondFloorByDistrict(); // 2.6
        //program.GetSoldApartmentsCountByRealtor(); // 2.7
        //program.GetTopThreeMostExpensiveRealEstateByDistrict(); // 2.8
        //program.GetYearsWithMoreThanTwoSalesByRealtor("Иванов Иван Иванович"); // 2.9
        //program.GetYearsWithTwoToThreeRealEstates(); // 2.10
        //program.GetRealEstatesWithPriceDifferenceLessThanTwentyPercent(); // 2.11
        //program.GetApartmentsWithPricePerSquareMeterBelowAverageByDistrict(); // 2.12
        //program.GetRealtorsWithNoSalesThisYear(); // 2.13
        //program.GetSalesInfoByDistrictForPreviousAndCurrentYears(); // 2.14
        //program.GetAverageEvaluationByCriterionForRealEstate(1); // 2.15

    }

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
