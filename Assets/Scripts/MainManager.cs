using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    // Public variables for assigning in the Unity Inspector
    public Brick BrickPrefab; // Prefab for the bricks in the game
    public int LineCount = 6; // Number of lines of bricks
    public Rigidbody Ball; // Rigidbody of the ball

    public Text ScoreText; // Text component for displaying the current score
    public Text BestScoreText; // Text component for displaying the best score
    public GameObject GameOverText; // Game object for showing game over text
    public GameObject InputFieldNewBestScore; // Game object for inputting a new best score

    // Private variables for internal use within the script
    private bool m_Started = false; // Flag to track if the game has started
    private int m_Points; // Current score
    public int bestScore; // Best score obtained

    public bool m_GameOver = false; // Flag to track if the game is over
    private bool IsHighScore = false; // Flag to check if the current score is a high score

    // Start is called before the first frame update
    void Start()
    {      

        DataHandler.Instance.LoadText();

        // Constants for brick placement
        const float step = 0.6f; // Horizontal step between bricks
        int perLine = Mathf.FloorToInt(4.0f / step); // Number of bricks per line

        // Array of point values for each line of bricks
        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };

        // Loop to instantiate bricks and set their properties
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                // Calculate the position for each brick
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);

                // Instantiate a brick and set its properties
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint); // Add listener for when a brick is destroyed
            }
        }

        // Load the best score from DataHandler and display it
        bestScore = DataHandler.Instance.bestScore;
        string bestPlayerName = DataHandler.Instance.userInput;

        if (bestScore == 0)
        {
            BestScoreText.text = "No one has the best score yet";
        }
        else
        {
            BestScoreText.text = $"{bestPlayerName} has the best score: {bestScore}";
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;

                // Launch the ball with a random direction
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();
                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver && !IsHighScore)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Reload the scene when Spacebar is pressed after game over
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point; // Increase the score when a brick is destroyed
        ScoreText.text = $"Score : {m_Points}"; // Update the score text to display the current score
    }

    public void GameOver()
    {
        m_GameOver = true; // Set the game to be over        

        // Compare the scores and set IsHighScore accordingly
        CompareScore();

        if (IsHighScore == true)
        {
            InputFieldNewBestScore.SetActive(true); // Activate the input field for a new best score
        }
        else
        {
            GameOverText.SetActive(true); // Activate the game over text
            // When Spacebar is pressed after game over, the scene will be reloaded in the Update method.
        }
    }

    void CompareScore()
    {
        // Set currentScore to m_Points
        int currentScore = m_Points;        

        // Compare currentScore to bestScore
        IsHighScore = isHighScore(currentScore, bestScore);
    }

    public int GetCurrentScore()
    {
        return m_Points; // Return the current score
    }

    public void UpdateBestScore(int newBestScore)
    {
        if (newBestScore > bestScore)
        {
            bestScore = newBestScore;
            DataHandler.Instance.bestScore = bestScore; // Update best score in DataHandler
            DataHandler.Instance.SaveText(); // Save the updated data
        }
    }

    public bool isHighScore(int currentScore, int bestScore)
    {
        return currentScore > bestScore; // Check if the current score is higher than the best score
    }    
}
