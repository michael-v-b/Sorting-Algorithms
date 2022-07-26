using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectionSort : sortingAlgo
{
    public Color activeColor;
    public Color currentColor;
    public Color defaultColor = Color.red;



    float min;
    float height;
    int minIndex;
    int timer = 1;
    bool started = false;
    bool sorted = false;
    audioManager am;
    private void Start()
    {
        started = false;
        sortingEvent.current.sortStarted += sort;
        am = audioManager.main;
    }
    private void OnEnable()
    {


        started = false;
        am = audioManager.main;
        if (graphManager.graphMngr != null)
        {
            graphManager.graphMngr.setMax(100);
        }
        //Debug.Log("selectionSort enabled")
        if (graphManager.graphMngr != null)
        {
            graphManager.graphMngr.setActive(activeColor);
            graphManager.graphMngr.setCurrent(currentColor);
            graphManager.graphMngr.setDefault(defaultColor);
        }
        if (sortingEvent.current != null)
        {
            sortingEvent.current.sortStarted += sort;
        }


    }
    private void OnDisable()
    {
        sortingEvent.current.sortStarted -= sort;
    }

    public void sort()
    {
        Debug.Log("happens");
        if (!started && !graphManager.beingRando)
        {
            StartCoroutine(selection());
        }
    }

    IEnumerator selection()
    {
        started = true;
        yield return null;

        for (int i = 0; i < bars.Count; i++)
        {
            sorted = true;
            min = float.MaxValue;

            setColor(i, activeColor);


            am.play("barSound", getPitch(getIndex(i)));



            for (int j = i; j < bars.Count; j++)
            {

                if (graphManager.beingRando)
                {

                    started = false;
                    yield break;
                }

                setColor(j, activeColor);
                if (timer == getTimer())
                {
                    timer = 1;
                    yield return new WaitForSeconds(graphManager.time);
                }
                timer++;
                setColor(j, defaultColor);
                am.play("barSound", getPitch(getIndex(j)));
                height = bars[j].transform.localScale.y;
                if (height < min)
                {
                    setColor(minIndex, defaultColor);
                    min = height;
                    minIndex = j;
                    setColor(minIndex, currentColor);
                }
                if (j == 0)
                {

                    sorted = false;
                }
                else if (bars[j - 1].transform.localScale.y > bars[j].transform.localScale.y)
                {
                    sorted = false;
                }


            }
            if (sorted)
            {
                sorted = false;

                break;
            }

            swap(i, minIndex);
            setColor(i, defaultColor);


        }
        yield return checkSort(activeColor, currentColor, defaultColor);
        sortingEvent.sorting = false;
        sorted = false;
        started = false;
    }
}
