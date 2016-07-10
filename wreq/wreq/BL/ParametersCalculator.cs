using System;
using wreq.BL.Abstract;

namespace wreq.BL
{
    public class ParametersCalculator : IParametersCalculator
    {
        //constants
        public double StefanBoltzmanConstant { get { return 4.903e-9; } }

        public double SunConstant { get { return 0.082; } }

        public double PsychrometricConstant { get { return 0.665e-3; } }

        public double CloudyRadiationRatio { get { return 0.25; } }

        public double ClearRadiationRatio { get { return 0.5; } }

        public double ReferenceAlbedo { get { return 0.23; } }
        
        public double Depletion(double depletionPrev, double precipitation, double irrigation, double evapotranspiration)
        {
            double waterDepletion = depletionPrev - precipitation - irrigation + evapotranspiration;
            return waterDepletion < 0 ? 0 : waterDepletion;
        }

        public double TotalAvailableWater(double fieldWaterCapacity, double wiltWaterCapacity, double rootDepth)
        {
            return 1000.0 * (fieldWaterCapacity - wiltWaterCapacity) * rootDepth;
        }

        public double ReadilyAvailableWater(double totalAvailableWater, double waterExtractionRate)
        {
            return totalAvailableWater * waterExtractionRate;
        }

        public double CropEvapotranspiration(double referenceEvapotranspiration, double baseCultureCoefficient, double evaporationCoefficient)
        {
            return referenceEvapotranspiration * (baseCultureCoefficient + evaporationCoefficient);
        }

        public double ReferenceEvapotranspiration(double netRadiation, double meanDailyTemperature, double windSpeed, double saturationVapourPressure, double actualVapourPressure, double slopeVapourPressureCurve, double atmosphericPressure)
        {
            return (0.408 * slopeVapourPressureCurve * netRadiation + (PsychrometricConstant * atmosphericPressure * 900 / (meanDailyTemperature + 273)) * windSpeed * (saturationVapourPressure - actualVapourPressure)) /
                (slopeVapourPressureCurve + PsychrometricConstant * atmosphericPressure * (1 + 0.34 * windSpeed));
        }

        public double NetRadiation(double netShortWaveRadiation, double netLongWaveRadiation)
        {
            return netShortWaveRadiation - netLongWaveRadiation;
        }

        public double NetShortWaveRadiation(double albedo, double shortWaveRadiation)
        {
            return (1 - albedo) * shortWaveRadiation;
        }

        public double ShortWaveRadiation(double actualDaylightHours, double maxDaylightHours, double extraterrestrialRadiation, double cloudyRadiationRatio, double clearRadiationRatio)
        {
            return (cloudyRadiationRatio + clearRadiationRatio * actualDaylightHours / maxDaylightHours) * extraterrestrialRadiation;
        }

        public double MaxDaylightHours(double sunsetAngle)
        {
            return sunsetAngle * 24 / Math.PI;
        }

        public double ExtraterrestrialRadiation(double sunEarthDistance, double sunsetAngle, double lattitude, double sunTilt)
        {
            return 24 * 60 / Math.PI * SunConstant * sunEarthDistance * (sunsetAngle * Math.Sin(lattitude) * Math.Sin(sunTilt) + Math.Cos(lattitude) * Math.Cos(sunTilt) * Math.Sin(sunsetAngle));
        }

        public double SunEarthDistance(int dayNumber)
        {
            return 1 + 0.033 * Math.Cos((2 * Math.PI / 365) * dayNumber);
        }

        public double SunTilt(int dayNumber)
        {
            return 0.409 * Math.Sin((2 * Math.PI / 365) * dayNumber - 1.39);
        }

        public double SunsetAngle(double lattitude, double sunTilt)
        {
            return Math.Acos(-Math.Tan(lattitude) * Math.Tan(sunTilt));
        }

        public double NetLongWaveRadiation(double tempMax, double tempMin, double actualVapourPressure, double shortWaveRadiation, double clearSkyRadiation)
        {
            return StefanBoltzmanConstant * ((Math.Pow(tempMax + 273.15, 4) + Math.Pow(tempMin + 273.15, 4)) / 2) * (0.34 - 0.14 * Math.Sqrt(actualVapourPressure)) * (1.35 * shortWaveRadiation / clearSkyRadiation - 0.35);
        }

        public double ClearSkyRadiation(double altitude, double extraterrestrialRadiation)
        {
            return (0.75 + 2e-5 * altitude) * extraterrestrialRadiation;
        }

        public double SaturationVapourPressure(double tempMax, double tempMin)
        {
            return (SaturationVapourPressure(tempMax) + SaturationVapourPressure(tempMin)) / 2;
        }

        public double SaturationVapourPressure(double temperature)
        {
            return 0.6108 * Math.Exp(17.27 * temperature / (temperature + 237.3));
        }

        public double ActualVapourPressure(double tempMin, double tempMax, double humidity)
        {
            return ((SaturationVapourPressure(tempMin) + SaturationVapourPressure(tempMax)) * humidity / 100) / 2;
        }

        public double SlopeVapourPressureCurve(double temperature)
        {
            return 4098 * SaturationVapourPressure(temperature) / Math.Pow(temperature + 237.3, 2);
        }

