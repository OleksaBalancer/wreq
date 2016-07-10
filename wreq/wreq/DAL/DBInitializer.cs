using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using wreq.Models.Entities;

namespace wreq.DAL
{
    public class DBInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext ctx)
        {
            //todo set localization for default values
            InitializeIdentity(ctx);

            #region Initializing soil types
            List<SoilType> soilTypes = new List<SoilType>() {
                new SoilType()
                {
                    Name = "Песок",
                    FieldWaterCapacity = 0.12,
                    WiltWaterCapacity = 0.045,
                    ReadilyEvaporableWater = 4.5,
                    TotalEvaporableWater = 9,

                },
                new SoilType()
                {
                    Name = "Глинистый песок",
                    FieldWaterCapacity = 0.15,
                    WiltWaterCapacity = 0.065,
                    ReadilyEvaporableWater = 6,
                    TotalEvaporableWater = 11.5,

                },
                new SoilType()
                {
                    Name = "Супесь",
                    FieldWaterCapacity = 0.12,
                    WiltWaterCapacity = 0.045,
                    ReadilyEvaporableWater = 8,
                    TotalEvaporableWater = 17.5,

                },
                new SoilType()
                {
                    Name = "Суглинок",
                    FieldWaterCapacity = 0.25,
                    WiltWaterCapacity = 0.12,
                    ReadilyEvaporableWater = 9,
                    TotalEvaporableWater = 19,

                },
                new SoilType()
                {
                    Name = "Иловатый суглинок",
                    FieldWaterCapacity = 0.29,
                    WiltWaterCapacity = 0.15,
                    ReadilyEvaporableWater = 9.5,
                    TotalEvaporableWater = 21.5,

                },
                new SoilType()
                {
                    Name = "Ил",
                    FieldWaterCapacity = 0.32,
                    WiltWaterCapacity = 0.17,
                    ReadilyEvaporableWater = 9.5,
                    TotalEvaporableWater = 24,

                },
                new SoilType()
                {
                    Name = "Иловатая глина",
                    FieldWaterCapacity = 0.36,
                    WiltWaterCapacity = 0.23,
                    ReadilyEvaporableWater = 10,
                    TotalEvaporableWater = 25,

                },
                new SoilType()
                {
                    Name = "Глина",
                    FieldWaterCapacity = 0.26,
                    WiltWaterCapacity = 0.22,
                    ReadilyEvaporableWater = 10,
                    TotalEvaporableWater = 25.5,
                }
            };

            ctx.SoilTypes.AddRange(soilTypes);

            #endregion

            #region Initializing irrigation types
            List<IrrigationType> irrigationTypes = new List<IrrigationType>() {
                new IrrigationType()
                {
                    Name = "Бассейновое орошение",
                    WettedSoilAreaFraction = 1.0,

                },
                new IrrigationType()
                {
                    Name = "Дождевание",
                    WettedSoilAreaFraction = 1.0,

                },
                new IrrigationType()
                {
                    Name = "Бороздковое орошение",
                    WettedSoilAreaFraction = 0.5,

                },
                new IrrigationType()
                {
                    Name = "Капельное орошение",
                    WettedSoilAreaFraction = 0.33,
                }
            };

            ctx.IrrigationTypes.AddRange(irrigationTypes);
            #endregion

            #region Initializing cultures
            List<Culture> cultures = new List<Culture>() {
                new Culture()
                {
                    Name = "Брокколи",
                    KcIni = 0.15,
                    KcMid = 0.95,
                    KcEnd = 0.85,
                    MaxHeight = 0.3,
                    MaxRootDepth = 0.5,
                    WaterExtraction = 0.45,
                    YieldCoefficient = 0.95,
                    LengthIni = 35,
                    LengthDev =45,
                    LengthMid = 40,
                    LengthLate = 15
                },
                new Culture()
                {
                    Name = "Капуста белокочанная",
                    KcIni = 0.15,
                    KcMid = 0.95,
                    KcEnd = 0.85,
                    MaxHeight = 0.4,
                    MaxRootDepth = 0.6,
                    WaterExtraction = 0.45,
                    YieldCoefficient = 0.95,
                    LengthIni = 40,
                    LengthDev =60,
                    LengthMid = 50,
                    LengthLate = 15
                },new Culture()
                {
                    Name = "Морковь",
                    KcIni = 0.15,
                    KcMid = 0.90,
                    KcEnd = 0.85,
                    MaxHeight = 0.3,
                    MaxRootDepth = 0.7,
                    WaterExtraction = 0.35,
                    YieldCoefficient = 0.9,
                    LengthIni = 25,
                    LengthDev =40,
                    LengthMid = 60,
                    LengthLate = 25
                },
                new Culture()
                {
                    Name = "Лук",
                    KcIni = 0.15,
                    KcMid = 0.95,
                    KcEnd = 0.65,
                    MaxHeight = 0.4,
                    MaxRootDepth = 0.4,
                    WaterExtraction = 0.3,
                    YieldCoefficient = 1.1,
                    LengthIni = 15,
                    LengthDev =30,
                    LengthMid = 80,
                    LengthLate = 40
                },
                new Culture()
                {
                    Name = "Перец сладкий",
                    KcIni = 0.15,
                    KcMid = 1.0,
                    KcEnd = 0.8,
                    MaxHeight = 0.7,
                    MaxRootDepth = 0.7,
                    WaterExtraction = 0.3,
                    YieldCoefficient = 1.05,
                    LengthIni = 30,
                    LengthDev =40,
                    LengthMid = 50,
                    LengthLate = 30
                },
                new Culture()
                {
                    Name = "Томат",
                    KcIni = 0.15,
                    KcMid = 1.1,
                    KcEnd = 0.7,
                    MaxHeight = 0.6,
                    MaxRootDepth = 0.8,
                    WaterExtraction = 0.4,
                    YieldCoefficient = 1.1,
                    LengthIni = 30,
                    LengthDev =40,
                    LengthMid = 50,
                    LengthLate = 60
                },new Culture()
                {
                    Name = "Дыня",
                    KcIni = 0.15,
                    KcMid = 0.75,
                    KcEnd = 0.5,
                    MaxHeight = 0.3,
                    MaxRootDepth = 1,
                    WaterExtraction = 0.35,
                    YieldCoefficient = 1.1,
                    LengthIni = 25,
                    LengthDev =30,
                    LengthMid = 50,
                    LengthLate = 20
                },
                new Culture()
                {
                    Name = "Огурец",
                    KcIni = 0.15,
                    KcMid = 0.95,
                    KcEnd = 0.7,
                    MaxHeight = 0.3,
                    MaxRootDepth = 1,
                    WaterExtraction = 0.5,
                    YieldCoefficient = 1.1,
                    LengthIni = 20,
                    LengthDev =30,
                    LengthMid = 40,
                    LengthLate = 20
                },
                new Culture()
                {
                    Name = "Арбуз",
                    KcIni = 0.15,
                    KcMid = 0.95,
                    KcEnd = 0.7,
                    MaxHeight = 0.4,
                    MaxRootDepth = 1,
                    WaterExtraction = 0.4,
                    YieldCoefficient = 1.1,
                    LengthIni = 20,
                    LengthDev =30,
                    LengthMid = 30,
                    LengthLate = 30
                },
                new Culture()
                {
                    Name = "Свекла",
                    KcIni = 0.15,
                    KcMid = 0.95,
                    KcEnd = 0.85,
                    MaxHeight = 0.3,
                    MaxRootDepth = 0.6,
                    WaterExtraction = 0.5,
                    YieldCoefficient = 1.0,
                    LengthIni = 25,
                    LengthDev =30,
                    LengthMid = 25,
                    LengthLate = 15
                },new Culture()
                {
                    Name = "Картофель",
                    KcIni = 0.15,
                    KcMid = 1.1,
                    KcEnd = 0.65,
                    MaxHeight = 0.5,
                    MaxRootDepth = 0.5,
                    WaterExtraction = 0.35,
                    YieldCoefficient = 1.1,
                    LengthIni = 30,
                    LengthDev =30,
                    LengthMid = 50,
                    LengthLate = 30
                },
                new Culture()
                {
                    Name = "Горох",
                    KcIni = 0.15,
                    KcMid = 1.0,
                    KcEnd = 0.8,
                    MaxHeight = 0.4,
                    MaxRootDepth = 0.6,
                    WaterExtraction = 0.45,
                    YieldCoefficient = 1.15,
                    LengthIni = 20,
                    LengthDev =30,
                    LengthMid = 35,
                    LengthLate = 20
                },
                new Culture()
                {
                    Name = "Фасоль",
                    KcIni = 0.15,
                    KcMid = 1.1,
                    KcEnd = 0.25,
                    MaxHeight = 0.4,
                    MaxRootDepth = 0.6,
                    WaterExtraction = 0.45,
                    YieldCoefficient = 1.15,
                    LengthIni = 20,
                    LengthDev =30,
                    LengthMid = 35,
                    LengthLate = 20
                },
                new Culture()
                {
                    Name = "Хлопок",
                    KcIni = 0.15,
                    KcMid = 1.15,
                    KcEnd = 0.45,
                    MaxHeight = 1.5,
                    MaxRootDepth = 1.3,
                    WaterExtraction = 0.65,
                    YieldCoefficient = 0.85,
                    LengthIni = 40,
                    LengthDev =60,
                    LengthMid = 50,
                    LengthLate = 50
                },
                new Culture()
                {
                    Name = "Подсолнечник",
                    KcIni = 0.15,
                    KcMid = 0.1,
                    KcEnd = 0.25,
                    MaxHeight = 2,
                    MaxRootDepth = 1,
                    WaterExtraction = 0.45,
                    YieldCoefficient = 0.95,
                    LengthIni = 30,
                    LengthDev =40,
                    LengthMid = 50,
                    LengthLate = 30
                },
                new Culture()
                {
                    Name = "Ячмень",
                    KcIni = 0.15,
                    KcMid = 1.1,
                    KcEnd = 0.15,
                    MaxHeight = 1,
                    MaxRootDepth = 1.2,
                    WaterExtraction = 0.55,
                    YieldCoefficient = 1.15,
                    LengthIni = 30,
                    LengthDev =30,
                    LengthMid = 50,
                    LengthLate = 30
                },
                new Culture()
                {
                    Name = "Овес",
                    KcIni = 0.15,
                    KcMid = 1.1,
                    KcEnd = 0.15,
                    MaxHeight = 1,
                    MaxRootDepth = 1.2,
                    WaterExtraction = 0.55,
                    YieldCoefficient = 1.15,
                    LengthIni = 35,
                    LengthDev =35,
                    LengthMid = 50,
                    LengthLate = 30
                },new Culture()
                {
                    Name = "Пшеница",
                    KcIni = 0.15,
                    KcMid = 1.1,
                    KcEnd = 0.2,
                    MaxHeight = 1,
                    MaxRootDepth = 1.2,
                    WaterExtraction = 0.55,
                    YieldCoefficient = 1.15,
                    LengthIni = 30,
                    LengthDev =35,
                    LengthMid = 55,
                    LengthLate = 30
                },
                new Culture()
                {
                    Name = "Кукуруза",
                    KcIni = 0.15,
                    KcMid = 1.1,
                    KcEnd = 0.5,
                    MaxHeight = 1.7,
                    MaxRootDepth = 1.2,
                    WaterExtraction = 0.5,
                    YieldCoefficient = 1.25,
                    LengthIni = 30,
                    LengthDev =50,
                    LengthMid = 55,
                    LengthLate = 45
                },
                new Culture()
                {
                    Name = "Рис",
                    KcIni = 1,
                    KcMid = 1.15,
                    KcEnd = 0.6,
                    MaxHeight = 1,
                    MaxRootDepth = 0.65,
                    WaterExtraction = 0.2,
                    YieldCoefficient = 1,
                    LengthIni = 30,
                    LengthDev =30,
                    LengthMid = 65,
                    LengthLate = 35
                }
            };

            ctx.Cultures.AddRange(cultures);
            #endregion

            #region Initializing weather data

            List<WeatherRecord> weatherRecords = new List<WeatherRecord>()
            {
                new WeatherRecord()
{
Date = new DateTime(2016, 5, 1),
AtmosphericPressure = 1013.94749450684,
Humidity = 0.627499997615814,
Precipitation = 0,
TempMax = 24.3324999809265,
TempMin = 10.4824999570847,
WindSpeed = 1.010000012815,
DaylightHours = 14.5918055555556
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 2),
AtmosphericPressure = 1013.38751220703,
Humidity = 0.697499997913837,
Precipitation = 0,
TempMax = 20.8324999809265,
TempMin = 11.2874999046326,
WindSpeed = 1.85500002838671,
DaylightHours = 14.6445833333333
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 3),
AtmosphericPressure = 1012.74000549316,
Humidity = 0.64750000834465,
Precipitation = 0,
TempMax = 23.1775000095367,
TempMin = 9.4449999332428,
WindSpeed = 1.6000000089407,
DaylightHours = 14.6970833333333
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 4),
AtmosphericPressure = 1011.41749572754,
Humidity = 0.587499998509884,
Precipitation = 0,
TempMax = 23.647500038147,
TempMin = 10.740000128746,
WindSpeed = 1.70749999582767,
DaylightHours = 14.7488194444444
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 5),
AtmosphericPressure = 1013.03997802734,
Humidity = 0.560000002384186,
Precipitation = 0,
TempMax = 21.2099990844727,
TempMin = 7.8899998664856,
WindSpeed = 4.55999994277954,
DaylightHours = 14.8183333333333
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 6),
AtmosphericPressure = 1012.51000976563,
Humidity = 0.720000028610229,
Precipitation = 0,
TempMax = 18.9799995422363,
TempMin = 7.65999984741211,
WindSpeed = 4.34999990463257,
DaylightHours = 14.8688888888889
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 7),
AtmosphericPressure = 1014.53002929688,
Humidity = 0.839999973773956,
Precipitation = 0,
TempMax = 18.3099994659424,
TempMin = 9.13000011444092,
WindSpeed = 4.30999994277954,
DaylightHours = 14.9188888888889
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 8),
AtmosphericPressure = 1019.33001708984,
Humidity = 0.759999990463257,
Precipitation = 0,
TempMax = 21.1200008392334,
TempMin = 9.05000019073486,
WindSpeed = 3.66000008583069,
DaylightHours = 14.9686111111111
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 9),
AtmosphericPressure = 1023.28997802734,
Humidity = 0.670000016689301,
Precipitation = 0,
TempMax = 22.1700000762939,
TempMin = 10.1800003051758,
WindSpeed = 4.86999988555908,
DaylightHours = 15.0175
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 10),
AtmosphericPressure = 1023.64001464844,
Humidity = 0.579999983310699,
Precipitation = 0,
TempMax = 21.6299991607666,
TempMin = 9.84000015258789,
WindSpeed = 3.33999991416931,
DaylightHours = 15.0658333333333
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 11),
AtmosphericPressure = 1015.90748596191,
Humidity = 0.53999999910593,
Precipitation = 0,
TempMax = 26.2000002861023,
TempMin = 13.1600000858307,
WindSpeed = 2.30750000476837,
DaylightHours = 15.0969444444444
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 12),
AtmosphericPressure = 1014.57000732422,
Humidity = 0.552500002086163,
Precipitation = 0,
TempMax = 26.5424995422363,
TempMin = 12.0875000953674,
WindSpeed = 2.21249997615814,
DaylightHours = 15.1440277777778
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 13),
AtmosphericPressure = 1011.26000976563,
Humidity = 0.574999995529652,
Precipitation = 0,
TempMax = 25.3125,
TempMin = 12.7849998474121,
WindSpeed = 1.70999997854233,
DaylightHours = 15.1907638888889
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 14),
AtmosphericPressure = 1009.74749755859,
Humidity = 0.662499994039536,
Precipitation = 0,
TempMax = 24.4650001525879,
TempMin = 13.2124998569489,
WindSpeed = 2.48499998450279,
DaylightHours = 15.2365972222222
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 15),
AtmosphericPressure = 1009.15501403809,
Humidity = 0.64750000834465,
Precipitation = 0,
TempMax = 26.2875003814697,
TempMin = 13.2149996757507,
WindSpeed = 2.81999997794628,
DaylightHours = 15.2817361111111
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 16),
AtmosphericPressure = 1011.41499328613,
Humidity = 0.677499994635582,
Precipitation = 0,
TempMax = 24.1399998664856,
TempMin = 13.397500038147,
WindSpeed = 2.76249992847443,
DaylightHours = 15.32625
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 17),
AtmosphericPressure = 1011.54249572754,
Humidity = 0.642499998211861,
Precipitation = 0,
TempMax = 25.6699995994568,
TempMin = 13.129999756813,
WindSpeed = 2.0450000166893,
DaylightHours = 15.3696527777778
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 18),
AtmosphericPressure = 1013.03248596191,
Humidity = 0.652499988675117,
Precipitation = 0,
TempMax = 25.4800000190735,
TempMin = 13.4500000476837,
WindSpeed = 2.09750007092953,
DaylightHours = 15.4125
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 19),
AtmosphericPressure = 1015.61250305176,
Humidity = 0.60249999165535,
Precipitation = 0,
TempMax = 26.3249998092651,
TempMin = 13.7549996376038,
WindSpeed = 2.22999998927116,
DaylightHours = 15.454375
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 20),
AtmosphericPressure = 1017.84498596191,
Humidity = 0.55749998241663,
Precipitation = 0,
TempMax = 28.0675001144409,
TempMin = 14.6275000572205,
WindSpeed = 2.52000005543232,
DaylightHours = 15.4955555555556
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 21),
AtmosphericPressure = 1017.30000305176,
Humidity = 0.529999986290932,
Precipitation = 0,
TempMax = 27.0650000572205,
TempMin = 13.7225003242493,
WindSpeed = 2.96499997377396,
DaylightHours = 15.5356944444444
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 22),
AtmosphericPressure = 1015.50749206543,
Humidity = 0.530000001192093,
Precipitation = 0,
TempMax = 28.2600002288818,
TempMin = 14.0450000762939,
WindSpeed = 2.68249994516373,
DaylightHours = 15.575
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 23),
AtmosphericPressure = 1012.64999389648,
Humidity = 0.517500005662441,
Precipitation = 0,
TempMax = 28.3499999046326,
TempMin = 15.8150000572205,
WindSpeed = 3.07749998569489,
DaylightHours = 15.6133333333333
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 24),
AtmosphericPressure = 1010.74249267578,
Humidity = 0.552500002086163,
Precipitation = 0,
TempMax = 27.1399998664856,
TempMin = 15.1425001621246,
WindSpeed = 4.00500005483627,
DaylightHours = 15.6506944444444
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 25),
AtmosphericPressure = 1010.68998718262,
Humidity = 0.657499998807907,
Precipitation = 0,
TempMax = 23.1799998283386,
TempMin = 13.6150000095367,
WindSpeed = 3.74499994516373,
DaylightHours = 15.6870138888889
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 26),
AtmosphericPressure = 1010.99749755859,
Humidity = 0.632499992847443,
Precipitation = 0,
TempMax = 24.7499997615814,
TempMin = 14.327499628067,
WindSpeed = 2.58250001072884,
DaylightHours = 15.7223611111111
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 27),
AtmosphericPressure = 1010.07250976563,
Humidity = 0.637499988079071,
Precipitation = 0,
TempMax = 25.4300003051758,
TempMin = 13.8250000476837,
WindSpeed = 1.44499999284744,
DaylightHours = 15.7567361111111
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 28),
AtmosphericPressure = 1008.33250427246,
Humidity = 0.652500003576279,
Precipitation = 0,
TempMax = 24.8674998283386,
TempMin = 13.3774998188019,
WindSpeed = 1.20749999582767,
DaylightHours = 15.7898611111111
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 29),
AtmosphericPressure = 1008.6875,
Humidity = 0.715000003576279,
Precipitation = 0,
TempMax = 23.7400002479553,
TempMin = 13.4549999237061,
WindSpeed = 1.56999999284744,
DaylightHours = 15.821875
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 30),
AtmosphericPressure = 1010.67501831055,
Humidity = 0.714999988675117,
Precipitation = 0,
TempMax = 24.2374997138977,
TempMin = 14.1624999046326,
WindSpeed = 1.92750003188848,
DaylightHours = 15.8529166666667
},
new WeatherRecord()
{
Date = new DateTime(2016, 5, 31),
AtmosphericPressure = 1010.87750244141,
Humidity = 0.680000007152557,
Precipitation = 0,
TempMax = 22.6675000190735,
TempMin = 14.2725002765656,
WindSpeed = 2.43250003457069,
DaylightHours = 15.8827777777778
}


            };

            ctx.WeatherRecords.AddRange(weatherRecords);
            #endregion

            #region Initializing fieds
            List<Field> fields = new List<Field>() {
                new Field()
                {
                    Name = "11111",
                    Area = 100,
                    Description = "test field 1",
                    IrrigationType = irrigationTypes[2],
                    SoilType = soilTypes[3],
                    Latitude = 46,
                    Longitude = 32,
                    Altitude = 100,
                    WeatherRecords = weatherRecords
                }
            };

            ctx.Fields.AddRange(fields);
            #endregion

            #region Initializing crops
            List<Crop> crops = new List<Crop>() {
                new Crop()
                {
                    Name = "first crop",
                    Culture = cultures[17],
                    Field = fields[0],
                    LengthIni = 20,
                    LengthDev = 25,
                    LengthMid = 60,
                    LengthLate = 30,
                    DateSeeded = new DateTime(2016,05,01),
                    Description="test oat crop"
                }
            };

            ctx.Crops.AddRange(crops);
            #endregion

            ctx.SaveChanges();
        }

        private void InitializeIdentity(ApplicationDbContext ctx)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ctx));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(ctx));
            string role = "Admin";
            string password = "123456";

            if (!RoleManager.RoleExists(role))
            {
                var roleresult = RoleManager.Create(new IdentityRole(role));
            }

            var Admin = new ApplicationUser();
            Admin.UserName = role;
            var Adminresult = UserManager.Create(Admin, password);

            if (Adminresult.Succeeded)
            {
                UserManager.AddToRole(Admin.Id, role);
            }
        }

    }
}