using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    
    [HideInInspector]
    public Anims anims;

    public void RunAnimationController(Anims anim, Animator animator)
    {        
        switch (anim)
        {
            case Anims.CupFall:
                animator.SetTrigger("CupFall");
                break;
            case Anims.CupDrink:
                animator.SetTrigger("CupDrink");
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
