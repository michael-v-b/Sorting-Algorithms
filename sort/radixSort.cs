using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class radixSort : sortingAlgo
{
    public Color activeColor = Color.red;
    public Color currentColor = Color.green;
    public Color defaultColor = Color.white;
    audioManager am;
    int currentRefillIndex = 0;
    bool[] visited;
    GameObject[] tempCount;
    bool started = false;
    int timer = 1;
    private void OnEnable()
    {
        started = false;
        graphManager.graphMngr.setMax(500);
        graphManager.graphMngr.setActive(activeColor);
        graphManager.graphMngr.setCurrent(currentColor);
        graphManager.graphMngr.setDefault(defaultColor);
        am = audioManager.main;

        sortingEvent.current.sortStarted += sort;
    }
    private void OnDisable()
    {


        resettempCount();
        started = false;
        sortingEvent.current.sortStarted -= sort;


        Debug.Log("radixGets disabled");


    }

    void sort()
    {



        if (!started && !graphManager.beingRando)
        {
            currentRefillIndex = 0;
            visited = new bool[bars.Count];
            tempCount = new GameObject[bars.Count];
            StartCoroutine(count());
        }
    }

    IEnumerator count()
    {
        started = true;



        int size = bars.Count + 1;
        int iterations = 0;
        int[] counts = new int[10];
        int currDigit;
        // establishes the amount of iterations I need for sort
        while (size >= 1)
        {
            size /= 10;
            iterations++;

        }

        for (int i = 1; i <= iterations; i++)
        {
            //reset counts
            for (int j = 0; j < counts.Length; j++)
            {
                counts[j] = 0;
            }

            //counts values from 0-9 in the ith digit
            for (int j = 0; j < bars.Count; j++)
            {
                //stops program if randomized
                if (graphManager.beingRando)
                {
                    Debug.Log("checking");
                    started = false;
                    resettempCount();
                    yield break;
                }

                setColor(j, activeColor);
                am.play("barSound", getPitch(getIndex(j)));
                if (timer == getTimer())
                {
                    timer = 1;
                    yield return new WaitForSeconds(graphManager.time);
                }
                timer++;
                currDigit = findDigit(j, i);

                counts[currDigit]++;
                setColor(j, defaultColor);
            }


            counts = adjustCountsArr(counts);






            // fill tempCount array
            for (int j = 0; j < bars.Count; j++)
            {
                currentRefillIndex = j;
                currDigit = findDigit(j, i);
                tempCount[counts[currDigit]] = bars[j];
                tempCount[counts[currDigit]].GetComponent<SpriteRenderer>().color = currentColor;
                counts[currDigit]++;

            }


            //refill main array
            for (int j = 0; j < tempCount.Length; j++)
            {
                if (timer == getTimer())
                {
                    timer = 1;
                    yield return new WaitForSeconds(graphManager.time);
                }
                timer++;
                //stops program if randomized
                if (graphManager.beingRando)
                {

                    started = false;
                    resettempCount();
                    yield break;
                }
                if (tempCount[j] == null)
                {
                    Debug.Log("tempCount is null");
                }
                bars[j] = tempCount[j];
                //puts bar j in proper position related to index
                if (bars[j] != null)
                {
                    bars[j].transform.position = new Vector2(graphManager.graphMngr.getPos(j), bars[j].transform.position.y);
                }
                else
                {
                    Debug.Log("why is this null now");
                }

                setColor(j, defaultColor);
                tempCount[j] = null;
            }

        }

        yield return checkSort(activeColor, currentColor, defaultColor);
        sortingEvent.sorting = false;
        started = false;
    }

    void resettempCount()
    {
        for (int j = 0; j < tempCount.Length; j++)
        {
            if (tempCount[j] == null)
            {
                continue;
            }
            bars[j] = tempCount[j];
            //sets position
            bars[j].transform.position = new Vector2(graphManager.graphMngr.getPos(j), bars[j].transform.position.y);

            setColor(j, defaultColor);
            tempCount[j] = null;

        }
    }


    void printArray(int[] counts)
    {
        String output = "";
        for (int i = 0; i < counts.Length; i++)
        {
            output += $" {i}: {counts[i]} ";
        }
        Debug.Log(output);
    }


    int[] adjustCountsArr(int[] counts)
    {

        int sum = 0;
        //set array to sums
        for (int i = 0; i < counts.Length; i++)
        {
            sum += counts[i];
            counts[i] = sum;

        }
        //move all sums up by one
        for (int i = counts.Length - 1; i > 0; i--)
        {
            counts[i] = counts[i - 1];
        }
        //set first value to 0
        counts[0] = 0;
        return counts;
    }

    int findDigit(int indx, int it)
    {
        int height = getIndex(indx);

        int output = (int)(height % Math.Pow(10, it));

        if (it > 1)
        {
            output /= (int)Math.Pow(10, it - 1);
        }
        return output;
    }



}
