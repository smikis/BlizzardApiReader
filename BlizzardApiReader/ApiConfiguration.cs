using BlizzardApiReader.Core.Enums;
using BlizzardApiReader.Core.Extensions;
using System;

namespace BlizzardApiReader
{
    public class ApiConfiguration
    {
        public Region ApiRegion;
        public Locale ResultLocale;
        public string ApiKey;

        /// <summary>
        /// Initialize ApiConfiguration with default configurations and no api key
        /// </summary>
        public ApiConfiguration() : this(null)
        {

        }

        /// <summary>
        /// Initialize ApiConfiguration with default configurations and api key
        /// </summary>
        /// <param name="apiKey"></param>
        public ApiConfiguration(string apiKey) : this(Region.US, Locale.en_US, apiKey)
        {

        }

        /// <summary>
        /// Initialize ApiConfiguration
        /// </summary>
        /// <param name="region"></param>
        /// <param name="locale"></param>
        /// <param name="apiKey"></param>
        public ApiConfiguration(Region region, Locale locale, string apiKey)
        {
            ApiRegion = region;
            ResultLocale = locale;
            ApiKey = apiKey;
        }

        public static ApiConfiguration CreateEmpty()
        {
            return new ApiConfiguration();
        }

        public ApiConfiguration SetApiKey(string key)
        {
            ApiKey = key;
            return this;
        }

        public ApiConfiguration SetRegion(Region region)
        {
            ApiRegion = region;
            return this;
        }

        public ApiConfiguration SetLocale(Locale locale)
        {
            ResultLocale = locale;
            return this;
        }

        /// <summary>
        /// Will use the default locale for the configuration region, must be called only after setting the Region
        /// </summary>
        /// <returns></returns>
        public ApiConfiguration UseDefaultLocale()
        {
            ResultLocale = ApiRegion.GetDefaultLocale();
            return this;
        }


        /// <summary>
        /// Declare this Configuration as the global default configuration, it will be used when no configuration is provided to the api reader.
        /// </summary>  
        public ApiConfiguration DeclareAsDefault()
        {
            ApiReader.SetDefaultConfiguration(this);
            return this;
        }

        public string GetLocaleString()
        {
            return ResultLocale.GetEnumName();
        }
        public string GetRegionString()
        {
            return ApiRegion.GetEnumName();
        }
    }

    public static class RegionExtensions
    {
        public static Locale GetDefaultLocale(this Region region)
        {
            switch (region)
            {
                case Region.EU:
                    return Locale.en_GB;
                case Region.KR:
                    return Locale.ko_KR;
                case Region.TW:
                    return Locale.zh_TW;
                case Region.SEA:
                case Region.US:
                    return Locale.en_US;
                default:
                    throw new NotImplementedException($"The {nameof(Region)} [{region.ToString()}] does not have an associated {nameof(Locale)}");
            }
        }
    }
}
