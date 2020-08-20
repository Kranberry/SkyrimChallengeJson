using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyrim_challenge.cs
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    class NewCharacterSetup
    {
        [JsonProperty(propertyName: "Race")]
        public List<string> races { get; set; }

        [JsonProperty(propertyName: "Skills")]
        public List<Skills> skills { get; set; }

        [JsonProperty(propertyName: "Objectives")]
        public List<Objectives> objectives { get; set; }
        [JsonProperty(propertyName: "AlternativeObjectives")]
        public List<Objectives> alternativeObjectives { get; set; }

        [JsonProperty(propertyName: "FollowerLocation")]
        public List<string> followerLocations { get; set; }

        public NewCharacterSetup()
        {
            races = new List<string>();
            skills = new List<Skills>();
            objectives = new List<Objectives>();
            followerLocations = new List<string>();
        }

        public void setDefaultSettings(bool isAlternative = false)
        {
            races = new List<string>()
            {
                "Altmer",
                "Nord",
                "Bosmer",
                "Breton",
                "Dunmer",
                "Imperial",
                "Khajit",
                "Orc", 
                "Redguard",
                "Argonian"
            };

            if (!isAlternative)
            {
                objectives = new List<Objectives>
                {
                    new Objectives("Main Quest", 3),
                    new Objectives("Thane 5 cities", 3),
                    new Objectives("DawnGuard Vampire", 3, 15, new List<string>(){ "DawnGuard Human" }),
                    new Objectives("DawnGuard Human", 3, 15, new List<string>(){ "DawnGuard Vampire" }),
                    new Objectives("Thieves Guild", 3, 5),
                    new Objectives("The Dark Brotherhood", 3, 5),
                    new Objectives("College Of Winterhold", 3, 7),
                    new Objectives("DragonBorn", 3, 20),
                    new Objectives("The Man Who Cried Wolf", 2, 7),
                    new Objectives("Forsworn", 2),
                    new Objectives("Imperials", 2, new List<string>(){ "Stormcloaks" }),
                    new Objectives("Stormcloaks", 2,new List<string>(){ "Imperials" }),
                    new Objectives("Talking Dog", 2, 10),
                    new Objectives("Mind Of Madness", 1),
                    new Objectives("Coming Of Age", 1),
                    new Objectives("Kill Ebony Warrior", 4, 50),
                    new Objectives("Choose One, No Fast Travel", true)
                };
            }

            if (isAlternative)
            {
                objectives = new List<Objectives>
                {
                    new Objectives("Main Quest", 3),
                    new Objectives("Thane 5 cities", 3),
                    new Objectives("DawnGuard Vampire", 3, 15, new List<string>(){ "DawnGuard Human" }),
                    new Objectives("DawnGuard Human", 3, 15, new List<string>(){ "DawnGuard Vampire" }),
                    new Objectives("Thieves Guild", 3, 5),
                    new Objectives("The Dark Brotherhood", 3, 5),
                    new Objectives("College Of Winterhold", 3, 7),
                    new Objectives("DragonBorn", 3, 20),
                    new Objectives("The Man Who Cried Wolf", 2, 7),
                    new Objectives("Forsworn", 2),
                    new Objectives("Imperials", 2, new List<string>(){ "Stormcloaks" }),
                    new Objectives("Stormcloaks", 2,new List<string>(){ "Imperials" }),
                    new Objectives("Talking Dog", 2, 10),
                    new Objectives("Mind Of Madness", 1),
                    new Objectives("Coming Of Age", 1),
                    new Objectives(new List<string>() { "Mind Of Madness", "Coming Of Age" } ,"Kill Ebony Warrior", 4, 50),
                    new Objectives("Choose One, No Fast Travel", true)
                };
            }

            skills = new List<Skills>()
            {
                new Skills("One Handed", "Attack"),
                new Skills("Two Handed", "Attack"),
                new Skills("Destruction", "Attack"),
                new Skills("Conjuration"),
                new Skills("Illusion"),
                new Skills("Alchemy"),
                new Skills("Speech"),
                new Skills("Sneak"),
                new Skills("Lockpicking"),
                new Skills("Smithing"),
                new Skills("Archery", "Attack"),
                new Skills("Block"),
                new Skills("Light Armor"),
                new Skills("Heavy Armor"),
                new Skills("Enchanting"),
                new Skills("Alteration"),
                new Skills("Restoration"),
                new Skills("Pickpocket")
            };

            followerLocations = new List<string>()
            {
                "Whiterun",
                "Riften",
                "Markarth",
                "Solitude",
                "Dawnstar",
                "Morthal",
                "Winterhold",
                "Falkreath",
                "Windhelm",
                "Riverwood",
                "Out in the world"
            };
        }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    class Skills
    {
        [JsonProperty(propertyName: "Skill")]
        public string skill { get; set; }
        [JsonProperty(propertyName: "Type")]
        public string type { get; set; }

        [JsonConstructor]
        public Skills(string skill, string type)
        {
            this.skill = skill;
            this.type = type;
        }

        public Skills(string skill)
        {
            this.skill = skill;
        }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Objectives
    {
        [JsonProperty(propertyName: "Objective")]
        public string objective { get; set; }
        [JsonProperty(propertyName: "LevelRequired")]
        public int levelRequired { get; set; }
        [JsonProperty(propertyName: "MustHaveFinishedTheseObjectives")]
        public List<string> mustHaveFinishedTheseObjectives { get; set; }
        [JsonProperty(propertyName:"InRelationTo")]
        public List<string> inRelationTo { get; set; }
        [JsonProperty(propertyName: "SkillReward")]
        public int skillReward { get; set; }

        [JsonProperty(propertyName: "NoFastTravel")]
        public bool noFastTravel { get; set; }

        [JsonConstructor]
        public Objectives(List<string> mustHaveFinishedTheseObjectives, string objective, int skillReward, int levelRequired)
        {
            this.objective = objective;
            this.levelRequired = levelRequired;
            this.mustHaveFinishedTheseObjectives = mustHaveFinishedTheseObjectives;
            this.skillReward = skillReward;
        }
        public Objectives(List<string> mustHaveFinishedTheseObjectives, string objective, int skillReward, int levelRequired, List<String> inRelationTo)
        {
            this.inRelationTo = inRelationTo;
            this.objective = objective;
            this.levelRequired = levelRequired;
            this.mustHaveFinishedTheseObjectives = mustHaveFinishedTheseObjectives;
            this.skillReward = skillReward;
        }
        public Objectives(string objective, int skillReward, int levelRequired, List<string> inRelationTo)
        {
            this.inRelationTo = inRelationTo;
            this.objective = objective;
            this.levelRequired = levelRequired;
            this.skillReward = skillReward;
        }
        public Objectives(string objective, int skillReward, int levelRequired)
        {
            this.objective = objective;
            this.levelRequired = levelRequired;
            this.skillReward = skillReward;
        }
        public Objectives(string objective, int skillReward, List<string> inRelationTo)
        {
            this.inRelationTo = inRelationTo;
            this.objective = objective;
            this.skillReward = skillReward;
        }
        public Objectives(string objective, int skillReward)
        {
            this.objective = objective;
            this.skillReward = skillReward;
        }
        public Objectives(string objective, bool noFastTravel)
        {
            this.objective = objective;
            this.noFastTravel = noFastTravel;
        }
    }
}
