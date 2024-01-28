using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBaby : MonoBehaviour
{

    private bool isRocking = false;
    private Vector2 babyStartPOS;

    private void OnMouseDown()
    {
        StartRockingBaby();
    }

    private void OnMouseUp()
    {
        Debug.Log("Stop Rocking! Mouse Up");
        StopRockingBaby();
    }

    private void Start()
    {
        babyStartPOS = this.transform.position;
    }

    private void Update()
    {
        if (isRocking)
        {
            Vector2 cursorPOS = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = new Vector2(cursorPOS.x, this.transform.position.y);
        }
    }

    private void StartRockingBaby()
    {
        //babyStartPOS.position = this.transform.position;
        isRocking = true;
    }

    private void StopRockingBaby()
    {
        Debug.Log("Stop Rocking function");
        isRocking = false;
        this.transform.position = babyStartPOS;
    }

}
