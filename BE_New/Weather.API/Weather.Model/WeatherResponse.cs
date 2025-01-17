﻿using Newtonsoft.Json;

namespace Weather.Model
{
    public class Daily
    {
        [JsonProperty("time")]
        public List<string>? Time { get; set; }

        [JsonProperty("temperature_2m_max")]
        public List<double>? Temperature2mMax { get; set; }

        [JsonProperty("temperature_2m_mean")]
        public List<double>? Temperature2mMean { get; set; }

        [JsonProperty("rain_sum")]
        public List<double>? RainSum { get; set; }

        [JsonProperty("relative_humidity_2m_mean")]
        public List<double>? RelativeHumidity2mMean { get; set; }
    }

    public class DailyUnits
    {
        [JsonProperty("time")]
        public string? Time { get; set; }

        [JsonProperty("temperature_2m_max")]
        public string? Temperature2mMax { get; set; }

        [JsonProperty("temperature_2m_mean")]
        public string? Temperature2mMean { get; set; }

        [JsonProperty("rain_sum")]
        public string? RainSum { get; set; }

        [JsonProperty("relative_humidity_2m_mean")]
        public string? RelativeHumidity2mMean { get; set; }
    }

    public class WeatherResponse
    {
        [JsonProperty("latitude")]
        public double? Latitude { get; set; }

        [JsonProperty("longitude")]
        public double? Longitude { get; set; }

        [JsonProperty("generationtime_ms")]
        public double? GenerationtimeMs { get; set; }

        [JsonProperty("utc_offset_seconds")]
        public int? UtcOffsetSeconds { get; set; }

        [JsonProperty("timezone")]
        public string? Timezone { get; set; }

        [JsonProperty("timezone_abbreviation")]
        public string? TimezoneAbbreviation { get; set; }

        [JsonProperty("elevation")]
        public double? Elevation { get; set; }

        [JsonProperty("daily_units")]
        public DailyUnits? DailyUnits { get; set; }

        [JsonProperty("daily")]
        public Daily? Daily { get; set; }
    }
}
