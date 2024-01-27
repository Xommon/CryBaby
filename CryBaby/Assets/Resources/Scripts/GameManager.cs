using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int happiness = 25;
    private float secondsLeftRaw;
    public int secondsLeft;

    private void Start()
    {
        secondsLeftRaw = secondsLeft;
    }

    private void Update()
    {
        if (secondsLeft > 0)
        {
            secondsLeftRaw -= Time.deltaTime;
            secondsLeft = Mathf.CeilToInt(secondsLeftRaw);
        }
        else
        {
            secondsLeft = 0;
        }
    }
}
