using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Stats
    public int happiness;
    private float secondsRaw;
    public int[] time;
    private bool gameStarted = false;

    // UI
    public TextMeshProUGUI timerDisplay;
    public GameObject startPanel;

    public void StartGame()
    {
        gameStarted = true;
        startPanel.SetActive(false);

        // Set up clock
        timerDisplay.gameObject.SetActive(true);
        time[1] = 60 - time[1];
        time[0] = 7 - time[0];
        if (time[1] != 0)
        {
            time[0] -= 1;
        }
        if (time[1] == 60)
        {
            time[1] = 0;
        }
        secondsRaw = time[1];
    }

    private void Start()
    {
        startPanel.SetActive(true);
        timerDisplay.gameObject.SetActive(false);
        //secondsRaw = seconds;
    }

    private void Update()
    {
        // Skip the following code if the game has not started
        if (!gameStarted)
        {
            return;
        }

        // Timer
        if (time[0] < 7)
        {
            secondsRaw += Time.deltaTime;
            time[1] = Mathf.FloorToInt(secondsRaw);

            if (time[1] == 60)
            {
                time[0]++;
                secondsRaw = 0;
            }
        }
        else
        {
            time[1] = 0;
        }

        timerDisplay.text = time[0].ToString() + ":" + time[1].ToString("D2");
    }
}
