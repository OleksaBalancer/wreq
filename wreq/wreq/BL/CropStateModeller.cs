using System;
using System.Collections.Generic;
using System.Linq;
using wreq.BL.Abstract;
using wreq.Models.Entities;

namespace wreq.BL
{
    public class CropStateModeller : ICropStateModeller
    {
        IParametersCalculator _calculator;
        IWeatherManager _weatherManager;

        public CropStateModeller(IParametersCalculator calculator, IWeatherManager weatherManager)
        {
            _weatherManager = weatherManager;
            _calculator = calculator;
        }

        CropStateRecord DetermineCropState(Crop crop, CropStateRecord prevCropState, WeatherRecord weatherRecord, bool adjusting = false, double irrigationPlanned = 0, double waterConsumptionCoefficient = 1)
        {

            //ET0
            int dayOfGrowth = (int)Math.Ceiling((weatherRecord.Date - crop.DateSeeded).TotalDays);

            double sunEarthDistance = _calculator.SunEarthDistance(weatherRecord.Date.DayOfYear);

            double sunTilt = _calculator.SunTilt(weatherRecord.Date.DayOfYear);

            double sunsetAngle = _calculator.SunsetAngle(crop.Field.Latitude * (Math.PI / 180), sunTilt);

            double extraterrestrialRadiation = _calculator.ExtraterrestrialRadiation(sunEarthDistance, sunsetAngle, crop.Field.Latitude * (Math.PI / 180), sunTilt);

            double maxDaylightHours = _calculator.MaxDaylightHours(sunsetAngle);

            double shortWaveRadiation = _calculator.ShortWaveRadiation(weatherRecord.DaylightHours, maxDaylightHours, extraterrestrialRadiation, _calculator.CloudyRadiationRatio, _calculator.ClearRadiationRatio);

            double netShortWaveRadiation = _calculator.NetShortWaveRadiation(_calculator.ReferenceAlbedo, shortWaveRadiation);

            double actualVapourPressure = _calculator.ActualVapourPressure(weatherRecord.TempMin, weatherRecord.TempMax, weatherRecord.Humidity);

            double clearSkyRadiation = _calculator.ClearSkyRadiation(crop.Field.Altitude, extraterrestrialRadiation);

            double netLongWaveRadiation = _calculator.NetLongWaveRadiation(weatherRecord.TempMax, weatherRecord.TempMin, actualVapourPressure, shortWaveRadiation, clearSkyRadiation);

            double netRadiation = _calculator.NetRadiation(netShortWaveRadiation, netLongWaveRadiation);

            double saturationVapourPressure = _calculator.SaturationVapourPressure(weatherRecord.TempMax, weatherRecord.TempMin);

            double slopeVapourPressureCurve = _calculator.SlopeVapourPressureCurve(weatherRecord.TempMean);

            double referenceEvapotranspiration = _calculator.ReferenceEvapotranspiration(netRadiation, weatherRecord.TempMean, weatherRecord.WindSpeed, saturationVapourPressure, actualVapourPressure, slopeVapourPressureCurve, weatherRecord.AtmosphericPressure);
            
            //ETc
            double libBaseCropCoefficient = _calculator.LibBaseCropCoefficient(dayOfGrowth, crop.LengthIni, crop.LengthDev, crop.LengthMid, crop.LengthLate, crop.Culture.KcIni, crop.Culture.KcMid, crop.Culture.KcEnd);

            double currentHeight = _calculator.CurrentHeight(dayOfGrowth, crop.Culture.MaxHeight, crop.LengthIni, crop.LengthDev);

            double adjustedBaseCropCoefficient = _calculator.AdjustedBaseCropCoefficient(libBaseCropCoefficient, weatherRecord.WindSpeed, weatherRecord.Humidity, currentHeight);

            double maxCropCoefficient = _calculator.MaxCropCoefficient(weatherRecord.WindSpeed, weatherRecord.Humidity, currentHeight, adjustedBaseCropCoefficient);

            double coveredSoilFraction = _calculator.CoveredSoilFraction(adjustedBaseCropCoefficient, _calculator.AdjustedBaseCropCoefficient(crop.Culture.KcIni, weatherRecord.WindSpeed, weatherRecord.Humidity, currentHeight), maxCropCoefficient, currentHeight);

            double exposedWettedSoilFraction = _calculator.ExposedWettedSoilFraction(crop.Field.IrrigationType.WettedSoilAreaFraction, coveredSoilFraction);

            double evaporationReductionCoefficient = _calculator.EvaporationReductionCoefficient(prevCropState.EvaporationDepth, crop.Field.SoilType.TotalEvaporableWater, crop.Field.SoilType.ReadilyEvaporableWater);

            double evaporationCoefficient = _calculator.EvaporationCoefficient(adjustedBaseCropCoefficient, maxCropCoefficient, evaporationReductionCoefficient, exposedWettedSoilFraction);

            double currentRootDepth = _calculator.CurrentRootDepth(dayOfGrowth, crop.Culture.MaxRootDepth, crop.LengthIni, crop.LengthDev, crop.LengthMid);

            double totalAvailableWater = _calculator.TotalAvailableWater(crop.Field.SoilType.FieldWaterCapacity, crop.Field.SoilType.WiltWaterCapacity, currentRootDepth);

            double readilyAvailableWater = _calculator.ReadilyAvailableWater(totalAvailableWater, crop.Culture.WaterExtraction);

            double irrigation = 0;
            double waterStressCoefficient = 1;
            if (prevCropState.WaterDepletion >= readilyAvailableWater)
            {
                if (!adjusting)
                {
                    irrigation = prevCropState.WaterDepletion; //for current day
                }
                else
                {
                    irrigation = irrigationPlanned * waterConsumptionCoefficient;
                }

            }

            double irrigationActual = crop.Irrigations.Where(x => x.Date.Date == weatherRecord.Date.Date).Select(x => x.Depth).FirstOrDefault();

            if (irrigationActual != 0)
            {
                irrigation = irrigationActual;
            }

            if (prevCropState.WaterDepletion - irrigation >= readilyAvailableWater)
            {
                waterStressCoefficient = _calculator.WaterStressCoefficient(totalAvailableWater, prevCropState.WaterDepletion - irrigation, readilyAvailableWater, crop.Culture.WaterExtraction);
            }

            //for the end of current day
            double evaporationDepth = _calculator.EvaporationDepth(prevCropState.EvaporationDepth, weatherRecord.Precipitation, irrigation, crop.Field.IrrigationType.WettedSoilAreaFraction, evaporationCoefficient, referenceEvapotranspiration, exposedWettedSoilFraction);

            double cropEvapotranspiration = _calculator.CropEvapotranspiration(referenceEvapotranspiration, adjustedBaseCropCoefficient, evaporationCoefficient);
            double cropEvapotranspirationAdjusted = _calculator.CropEvapotranspirationAdjusted(cropEvapotranspiration, waterStressCoefficient);

            double waterDepletion = _calculator.Depletion(prevCropState.WaterDepletion, weatherRecord.Precipitation, irrigation, cropEvapotranspirationAdjusted);

            return new CropStateRecord()
            {
                Crop = crop,
                Date = weatherRecord.Date,
                PlannedIrrigation = irrigation,
                WaterDepletion = waterDepletion,
                EvaporationDepth = evaporationDepth,
                YieldReduction = _calculator.YieldReduction(cropEvapotranspiration, cropEvapotranspirationAdjusted, crop.Culture.YieldCoefficient)
            };

        }

