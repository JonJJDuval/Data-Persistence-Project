using UnityEngine.UI;
using UnityEngine;
using System.IO;

public class DataHandler : MonoBehaviour

{   // Static reference to the DataHandler instance for easy access
    public static DataHandler Instance;

    // Player's name and best score data
    public string userInput = "";          
    public int bestScore = 0;   

    private void Awake()
    {
        // Load saved data when the script is awakened
        LoadText();

        // Ensure there's only one DataHandler instance           
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return; // Exit early if this isn't the first instance
        }
    }
    [System.Serializable]
    class SaveData
    {
        public string textSaved;
        public int bestScore; // Add bestScore to the SaveData class
    }
    public void SaveText()
    {
        // Create a new instance of SaveData and fill its properties
        SaveData data = new SaveData();
        data.textSaved = userInput;
        data.bestScore = bestScore;

        // Convert the data to JSON
        string json = JsonUtility.ToJson(data);

        // Write the JSON data to a file
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadText()//Reversal of SaveText method
    {
        // Load saved data from a file
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            // Update user input and best score with loaded data
            userInput = data.textSaved;
            bestScore = data.bestScore; 
        }         
    }
}
