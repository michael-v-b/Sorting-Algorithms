using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeUIManager : MonoBehaviour
{
    int index = 0;
    [SerializeField] TextMeshProUGUI speedText;
    string[] timeStrings = {"1x Speed", ".75x speed", ".5x Speed", ".25x Speed"};
    float[] times = { 0f,.03f, .06f, .12f}; 

    public void increment ()
    {
        if (index >= timeStrings.Length-1)
        {
            index = 0;
        } else
        {
            index++;
        }


            graphManager.setTime(times[index]);
        speedText.text = timeStrings[index];

    }

    public void decrement()
    {
        if(index <= 0)
        {
            index = timeStrings.Length - 1;
        } else
        {
            index--;
        }
        graphManager.setTime(times[index]);
        speedText.text = timeStrings[index];
    }
}
