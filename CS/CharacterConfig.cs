using SkyrimChallenge;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Skyrim_challenge.cs
{
    class CharacterConfig
    {
        public string Race { get; set; }
        public string AlternativeStartLocation { get; set; }
        public uint Level { get; set; }
        private List<Objectives> AvailavleObjectives = new List<Objectives>();
        public string CurrentObjective { get; set; }
        public List<string> FinishedObjectives;
        public List<string> AcquiredSkills;
        public string FollowerLocation { get; set; }
        private Data Data;
        private ChooseOneNoFastTravelWindow NFT;
        private bool FinishedChallenge;


        public CharacterConfig()
        {
            Level = 0;
            FinishedObjectives = new List<string>();
            AcquiredSkills = new List<string>();
        }

        // This method handles the resetting of a character
        public void CreateNewCharacter(Data data, NewCharacterSetup newCharacterSetup)
        {// Put default values in from the setup config
            this.Data = data;
            Level = 0;
            data.resetCount++;
            Random rand = new Random();
            FinishedObjectives.Clear();
            AcquiredSkills.Clear();
            Race = newCharacterSetup.Races[rand.Next(0, newCharacterSetup.Races.Count())];
            FollowerLocation = newCharacterSetup.FollowerLocations[rand.Next(0, newCharacterSetup.FollowerLocations.Count())];
            AlternativeStartLocation = newCharacterSetup.AlternativeStartLocations[rand.Next(0, newCharacterSetup.AlternativeStartLocations.Count())];

            GetNewSkills(newCharacterSetup, true);
            GetNewObjective(newCharacterSetup);
        }

        // This method is called whenever an objective is finished. This will get you new skills and a new objective.
        public void ObjectiveFinished(NewCharacterSetup newCharacterSetup, uint level, string currentObjective)
        {
            this.Level = level;
            GetNewSkills(newCharacterSetup, false, currentObjective);
            GetNewObjective(newCharacterSetup, currentObjective);
        }

        // This one makes sure you get skills
        private void GetNewSkills(NewCharacterSetup newCharacterSetup, bool newCharacter, string currentObjective = null)
        {
            string currentObjectiveTrimmed = currentObjective == null ? null : currentObjective.Replace(" NFT", "");
            Random rand = new Random();
            string skillToBe;
            string skillToBeType;
            Skills skillToBeRaw;
            // Will throw exeption that there is no such thing in sequence when all objectives are finished
            if (FinishedObjectives.Count == newCharacterSetup.Objectives.Count - 1)
            {
                FinishedTheChallenge();
                return;
            }
            int skillsToBeAdded = newCharacter ? (int)Data.startingSkillCount : newCharacterSetup.Objectives.First(item => item.objective == currentObjectiveTrimmed).skillReward;

            // Adds skills of the same amount written down in data
            if (AcquiredSkills.Count != newCharacterSetup.Skills.Count)
            {
                for (int i = 0; i < skillsToBeAdded; i++)
                {
                    if (AcquiredSkills.Count != newCharacterSetup.Skills.Count)
                    {
                        skillToBeRaw = newCharacterSetup.Skills[rand.Next(0, newCharacterSetup.Skills.Count())];
                        skillToBeType = skillToBeRaw.type;
                        // One skill will always be of the attacking type. The first one when you reset.
                        if (skillToBeType == "Attack" && i < 1 && newCharacter)
                        {
                            skillToBe = skillToBeRaw.skill;
                        }
                        else if (skillToBeType != "Attack" && i < 1 && newCharacter)
                        {
                            i--;
                            continue;
                        }
                        skillToBe = skillToBeRaw.skill;

                        if (AcquiredSkills.Contains(skillToBe))
                        {
                            i--;
                            continue;
                        }
                        AcquiredSkills.Add(skillToBe);
                    }
                }
            }

        }

        // This method handles getting objectives
        private void GetNewObjective(NewCharacterSetup newCharacterSetup, string currentObjective = null)
        {
            if (!FinishedChallenge)
            {
                AvailavleObjectives.Clear();
                if (currentObjective != null)
                {
                    // This loop will check every objective if the objective with the same realtion to it is finished, and will be marked with ITF (Impossible To Finish)
                    foreach(Objectives objective in newCharacterSetup.Objectives)
                    {   
                        if (objective.inRelationTo == null)
                            continue;
                        if (objective.inRelationTo.Contains(currentObjective.Replace(" NFT", "")))
                            FinishedObjectives.Add(objective.objective + " ITF");
                    }
                    FinishedObjectives.Add(currentObjective);
                }
                // This will finish the challenge if every objective is finished. -1 is to count out no fast travel.
                if (FinishedObjectives.Count == newCharacterSetup.Objectives.Count - 1)
                {
                    FinishedTheChallenge();
                    return;
                }

                Random rand = new Random();
                List<Objectives> objectives = newCharacterSetup.Objectives;

                // Gets the objective available for the player
                foreach (Objectives element in objectives)
                {
                    if (element.levelRequired <= Level && !IsItFinished(newCharacterSetup, element.objective) && arePreviousObjectivesFinished(newCharacterSetup, element.objective))
                    {
                        AvailavleObjectives.Add(element);
                    }
                    else
                        continue;
                }

                // First time I ever used a do while loop.
                // Always get one objective.
                do
                {
                    Objectives objectivesRaw = AvailavleObjectives[rand.Next(0, AvailavleObjectives.Count)];
                    //Objectives objectivesRaw = newCharacterSetup.objectives[rand.Next(0, newCharacterSetup.objectives.Count())];
                    if (objectivesRaw.noFastTravel)
                        currentObjective = ChooseOneNoFastTravel(newCharacterSetup);
                    else
                        currentObjective = objectivesRaw.objective;
                } while (currentObjective == null || FinishedObjectives.Contains(currentObjective));

                this.CurrentObjective = currentObjective;
            }
        }
        
        // Will make it so that you can't continue pressing objective complete after finishing the last one
        public void FinishedTheChallenge()
        {
            CurrentObjective = "You won the challenge!";
            FinishedChallenge = true;
        }

        // This method will check if the previous mustHaveFinished objectives are finished.
        private bool arePreviousObjectivesFinished(NewCharacterSetup newCharacterSetup, string element)
        {                           // Relative to how many objectives must be finished
            int amountOfObjectives, amountOfFinishedObjetives = 0;

            Objectives objectiveInQuestion = newCharacterSetup.Objectives.First(item => item.objective == element);

            if (objectiveInQuestion.mustHaveFinishedTheseObjectives == null)
                return true;

            amountOfObjectives = objectiveInQuestion.mustHaveFinishedTheseObjectives.Count();
            for (int i = 0; i < amountOfObjectives; i++)
            {
                if (FinishedObjectives.Contains(objectiveInQuestion.mustHaveFinishedTheseObjectives[i]) || FinishedObjectives.Contains(objectiveInQuestion.mustHaveFinishedTheseObjectives[i] + " ITF") || FinishedObjectives.Contains(objectiveInQuestion.mustHaveFinishedTheseObjectives[i] + " NFT"))
                    amountOfFinishedObjetives++;
            }
            MessageBox.Show(objectiveInQuestion.objective + " : " + amountOfObjectives + " : " + amountOfFinishedObjetives);
            return amountOfFinishedObjetives == amountOfObjectives ? true : false;
        }

        // Will check if the objective in question is already complete
        private bool IsItFinished(NewCharacterSetup newCharacterSetup ,string element)
        {
            if (FinishedObjectives.Count == 0)
                return false;
           
            for (int i = 0; i < FinishedObjectives.Count; i++)
            {
                if (element == FinishedObjectives[i].Replace(" NFT", "") || element == FinishedObjectives[i].Replace(" ITF", ""))
                {
                    return true;
                }
            }
            return false;
        }

        // Handles every choose one no fast travel thingy
        private string ChooseOneNoFastTravel(NewCharacterSetup newCharacterSetup)
        {
            Random rand = new Random();
            AvailavleObjectives.Remove(AvailavleObjectives.First(item => item.noFastTravel));

            NFT = new ChooseOneNoFastTravelWindow(AvailavleObjectives);
            if(!(bool)NFT.ShowDialog())
            {
                NFT.choosenObjective = AvailavleObjectives[rand.Next(0, AvailavleObjectives.Count)].objective;
            }

            return NFT.choosenObjective + " NFT";
        }
    }

}
