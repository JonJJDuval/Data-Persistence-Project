using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    // Reference to the Text component for displaying the best player's name and score
    public Text bestPlayerText;

    private void Start()
    {
        // Retrieve the best player's name and score from DataHandler
        string bestPlayerName = DataHandler.Instance.userInput;
        int bestScore = DataHandler.Instance.bestScore;

        // Check if the best score is 0 and display the appropriate text
        if (bestScore == 0)
        {
            bestPlayerText.text = "No one has the best score yet";
        }
        else
        {
            // Format the text to display the best player's name and score
            string bestPlayerInfo = $"{bestPlayerName} has the best score: {bestScore}";
            bestPlayerText.text = bestPlayerInfo;
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR        
        UnityEditor.EditorApplication.ExitPlaymode();
#else
// Quit the application (for standalone builds)
Application.Quit();
#endif
    }
    public void RebootBestScore()
    {
        // Reset the best score to 0 in DataHandler
        DataHandler.Instance.bestScore = 0;
        DataHandler.Instance.SaveText();

        // Update the Text component to display "No one has the best score yet"
        bestPlayerText.text = "No one has the best score yet";
    }
}
