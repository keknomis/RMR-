using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace footballApp.Pages.Model
{
    public class League
    {
        [JsonProperty("country_id", NullValueHandling = NullValueHandling.Ignore)]
        public string country_id { get; set; }

        [JsonProperty("country_name", NullValueHandling = NullValueHandling.Ignore)]
        public string country_name { get; set; }

        [JsonProperty("league_id", NullValueHandling = NullValueHandling.Ignore)]
        public string league_id { get; set; }

        [JsonProperty("league_name", NullValueHandling = NullValueHandling.Ignore)]
        public string league_name { get; set; }

        [JsonProperty("league_season", NullValueHandling = NullValueHandling.Ignore)]
        public string league_season { get; set; }

        [JsonProperty("league_logo", NullValueHandling = NullValueHandling.Ignore)]
        public string league_logo { get; set; }

        [JsonProperty("country_logo", NullValueHandling = NullValueHandling.Ignore)]
        public string country_logo { get; set; }

        public string UserUid { get; set; }

        public Guid LeagaueId { get; set; }
    }
}