        public double LibBaseCropCoefficient(int day, int lengthIni, int lengthDev, int lengthMid, int lengthEnd, double kcIni, double kcMid, double kcEnd)
        {
            if (day <= lengthIni)
            {
                return kcIni;
            }
            if (day <= lengthIni + lengthDev)
            {
                return DailyBaseCropCoefficient(day, lengthDev, lengthIni, kcIni, kcMid);
            }
            if (day <= lengthIni + lengthDev + lengthMid)
            {
                return kcMid;
            }
            if (day <= lengthIni + lengthDev + lengthMid + lengthEnd)
            {
                return DailyBaseCropCoefficient(day, lengthEnd, lengthIni + lengthDev + lengthMid, kcMid, kcEnd);
            }
            throw new ArgumentException("Day number is bigger than plant growth period");
        }

        public double AdjustedBaseCropCoefficient(double libCropCoefficient, double windSpeed, double humidity, double height)
        {
            return libCropCoefficient + (0.04 * (windSpeed - 2) - 0.004 * (humidity - 45)) * Math.Pow(height / 3, 0.3);
        }

        public double DailyBaseCropCoefficient(int day, int stageLength, int previousStageLengthSum, double prevCropCoefficient, double nextCropCoefficient)
        {
            return prevCropCoefficient + ((day - previousStageLengthSum) / (double)stageLength) * (nextCropCoefficient - prevCropCoefficient);
        }

        public double EvaporationCoefficient(double baseCropCoefficient, double maxCropCoefficient, double evaporationReductionCoefficient, double exposedWettedSoilFraction)
        {
            return Math.Min(evaporationReductionCoefficient * (maxCropCoefficient - baseCropCoefficient), exposedWettedSoilFraction * maxCropCoefficient);
        }

        public double MaxCropCoefficient(double windSpeed, double humidity, double height, double baseCropCoefficient)
        {
            return Math.Max(
                1.2 + (0.04 * (windSpeed - 2) - 0.004 * (humidity - 45)) * Math.Pow(height / 3, 0.3),
                baseCropCoefficient + 0.05
                );
        }

        public double EvaporationDepth(double evaporationDepthPrev, double precipitation, double irrigation, double wateredSoilFraction, double evaporationCoefficient, double referenceEvapotranspiration, double exposedWateredSoilFraction)
        {
            double evaporationDepth = evaporationDepthPrev - precipitation - irrigation / wateredSoilFraction + evaporationCoefficient * referenceEvapotranspiration / exposedWateredSoilFraction;
            return evaporationDepth < 0 ? 0 : evaporationDepth;
        }

        public double EvaporationReductionCoefficient(double evaporationDepth, double totalEvaporableWater, double readilyEvaporableWater)
        {
            return evaporationDepth <= readilyEvaporableWater ? 1 : (totalEvaporableWater - evaporationDepth) / (totalEvaporableWater - readilyEvaporableWater);
        }

        public double ExposedWettedSoilFraction(double wettedSoilFraction, double coveredSoilFraction)
        {
            return Math.Min(1 - coveredSoilFraction, wettedSoilFraction);
        }

        public double CoveredSoilFraction(double baseCropCoefficient, double iniBaseCropCoefficient, double maxCropCoefficient, double height)
        {
            return Math.Pow((baseCropCoefficient - iniBaseCropCoefficient) / (maxCropCoefficient - iniBaseCropCoefficient), 1 + 0.5 * height);
        }

        public double CurrentHeight(int day, double maxHeight, int Lengthini, int LengthDev)
        {
            return day <= (Lengthini + LengthDev) ? day * maxHeight / (Lengthini + LengthDev) : maxHeight;
        }

        //public double CurrentRootDepth(double maxDepth, double baseCropCoefficient, double iniBaseCropCoefficient, double midBaseCropCoefficient)
        //{
        //    return 0.1 + (maxDepth - 0.1) * (baseCropCoefficient - iniBaseCropCoefficient) / (midBaseCropCoefficient - iniBaseCropCoefficient);
        //}

        public double CurrentRootDepth(int day, double maxRootDepth, int Lengthini, int LengthDev, int LengthMid)
        {
            return day <= (Lengthini + LengthDev + LengthMid) ? day * maxRootDepth / (Lengthini + LengthDev + LengthMid) : maxRootDepth;
        }

        public double WaterStressCoefficient(double totalAvailableWater, double depletion, double readilyAvailableWater, double waterExtractionRate)
        {
            return (totalAvailableWater - depletion) / ((1 - waterExtractionRate) * totalAvailableWater);
        }

        public double CropEvapotranspirationAdjusted(double cropEvapotranspiration, double waterStressCoefficient)
        {
            return cropEvapotranspiration * waterStressCoefficient;
        }

        public double YieldReduction(double cropEvapotranspiration, double cropEvapotranspirationAdjusted, double yieldDependencyCoefficient)
        {
            return yieldDependencyCoefficient * (1 - cropEvapotranspirationAdjusted / cropEvapotranspiration);
        }

        static public double DepthToVolume(double depth, double area, double wettedSoilFraction)
        {
            return depth * area * wettedSoilFraction * 1e-3;
        }

        static public double VolumeToDepth(double volume, double area, double wettedSoilFraction)
        {
            return volume / (area * wettedSoilFraction * 1e-3);
        }
    }
}