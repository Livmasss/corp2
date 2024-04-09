using System;
using System.Collections.Generic;

class Program
{
    static Program program = new Program();
    Solution solution = new Solution();
    static void Main(string[] args)
    {
        // вызова метода для выполнения запроса 
        program.initiateDataBase();
        program.task3();
    }

    private void task1() {
        solution.GetRealEstateByPriceAndDistrict(1000000, 2000000, "Ленинский");
    }

    private void task2() {
        solution.GetRealtorsSellingTwoRoomObjects();
    }

    private void task3() {
        solution.GetTotalPriceOfTwoRoomObjectsByDistrict("Кировский");
    }

    private void task4() {
        solution.GetMaxAndMinPricesSoldByRealtor("Иванов"); 
    }

    private void task5() {
        solution.GetAverageSafetyScoreOfApartmentsSoldByRealtor("Иванов");
    }

    private void task6() {
        solution.GetRealEstateCountOnSecondFloorByDistrict();
    }

    private void task7() {
        solution.GetSoldApartmentsCountByRealtor(); 
    }

    private void task8() {
        solution.GetTopThreeMostExpensiveRealEstateByDistrict();
    }

    private void task9() {
        solution.GetYearsWithMoreThanTwoSalesByRealtor("Иванов Иван Иванович");
    }

    private void task10() {
        solution.GetYearsWithTwoToThreeRealEstates();
    }

    private void task11() {
        solution.GetRealEstatesWithPriceDifferenceLessThanTwentyPercent(); 
    }

    private void task12() {
        solution.GetApartmentsWithPricePerSquareMeterBelowAverageByDistrict();
    }

    private void task13() {
        solution.GetRealtorsWithNoSalesThisYear();
    }

    private void task14() {
        solution.GetSalesInfoByDistrictForPreviousAndCurrentYears();
    }

    private void task15() {
        solution.GetAverageEvaluationByCriterionForRealEstate(1);
    }

    private void initiateDataBase() {
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
    }

}
