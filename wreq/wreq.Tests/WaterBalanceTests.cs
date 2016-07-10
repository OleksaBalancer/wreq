using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wreq.DAL;
using wreq.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using wreq.BL;

namespace wreq.Tests
{

    //[TestClass]
    //public class WaterBalanceParametersTest
    //{
    //    [TestMethod]
    //    public void SaturationVapourPressureTest()
    //    {
    //        double tempMin = 15;
    //        double tempMax = 24.5;

    //        double saturationVapourPressure = WaterBalanceCalculator.SaturationVapourPressure(tempMax, tempMin);

    //        Assert.AreEqual(2.39, Math.Round(saturationVapourPressure, 2));

    //    }

    //    [TestMethod]
    //    public void ActualVapourPressureTest()
    //    {
    //        double tempMin = 18;
    //        double tempMax = 25;
    //        double humidity = 68;

    //        double actualVapourPressure = WaterBalanceCalculator.ActualVapourPressure(tempMax, tempMin, humidity);

    //        Assert.AreEqual(1.78, Math.Round(actualVapourPressure, 2));
    //    }

    //    [TestMethod]
    //    public void ExtraTerrestrialRadiationTest()
    //    {
    //        int day = new DateTime(2015, 09, 03).DayOfYear;
    //        double lattitude = -20 * (Math.PI / 180);
    //        double distance = WaterBalanceCalculator.SunEarthDistance(day);
    //        double sunTilt = WaterBalanceCalculator.SunTilt(day);
    //        double sunsetAngle = WaterBalanceCalculator.SunsetAngle(lattitude, sunTilt);
    //        double extraterrestrialRadiation = WaterBalanceCalculator.ExtraterrestrialRadiation(distance, sunsetAngle, lattitude, sunTilt);


    //        Assert.AreEqual(32.2, Math.Round(extraterrestrialRadiation, 1));
    //    }

    //    [TestMethod]
    //    public void DaylightHoursTest()
    //    {
    //        int day = new DateTime(2015, 09, 03).DayOfYear;
    //        double lattitude = -20 * (Math.PI / 180);
    //        double distance = WaterBalanceCalculator.SunEarthDistance(day);
    //        double sunTilt = WaterBalanceCalculator.SunTilt(day);
    //        double sunsetAngle = WaterBalanceCalculator.SunsetAngle(lattitude, sunTilt);
    //        double daylightHours = WaterBalanceCalculator.MaxDaylightHours(sunsetAngle);

    //        Assert.AreEqual(11.7, Math.Round(daylightHours, 1));
    //    }

    //    [TestMethod]
    //    public void ShortWaveRadiationTest()
    //    {
    //        int day = new DateTime(2015, 05, 15).DayOfYear;
    //        double lattitude = -22.9 * (Math.PI / 180);
    //        double distance = WaterBalanceCalculator.SunEarthDistance(day);
    //        double sunTilt = WaterBalanceCalculator.SunTilt(day);
    //        double sunsetAngle = WaterBalanceCalculator.SunsetAngle(lattitude, sunTilt);
    //        double daylightHours = WaterBalanceCalculator.MaxDaylightHours(sunsetAngle);
    //        double extraterrestrialRadiation = WaterBalanceCalculator.ExtraterrestrialRadiation(distance, sunsetAngle, lattitude, sunTilt);
    //        double shortWaveRadiation = WaterBalanceCalculator.ShortWaveRadiation(7.1, daylightHours, extraterrestrialRadiation, 0.25, 0.5);

    //        Assert.AreEqual(14.5, Math.Round(shortWaveRadiation, 1));
    //    }

    //    [TestMethod]
    //    public void NetLongWaveRadiationTest()
    //    {
    //        int day = new DateTime(2015, 05, 15).DayOfYear;
    //        double lattitude = -22.7 * (Math.PI / 180);
    //        double distance = WaterBalanceCalculator.SunEarthDistance(day);
    //        double sunTilt = WaterBalanceCalculator.SunTilt(day);
    //        double sunsetAngle = WaterBalanceCalculator.SunsetAngle(lattitude, sunTilt);
    //        double daylightHours = WaterBalanceCalculator.MaxDaylightHours(sunsetAngle);
    //        double extraterrestrialRadiation = WaterBalanceCalculator.ExtraterrestrialRadiation(distance, sunsetAngle, lattitude, sunTilt);
    //        double tempMax = 25.1;
    //        double tempMin = 19.1;
    //        double actualVapourPressure = WaterBalanceCalculator.ActualVapourPressure(tempMin, tempMax, 45);
    //        double shortWaveRadiation = WaterBalanceCalculator.ShortWaveRadiation(7.1, daylightHours, extraterrestrialRadiation, 0.25, 0.5);
    //        double clearSkyRadiation = WaterBalanceCalculator.ClearSkyRadiation(0, extraterrestrialRadiation);

