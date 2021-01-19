using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace footballApp.Model
{
    public class Livescore
    {
        [JsonProperty("match_id", NullValueHandling = NullValueHandling.Ignore)]
        public string match_id { get; set; }

        [JsonProperty("country_id", NullValueHandling = NullValueHandling.Ignore)]
        public string country_id { get; set; }

        [JsonProperty("country_name", NullValueHandling = NullValueHandling.Ignore)]
        public string country_name { get; set; }

        [JsonProperty("league_id", NullValueHandling = NullValueHandling.Ignore)]
        public string league_id { get; set; }

        [JsonProperty("league_name", NullValueHandling = NullValueHandling.Ignore)]
        public string league_name { get; set; }

        [JsonProperty("match_date", NullValueHandling = NullValueHandling.Ignore)]
        public string match_date { get; set; }

        [JsonProperty("match_status", NullValueHandling = NullValueHandling.Ignore)]
        public string match_status { get; set; }

        [JsonProperty("match_time", NullValueHandling = NullValueHandling.Ignore)]
        public string match_time { get; set; }

        [JsonProperty("match_hometeam_id", NullValueHandling = NullValueHandling.Ignore)]
        public string match_hometeam_id { get; set; }

        [JsonProperty("match_hometeam_name", NullValueHandling = NullValueHandling.Ignore)]
        public string match_hometeam_name { get; set; }

        [JsonProperty("match_hometeam_score", NullValueHandling = NullValueHandling.Ignore)]
        public string match_hometeam_score { get; set; }

        [JsonProperty("match_awayteam_name", NullValueHandling = NullValueHandling.Ignore)]
        public string match_awayteam_name { get; set; }

        [JsonProperty("match_awayteam_id", NullValueHandling = NullValueHandling.Ignore)]
        public string match_awayteam_id { get; set; }

        [JsonProperty("match_awayteam_score", NullValueHandling = NullValueHandling.Ignore)]
        public string match_awayteam_score { get; set; }

        [JsonProperty("match_hometeam_halftime_score", NullValueHandling = NullValueHandling.Ignore)]
        public string match_hometeam_halftime_score { get; set; }

        [JsonProperty("match_awayteam_halftime_score", NullValueHandling = NullValueHandling.Ignore)]
        public string match_awayteam_halftime_score { get; set; }

        [JsonProperty("match_hometeam_extra_score", NullValueHandling = NullValueHandling.Ignore)]
        public string match_hometeam_extra_score { get; set; }

        [JsonProperty("match_awayteam_extra_score", NullValueHandling = NullValueHandling.Ignore)]
        public string match_awayteam_extra_score { get; set; }

        [JsonProperty("match_hometeam_penalty_score", NullValueHandling = NullValueHandling.Ignore)]
        public string match_hometeam_penalty_score { get; set; }

        [JsonProperty("match_awayteam_penalty_score", NullValueHandling = NullValueHandling.Ignore)]
        public string match_awayteam_penalty_score { get; set; }

        [JsonProperty("match_hometeam_ft_score", NullValueHandling = NullValueHandling.Ignore)]
        public string match_hometeam_ft_score { get; set; }

        [JsonProperty("match_awayteam_ft_score", NullValueHandling = NullValueHandling.Ignore)]
        public string match_awayteam_ft_score { get; set; }

        [JsonProperty("match_hometeam_system", NullValueHandling = NullValueHandling.Ignore)]
        public string match_hometeam_system { get; set; }

        [JsonProperty("match_awayteam_system", NullValueHandling = NullValueHandling.Ignore)]
        public string match_awayteam_system { get; set; }

        [JsonProperty("match_live", NullValueHandling = NullValueHandling.Ignore)]
        public string match_live { get; set; }

        [JsonProperty("match_round", NullValueHandling = NullValueHandling.Ignore)]
        public string match_round { get; set; }

        [JsonProperty("match_stadium", NullValueHandling = NullValueHandling.Ignore)]
        public string match_stadium { get; set; }

        [JsonProperty("match_referee", NullValueHandling = NullValueHandling.Ignore)]
        public string match_referee { get; set; }

        [JsonProperty("team_home_badge", NullValueHandling = NullValueHandling.Ignore)]
        public string team_home_badge { get; set; }

        [JsonProperty("team_away_badge", NullValueHandling = NullValueHandling.Ignore)]
        public string team_away_badge { get; set; }

        [JsonProperty("league_logo", NullValueHandling = NullValueHandling.Ignore)]
        public string league_logo { get; set; }

        [JsonProperty("country_logo", NullValueHandling = NullValueHandling.Ignore)]
        public string country_logo { get; set; }

        [JsonProperty("goalscorer", NullValueHandling = NullValueHandling.Ignore)]
        public List<Goalscorer> goalscorer { get; set; }

        [JsonProperty("cards", NullValueHandling = NullValueHandling.Ignore)]
        public List<Card> cards { get; set; }

        [JsonProperty("substitutions", NullValueHandling = NullValueHandling.Ignore)]
        public Substitutions substitutions { get; set; }

        [JsonProperty("lineup", NullValueHandling = NullValueHandling.Ignore)]
        public Lineup lineup { get; set; }

        [JsonProperty("statistics", NullValueHandling = NullValueHandling.Ignore)]
        public List<Statistic> statistics { get; set; }

        public string MatchStatus
        {
            get
            {
                if (match_status == "Finished")
                    return match_hometeam_score + " - " + match_awayteam_score;
                else
                    return "vs";
            }
            set
            {
                MatchStatus = value;
            }
        }

        public List<Goalscorer> HomeGoalScorers
        {
            get
            {
                if (match_status == "Finished")
                {
                    List<Goalscorer> listHome = new List<Goalscorer>();
                    foreach (var i in goalscorer)
                    {
                        if (i.home_scorer != "")
                            listHome.Add(i);

                    }
                    return listHome;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HomeGoalScorers = value;
            }
        }

        public List<Goalscorer> AwayGoalScorers
        {
            get
            {
                if (match_status == "Finished")
                {
                    List<Goalscorer> listAway = new List<Goalscorer>();
                    foreach (var i in goalscorer)
                    {
                        if (i.away_scorer != "")
                            listAway.Add(i);

                    }
                    return listAway;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                AwayGoalScorers = value;
            }
        }

        public List<Card> HomeCard
        {
            get
            {
                List<Card> kartoni = new List<Card>();
                foreach (var i in cards)
                {
                    if ((i.card == "yellow card" || i.card == "red card") && i.home_fault != "" && i.time != "")
                    {
                        kartoni.Add(i);
                    }
                }
                return kartoni;
            }
            set
            {
                HomeCard = value;
            }
        }

        public List<Card> AwayCard
        {
            get
            {
                List<Card> kartoni = new List<Card>();
                foreach (var i in cards)
                {
                    if ((i.card == "yellow card" || i.card == "red card") && i.away_fault != "" && i.time != "")
                    {
                        kartoni.Add(i);
                    }
                }
                return kartoni;
            }
            set
            {
                AwayCard = value;
            }
        }

    }

    public class Goalscorer
    {
        [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
        public string time { get; set; }

        [JsonProperty("home_scorer", NullValueHandling = NullValueHandling.Ignore)]
        public string home_scorer { get; set; }

        [JsonProperty("home_scorer_id", NullValueHandling = NullValueHandling.Ignore)]
        public string home_scorer_id { get; set; }

        [JsonProperty("home_assist", NullValueHandling = NullValueHandling.Ignore)]
        public string home_assist { get; set; }

        [JsonProperty("home_assist_id", NullValueHandling = NullValueHandling.Ignore)]
        public string home_assist_id { get; set; }

        [JsonProperty("score", NullValueHandling = NullValueHandling.Ignore)]
        public string score { get; set; }

        [JsonProperty("away_scorer", NullValueHandling = NullValueHandling.Ignore)]
        public string away_scorer { get; set; }

        [JsonProperty("away_scorer_id", NullValueHandling = NullValueHandling.Ignore)]
        public string away_scorer_id { get; set; }

        [JsonProperty("away_assist", NullValueHandling = NullValueHandling.Ignore)]
        public string away_assist { get; set; }

        [JsonProperty("away_assist_id", NullValueHandling = NullValueHandling.Ignore)]
        public string away_assist_id { get; set; }

        [JsonProperty("info", NullValueHandling = NullValueHandling.Ignore)]
        public string info { get; set; }
    }

    public class Card
    {
        [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
        public string time { get; set; }

        [JsonProperty("home_fault", NullValueHandling = NullValueHandling.Ignore)]
        public string home_fault { get; set; }

        [JsonProperty("card", NullValueHandling = NullValueHandling.Ignore)]
        public string card { get; set; }

        [JsonProperty("away_fault", NullValueHandling = NullValueHandling.Ignore)]
        public string away_fault { get; set; }

        [JsonProperty("info", NullValueHandling = NullValueHandling.Ignore)]
        public string info { get; set; }

        public string CardImg
        {
            get
            {
                if (card == "yellow card")
                {
                    string source = "https://icon2.cleanpng.com/20180325/tbq/kisspng-penalty-card-yellow-card-association-football-refe-sim-cards-5ab74207b121f1.8732925815219594317255.jpg";
                    return source;
                }
                else if (card == "red card")
                {
                    string source = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e7/Red_card.svg/1200px-Red_card.svg.png";
                    return source;
                }

                return "";
            }
            set
            {
                CardImg = value;
            }
        }
    }

    public class Home
    {
        [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
        public string time { get; set; }

        [JsonProperty("substitution", NullValueHandling = NullValueHandling.Ignore)]
        public string substitution { get; set; }
    }

    public class Away
    {
        [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
        public string time { get; set; }

        [JsonProperty("substitution", NullValueHandling = NullValueHandling.Ignore)]
        public string substitution { get; set; }
    }

    public class Substitutions
    {
        [JsonProperty("home", NullValueHandling = NullValueHandling.Ignore)]
        public List<Home> home { get; set; }

        [JsonProperty("away", NullValueHandling = NullValueHandling.Ignore)]
        public List<Away> away { get; set; }
    }

    public class StartingLineup
    {
        [JsonProperty("lineup_player", NullValueHandling = NullValueHandling.Ignore)]
        public string lineup_player { get; set; }

        [JsonProperty("lineup_number", NullValueHandling = NullValueHandling.Ignore)]
        public string lineup_number { get; set; }

        [JsonProperty("lineup_position", NullValueHandling = NullValueHandling.Ignore)]
        public string lineup_position { get; set; }

        [JsonProperty("player_key", NullValueHandling = NullValueHandling.Ignore)]
        public string player_key { get; set; }
    }

    public class Substitute
    {
        [JsonProperty("lineup_player", NullValueHandling = NullValueHandling.Ignore)]
        public string lineup_player { get; set; }

        [JsonProperty("lineup_number", NullValueHandling = NullValueHandling.Ignore)]
        public string lineup_number { get; set; }

        [JsonProperty("lineup_position", NullValueHandling = NullValueHandling.Ignore)]
        public string lineup_position { get; set; }

        [JsonProperty("player_key", NullValueHandling = NullValueHandling.Ignore)]
        public string player_key { get; set; }
    }

    public class Coach
    {
        [JsonProperty("lineup_player", NullValueHandling = NullValueHandling.Ignore)]
        public string lineup_player { get; set; }

        [JsonProperty("lineup_number", NullValueHandling = NullValueHandling.Ignore)]
        public string lineup_number { get; set; }

        [JsonProperty("lineup_position", NullValueHandling = NullValueHandling.Ignore)]
        public string lineup_position { get; set; }

        [JsonProperty("player_key", NullValueHandling = NullValueHandling.Ignore)]
        public string player_key { get; set; }
    }

    public class Home2
    {
        [JsonProperty("starting_lineups", NullValueHandling = NullValueHandling.Ignore)]
        public List<StartingLineup> starting_lineups { get; set; }

        [JsonProperty("substitutes", NullValueHandling = NullValueHandling.Ignore)]
        public List<Substitute> substitutes { get; set; }

        [JsonProperty("coach", NullValueHandling = NullValueHandling.Ignore)]
        public List<Coach> coach { get; set; }

        [JsonProperty("missing_players", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> missing_players { get; set; }
    }

    public class StartingLineup2
    {
        [JsonProperty("lineup_player", NullValueHandling = NullValueHandling.Ignore)]
        public string lineup_player { get; set; }

        [JsonProperty("lineup_number", NullValueHandling = NullValueHandling.Ignore)]
        public string lineup_number { get; set; }

        [JsonProperty("lineup_position", NullValueHandling = NullValueHandling.Ignore)]
        public string lineup_position { get; set; }

        [JsonProperty("player_key", NullValueHandling = NullValueHandling.Ignore)]
        public string player_key { get; set; }
    }

    public class Substitute2
    {
        [JsonProperty("lineup_player", NullValueHandling = NullValueHandling.Ignore)]
        public string lineup_player { get; set; }

        [JsonProperty("lineup_number", NullValueHandling = NullValueHandling.Ignore)]
        public string lineup_number { get; set; }

        [JsonProperty("lineup_position", NullValueHandling = NullValueHandling.Ignore)]
        public string lineup_position { get; set; }

        [JsonProperty("player_key", NullValueHandling = NullValueHandling.Ignore)]
        public string player_key { get; set; }
    }

    public class Coach2
    {
        [JsonProperty("lineup_player", NullValueHandling = NullValueHandling.Ignore)]
        public string lineup_player { get; set; }

        [JsonProperty("lineup_number", NullValueHandling = NullValueHandling.Ignore)]
        public string lineup_number { get; set; }

        [JsonProperty("lineup_position", NullValueHandling = NullValueHandling.Ignore)]
        public string lineup_position { get; set; }

        [JsonProperty("player_key", NullValueHandling = NullValueHandling.Ignore)]
        public string player_key { get; set; }
    }

    public class Away2
    {
        [JsonProperty("starting_lineups", NullValueHandling = NullValueHandling.Ignore)]
        public List<StartingLineup2> starting_lineups { get; set; }

        [JsonProperty("substitutes", NullValueHandling = NullValueHandling.Ignore)]
        public List<Substitute2> substitutes { get; set; }

        [JsonProperty("coach", NullValueHandling = NullValueHandling.Ignore)]
        public List<Coach2> coach { get; set; }

        [JsonProperty("missing_players", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> missing_players { get; set; }
    }

    public class Lineup
    {
        [JsonProperty("home", NullValueHandling = NullValueHandling.Ignore)]
        public Home2 home { get; set; }

        [JsonProperty("away", NullValueHandling = NullValueHandling.Ignore)]
        public Away2 away { get; set; }
    }

    public class Statistic
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string type { get; set; }

        [JsonProperty("home", NullValueHandling = NullValueHandling.Ignore)]
        public string home { get; set; }

        [JsonProperty("away", NullValueHandling = NullValueHandling.Ignore)]
        public string away { get; set; }
    }
}