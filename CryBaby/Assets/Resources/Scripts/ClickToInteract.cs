using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToInteract : MonoBehaviour
{

    public GameManager gameManager;
    [SerializeField]
    private Interaction interactionIndex;
    [HideInInspector]
    public Animation anim;
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

    public void RunAnimation()
    {
        anim.Play();
    }

    public enum Interaction
    {
        BottleIn, BottleOut, Burp, Nap, ChangeDiaper, Rock, StopRocking, BangPot, ClownToy, HandPuppet, Rattle, SillyFace, Cat, Matches, Cup
    }
}
