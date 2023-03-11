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
            CurrentLevel.Text = jsonReaderWriter.jsonCharacterConfig.Level.ToString();

            CurrentObjective.Text = jsonReaderWriter.jsonCharacterConfig.CurrentObjective;
            CurrentRace.Text = jsonReaderWriter.jsonCharacterConfig.Race;
            CurrentFollower.Text = jsonReaderWriter.jsonCharacterConfig.FollowerLocation;
            AlternativeStartText.Text = jsonReaderWriter.jsonCharacterConfig.AlternativeStartLocation;

            foreach (string element in jsonReaderWriter.jsonCharacterConfig.AcquiredSkills)
            {
                CurrentSkills.Text += element + "\n";
            }
            for (int i = 0; i < jsonReaderWriter.jsonCharacterConfig.FinishedObjectives.Count(); i++)
            {
                FinishedObjectives.Text += jsonReaderWriter.jsonCharacterConfig.FinishedObjectives[i];
                if (i != jsonReaderWriter.jsonCharacterConfig.FinishedObjectives.Count() - 1)
                    FinishedObjectives.Text += "\n";
            }

            // Will fill in the raw text
            RawText.Text += jsonReaderWriter.jsonCharacterConfig.Race + "\n--------------\n" + jsonReaderWriter.jsonCharacterConfig.CurrentObjective + "\n--------------\n";
            for (int i = 0; i < jsonReaderWriter.jsonCharacterConfig.FinishedObjectives.Count(); i++)
            {
                RawText.Text += jsonReaderWriter.jsonCharacterConfig.FinishedObjectives[i];
                if (i != jsonReaderWriter.jsonCharacterConfig.FinishedObjectives.Count() - 1)
                    RawText.Text += "\n";
            }
            RawText.Text += "\n--------------\n";
            for (int i = 0; i < jsonReaderWriter.jsonCharacterConfig.AcquiredSkills.Count(); i++)
            {
                RawText.Text += jsonReaderWriter.jsonCharacterConfig.AcquiredSkills[i];
                if (i != jsonReaderWriter.jsonCharacterConfig.AcquiredSkills.Count() - 1)
                    RawText.Text += "\n";
            }
            RawText.Text += "\n--------------\n" + jsonReaderWriter.jsonCharacterConfig.FollowerLocation;
        }

        private void AlternativeStart_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NewCharacter_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
