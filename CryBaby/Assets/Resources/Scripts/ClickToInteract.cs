using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToInteract : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private Interaction interactionIndex;
    private Animation anim;
    private Animator animator;

    private void Start()
    {
        if (GetComponent<Animation>() != null)
            anim = GetComponent<Animation>();
        if (GetComponent <Animator>() != null)
            animator = GetComponent<Animator>();
    }

    private void OnMouseUp()
    {
        CallInteractionOnGameManager();
        if (GetComponent<Animation>() != null)
            RunAnimation();
    }

    private void CallInteractionOnGameManager()
    {
        if (GetComponent<Animator>() != null)
        {
            gameManager.Interaction((int)interactionIndex, animator);
        }
        else
        {
            gameManager.Interaction((int)interactionIndex);
        }
            
    }

    private void RunAnimation()
    {
        Debug.Log("Run");
        anim.Play();
    }

    private enum Interaction
    {
        BottleIn, BottleOut, Burp, Nap, ChangeDiaper, Rock, StopRocking, BangPot, ClownToy, HandPuppet, Rattle, SillyFace, Cat, Matches, Cup
    }
}