    //        double netLongWaveRadiation = WaterBalanceCalculator.NetLongWaveRadiation(tempMax, tempMin, 2.1, shortWaveRadiation, clearSkyRadiation);

    //        Assert.AreEqual(3.5, Math.Round(netLongWaveRadiation, 1));
    //    }

    //    [TestMethod]
    //    public void ReferenceEvapotranspirationTest()
    //    {

    //        double tempMax = 21.5;
    //        double tempMin = 12.3;
    //        double humidity = 84 / 2 + 63 / 2;
    //        double windSpeed = 2.078;
    //        double slope = WaterBalanceCalculator.SlopeVapourPressureCurve(tempMax / 2 + tempMin / 2);
    //        double pressure = 100.1;
    //        double saturationPressure = WaterBalanceCalculator.SaturationVapourPressure(tempMax, tempMin);
    //        double actualPressure = WaterBalanceCalculator.ActualVapourPressure(tempMin, tempMax, humidity);
    //        int day = new DateTime(2015, 07, 06).DayOfYear;
    //        double lattitude = 50.80 * (Math.PI / 180);
    //        double distance = WaterBalanceCalculator.SunEarthDistance(day);
    //        double sunTilt = WaterBalanceCalculator.SunTilt(day);
    //        double sunsetAngle = WaterBalanceCalculator.SunsetAngle(lattitude, sunTilt);
    //        double extraterrestrialRadiation = WaterBalanceCalculator.ExtraterrestrialRadiation(distance, sunsetAngle, lattitude, sunTilt);
    //        double daylightHours = WaterBalanceCalculator.MaxDaylightHours(sunsetAngle);
    //        double actualDaylightHours = 9.25;
    //        double shortWaveRadiation = WaterBalanceCalculator.ShortWaveRadiation(actualDaylightHours, daylightHours, extraterrestrialRadiation, 0.25, 0.5);
    //        double clearSkyRadiation = WaterBalanceCalculator.ClearSkyRadiation(0, extraterrestrialRadiation);
    //        double netLongWaveRadiation = WaterBalanceCalculator.NetLongWaveRadiation(tempMax, tempMin, 2.1, shortWaveRadiation, clearSkyRadiation);
    //        double netShortWaveRadiation = WaterBalanceCalculator.NetShortWaveRadiation(0.23, shortWaveRadiation);
    //        double netRadiation = WaterBalanceCalculator.NetRadiation(netShortWaveRadiation, netLongWaveRadiation);
    //        double referenceEvapotranspiration = WaterBalanceCalculator.ReferenceEvapotranspiration(netRadiation, tempMax / 2 + tempMin / 2, windSpeed, saturationPressure, actualPressure, slope, pressure);

    //        Assert.AreEqual(4, Math.Round(referenceEvapotranspiration, 1));
    //    }

    //    [TestMethod]
    //    public void DailyBaseCropCoefficientTest()
    //    {
    //        int lIni = 25;
    //        int lDev = 25;
    //        int lMid = 30;
    //        int lEnd = 20;
    //        double kcIni = 0.15;
    //        double kcMid = 1.14;
    //        double KcEnd = 0.25;

    //        int day1 = 12;
    //        int day2 = 37;
    //        int day3 = 65;
    //        int day4 = 90;

    //        double kc1 = WaterBalanceCalculator.LibBaseCropCoefficient(day1, lIni, lDev, lMid, lEnd, kcIni, kcMid, KcEnd);
    //        double kc2 = WaterBalanceCalculator.LibBaseCropCoefficient(day2, lIni, lDev, lMid, lEnd, kcIni, kcMid, KcEnd);
    //        double kc3 = WaterBalanceCalculator.LibBaseCropCoefficient(day3, lIni, lDev, lMid, lEnd, kcIni, kcMid, KcEnd);
    //        double kc4 = WaterBalanceCalculator.LibBaseCropCoefficient(day4, lIni, lDev, lMid, lEnd, kcIni, kcMid, KcEnd);

