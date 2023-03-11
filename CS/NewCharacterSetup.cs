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
        public List<string> Races { get; set; }

        [JsonProperty(propertyName: "Skills")]
        public List<Skills> Skills { get; set; }

        [JsonProperty(propertyName: "Objectives")]
        public List<Objectives> Objectives { get; set; }
        [JsonProperty(propertyName: "AlternativeObjectives")]
        public List<Objectives> AlternativeObjectives { get; set; }

        [JsonProperty(propertyName: "FollowerLocation")]
        public List<string> FollowerLocations { get; set; }

        [JsonProperty(propertyName: "AlternativeStartLocations")]
        public List<string> AlternativeStartLocations { get; set; }

        public NewCharacterSetup()
        {
            Races = new List<string>();
            Skills = new List<Skills>();
            Objectives = new List<Objectives>();
            FollowerLocations = new List<string>();
            AlternativeStartLocations = new List<string>();
        }

        public void SetDefaultSettings(bool isAlternative = false)
        {
            Races = new List<string>()
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
                Objectives = new List<Objectives>
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
                Objectives = new List<Objectives>
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

            Skills = new List<Skills>()
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

            FollowerLocations = new List<string>()
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

            AlternativeStartLocations = new List<string>()
            {
                "I came by ship to skyrim - Solitude",
                "I came by ship to skyrim - Dawnstar",
                "I came by ship to skyrim - Windhelm",
                "I own property in one of the holds - Proudspire Manor",
                "I own property in one of the holds - Vlindrel Hall",
                "I own property in one of the holds - Honeyside",
                "I own property in one of the holds - Breezehome",
                "I own property in one of the holds - Shoal's Rest Farm",
                "I'm a new member of a guild - The Companions",
                "I'm a new member of a guild - The College of Winterhold",
                "I'm a new member of a guild - Dark Brotherhood",
                "I'm a new member of a guild - Thieves Guild",
                "I'm a new member of a guild - The Dawnguard",
                "I'm a new member of a guild - Lord Harkon's Court",
                "I'm a patron at a local inn - Windspeak Inn",
                "I'm a patron at a local inn - Four Shields Tavern",
                "I'm a patron at a local inn - Dead Man's Drink",
                "I'm a patron at a local inn - Vilemyr Inn",
                "I'm a patron at a local inn - Braidwood Inn",
                "I'm a patron at a local inn - Moorside Inn",
                "I'm a patron at a local inn - Nightgate Inn",
                "I'm a patron at a local inn - Sleeping Giant Inn",
                "I'm a patron at a local inn - Frostfruit Inn",
                "I'm a patron at a local inn - The Frozen Hearth",
                "I'm a soldier in the army - Imperial Legion",
                "I'm a soldier in the army - Stormcloak Rebellion",
                "I got caught crossing the border illegally (Vanilla)",
                "I'm an outlaw in the wilds",
                "I am a Vigilant of Stendarr",
                "I am camping in the woods",
                "I was shipwrecked off the coast!",
                "I am a vampire in a secluded lair",
                "I am a necromancer in a secret location",
                "I was attacked and left for dead",
                "Escape this prison cell",
                "Race special",
                "Surprise me"
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
