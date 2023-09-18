using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainUIHandler : MonoBehaviour    
{
    public InputField inputField;

    private void Start()
    {
        // Initialize the InputField with the current best player name if available
        if (DataHandler.Instance != null)
        {
            inputField.text = DataHandler.Instance.userInput;
        }
    }

    public void Replay()
    {
        // Find the instance of MainManager
        MainManager mainManager = FindObjectOfType<MainManager>();
        
        if (mainManager != null)
        {
            // Calculate the new best score based on the current game's score
            int newBestScore = mainManager.GetCurrentScore();

            // Update the best score if it's beaten
            mainManager.UpdateBestScore(newBestScore);
        }

        // Save the player's name and best score, then reload the scene
        DataHandler.Instance.userInput = inputField.text;
        DataHandler.Instance.SaveText();
        inputField.text = ""; // Reset the InputField value
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoMenu()
    {
        // Find the instance of MainManager
        MainManager mainManager = FindObjectOfType<MainManager>();

        if (mainManager != null)
        {
            // Calculate the new best score based on the current game's score
            int newBestScore = mainManager.GetCurrentScore();

            // Update the best score if it's beaten
            mainManager.UpdateBestScore(newBestScore);
        }

        // Save the player's name and best score, then reload the scene
        DataHandler.Instance.userInput = inputField.text;
        DataHandler.Instance.SaveText();
        
        // Load the main menu scene
        SceneManager.LoadScene(0);
    }

    public void Exit()//When you click en Exit Button
    {
        // Find the instance of MainManager
        MainManager mainManager = FindObjectOfType<MainManager>();

        if (mainManager != null)
        {
            // Calculate the new best score based on the current game's score
            int newBestScore = mainManager.GetCurrentScore();

            // Update the best score if it's beaten
            mainManager.UpdateBestScore(newBestScore);
        }

        // Save the player's name and best score, then reload the scene
        DataHandler.Instance.userInput = inputField.text;
        DataHandler.Instance.SaveText();        

#if UNITY_EDITOR
        // Exit Play Mode in the Unity Editor
        UnityEditor.EditorApplication.ExitPlaymode();
#else
// Quit the application (for standalone builds)
Application.Quit();
#endif
    }   
}