    //        Assert.AreEqual(0.15, Math.Round(kc1, 2));
    //        Assert.AreEqual(0.63, Math.Round(kc2, 2));
    //        Assert.AreEqual(1.14, Math.Round(kc3, 2));
    //        Assert.AreEqual(0.7, Math.Round(kc4, 2));
    //    }

    //    [TestMethod]
    //    public void GettingIrrigationPlans()
    //    {
    //        #region Initializing weather data

    //        List<WeatherRecord> weatherRecords = new List<WeatherRecord>()
    //        {
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,01),
    //                AtmosphericPressure = 102.1,
    //                Humidity = 45,
    //                Precipitation = 0,
    //                TempMax = 27,
    //                TempMin = 13,
    //                WindSpeed = 5,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,02),
    //                AtmosphericPressure = 102.1,
    //                Humidity = 45,
    //                Precipitation = 0,
    //                TempMax = 27,
    //                TempMin = 13,
    //                WindSpeed = 5,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,03),
    //                AtmosphericPressure = 102.1,
    //                Humidity = 45,
    //                Precipitation = 0,
    //                TempMax = 27,
    //                TempMin = 13,
    //                WindSpeed = 5,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,04),
    //                AtmosphericPressure = 100.0,
    //                Humidity = 45,
    //                Precipitation = 0,
    //                TempMax = 20,
    //                TempMin = 10,
    //                WindSpeed = 4,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,05),
    //                AtmosphericPressure = 92.1,
    //                Humidity = 60,
    //                Precipitation = 0,
    //                TempMax = 15,
    //                TempMin = 7,
    //                WindSpeed = 3,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,06),
    //                AtmosphericPressure = 110.0,
    //                Humidity = 40,
    //                Precipitation = 0,
    //                TempMax = 21,
    //                TempMin = 11,
    //                WindSpeed = 3,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,07),
    //                AtmosphericPressure = 120.0,
    //                Humidity = 30,
    //                Precipitation = 0,
    //                TempMax = 25,
    //                TempMin = 15,
    //                WindSpeed = 4,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,08),
    //                AtmosphericPressure = 120.0,
    //                Humidity = 30,
    //                Precipitation = 0,
    //                TempMax = 25,
    //                TempMin = 15,
    //                WindSpeed = 4,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,09),
    //                AtmosphericPressure = 120.0,
    //                Humidity = 30,
    //                Precipitation = 0,
    //                TempMax = 25,
    //                TempMin = 15,
    //                WindSpeed = 4,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,10),
    //                AtmosphericPressure = 120.0,
    //                Humidity = 30,
    //                Precipitation = 0,
    //                TempMax = 25,
    //                TempMin = 15,
    //                WindSpeed = 4,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,11),
    //                AtmosphericPressure = 120.0,
    //                Humidity = 30,
    //                Precipitation = 0,
    //                TempMax = 25,
    //                TempMin = 15,
    //                WindSpeed = 4,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,12),
    //                AtmosphericPressure = 120.0,
    //                Humidity = 30,
    //                Precipitation = 0,
    //                TempMax = 25,
    //                TempMin = 15,
    //                WindSpeed = 4,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,13),
    //                AtmosphericPressure = 120.0,
    //                Humidity = 30,
    //                Precipitation = 0,
    //                TempMax = 25,
    //                TempMin = 15,
    //                WindSpeed = 4,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,14),
    //                AtmosphericPressure = 120.0,
    //                Humidity = 30,
    //                Precipitation = 0,
    //                TempMax = 25,
    //                TempMin = 15,
    //                WindSpeed = 4,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,15),
    //                AtmosphericPressure = 120.0,
    //                Humidity = 30,
    //                Precipitation = 0,
    //                TempMax = 25,
    //                TempMin = 15,
    //                WindSpeed = 4,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,16),
    //                AtmosphericPressure = 120.0,
    //                Humidity = 30,
    //                Precipitation = 0,
    //                TempMax = 25,
    //                TempMin = 15,
    //                WindSpeed = 4,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,17),
    //                AtmosphericPressure = 120.0,
    //                Humidity = 30,
    //                Precipitation = 0,
    //                TempMax = 25,
    //                TempMin = 15,
    //                WindSpeed = 4,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,18),
    //                AtmosphericPressure = 120.0,
    //                Humidity = 30,
    //                Precipitation = 0,
    //                TempMax = 25,
    //                TempMin = 15,
    //                WindSpeed = 4,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,19),
    //                AtmosphericPressure = 120.0,
    //                Humidity = 30,
    //                Precipitation = 0,
    //                TempMax = 25,
    //                TempMin = 15,
    //                WindSpeed = 4,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,20),
    //                AtmosphericPressure = 120.0,
    //                Humidity = 30,
    //                Precipitation = 0,
    //                TempMax = 25,
    //                TempMin = 15,
    //                WindSpeed = 4,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,21),
    //                AtmosphericPressure = 120.0,
    //                Humidity = 30,
    //                Precipitation = 0,
    //                TempMax = 25,
    //                TempMin = 15,
    //                WindSpeed = 4,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,22),
    //                AtmosphericPressure = 120.0,
    //                Humidity = 30,
    //                Precipitation = 0,
    //                TempMax = 25,
    //                TempMin = 15,
    //                WindSpeed = 4,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,23),
    //                AtmosphericPressure = 120.0,
    //                Humidity = 30,
    //                Precipitation = 0,
    //                TempMax = 25,
    //                TempMin = 15,
    //                WindSpeed = 4,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,24),
    //                AtmosphericPressure = 120.0,
    //                Humidity = 30,
    //                Precipitation = 0,
    //                TempMax = 25,
    //                TempMin = 15,
    //                WindSpeed = 4,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,25),
    //                AtmosphericPressure = 120.0,
    //                Humidity = 30,
    //                Precipitation = 0,
    //                TempMax = 25,
    //                TempMin = 15,
    //                WindSpeed = 4,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,26),
    //                AtmosphericPressure = 120.0,
    //                Humidity = 30,
    //                Precipitation = 0,
    //                TempMax = 25,
    //                TempMin = 15,
    //                WindSpeed = 4,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,27),
    //                AtmosphericPressure = 120.0,
    //                Humidity = 30,
    //                Precipitation = 0,
    //                TempMax = 25,
    //                TempMin = 15,
    //                WindSpeed = 4,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,28),
    //                AtmosphericPressure = 120.0,
    //                Humidity = 30,
    //                Precipitation = 0,
    //                TempMax = 25,
    //                TempMin = 15,
    //                WindSpeed = 4,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,29),
    //                AtmosphericPressure = 120.0,
    //                Humidity = 30,
    //                Precipitation = 0,
    //                TempMax = 25,
    //                TempMin = 15,
    //                WindSpeed = 4,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,30),
    //                AtmosphericPressure = 120.0,
    //                Humidity = 30,
    //                Precipitation = 0,
    //                TempMax = 25,
    //                TempMin = 15,
    //                WindSpeed = 4,
    //                DaylightHours = 9
    //            },
    //            new WeatherRecord()
    //            {
    //                Date = new DateTime(2016,03,31),
    //                AtmosphericPressure = 120.0,
    //                Humidity = 30,
    //                Precipitation = 0,
    //                TempMax = 25,
    //                TempMin = 15,
    //                WindSpeed = 4,
    //                DaylightHours = 9
    //            }
    //        };

    //        #endregion

    //        List<SoilType> soilTypes = new List<SoilType>() {

    //            new SoilType()
    //            {
    //                Name = "Clay sand",
    //                FieldWaterCapacity = 0.15,
    //                WiltWaterCapacity = 0.065,
    //                ReadilyEvaporableWater = 6,
    //                TotalEvaporableWater = 11.5,
    //                Description = "Average clay sand"
    //            }
    //        };

    //        List<IrrigationType> irrigationTypes = new List<IrrigationType>() {
    //            new IrrigationType()
    //            {
    //                Name = "drip irrigation",
    //                WettedSoilAreaFraction = 0.33,
    //                Description = "Average drip irrigation"
    //            }
    //        };

    //        List<Culture> cultures = new List<Culture>() {
    //            new Culture()
    //            {
    //                Name = "oat",
    //                KcIni = 0.15,
    //                KcMid = 1.10,
    //                KcEnd = 0.15,
    //                MaxHeight = 1.0,
    //                MaxRootDepth = 1.25,
    //                WaterExtraction = 0.55,
    //                YieldCoefficient = 1.05,
    //                Description = "Average oat"
    //            }
    //        };
    //        List<Field> fields = new List<Field>() {
    //            new Field()
    //            {
    //                Name = "wateredfield",
    //                Area = 100,
    //                Description = "test field 1",
    //                IrrigationType = irrigationTypes[0],
    //                SoilType = soilTypes[0],
    //                Latitude = 45,
    //                Altitude = 300
    //            }
    //        };

    //        List<Crop> crops = new List<Crop>() {
    //            new Crop()
    //            {
    //                Name = "first crop",
    //                Culture = cultures[0],
    //                Field = fields[0],
    //                LengthIni = 20,
    //                LengthDev = 25,
    //                LengthMid = 60,
    //                LengthLate = 30,
    //                DateSeeded = new DateTime(2016,02,15),
    //                Description="test crop",
    //                WeatherRecords = weatherRecords,
    //                CropStateRecords = new List<CropStateRecord>()
    //                {
    //                    new CropStateRecord()
    //                    {
    //                        Date = new DateTime(2016,02,29),
    //                        EvaporationDepth = 0, 
    //                        PlannedIrrigation = 0,
    //                        WaterDepletion = 0,
    //                        YieldReduction = 0
    //                    }
    //                }
    //            }
    //        };


    //        List<CropStateRecord> cropStates1 = WaterBalanceCalculator.PlanIrrigations(crops[0], crops[0].WeatherRecords).ToList();
    //        List<CropStateRecord> irrigations1 = cropStates1.Where(x => x.PlannedIrrigation != 0).ToList();
    //        double yieldLoss1 = cropStates1.Average(x => x.YieldReduction);
    //        double water1 = irrigations1.Sum(x => x.PlannedIrrigation);

    //        List<CropStateRecord> cropStates2 = WaterBalanceCalculator.PlanIrrigations(crops[0], crops[0].WeatherRecords, 55).ToList();
    //        List<CropStateRecord> irrigations2 = cropStates2.Where(x => x.PlannedIrrigation != 0).ToList();
    //        double yieldLoss2 = cropStates2.Average(x => x.YieldReduction);
    //        double water2 = irrigations2.Sum(x => x.PlannedIrrigation);


    //        Assert.AreEqual(55, water2, 0.1);
    //    }

    //    [TestMethod]
    //    public void GettingWeatherRecords()
    //    {
    //        #region Initializing weather data

    //        List<WeatherRecord> weatherRecords = new List<WeatherRecord>()
    //        {
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 1),
    //            AtmosphericPressure = 1013.94749450684,
    //            Humidity = 0.627499997615814,
    //            Precipitation = 0,
    //            TempMax = 24.3324999809265,
    //            TempMin = 10.4824999570847,
    //            WindSpeed = 1.010000012815,
    //            DaylightHours = 14.5918055555556
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 2),
    //            AtmosphericPressure = 1013.38751220703,
    //            Humidity = 0.697499997913837,
    //            Precipitation = 0,
    //            TempMax = 20.8324999809265,
    //            TempMin = 11.2874999046326,
    //            WindSpeed = 1.85500002838671,
    //            DaylightHours = 14.6445833333333
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 3),
    //            AtmosphericPressure = 1012.74000549316,
    //            Humidity = 0.64750000834465,
    //            Precipitation = 0,
    //            TempMax = 23.1775000095367,
    //            TempMin = 9.4449999332428,
    //            WindSpeed = 1.6000000089407,
    //            DaylightHours = 14.6970833333333
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 4),
    //            AtmosphericPressure = 1011.41749572754,
    //            Humidity = 0.587499998509884,
    //            Precipitation = 0,
    //            TempMax = 23.647500038147,
    //            TempMin = 10.740000128746,
    //            WindSpeed = 1.70749999582767,
    //            DaylightHours = 14.7488194444444
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 5),
    //            AtmosphericPressure = 1013.03997802734,
    //            Humidity = 0.560000002384186,
    //            Precipitation = 0,
    //            TempMax = 21.2099990844727,
    //            TempMin = 7.8899998664856,
    //            WindSpeed = 4.55999994277954,
    //            DaylightHours = 14.8183333333333
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 6),
    //            AtmosphericPressure = 1012.51000976563,
    //            Humidity = 0.720000028610229,
    //            Precipitation = 0,
    //            TempMax = 18.9799995422363,
    //            TempMin = 7.65999984741211,
    //            WindSpeed = 4.34999990463257,
    //            DaylightHours = 14.8688888888889
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 7),
    //            AtmosphericPressure = 1014.53002929688,
    //            Humidity = 0.839999973773956,
    //            Precipitation = 0,
    //            TempMax = 18.3099994659424,
    //            TempMin = 9.13000011444092,
    //            WindSpeed = 4.30999994277954,
    //            DaylightHours = 14.9188888888889
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 8),
    //            AtmosphericPressure = 1019.33001708984,
    //            Humidity = 0.759999990463257,
    //            Precipitation = 0,
    //            TempMax = 21.1200008392334,
    //            TempMin = 9.05000019073486,
    //            WindSpeed = 3.66000008583069,
    //            DaylightHours = 14.9686111111111
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 9),
    //            AtmosphericPressure = 1023.28997802734,
    //            Humidity = 0.670000016689301,
    //            Precipitation = 0,
    //            TempMax = 22.1700000762939,
    //            TempMin = 10.1800003051758,
    //            WindSpeed = 4.86999988555908,
    //            DaylightHours = 15.0175
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 10),
    //            AtmosphericPressure = 1023.64001464844,
    //            Humidity = 0.579999983310699,
    //            Precipitation = 0,
    //            TempMax = 21.6299991607666,
    //            TempMin = 9.84000015258789,
    //            WindSpeed = 3.33999991416931,
    //            DaylightHours = 15.0658333333333
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 11),
    //            AtmosphericPressure = 1015.90748596191,
    //            Humidity = 0.53999999910593,
    //            Precipitation = 0,
    //            TempMax = 26.2000002861023,
    //            TempMin = 13.1600000858307,
    //            WindSpeed = 2.30750000476837,
    //            DaylightHours = 15.0969444444444
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 12),
    //            AtmosphericPressure = 1014.57000732422,
    //            Humidity = 0.552500002086163,
    //            Precipitation = 0,
    //            TempMax = 26.5424995422363,
    //            TempMin = 12.0875000953674,
    //            WindSpeed = 2.21249997615814,
    //            DaylightHours = 15.1440277777778
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 13),
    //            AtmosphericPressure = 1011.26000976563,
    //            Humidity = 0.574999995529652,
    //            Precipitation = 0,
    //            TempMax = 25.3125,
    //            TempMin = 12.7849998474121,
    //            WindSpeed = 1.70999997854233,
    //            DaylightHours = 15.1907638888889
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 14),
    //            AtmosphericPressure = 1009.74749755859,
    //            Humidity = 0.662499994039536,
    //            Precipitation = 0,
    //            TempMax = 24.4650001525879,
    //            TempMin = 13.2124998569489,
    //            WindSpeed = 2.48499998450279,
    //            DaylightHours = 15.2365972222222
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 15),
    //            AtmosphericPressure = 1009.15501403809,
    //            Humidity = 0.64750000834465,
    //            Precipitation = 0,
    //            TempMax = 26.2875003814697,
    //            TempMin = 13.2149996757507,
    //            WindSpeed = 2.81999997794628,
    //            DaylightHours = 15.2817361111111
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 16),
    //            AtmosphericPressure = 1011.41499328613,
    //            Humidity = 0.677499994635582,
    //            Precipitation = 0,
    //            TempMax = 24.1399998664856,
    //            TempMin = 13.397500038147,
    //            WindSpeed = 2.76249992847443,
    //            DaylightHours = 15.32625
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 17),
    //            AtmosphericPressure = 1011.54249572754,
    //            Humidity = 0.642499998211861,
    //            Precipitation = 0,
    //            TempMax = 25.6699995994568,
    //            TempMin = 13.129999756813,
    //            WindSpeed = 2.0450000166893,
    //            DaylightHours = 15.3696527777778
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 18),
    //            AtmosphericPressure = 1013.03248596191,
    //            Humidity = 0.652499988675117,
    //            Precipitation = 0,
    //            TempMax = 25.4800000190735,
    //            TempMin = 13.4500000476837,
    //            WindSpeed = 2.09750007092953,
    //            DaylightHours = 15.4125
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 19),
    //            AtmosphericPressure = 1015.61250305176,
    //            Humidity = 0.60249999165535,
    //            Precipitation = 0,
    //            TempMax = 26.3249998092651,
    //            TempMin = 13.7549996376038,
    //            WindSpeed = 2.22999998927116,
    //            DaylightHours = 15.454375
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 20),
    //            AtmosphericPressure = 1017.84498596191,
    //            Humidity = 0.55749998241663,
    //            Precipitation = 0,
    //            TempMax = 28.0675001144409,
    //            TempMin = 14.6275000572205,
    //            WindSpeed = 2.52000005543232,
    //            DaylightHours = 15.4955555555556
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 21),
    //            AtmosphericPressure = 1017.30000305176,
    //            Humidity = 0.529999986290932,
    //            Precipitation = 0,
    //            TempMax = 27.0650000572205,
    //            TempMin = 13.7225003242493,
    //            WindSpeed = 2.96499997377396,
    //            DaylightHours = 15.5356944444444
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 22),
    //            AtmosphericPressure = 1015.50749206543,
    //            Humidity = 0.530000001192093,
    //            Precipitation = 0,
    //            TempMax = 28.2600002288818,
    //            TempMin = 14.0450000762939,
    //            WindSpeed = 2.68249994516373,
    //            DaylightHours = 15.575
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 23),
    //            AtmosphericPressure = 1012.64999389648,
    //            Humidity = 0.517500005662441,
    //            Precipitation = 0,
    //            TempMax = 28.3499999046326,
    //            TempMin = 15.8150000572205,
    //            WindSpeed = 3.07749998569489,
    //            DaylightHours = 15.6133333333333
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 24),
    //            AtmosphericPressure = 1010.74249267578,
    //            Humidity = 0.552500002086163,
    //            Precipitation = 0,
    //            TempMax = 27.1399998664856,
    //            TempMin = 15.1425001621246,
    //            WindSpeed = 4.00500005483627,
    //            DaylightHours = 15.6506944444444
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 25),
    //            AtmosphericPressure = 1010.68998718262,
    //            Humidity = 0.657499998807907,
    //            Precipitation = 0,
    //            TempMax = 23.1799998283386,
    //            TempMin = 13.6150000095367,
    //            WindSpeed = 3.74499994516373,
    //            DaylightHours = 15.6870138888889
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 26),
    //            AtmosphericPressure = 1010.99749755859,
    //            Humidity = 0.632499992847443,
    //            Precipitation = 0,
    //            TempMax = 24.7499997615814,
    //            TempMin = 14.327499628067,
    //            WindSpeed = 2.58250001072884,
    //            DaylightHours = 15.7223611111111
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 27),
    //            AtmosphericPressure = 1010.07250976563,
    //            Humidity = 0.637499988079071,
    //            Precipitation = 0,
    //            TempMax = 25.4300003051758,
    //            TempMin = 13.8250000476837,
    //            WindSpeed = 1.44499999284744,
    //            DaylightHours = 15.7567361111111
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 28),
    //            AtmosphericPressure = 1008.33250427246,
    //            Humidity = 0.652500003576279,
    //            Precipitation = 0,
    //            TempMax = 24.8674998283386,
    //            TempMin = 13.3774998188019,
    //            WindSpeed = 1.20749999582767,
    //            DaylightHours = 15.7898611111111
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 29),
    //            AtmosphericPressure = 1008.6875,
    //            Humidity = 0.715000003576279,
    //            Precipitation = 0,
    //            TempMax = 23.7400002479553,
    //            TempMin = 13.4549999237061,
    //            WindSpeed = 1.56999999284744,
    //            DaylightHours = 15.821875
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 30),
    //            AtmosphericPressure = 1010.67501831055,
    //            Humidity = 0.714999988675117,
    //            Precipitation = 0,
    //            TempMax = 24.2374997138977,
    //            TempMin = 14.1624999046326,
    //            WindSpeed = 1.92750003188848,
    //            DaylightHours = 15.8529166666667
    //            },
    //            new WeatherRecord()
    //            {
    //            Date = new DateTime(2016, 5, 31),
    //            AtmosphericPressure = 1010.87750244141,
    //            Humidity = 0.680000007152557,
    //            Precipitation = 0,
    //            TempMax = 22.6675000190735,
    //            TempMin = 14.2725002765656,
    //            WindSpeed = 2.43250003457069,
    //            DaylightHours = 15.8827777777778
    //            }
    //        };
    //        #endregion

    //        List<SoilType> soilTypes = new List<SoilType>() {

    //            new SoilType()
    //            {
    //                Name = "Clay sand",
    //                FieldWaterCapacity = 0.15,
    //                WiltWaterCapacity = 0.065,
    //                ReadilyEvaporableWater = 6,
    //                TotalEvaporableWater = 11.5,
    //                Description = "Average clay sand"
    //            }
    //        };

    //        List<IrrigationType> irrigationTypes = new List<IrrigationType>() {
    //            new IrrigationType()
    //            {
    //                Name = "drip irrigation",
    //                WettedSoilAreaFraction = 0.33,
    //                Description = "Average drip irrigation"
    //            }
    //        };

    //        List<Culture> cultures = new List<Culture>() {
    //            new Culture()
    //            {
    //                Name = "oat",
    //                KcIni = 0.15,
    //                KcMid = 1.10,
    //                KcEnd = 0.15,
    //                MaxHeight = 1.0,
    //                MaxRootDepth = 1.25,
    //                WaterExtraction = 0.55,
    //                YieldCoefficient = 1.05,
    //                Description = "Average oat"
    //            }
    //        };
    //        List<Field> fields = new List<Field>() {
    //            new Field()
    //            {
    //                Name = "wateredfield",
    //                Area = 100,
    //                Description = "test field 1",
    //                IrrigationType = irrigationTypes[0],
    //                SoilType = soilTypes[0],
    //                Latitude = 49,
    //                Longitude = 36,
    //                Altitude = 100
    //            }
    //        };

    //        List<Crop> crops = new List<Crop>() {
    //            new Crop()
    //            {
    //                Name = "first crop",
    //                Culture = cultures[0],
    //                Field = fields[0],
    //                LengthIni = 20,
    //                LengthDev = 25,
    //                LengthMid = 60,
    //                LengthLate = 30,
    //                DateSeeded = new DateTime(2016,05,01),
    //                Description="test crop",
    //                CropStateRecords = new List<CropStateRecord>()
    //                {
    //                    new CropStateRecord()
    //                    {
    //                        Date = new DateTime(2016,05,01),
    //                        EvaporationDepth = 0, 
    //                        PlannedIrrigation = 0,
    //                        WaterDepletion = 0,
    //                        YieldReduction = 0
    //                    }
    //                }
    //            }
    //        };
    //        DateTime dateBegin = new DateTime(2016, 05, 01);
    //        DateTime dateEnd = new DateTime(2016, 05, 31);

    //        crops[0].WeatherRecords = weatherRecords;

    //        List<WeatherRecord> weatherRecordsNew = WaterBalanceCalculator.GetWeatherRecords(dateBegin, dateEnd, crops[0]).ToList();

    //        //StringBuilder sb = new StringBuilder();


    //        //foreach (WeatherRecord w in weatherRecords)
    //        //{
    //        //    sb.AppendLine("new WeatherRecord()");
    //        //    sb.AppendLine("{");
    //        //    sb.AppendLine($"Date = new DateTime({w.Date.Year}, {w.Date.Month}, {w.Date.Day}),");
    //        //    sb.AppendLine($"AtmosphericPressure = {w.AtmosphericPressure},");
    //        //    sb.AppendLine($"Humidity = {w.Humidity},");
    //        //    sb.AppendLine($"Precipitation = {w.Precipitation},");
    //        //    sb.AppendLine($"TempMax = {w.TempMax},");
    //        //    sb.AppendLine($"TempMin = {w.TempMin},");
    //        //    sb.AppendLine($"WindSpeed = {w.WindSpeed},");
    //        //    sb.AppendLine($"DaylightHours = {w.DaylightHours}");
    //        //    sb.AppendLine("},");
    //        //}

    //        //StreamWriter file = new StreamWriter($"weatherRecords {dateBegin.ToString("dd-MM-yyyy")} - {dateEnd.ToString("dd-MM-yyyy")}.txt");
    //        //file.WriteLine(sb.ToString());
    //        //file.Close();
    //        //file.Dispose();


    //        Assert.AreEqual(0, weatherRecordsNew.Count);
    //    }
    //}

