using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class sortingAlgo : MonoBehaviour
{
    


    public static List<GameObject> bars = new List<GameObject>();
    static int timer = 4;
  
    //only swaps the physical positions of a and b
    public static void swapPositions(int a, int b)
    {
        
        Vector3 tempPosition;
        tempPosition = bars[a].transform.position;
        bars[a].transform.position = new Vector3(bars[b].transform.position.x, bars[a].transform.position.y, 0);
        bars[b].transform.position = new Vector3(tempPosition.x, bars[b].transform.position.y, 0);
    }
    
    //switches both the physical placement and placement in the bars list of objects a and b
    public static void swap(int a, int b)
    {
        swapPositions(a,b);
        GameObject tempObject;
        //swaps place in bars;
        tempObject = bars[a];
        bars[a] = bars[b];
        bars[b] = tempObject;
        
       
    }

    

    //sets color, color,  of the bar at index i
    public static void setColor(int index, Color color)
    {
        bars[index].GetComponent<SpriteRenderer>().color = color;
    }

    //given an index plays the correct pitch of the marimba
    public static float getPitch(int index)
    {
        float pitch = 1 + ((float)index / ((bars.Count - 1)));
        return pitch;
    }
    //given current array, index, it returns the index of that value
    public static int getIndex(int i)
    {
        float h = bars[i].transform.localScale.y;
        return (int)Mathf.Round((((h * bars.Count) / 8f) - 1));

    }

    public static int getTimer()
    {
        return timer;

    }
    public static void setTimer(int t)
    {
        timer = t;
    }

    //checks array after algorithm is finished to see if it's sorted correctly
    public IEnumerator checkSort(Color activeColor,Color currentColor,Color defaultColor)
    {
        int tempTime = 1;
        
        audioManager am = audioManager.main;
        bool sorted = true;
        for(int i = 0; i< bars.Count-1; i++)
        {
            if(tempTime == timer)
            {
                tempTime = 1;
                yield return new WaitForSeconds(graphManager.time);
            }
            tempTime++;
            
            
            setColor(i + 1, activeColor);
            setColor(i, currentColor);
            if(bars[i].transform.localScale.y > bars[i+1].transform.localScale.y)
            {
                sorted = false;
            }
            am.play("barSound", getPitch(i));
            
        }
        am.play("barSound", 2);
        for(int i = 0; i < bars.Count; i++)
        {
            setColor(i, defaultColor);
        }
        if(sorted)
        {
            Debug.Log("sorted");
        }

    }

    




}
