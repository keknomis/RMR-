using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace footballApp.Model
{
    public class Club
    {
        [JsonProperty("team_key", NullValueHandling = NullValueHandling.Ignore)]
        public string team_key { get; set; }

        [JsonProperty("team_name", NullValueHandling = NullValueHandling.Ignore)]
        public string team_name { get; set; }

        [JsonProperty("team_badge", NullValueHandling = NullValueHandling.Ignore)]
        public string team_badge { get; set; }

        [JsonProperty("players", NullValueHandling = NullValueHandling.Ignore)]
        public List<Player> players { get; set; }

        [JsonProperty("coaches", NullValueHandling = NullValueHandling.Ignore)]
        public List<Coach> coaches { get; set; }

        public string UserUid { get; set; }

        public class Player
        {
            [JsonProperty("player_key", NullValueHandling = NullValueHandling.Ignore)]
            public object player_key { get; set; }

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
        }

        public class Coach
        {
            [JsonProperty("coach_name", NullValueHandling = NullValueHandling.Ignore)]
            public string coach_name { get; set; }

            [JsonProperty("coach_country", NullValueHandling = NullValueHandling.Ignore)]
            public string coach_country { get; set; }

            [JsonProperty("coach_age", NullValueHandling = NullValueHandling.Ignore)]
            public string coach_age { get; set; }
        }
    }
}
