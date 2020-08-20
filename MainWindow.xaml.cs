using Skyrim_challenge.cs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SkyrimChallenge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        JsonReaderWriter jsonReaderWriter = new JsonReaderWriter();

        public MainWindow()
        {
            InitializeComponent();

            if (!File.Exists(jsonReaderWriter.dataPath))
                jsonReaderWriter.CreateDefaultDataFile();
            jsonReaderWriter.DeserializeJsons();

            NewCharacter.Click += NewCharacter_ButtonClick;
            LoadCharacter.Click += LoadCharacter_ButtonClick;
            ObjectiveFinished.Click += ObjectiveFinished_ButtonClick;
            
        }

        private void NewCharacter_ButtonClick(object sender, RoutedEventArgs e)
        {   // Will create a new character by calling a method inside jsonreaderwriter. And stuff.
            jsonReaderWriter.CreateNewCharacter();
            WriteToTextBoxes();

        }

        private void LoadCharacter_ButtonClick(object sender, RoutedEventArgs e)
        {
            WriteToTextBoxes();
        }

        public void ObjectiveFinished_ButtonClick(object sender, RoutedEventArgs e)
        {
            jsonReaderWriter.ObjectiveComplete(Convert.ToUInt32(CurrentLevel.Text), CurrentObjective.Text);
            WriteToTextBoxes();
        }

        private void WriteToTextBoxes()
        {

            jsonReaderWriter.DeserializeJsons();

            RawText.Text = "";
            CurrentSkills.Text = "";
            FinishedObjectives.Text = "";
            CurrentLevel.Text = jsonReaderWriter.jsonCharacterConfig.level.ToString();

            CurrentObjective.Text = jsonReaderWriter.jsonCharacterConfig.currentObjective;
            CurrentRace.Text = jsonReaderWriter.jsonCharacterConfig.race;
            CurrentFollower.Text = jsonReaderWriter.jsonCharacterConfig.followerLocation;

            foreach (string element in jsonReaderWriter.jsonCharacterConfig.acquiredSkills)
            {
                CurrentSkills.Text += element + "\n";
            }
            for (int i = 0; i < jsonReaderWriter.jsonCharacterConfig.finishedObjectives.Count(); i++)
            {
                FinishedObjectives.Text += jsonReaderWriter.jsonCharacterConfig.finishedObjectives[i];
                if (i != jsonReaderWriter.jsonCharacterConfig.finishedObjectives.Count() - 1)
                    FinishedObjectives.Text += "\n";
            }

            // Will fill in the raw text
            RawText.Text += jsonReaderWriter.jsonCharacterConfig.race + "\n--------------\n" + jsonReaderWriter.jsonCharacterConfig.currentObjective + "\n--------------\n";
            for (int i = 0; i < jsonReaderWriter.jsonCharacterConfig.finishedObjectives.Count(); i++)
            {
                RawText.Text += jsonReaderWriter.jsonCharacterConfig.finishedObjectives[i];
                if (i != jsonReaderWriter.jsonCharacterConfig.finishedObjectives.Count() - 1)
                    RawText.Text += "\n";
            }
            RawText.Text += "\n--------------\n";
            for (int i = 0; i < jsonReaderWriter.jsonCharacterConfig.acquiredSkills.Count(); i++)
            {
                RawText.Text += jsonReaderWriter.jsonCharacterConfig.acquiredSkills[i];
                if (i != jsonReaderWriter.jsonCharacterConfig.acquiredSkills.Count() - 1)
                    RawText.Text += "\n";
            }
            RawText.Text += "\n--------------\n" + jsonReaderWriter.jsonCharacterConfig.followerLocation;
        }
    }
}
