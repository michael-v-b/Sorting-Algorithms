using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mergeSort : sortingAlgo
{
    public Color activeColor;
    public Color currentColor;
    public Color defaultColor;
    audioManager am;
    int timer = 1;
    bool started;
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
        started = false;
        sortingEvent.current.sortStarted -= sort;
    }

    void sort()
    {
        if (!started && !graphManager.beingRando)
        {
            StartCoroutine(tempSort());
        }


    }
    IEnumerator tempSort()
    {
        started = true;
        yield return StartCoroutine(mergedSort(0, bars.Count - 1));
        if (graphManager.beingRando)
        {
            started = false;
            yield break;
        }
        yield return checkSort(activeColor, currentColor, defaultColor);
        sortingEvent.sorting = false;
        started = false;
    }

    IEnumerator mergedSort(int left, int right)
    {
        if (graphManager.beingRando)
        {
            started = false;
            yield break;
        }
        int mid = left + (int)Math.Ceiling(((right - left) / (double)2));
        int i = left;
        int j = mid;
        int front = left;

        if ((right - left) <= 0)
        {
            yield break;
        }





        setColor(left, activeColor);
        setColor(right, activeColor);
        setColor(mid, currentColor);
        am.play("barSound", getPitch(getIndex(mid)));
        if (timer == 3)
        {

            yield return new WaitForSeconds(graphManager.time);
        }
        setColor(left, defaultColor);
        setColor(right, defaultColor);
        setColor(mid, defaultColor);


        yield return mergedSort(left, mid - 1);
        yield return mergedSort(mid, right);



        //merges array

        while (i < mid && j <= right)
        {
            if (graphManager.beingRando)
            {
                started = false;
                yield break;
            }
            am.play("barSound", getPitch(getIndex(i)));

            setColor(i, activeColor);
            setColor(j, activeColor);
            if (timer == 3)
            {
                timer = 1;
                yield return new WaitForSeconds(graphManager.time);
            }
            timer++;
            am.play("barSound", getPitch(getIndex(j)));

            if (bars[i].transform.localScale.y < bars[j].transform.localScale.y)
            {

                setColor(i, defaultColor);
                setColor(j, defaultColor);
                i++;
            }
            else if (bars[i].transform.localScale.y > bars[j].transform.localScale.y)
            {

                setColor(i, defaultColor);
                setColor(j, defaultColor);

                sendToFront(front, j);

                mid++;
                i++;
                j++;
            }
            front++;

        }




        yield break;

    }

    void sendToFront(int front, int index)
    {
        int i;
        int j = index;
        if (index > 0)
        {


            i = index - 1;
        }
        else
        {
            i = index;
        }

        while (i >= front && j >= front + 1)
        {
            swap(i, j);
            i--;
            j--;

        }

    }




}
