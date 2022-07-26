using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class cocktailSort : sortingAlgo
{
    public Color activeColor = Color.red;
    public Color currentColor = Color.green;
    public Color defaultColor = Color.white;
    audioManager am;
    bool sorted = false;
    bool started;
    int timer = 1;
    private void OnEnable()
    {

        started = false;
        graphManager.graphMngr.setMax(100);
        graphManager.graphMngr.setActive(activeColor);
        graphManager.graphMngr.setCurrent(currentColor);
        graphManager.graphMngr.setDefault(defaultColor);
        am = audioManager.main;

        sortingEvent.current.sortStarted += sort;
        Debug.Log("sorting Event added");
    }
    private void OnDisable()
    {
        started = false;
        sortingEvent.current.sortStarted -= sort;
    }

    void sort()
    {
        if (!started && !graphManager.beingRando)
        {
            StartCoroutine(cocktail());
        }
    }

    IEnumerator cocktail()
    {
        started = true;
        int high = bars.Count - 1;
        int low = 0;
        while (low < high)
        {
            if (graphManager.beingRando)
            {

                started = false;
                yield break;
            }
            sorted = true;
            //go up
            for (int i = low; i < high; i++)
            {
                if (graphManager.beingRando)
                {

                    started = false;
                    yield break;
                }

                setColor(i, currentColor);
                setColor(i + 1, activeColor);
                am.play("barSound", getPitch(getIndex(i)));
                if (timer == getTimer())
                {
                    timer = 1;
                    yield return new WaitForSeconds(graphManager.time);
                }
                timer++;


                if (bars[i + 1].transform.localScale.y < bars[i].transform.localScale.y)
                {
                    swap(i, i + 1);
                    sorted = false;
                }
                setColor(i, defaultColor);
                setColor(i + 1, defaultColor);
            }
            high--;


            //go down
            for (int i = high; i > low; i--)
            {
                if (graphManager.beingRando)
                {

                    started = false;
                    yield break;
                }
                am.play("barSound", getPitch(getIndex(i)));
                setColor(i, currentColor);
                setColor(i - 1, activeColor);
                if (timer == getTimer())
                {
                    timer = 1;
                    yield return new WaitForSeconds(graphManager.time);
                }
                timer++;
                if (bars[i].transform.localScale.y < bars[i - 1].transform.localScale.y)
                {
                    swap(i, i - 1);
                    sorted = false;
                }
                setColor(i, defaultColor);
                setColor(i - 1, defaultColor);
            }
            low++;
            if (sorted)
            {
                sorted = false;
                break;
            }
        }

        yield return checkSort(activeColor, currentColor, defaultColor);
        //fin
        sortingEvent.sorting = false;
        started = false;

    }


}
