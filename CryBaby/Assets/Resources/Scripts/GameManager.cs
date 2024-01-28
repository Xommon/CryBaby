using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Runtime.InteropServices;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    // Game Stats
    private float secondsRaw;
    public int[] time;
    private bool gameStarted = false;
    private int totalScore;

    // Baby Stats
    public int happiness;
    public int hunger;
    public int energy;
    public int potty;
    private int lastSecond;
    private bool bottle = false;
    private bool nap = false;
    private bool burped = false;
    private bool rocking = false;

    // Interactions
    //private int lastInteraction;
    public int[] interactionCharges;
    public int[] interactionCoolDowns;
    private Vector3 lastMousePos;
    public float mouseSpeed;

    // 0 = Bottle In
    // 1 = Bottle Out
    // 2 = Burp
    // 3 = Nap
    // 4 = Change diaper
    // 5 = Start rocking
    // 6 = Stop rocking
    // 7 = Bang pot
    // 8 = Clown toy
    // 9 = Hand puppet
    // 10 = Rattle
    // 11 = Silly face
    // 12 = Cat
    // 13 = Matches
    // 14 = Cup

    // UI
    public TextMeshProUGUI timerDisplay;
    public GameObject startPanel;
    public GameObject endPanel;
    private CanvasGroup endPanelCanvasGroup;
    public TextMeshProUGUI endPanelText;
    private int messageIndex;
    public TextMeshProUGUI totalScoreText;

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
        // Cap stats
        if (happiness < 0)
        {
            happiness = 0;
        }
        if (happiness > 100)
        {
            happiness = 100;
        }
        if (hunger < 0)
        {
            hunger = 0;
        }
        if (energy < 0)
        {
            energy = 0;
        }
        if (potty < 0)
        {
            potty = 0;
        }

        // Mouse speed
        mouseSpeed = Vector3.Distance(lastMousePos, Input.mousePosition);
        lastMousePos = Input.mousePosition;

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
        if (float.Parse((secondsRaw - 0.01f).ToString("F2")) % 10 == 0)
        {
            hunger--;
            energy--;
            potty--;
        }

        // Check only every 1 whole second
        if (lastSecond != time[1])
        {
            // Bottle
            if (bottle)
            {
                hunger++;

                if (hunger == 10)
                {
                    burped = false;
                }
            }

            // Nap
            if (nap)
            {
                energy++;

                if (energy == 10)
                {
                    nap = false;
                    Animator animator = GameObject.FindGameObjectWithTag("Blanket").GetComponent<Animator>();
                    GetComponent<AnimationManager>().RunAnimationController(AnimationManager.Anims.WakeUp, animator);
                }
            }

            // Happiness decreases with hunger or energy are at 0
            if (hunger == 0 || energy == 0 || hunger > 10 || potty == 0)
            {
                happiness--;
            }

            // Decrease cooldowns
            for (int i = 0; i < interactionCoolDowns.Length; i++)
            {
                if (interactionCoolDowns[i] > 0)
                {
                    interactionCoolDowns[i]--;
                }
            }

            // Rocking
            if (rocking)
            {
                interactionCoolDowns[5]++;

                if (interactionCoolDowns[5] > 10 || mouseSpeed > 7)
                {
                    happiness -= 5;
                }
                else if (mouseSpeed > 3)
                {
                    happiness += 5;
                }
            }
            else if (!rocking && interactionCoolDowns[5] > 0)
            {
                interactionCoolDowns[5]--;
            }
        }

        // Time based checks
        lastSecond = time[1];
    }
    public void StartGame()
    {
        // Set game stats
        gameStarted = true;
        startPanel.SetActive(false);
        totalScore = 0;
        totalScoreText.gameObject.SetActive(false);

        // Set baby stats
        happiness = 30;
        hunger = Random.Range(0, 11);
        energy = Random.Range(0, 11);
        potty = 10;
        bottle = false;
        nap = false;
        burped = false;
        rocking = false;
        int positiveCharges = 0;
        int negativeCharges = 0;
        for (int i = 7; i < interactionCharges.Length; i++)
        {
            int chargeBias = 0;
            if (positiveCharges == (interactionCharges.Length - 7) / 2)
            {
                chargeBias = -1;
            }
            else if (negativeCharges == (interactionCharges.Length - 7) / 2)
            {
                chargeBias = 1;
            }

            if (chargeBias == 0)
            {
                if (Random.Range(0, 2) == 0)
                {
                    chargeBias = 1;
                }
                else
                {
                    chargeBias = -1;
                }
            }

            interactionCharges[i] = Random.Range(1, 3) * chargeBias * 10;
        }

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

        if (hunger > 10)
        {
            // Remove points for overfeeding
            hunger -= (hunger - 10);
        }

        totalScore = (happiness + hunger + energy + potty);

        if (totalScore > 100)
        {
            totalScore = 100;
        }

        // Parents' feedback
        if (totalScore < 60)
        {
            PrintMessage("You had all day with the baby and they're still crying?\nMaybe you should consider a different career.");
        }
        else if (totalScore < 80)
        {
            PrintMessage("Thank you for taking care of our baby.");
        }
        else
        {
            PrintMessage("You're a natural with babies!\nHere's a big tip for all your hard work!");
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
        else
        {
            yield return new WaitForSeconds(1);
            totalScoreText.text = totalScore.ToString() + "%";
            totalScoreText.gameObject.SetActive(true);
        }
    }

    // Baby Interactions
    public void Interaction()
    {
        int interactionIndex = int.Parse(inputfield.text);
        Interaction(interactionIndex);
        inputfield.text = "";
    }

    public void Interaction(int index, Animator animator = null) //Added optional arguments for unique animations - Jason
    {
        if (index == 0)
        {
            // Bottle in
            bottle = true;
        }
        else if (index == 1)
        {
            // Bottle out
            bottle = false;
        }
        else if (index == 2)
        {
            // Burp
            if (hunger > 9 && !burped)
            {
                happiness += 10;
                burped = true;
            }
        }
        else if (index == 3)
        {
            // Nap
            if (!nap)
            {
                nap = true;
                GetComponent<AnimationManager>().RunAnimationController(AnimationManager.Anims.StartNap, animator);
            }
            else
            {
                nap = false;
                GetComponent<AnimationManager>().RunAnimationController(AnimationManager.Anims.WakeUp, animator);
                //Decrease hapiness
            }
        }
        else if (index == 4)
        {
            // Change diaper
            potty = 10;
        }
        else if (index == 5)
        {
            // Rock
            rocking = true;
            interactionCoolDowns[5] = 0;
            lastMousePos = Input.mousePosition;
        }
        else if (index == 6)
        {
            // Stop rocking
            rocking = false;
        }
        //Added by Jason for cup interaction
        else if (index == 14)
        {
            if (Randomizer(1, 100) > 66)
            {
                GetComponent<AnimationManager>().RunAnimationController(AnimationManager.Anims.CupFall, animator);
            }
            else
            {
                GetComponent<AnimationManager>().RunAnimationController(AnimationManager.Anims.CupDrink, animator);
            }
            
        }
        else
        {
            // All other interactions
            if (interactionCoolDowns[index] == 0)
            {
                happiness += interactionCharges[index];
            }
            else
            {
                // Annoys the baby when you repeat interactions too quickly
                happiness -= 5;
            }

            if (interactionCharges[index] > 0)
            {
                interactionCoolDowns[index] = 20;
            }
        }
    }

    private int Randomizer(int min, int max)
    {
        int randomInt = Random.Range(min, max + 1);  
        
        return randomInt;
    }
}
