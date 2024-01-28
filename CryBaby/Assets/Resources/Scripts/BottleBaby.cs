using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BottleBaby : MonoBehaviour
{
    [SerializeField]
    private ClickToInteract clickToInteract;
    [SerializeField]
    private Transform baby;
    private Vector2 currentPOS;
    private Vector2 babyMouthPOS;
    private Quaternion bottleRotation;

    private void OnMouseDown()
    {
        StartBottleFeedingBaby();
    }

    private void OnMouseUp()
    {
        StopBottleFeedingBaby();
    }

    private void Start()
    {
        currentPOS = this.transform.position;
        bottleRotation = this.transform.rotation;
        babyMouthPOS = baby.position;
    }

    private void StartBottleFeedingBaby()
    {
        this.transform.position = babyMouthPOS;
        clickToInteract.gameManager.Interaction((int)ClickToInteract.Interaction.BottleIn);
        clickToInteract.RunAnimation();
    }

    private void StopBottleFeedingBaby()
    {
        clickToInteract.anim.Stop();
        this.transform.position = currentPOS;
        this.transform.rotation = bottleRotation;
    }
}
