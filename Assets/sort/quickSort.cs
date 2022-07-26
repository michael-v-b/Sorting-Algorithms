using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quickSort : sortingAlgo
{
    public Color activeColor;
    public Color currentColor;
    public Color defaultColor;
    audioManager am;
    bool started = false;

    int tempPi;
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
        started = false;
        graphManager.beingRando = false;
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
        yield return StartCoroutine(graphManager.graphMngr.tempRando());
        yield return StartCoroutine(quick(0, (bars.Count - 1)));
        if (graphManager.beingRando)
        {
            started = false;
            yield break;
        }
        yield return checkSort(activeColor, currentColor, defaultColor);
        sortingEvent.sorting = false;
        started = false;

    }


    IEnumerator quick(int left, int right)
    {
        if (graphManager.beingRando)
        {
            started = false;
            yield break;
        }


        int pi;

        if (right <= left)
        {
            yield break;
        }

        yield return part(left, right);
        pi = tempPi;


        yield return quick(left, pi - 1);

        // Debug.Log($"---------------------------------------------- [{left}, {right}]");


        yield return quick(pi + 1, right);


    }
    IEnumerator part(int left, int right)
    {
        // Debug.Log($"[{left},{right}]");
        float properIndex;


        int mid = left + (right - left) / 2;



        int partition = right;
        am.play("barSound", getPitch(getIndex(partition)));
        setColor(partition, currentColor);
        properIndex = ((bars[partition].transform.localScale.y * bars.Count) / 8) - 1;
        int i = left;
        for (int j = left; j < right; j++)
        {
            if (graphManager.beingRando)
            {
                started = false;
                yield break;
            }
            am.play("barSound", getPitch(getIndex(j)));
            if (j < i)
            {
                i = j;
            }
            setColor(i, activeColor);
            setColor(j, activeColor);
            if (timer == getTimer())
            {
                timer = 1;
                yield return new WaitForSeconds(graphManager.time);
            }
            timer++;
            if (bars[j].transform.localScale.y < bars[partition].transform.localScale.y)
            {
                setColor(i, defaultColor);
                setColor(j, defaultColor);
                swap(i, j);
                i++;
            }
            setColor(i, defaultColor);
            setColor(j, defaultColor);

        }
        setColor(partition, defaultColor);
        swap(partition, i);



        tempPi = i;
    }







}
