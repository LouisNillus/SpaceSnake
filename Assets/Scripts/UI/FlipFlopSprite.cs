using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipFlopSprite : MonoBehaviour
{

    public Image image;

    public Sprite A;
    public Sprite B;

    bool isA = true;

    public void SwapSprite()
    {
        if (isA)
        {
            image.sprite = B;
            isA = false;
        }
        else
        {
            image.sprite = A;
            isA = true;
        }
    }
}
