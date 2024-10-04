using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain
{
    public class Data
    {
        public int Rank { get; set; } = 0;
        public int Change { get; set; } = 0;
        public string Name { get; set; } = "";
        public string SteamName { get; set; } = "";
        public string XboxName { get; set; } = "";
        public string PsnName { get; set; } = "";
        public int LeagueNumber { get; set; } = 0;
        public string League { get; set; } = "";
        public int RankScore { get; set; } = 0;
        public int Cashouts { get; set; } = 0;
        public string Sponsor { get; set; } = "";
        public int Fans { get; set; } = 0; 
    }

    public class Meta
    {
        public string LeaderboardVersion { get; set; } = "";
        public string LeaderboardPlatform { get; set; } = "";
        public string NameFilter { get; set; } = "";
        public bool ReturnCountOnly { get; set; } = false;
    }

    public class Root
    {
        public Meta Meta { get; set; } = new Meta();
        public int Count { get; set; } = 0;
        public List<Data> Data { get; set; } = new List<Data>();
    }
}
