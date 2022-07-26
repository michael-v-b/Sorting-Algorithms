using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class heapSort : sortingAlgo
{
    public Color activeColor = Color.red;
    public Color currentColor = Color.green;
    public Color defaultColor = Color.white;
    audioManager am;
    bool started = false;
    int amountOfSinks;
    int timer = 1;
    private void OnEnable()
    {
        started = false;
        graphManager.graphMngr.setMax(200);
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
            StartCoroutine(heap());
        }
    }

    IEnumerator heap()
    {
        started = true;


        // heapify graph
        for (int i = 0; i < bars.Count; i++)
        {
            if (graphManager.beingRando)
            {
                started = false;
                yield break;
            }
            am.play("barSound", getPitch(getIndex(i)));
            setColor(i, activeColor);
            if (timer == getTimer())
            {
                timer = 1;
                yield return new WaitForSeconds(graphManager.time);
            }
            timer++;
            setColor(i, defaultColor);
            if (i != 0)
            {
                yield return heapify(i);

            }

        }

        //sort using heap
        for (int i = 0; i < bars.Count - 1; i++)
        {
            if (graphManager.beingRando)
            {
                started = false;
                yield break;
            }

            am.play("barSound", getPitch(getIndex(0)));
            swap(0, (bars.Count - 1 - i));
            if (timer == getTimer())
            {
                timer = 1;
                yield return new WaitForSeconds(graphManager.time);
            }
            timer++;

            amountOfSinks = 0;

            yield return sink(i + 1, 0);

        }
        yield return checkSort(activeColor, currentColor, defaultColor);
        sortingEvent.sorting = false;
        started = false;



    }
    IEnumerator heapify(int i)

    {
        int levelUp = ((i + 1) / 2) - 1;


        while (levelUp >= 0 && bars[i].transform.localScale.y > bars[levelUp].transform.localScale.y)
        {

            setColor(i, activeColor);
            if (timer == getTimer())
            {
                timer = 1;
                yield return new WaitForSeconds(graphManager.time);
            }
            timer++;
            //Debug.Log($"swapped {bars[i].transform.localScale.y} > {bars[levelUp].transform.localScale.y}");
            swap(i, levelUp);


            i = levelUp;
            levelUp = ((i + 1) / 2) - 1;

            setColor(i, defaultColor);
        }
    }


    //recursively sinks the value starting to its proper location
    IEnumerator sink(int amountSorted, int i)
    {
        if (amountSorted > bars.Count - 1)
        {
            yield break;
        }
        amountOfSinks++;
        if (timer == getTimer())
        {
            timer = 1;
            yield return new WaitForSeconds(graphManager.time);
        }
        timer++;

        int levelDown = ((i + 1) * 2) - 1;
        int heapSize = bars.Count - amountSorted - 1;
        int max;

        // if the current node in the heap has 2 branches
        if (levelDown + 1 <= heapSize)
        {

            //find which of the 2 branches are larger
            if (bars[levelDown].transform.localScale.y < bars[levelDown + 1].transform.localScale.y)
            {
                max = levelDown + 1;
            }
            else
            {
                max = levelDown;
            }

            //then if it's still larger than the current node, swap the max and the other branch
            //continue until N/A
            if (bars[i].transform.localScale.y < bars[max].transform.localScale.y)
            {

                swap(i, max);
                yield return sink(amountSorted, max);
            }
            yield break;

        }
        //if node has only one branch...
        else if (levelDown <= heapSize)
        {

            //test if branch is larger than node then swap
            max = levelDown;
            if (bars[i].transform.localScale.y < bars[max].transform.localScale.y)
            {
                swap(i, max);
            }
            yield break;
        }

        yield break;

    }




}



