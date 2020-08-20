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
        public string race { get; set; }
        public uint level { get; set; }
        private List<Objectives> availavleObjectives = new List<Objectives>();
        public string currentObjective { get; set; }
        public List<string> finishedObjectives;
        public List<string> acquiredSkills;
        public string followerLocation { get; set; }
        private Data data;
        private ChooseOneNoFastTravelWindow NFT;
        private bool finishedChallenge;

        public CharacterConfig()
        {
            level = 0;
            finishedObjectives = new List<string>();
            acquiredSkills = new List<string>();
        }

        // This method handles the resetting of a character
        public void CreateNewCharacter(Data data, NewCharacterSetup newCharacterSetup)
        {// Put default values in from the setup config
            this.data = data;
            level = 0;
            data.resetCount++;
            Random rand = new Random();
            finishedObjectives.Clear();
            acquiredSkills.Clear();
            race = newCharacterSetup.races[rand.Next(0, newCharacterSetup.races.Count())];
            followerLocation = newCharacterSetup.followerLocations[rand.Next(0, newCharacterSetup.followerLocations.Count())];

            GetNewSkills(newCharacterSetup, true);
            GetNewObjective(newCharacterSetup);
        }

        // This method is called whenever an objective is finished. This will get you new skills and a new objective.
        public void ObjectiveFinished(NewCharacterSetup newCharacterSetup, uint level, string currentObjective)
        {
            this.level = level;
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
            if (finishedObjectives.Count == newCharacterSetup.objectives.Count - 1)
            {
                FinishedTheChallenge();
                return;
            }
            int skillsToBeAdded = newCharacter ? (int)data.startingSkillCount : newCharacterSetup.objectives.First(item => item.objective == currentObjectiveTrimmed).skillReward;

            // Adds skills of the same amount written down in data
            if (acquiredSkills.Count != newCharacterSetup.skills.Count)
            {
                for (int i = 0; i < skillsToBeAdded; i++)
                {
                    if (acquiredSkills.Count != newCharacterSetup.skills.Count)
                    {
                        skillToBeRaw = newCharacterSetup.skills[rand.Next(0, newCharacterSetup.skills.Count())];
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

                        if (acquiredSkills.Contains(skillToBe))
                        {
                            i--;
                            continue;
                        }
                        acquiredSkills.Add(skillToBe);
                    }
                }
            }

        }

        // This method handles getting objectives
        private void GetNewObjective(NewCharacterSetup newCharacterSetup, string currentObjective = null)
        {
            if (!finishedChallenge)
            {
                availavleObjectives.Clear();
                if (currentObjective != null)
                {
                    // This loop will check every objective if the objective with the same realtion to it is finished, and will be marked with ITF (Impossible To Finish)
                    foreach(Objectives objective in newCharacterSetup.objectives)
                    {   
                        if (objective.inRelationTo == null)
                            continue;
                        if (objective.inRelationTo.Contains(currentObjective.Replace(" NFT", "")))
                            finishedObjectives.Add(objective.objective + " ITF");
                    }
                    finishedObjectives.Add(currentObjective);
                }
                // This will finish the challenge if every objective is finished. -1 is to count out no fast travel.
                if (finishedObjectives.Count == newCharacterSetup.objectives.Count - 1)
                {
                    FinishedTheChallenge();
                    return;
                }

                Random rand = new Random();
                List<Objectives> objectives = newCharacterSetup.objectives;

                // Gets the objective available for the player
                foreach (Objectives element in objectives)
                {
                    if (element.levelRequired <= level && !IsItFinished(newCharacterSetup, element.objective) && arePreviousObjectivesFinished(newCharacterSetup, element.objective))
                    {
                        availavleObjectives.Add(element);
                    }
                    else
                        continue;
                }

                // First time I ever used a do while loop.
                // Always get one objective.
                do
                {
                    Objectives objectivesRaw = availavleObjectives[rand.Next(0, availavleObjectives.Count)];
                    //Objectives objectivesRaw = newCharacterSetup.objectives[rand.Next(0, newCharacterSetup.objectives.Count())];
                    if (objectivesRaw.noFastTravel)
                        currentObjective = ChooseOneNoFastTravel(newCharacterSetup);
                    else
                        currentObjective = objectivesRaw.objective;
                } while (currentObjective == null || finishedObjectives.Contains(currentObjective));

                this.currentObjective = currentObjective;
            }
        }
        
        // Will make it so that you can't continue pressing objective complete after finishing the last one
        public void FinishedTheChallenge()
        {
            currentObjective = "You won the challenge!";
            finishedChallenge = true;
        }

        // This method will check if the previous mustHaveFinished objectives are finished.
        private bool arePreviousObjectivesFinished(NewCharacterSetup newCharacterSetup, string element)
        {                           // Relative to how many objectives must be finished
            int amountOfObjectives, amountOfFinishedObjetives = 0;

            Objectives objectiveInQuestion = newCharacterSetup.objectives.First(item => item.objective == element);

            if (objectiveInQuestion.mustHaveFinishedTheseObjectives == null)
                return true;

            amountOfObjectives = objectiveInQuestion.mustHaveFinishedTheseObjectives.Count();
            for (int i = 0; i < amountOfObjectives; i++)
            {
                if (finishedObjectives.Contains(objectiveInQuestion.mustHaveFinishedTheseObjectives[i]) || finishedObjectives.Contains(objectiveInQuestion.mustHaveFinishedTheseObjectives[i] + " ITF") || finishedObjectives.Contains(objectiveInQuestion.mustHaveFinishedTheseObjectives[i] + " NFT"))
                    amountOfFinishedObjetives++;
            }
            MessageBox.Show(objectiveInQuestion.objective + " : " + amountOfObjectives + " : " + amountOfFinishedObjetives);
            return amountOfFinishedObjetives == amountOfObjectives ? true : false;
        }

        // Will check if the objective in question is already complete
        private bool IsItFinished(NewCharacterSetup newCharacterSetup ,string element)
        {
            if (finishedObjectives.Count == 0)
                return false;
           
            for (int i = 0; i < finishedObjectives.Count; i++)
            {
                if (element == finishedObjectives[i].Replace(" NFT", "") || element == finishedObjectives[i].Replace(" ITF", ""))
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
            availavleObjectives.Remove(availavleObjectives.First(item => item.noFastTravel));

            NFT = new ChooseOneNoFastTravelWindow(availavleObjectives);
            if(!(bool)NFT.ShowDialog())
            {
                NFT.choosenObjective = availavleObjectives[rand.Next(0, availavleObjectives.Count)].objective;
            }

            return NFT.choosenObjective + " NFT";
        }
    }

}
