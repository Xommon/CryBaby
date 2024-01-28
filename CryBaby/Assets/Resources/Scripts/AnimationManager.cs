using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    
    [HideInInspector]
    public Anims anims;
    [SerializeField]
    private AudioManager audioManager;

    public void RunAnimationController(Anims anim, Animator animator)
    {        
        switch (anim)
        {
            case Anims.CupFall:
                animator.SetTrigger("CupFall");
                audioManager.Play("CupFall");
                break;
            case Anims.CupDrink:
                animator.SetTrigger("CupDrink");
                audioManager.Play("CupDrink");
                break;
            case Anims.StartNap:
                animator.SetTrigger("StartNap");
                break;
            case Anims.WakeUp:
                animator.SetTrigger("WakeUp");
                break;
            default:
                break;
        }
    }

    public enum Anims
    {
        CupFall, CupDrink, StartNap, WakeUp
    }
}
