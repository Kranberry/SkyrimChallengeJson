using Skyrim_challenge.cs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SkyrimChallenge
{
    /// <summary>
    /// Interaction logic for ChooseOneNoFastTravel.xaml
    /// </summary>
    public partial class ChooseOneNoFastTravelWindow: Window
    {
        private List<Button> buttons = new List<Button>();
        private List<Objectives> objectives = new List<Objectives>();
        public string choosenObjective { get; set; }

        public ChooseOneNoFastTravelWindow(List<Objectives> objectives)
        {
            this.objectives = objectives;
            InitializeComponent();
            AddChoices();

        }

        public void AddChoices()
        {
            for (int i = 0; i < objectives.Count; i++)
            {
                buttons.Add(new Button());
                buttons[i].Content = objectives[i].objective;
                buttons[i].IsEnabled = true;
                buttons[i].Background = Brushes.LightGray;
                buttons[i].BorderThickness = new Thickness(0.5);
                buttons[i].BorderBrush = Brushes.DarkSlateGray;
                buttons[i].FontWeight = FontWeights.Bold;
                buttons[i].Click += ButtonClick;
                stackPanel.Children.Add(buttons[i]);
            }
        }

        public void ButtonClick(object sender, RoutedEventArgs e)
        {
            choosenObjective = (string)((Button)sender).Content;
            this.DialogResult = true;
            this.Close();
        }
    }
}
