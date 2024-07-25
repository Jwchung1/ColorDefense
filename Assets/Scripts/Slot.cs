using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Slot : MonoBehaviour
{
    public Sprite[] slotImages;
    public Sprite initialImage;
    private int number;
    private Image slotImage;
    private int[] probability;
    void Start()
    {
        slotImage = GetComponent<Image>();
        probability = new int[]{3,7,9,10};
    }
    public int GetNumber()
    {
        return number;        
    }
    public void ChangeImage(int symbolNum)
    {
        if(symbolNum < probability[0])
        {
            number = 0;
        }
        else if(symbolNum >= probability[0] && symbolNum < probability[1])
        {
            number = 1;
        }
        else if(symbolNum >= probability[1] && symbolNum < probability[2])
        {
            number = 2;
        }
        else if(symbolNum >= probability[2] && symbolNum < probability[3])
        {
            number = 3;
        }
        /* slotImages
        0: Bomb
        1: Apple
        2: Star
        3: Seven
        */
        if(symbolNum == -1)
        {
            slotImage.sprite = initialImage;
        }
        else if(symbolNum == 0)
        {
            slotImage.sprite = slotImages[0];
        }
        else if(symbolNum == 1)
        {
            slotImage.sprite = slotImages[1];
        }
        else if(symbolNum == 2)
        {
            slotImage.sprite = slotImages[2];
        
        }
        else if(symbolNum == 3)
        {
            slotImage.sprite = slotImages[3];
        }
    }  
}
