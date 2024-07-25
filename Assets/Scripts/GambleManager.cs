using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class GambleManager : MonoBehaviour
{
    public static GambleManager instance;
    public Slot[] slots;
    public Button gambleButton;
    public int gambleCost = 30;
    void Awake()
    {
        instance = this;
    }

    public void Gamble()
    {
        if(PlayManager.instance.CanBuy(gambleCost))
        {
            PlayManager.instance.UseGold(gambleCost);
            gambleButton.interactable = false;
            StartCoroutine("RollSlot");
        }
    }
    IEnumerator RollSlot()
    {
        for(int i=0; i<10; i++)
        {
            foreach(Slot slot in slots)
            {
                slot.ChangeImage(UnityEngine.Random.Range(0,10));
            }
            yield return new WaitForSeconds(0.1f);
        }
        
        CalculateResult();
        gambleButton.interactable = true;
    }
    void CalculateResult()
    {
        List<int> slotResult = new List<int>();
        for(int i=0; i<slots.Length; i++)
        {
            slotResult.Add(slots[i].GetNumber());
        }

        int bombCount = 0;
        int appCount = 0;
        int starCount = 0;
        int sevenCount = 0;
        foreach(int num in slotResult)
        {
            if(num == 0)
                bombCount++;
            else if(num == 1)
                appCount++;
            else if(num == 2)
                starCount++;
            else if(num == 3)
                sevenCount++;
        }

        if(bombCount == 3)
        {
            PlayManager.instance.EarnGold(-200);
        }
        else if(bombCount == 2)
        {
            PlayManager.instance.EarnGold(-50);
        }
        else if(appCount == 3)
        {
            PlayManager.instance.EarnGold(50);
        }
        else if(appCount == 2)
        {
            PlayManager.instance.EarnGold(25);
        }
        else if(starCount == 3)
        {
            PlayManager.instance.EarnGold(1000);
        }
        else if(starCount == 2)
        {
            PlayManager.instance.EarnGold(100);
        }
        else if(sevenCount == 3)
        {
            PlayManager.instance.EarnGold(3000);
        }
        else if(sevenCount == 2)
        {
            PlayManager.instance.EarnGold(550);
        }
    }
}
