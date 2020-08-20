using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Skyrim_challenge.cs
{
    class Data
    {
        [JsonProperty(propertyName: "PathToCharacterFile")]
        public string characterConfigPath { get; set; } = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + @"\characterConfig.json";
        [JsonProperty(propertyName: "PathToCharacterSetupFile")]
        public string setupConfigPath { get; set; } = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
        [JsonProperty(propertyName: "StartingSkillCount")]
        public uint startingSkillCount { get; set; } = 3;
        [JsonProperty(propertyName: "PossibleAmountOfFollowers")]
        public uint possibleFollowerAmount { get; set; } = 1;
        [JsonProperty(propertyName: "ResetCount")]
        public int resetCount { get; set; } = -1;


        public void CreateDefaultData()
        {
            characterConfigPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + @"\characterConfig.json";
            setupConfigPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + @"\setupConfig.json";
            startingSkillCount = 3;
            possibleFollowerAmount = 1;
            resetCount = -1;
        }
    }
}
