using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToInteract : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private Interaction interactionIndex;

    private void OnMouseUp()
    {
        gameManager.Interaction((int)interactionIndex);
    }

    private enum Interaction
    {
        BottleIn, BottleOut, Burp, Nap, ChangeDiaper, Rock, StopRocking, BangPot, ClownToy, HandPuppet, Rattle, SillyFace, Cat, Matches, Cup
    }
}
