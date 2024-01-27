using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Runtime.InteropServices;

public class GameManager : MonoBehaviour
{
    // Game Stats
    private float secondsRaw;
    public int[] time;
    private bool gameStarted = false;

    // Baby Stats
    public int happiness;
    public int hunger;
    private int[] interactionCharges;
    private int lastSecond;
    private bool bottle = false;
    // 0 = Bottle In
    // 1 = Bottle Out

    // 1 = Bang pot
    // 2 = Clown toy
    // 3 = Hand puppet
    // 4 = Rattle
    // 5 = Silly face
    // 6 = Cat
    // 7 = Matches
    // 8 = Nap

    // UI
    public TextMeshProUGUI timerDisplay;
    public GameObject startPanel;
    public GameObject endPanel;
    private CanvasGroup endPanelCanvasGroup;
    public TextMeshProUGUI endPanelText;
    public int messageIndex;

    // Test
    public TMP_InputField inputfield;

    private void Start()
    {
        startPanel.SetActive(true);
        endPanel.SetActive(false);
        timerDisplay.gameObject.SetActive(false);
        endPanelCanvasGroup = endPanel.GetComponent<CanvasGroup>();
        endPanelCanvasGroup.alpha = 0;
    }

    private void Update()
    {
        // Fade in end panel
        if (!gameStarted && time[0] == 7)
        {
            endPanel.SetActive(true);
            endPanelCanvasGroup.alpha += 0.01f;
        }

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
            // Game over
            time[1] = 0;
            EndGame();
        }

        timerDisplay.text = time[0].ToString() + ":" + time[1].ToString("D2");

        // Baby stats
        if (float.Parse((secondsRaw - 0.01f).ToString("F2")) % 30 == 0)
        {
            hunger--;
        }

        if (bottle && lastSecond != time[1])
        {
            hunger++;
        }

        // Time based checks
        lastSecond = time[1];
    }
    public void StartGame()
    {
        gameStarted = true;
        startPanel.SetActive(false);

        // Set baby stats
        happiness = 30;
        hunger = Random.Range(0, 11);
        bottle = false;

        // Set up clock
        timerDisplay.gameObject.SetActive(true);
        time[1] = 60 - time[1];
        time[0] = 7 - time[0];
        if (time[1] != 0)
        {
            time[0] -= 1;
        }
        if (time[1] >= 60)
        {
            time[1] = 0;
        }
        secondsRaw = time[1];
    }

    private void EndGame()
    {
        gameStarted = false;

        if (happiness < 50)
        {
            PrintMessage("You had all day with the baby and they're still crying?\nMaybe you should consider a different career.");
        }
        else if (happiness < 100)
        {
            PrintMessage("Thank you for taking care of our baby.");
        }
        else
        {
            PrintMessage("Thank you for taking such great care of our baby!\nHere's a big tip for all your hard work!");
        }
    }

    public void PrintMessage(string message)
    {
        messageIndex = 0;
        endPanelText.text = "\"\"";
        StartCoroutine(PrintLetter(message));
    }

    IEnumerator PrintLetter(string message)
    {
        endPanelText.text = endPanelText.text.Insert(1 + messageIndex, message.Substring(messageIndex, 1));
        yield return new WaitForSeconds(0.05f);
        messageIndex++;

        if (messageIndex != message.Length)
        {
            StartCoroutine(PrintLetter(message));
        }
    }

    // Baby Interactions
    public void Interaction()
    {
        int interactionIndex = int.Parse(inputfield.text);
        Interaction(interactionIndex);

    }

    public void Interaction(int index)
    {
        switch (index)
        {
            case 0:
                bottle = true;
                break;
            case 1:
                bottle = false;
                break;
        }
    }
}
