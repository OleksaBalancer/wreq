namespace wreq.BL.Abstract
{
    public interface IParametersCalculator
    {

        double StefanBoltzmanConstant { get; }

        double SunConstant { get; }

        double PsychrometricConstant { get; }

        double CloudyRadiationRatio { get; }

        double ClearRadiationRatio { get; }

        double ReferenceAlbedo { get; }

        double Depletion(double depletionPrev, double precipitation, double irrigation, double evapotranspiration);

        double TotalAvailableWater(double fieldWaterCapacity, double wiltWaterCapacity, double rootDepth);

        double ReadilyAvailableWater(double totalAvailableWater, double waterExtractionRate);

        double CropEvapotranspiration(double referenceEvapotranspiration, double baseCultureCoefficient, double evaporationCoefficient);

        double ReferenceEvapotranspiration(double netRadiation, double meanDailyTemperature, double windSpeed, double saturationVapourPressure, double actualVapourPressure, double slopeVapourPressureCurve, double atmosphericPressure);

        double NetRadiation(double netShortWaveRadiation, double netLongWaveRadiation);

        double NetShortWaveRadiation(double albedo, double shortWaveRadiation);

        double ShortWaveRadiation(double actualDaylightHours, double maxDaylightHours, double extraterrestrialRadiation, double cloudyRadiationRatio, double clearRadiationRatio);

        double MaxDaylightHours(double sunsetAngle);

        double ExtraterrestrialRadiation(double sunEarthDistance, double sunsetAngle, double lattitude, double sunTilt);

        double SunEarthDistance(int dayNumber);

        double SunTilt(int dayNumber);

        double SunsetAngle(double lattitude, double sunTilt);

        double NetLongWaveRadiation(double tempMax, double tempMin, double actualVapourPressure, double shortWaveRadiation, double clearSkyRadiation);

        double ClearSkyRadiation(double altitude, double extraterrestrialRadiation);

        double SaturationVapourPressure(double tempMax, double tempMin);

        double SaturationVapourPressure(double temperature);

        double ActualVapourPressure(double tempMin, double tempMax, double humidity);

        double SlopeVapourPressureCurve(double temperature);

        double LibBaseCropCoefficient(int day, int lengthIni, int lengthDev, int lengthMid, int lengthEnd, double kcIni, double kcMid, double kcEnd);

        double AdjustedBaseCropCoefficient(double libCropCoefficient, double windSpeed, double humidity, double height);

        double DailyBaseCropCoefficient(int day, int stageLength, int previousStageLengthSum, double prevCropCoefficient, double nextCropCoefficient);

        double EvaporationCoefficient(double baseCropCoefficient, double maxCropCoefficient, double evaporationReductionCoefficient, double exposedWettedSoilFraction);

        double MaxCropCoefficient(double windSpeed, double humidity, double height, double baseCropCoefficient);

        double EvaporationDepth(double evaporationDepthPrev, double precipitation, double irrigation, double wateredSoilFraction, double evaporationCoefficient, double referenceEvapotranspiration, double exposedWateredSoilFraction);

        double EvaporationReductionCoefficient(double evaporationDepth, double totalEvaporableWater, double readilyEvaporableWater);

        double ExposedWettedSoilFraction(double wettedSoilFraction, double coveredSoilFraction);

        double CoveredSoilFraction(double baseCropCoefficient, double iniBaseCropCoefficient, double maxCropCoefficient, double height);

        double CurrentHeight(int day, double maxHeight, int Lengthini, int LengthDev);

        //double CurrentRootDepth(double maxDepth, double baseCropCoefficient, double iniBaseCropCoefficient, double midBaseCropCoefficient);
        double CurrentRootDepth(int day, double maxRootDepth, int Lengthini, int LengthDev, int LengthMid);

        double WaterStressCoefficient(double totalAvailableWater, double depletion, double readilyAvailableWater, double waterExtractionRate);

        double CropEvapotranspirationAdjusted(double cropEvapotranspiration, double waterStressCoefficient);

        double YieldReduction(double cropEvapotranspiration, double cropEvapotranspirationAdjusted, double yieldDependencyCoefficient);
    }
}
