using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverHighlight : MonoBehaviour
{
    [SerializeField]
    private Material highlightMaterial;
    private Material defaultMaterial;
    [SerializeField]
    private SpriteRenderer[] highlightedSprites;

    void OnMouseEnter()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        defaultMaterial = highlightedSprites[0].material;
        for (int i = 0; i < highlightedSprites.Length; i++)
        {
            highlightedSprites[i].material = highlightMaterial;
        }
        
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        for (int i = 0; i < highlightedSprites.Length; i++)
        {
            highlightedSprites[i].material = defaultMaterial;
        }
    }
}
