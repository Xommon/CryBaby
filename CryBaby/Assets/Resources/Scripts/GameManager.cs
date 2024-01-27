using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Stats
    public int happiness = 25;
    private float secondsLeftRaw;
    public int secondsLeft;
    private bool gameStarted = false;

    // UI
    public TextMeshProUGUI timerDisplay;
    public GameObject startPanel;

    public void StartGame()
    {
        gameStarted = true;
        startPanel.SetActive(false);
    }

    private void Start()
    {
        secondsLeftRaw = secondsLeft;
    }

    private void Update()
    {
        // Skip the following code if the game has not started
        if (!gameStarted)
        {
            return;
        }

        // Timer
        if (secondsLeft > 0)
        {
            secondsLeftRaw -= Time.deltaTime;
            secondsLeft = Mathf.CeilToInt(secondsLeftRaw);
        }
        else
        {
            secondsLeft = 0;
        }

        timerDisplay.text = secondsLeft.ToString();
    }
}
