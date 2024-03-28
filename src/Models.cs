using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Type
{
    public long TypeId { get; set; } // Код типа
    public string TypeName { get; set; } // Название типа
}

public class District
{
    public int DistrictId { get; set; } // Код района
    public string DistrictName { get; set; } // Название района
}

public class BuildingMaterial
{
    public int BuildingMaterialId { get; set; } // Код материала
    public string MaterialName { get; set; } // Название материала
}

public class RealEstateObject
{
    public int RealEstateObjectId { get; set; } // Код объекта
    public int DistrictId { get; set; } // Район
    public string Address { get; set; } // Адрес
    public int Floor { get; set; } // Этаж
    public int RoomCount { get; set; } // Количество комнат
    public long TypeId { get; set; } // Тип
    public int Status { get; set; } // Статус
    public double Price { get; set; } // Стоимость
    public string Description { get; set; } // Описание
    public int MaterialId { get; set; } // Материал здания
    public double Area { get; set; } // Площадь
    public DateTime AnnouncementDate { get; set; } // Дата объявления
}

public class EvaluationCriterion
{
    public int EvaluationCriterionId { get; set; } // Код критерия
    public string CriterionName { get; set; } // Название критерия
}

public class Evaluation
{
    public int EvaluationId { get; set; } // Код оценки
    public int RealEstateObjectId { get; set; } // Код объекта
    public DateTime EvaluationDate { get; set; } // Дата оценивания
    public int CriterionId { get; set; } // Код критерия
    public int Score { get; set; } // Оценка
}

public class Realtor
{
    public int RealtorId { get; set; } // Код риэлтора
    public string LastName { get; set; } // Фамилия
    public string FirstName { get; set; } // Имя
    public string MiddleName { get; set; } // Отчество
    public string ContactPhone { get; set; } // Контактный телефон
}

public class Sale
{
    public int SaleId { get; set; } // Код продажи
    public int RealEstateObjectId { get; set; } // Код объекта
    public DateTime SaleDate { get; set; } // Дата продажи
    public int RealtorId { get; set; } // Код риэлтора
    public double Price { get; set; } // Стоимость
}
