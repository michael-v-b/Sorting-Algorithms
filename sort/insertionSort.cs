using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class insertionSort : sortingAlgo
{

    bool started = false;
    public Color activeColor = Color.red;
    public Color currentColor = Color.green;
    public Color defaultColor = Color.white;
    audioManager am;
    float min = float.MaxValue;
    float time;
    float height;
    float height2;
    int j;
    int timer = 1;
    private void OnEnable()
    {
        started = false;
        graphManager.graphMngr.setMax(150);
        graphManager.graphMngr.setActive(activeColor);
        graphManager.graphMngr.setCurrent(currentColor);
        graphManager.graphMngr.setDefault(defaultColor);
        am = audioManager.main;

        sortingEvent.current.sortStarted += sort;
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
            StartCoroutine(insertion());
        }
    }
    IEnumerator insertion()
    {
        started = true;



        for (int i = 1; i < bars.Count; i++)
        {
            am.play("barSound", getPitch(getIndex(i)));
            setColor(i, activeColor);
            j = i;


            while (j >= 1 && bars[j].transform.localScale.y < bars[j - 1].transform.localScale.y)
            {
                if (graphManager.beingRando)
                {
                    started = false;
                    yield break;

                }
                am.play("barSound", getPitch(getIndex(j)));
                am.play("barSound", getPitch(getIndex(j - 1)));

                setColor(j, currentColor);
                setColor(j - 1, activeColor);

                if (timer == getTimer())
                {
                    timer = 1;
                    yield return new WaitForSeconds(graphManager.time);
                }
                timer++;
                swap(j, j - 1);



                setColor(j, defaultColor);
                setColor(j - 1, defaultColor);

                j--;

            }
            if (timer == getTimer())
            {
                timer = 1;
                yield return new WaitForSeconds(graphManager.time);
            }
            timer++;
            setColor(i, defaultColor);

        }
        yield return checkSort(activeColor, currentColor, defaultColor);
        sortingEvent.sorting = false;
        started = false;
    }
}
