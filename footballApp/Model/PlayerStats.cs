using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace footballApp.Pages.Model
{
    public class PlayerStats
    {
        [JsonProperty("player_key", NullValueHandling = NullValueHandling.Ignore)]
        public string player_key { get; set; }


        [JsonProperty("player_name", NullValueHandling = NullValueHandling.Ignore)]
        public string player_name { get; set; }


        [JsonProperty("player_number", NullValueHandling = NullValueHandling.Ignore)]
        public string player_number { get; set; }


        [JsonProperty("player_country", NullValueHandling = NullValueHandling.Ignore)]
        public string player_country { get; set; }


        [JsonProperty("player_type", NullValueHandling = NullValueHandling.Ignore)]
        public string player_type { get; set; }


        [JsonProperty("player_age", NullValueHandling = NullValueHandling.Ignore)]
        public string player_age { get; set; }


        [JsonProperty("player_match_played", NullValueHandling = NullValueHandling.Ignore)]
        public string player_match_played { get; set; }


        [JsonProperty("player_goals", NullValueHandling = NullValueHandling.Ignore)]
        public string player_goals { get; set; }


        [JsonProperty("player_yellow_cards", NullValueHandling = NullValueHandling.Ignore)]
        public string player_yellow_cards { get; set; }


        [JsonProperty("player_red_cards", NullValueHandling = NullValueHandling.Ignore)]
        public string player_red_cards { get; set; }


        [JsonProperty("team_name", NullValueHandling = NullValueHandling.Ignore)]
        public string team_name { get; set; }


        [JsonProperty("team_key", NullValueHandling = NullValueHandling.Ignore)]
        public string team_key { get; set; }

        //public PlayerStats()
        //{

        //}
    }
}