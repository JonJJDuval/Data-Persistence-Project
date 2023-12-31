using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float Speed = 2.0f;
    public float MaxMovement = 2.0f;
    public MainManager mainManager; 

    // Start is called before the first frame update
    void Start()
    {
        mainManager = FindObjectOfType<MainManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the game is not over before allowing paddle movement
        if (!mainManager.m_GameOver) // Correctly access m_GameOver through mainManager
        {
            float input = Input.GetAxis("Horizontal");

            Vector3 pos = transform.position;
            pos.x += input * Speed * Time.deltaTime;

            if (pos.x > MaxMovement)
                pos.x = MaxMovement;
            else if (pos.x < -MaxMovement)
                pos.x = -MaxMovement;

            transform.position = pos;
        }
    }
}