        double WaterUsageCoefficient(double waterUsageLimit, double volumePlanned)
        {
            return waterUsageLimit / volumePlanned;
        }

        public IEnumerable<CropStateRecord> ModelCropState(Crop crop, DateTime dateBegin, DateTime dateEnd)
        {
            List<CropStateRecord> cropStateRecords = new List<CropStateRecord>();

            CropStateRecord currCSR = new CropStateRecord()
            {
                Date = crop.DateSeeded
            };
            cropStateRecords.Add(currCSR);

            DateTime date = crop.DateSeeded.AddDays(1);

            while (date.Date <= dateEnd.Date)
            {
                WeatherRecord weatherRecord = GetWeatherRecord(date, crop);//get weather record
                currCSR = DetermineCropState(crop, currCSR, weatherRecord);//calc curr csr

                cropStateRecords.Add(currCSR); //add curr csr to list of results

                date = date.AddDays(1); // go to next day 
            }

            List<WaterLimit> waterLimits = crop.WaterLimits.OrderBy(x => x.DateBegin).ToList();

            //water limitation recalc
            foreach (WaterLimit wl in waterLimits)
            {
                if (!(wl.DateBegin > dateEnd && wl.DateEnd > dateEnd) && !(wl.DateBegin < dateBegin && wl.DateEnd < dateBegin))//check for each water limitation if it is between datebegin and dateend
                {
                    DateTime intervalBegin = new DateTime();
                    DateTime intervalEnd = new DateTime();
                    //if it is, specify interval
                    if (wl.DateBegin >= dateBegin && wl.DateEnd <= dateEnd)
                    {
                        intervalBegin = wl.DateBegin;
                        intervalEnd = wl.DateEnd;
                    }
                    if (wl.DateBegin <= dateBegin && wl.DateEnd >= dateEnd)
                    {
                        intervalBegin = dateBegin;
                        intervalEnd = dateEnd;
                    }
                    if (wl.DateBegin <= dateBegin && wl.DateEnd <= dateEnd)
                    {
                        intervalBegin = dateBegin;
                        intervalEnd = wl.DateEnd;
                    }
                    if (wl.DateBegin >= dateBegin && wl.DateEnd >= dateEnd)
                    {
                        intervalBegin = wl.DateBegin;
                        intervalEnd = dateEnd;
                    }

                    //remodel 
                    date = intervalBegin;
                    if (intervalBegin.Date == crop.DateSeeded.Date)
                    {
                        date = date.AddDays(1);
                    }

                    double totalPlannedVolume = ParametersCalculator.DepthToVolume(cropStateRecords.Where(x => x.Date >= intervalBegin && x.Date <= intervalEnd).Sum(x => x.PlannedIrrigation), crop.Field.Area, crop.Field.IrrigationType.WettedSoilAreaFraction);

                    double waterLimitVolume = wl.Volume * (intervalEnd - intervalBegin).TotalDays / (wl.DateEnd - wl.DateBegin).TotalDays;

                    double waterUsageCoefficient = WaterUsageCoefficient(waterLimitVolume, totalPlannedVolume);
                    List<CropStateRecord> newCSRs = cropStateRecords.Where(x => x.Date < intervalBegin).ToList();//truncate csrs

                    currCSR = cropStateRecords.Where(x => x.Date.Date == date.AddDays(-1).Date).First(); //get previous csr                    

                    while (date.Date <= dateEnd)
                    {
                        //currCSR = cropStateRecords.FirstOrDefault(x => x.Date.Date == date.Date);//get csr to change
                        double plannedIrrigation = cropStateRecords.FirstOrDefault(x => x.Date.Date == date.Date).PlannedIrrigation;
                        WeatherRecord weatherRecord = GetWeatherRecord(date, crop);//get weather record

                        if (date.Date <= intervalEnd.Date)
                        {
                            currCSR = DetermineCropState(crop, currCSR, weatherRecord, true, plannedIrrigation, waterUsageCoefficient);//calc curr csr
                        }
                        else
                        {
                            currCSR = DetermineCropState(crop, currCSR, weatherRecord);//calc curr csr
                        }
                        newCSRs.Add(currCSR); //add curr csr to list of results
                        date = date.AddDays(1); // go to next day 
                    }
                    cropStateRecords = newCSRs;
                }
            }

            return cropStateRecords.Where(x=>x.Date>dateBegin);
        }

        WeatherRecord GetWeatherRecord(DateTime date, Crop crop)
        {
            //check if there is wr for the date (not null, not forecasted)
            WeatherRecord currWR = crop.Field.WeatherRecords.FirstOrDefault(x => x.Date.Date == date.Date);
            if (currWR == null || currWR.IsForecasted) //if not exists create new
            {
                if (currWR != null)
                {
                    crop.Field.WeatherRecords.Remove(currWR); //remove old
                }
                if (date >= crop.DateSeeded && date < DateTime.Now.AddDays(6)) //if date is between date of sowing and today+6, calc based on one request
                {
                    currWR = _weatherManager.GetWeatherRecord(date, crop.Field.Latitude, crop.Field.Longitude);//if forecasted - tell so
                    if (date > DateTime.Now)
                    {
                        currWR.IsForecasted = true;
                    }
                }
                else //if date is in future past today+6, calc based on average historical data
                {
                    currWR = _weatherManager.GetAverageHistoricalWeatherRecord(date, crop.Field.Latitude, crop.Field.Longitude, 4);
                }

                crop.Field.WeatherRecords.Add(currWR);//add curr wr to crop
            }

            return currWR;
        }
    }
}