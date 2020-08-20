using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Skyrim_challenge.cs
{
    class JsonReaderWriter
    {
        Data data = new Data();
        Data jsonData;
        public string dataPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + @"\data.json";

        public string characterConfigPath { get; private set; }
        CharacterConfig characterConfig = new CharacterConfig();        // Basically only used for the default
        public CharacterConfig jsonCharacterConfig;    // Made to read from the created file

        public string setupConfigPath { get; private set; }
        NewCharacterSetup newCharacterSetup = new NewCharacterSetup();
        public NewCharacterSetup jsonNewCharacterSetup;

        public string alternativeSetupConfigPath { get; private set; }

        public JsonReaderWriter()
        {
            setupConfigPath = data.setupConfigPath + @"\setupConfig.json";
            alternativeSetupConfigPath = data.setupConfigPath + @"\alternativeSetupConfig.json";
            characterConfigPath = data.characterConfigPath;
        }

        // Set the setupconfig file path
        public void SetSetupFilePath(string path)
        {
            setupConfigPath = path;
        }

        // Set the character files path
        public void SetcharacterFilePath(string path)
        {
            characterConfigPath = path;
        }

        // Creates the data json file where tha paths are stored
        public void CreateDefaultDataFile() 
        {
            data.CreateDefaultData();
            File.WriteAllText(dataPath, JsonConvert.SerializeObject(data, Formatting.Indented));
        }

        // Create the default character json file of the path if it does not exists
        public void CreateNewCharacter()  
        {
            characterConfig.CreateNewCharacter(jsonData, jsonNewCharacterSetup);
            File.WriteAllText(characterConfigPath, JsonConvert.SerializeObject(characterConfig, Formatting.Indented));
            jsonCharacterConfig = JsonConvert.DeserializeObject<CharacterConfig>(File.ReadAllText(characterConfigPath));
            File.WriteAllText(dataPath, JsonConvert.SerializeObject(jsonData, Formatting.Indented));
            jsonData = JsonConvert.DeserializeObject<Data>(File.ReadAllText(dataPath));
        }

        // Create the default setup json file of the path if it does not exists.
        public void CreateDefaultSetupConfigFile()  
        {
            if (!File.Exists(setupConfigPath))
            {
                newCharacterSetup.setDefaultSettings();
                // Skriver ut alla properties i objektet. Formaterar det, och även ignorerar alla default värden
                File.WriteAllText(setupConfigPath, JsonConvert.SerializeObject(newCharacterSetup, Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }));
            }
            if (!File.Exists(alternativeSetupConfigPath))
            {
                newCharacterSetup.setDefaultSettings(true);
                File.WriteAllText(alternativeSetupConfigPath, JsonConvert.SerializeObject(newCharacterSetup, Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }));
            }
        }

        // Objective is complete. So give another
        public void ObjectiveComplete(uint level, string currentObjective)
        {
            characterConfig.ObjectiveFinished(jsonNewCharacterSetup, level, currentObjective);
            File.WriteAllText(characterConfigPath, JsonConvert.SerializeObject(characterConfig, Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }));
            jsonCharacterConfig = JsonConvert.DeserializeObject<CharacterConfig>(File.ReadAllText(characterConfigPath));
        }

        // Load this method when starting the application! This will load in all the paths.
        public void DeserializeJsons()
        {
            // Creates an object of the same class with all the information read from the file.
            // Create an object of the Data class, with the information inside data.json
            jsonData = JsonConvert.DeserializeObject<Data>(File.ReadAllText(dataPath));

            characterConfigPath = jsonData.characterConfigPath;
            setupConfigPath = jsonData.setupConfigPath;

            if (!File.Exists(setupConfigPath) || !File.Exists(alternativeSetupConfigPath))
                CreateDefaultSetupConfigFile();
            jsonNewCharacterSetup = JsonConvert.DeserializeObject<NewCharacterSetup>(File.ReadAllText(setupConfigPath));

            if (!File.Exists(characterConfigPath))
                CreateNewCharacter();
            jsonCharacterConfig = JsonConvert.DeserializeObject<CharacterConfig>(File.ReadAllText(characterConfigPath));


        }

    }
}
