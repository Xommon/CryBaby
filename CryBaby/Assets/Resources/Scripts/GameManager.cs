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

    // UI
    public TextMeshProUGUI timerDisplay;

    private void Start()
    {
        secondsLeftRaw = secondsLeft;
    }

    private void Update()
    {
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
