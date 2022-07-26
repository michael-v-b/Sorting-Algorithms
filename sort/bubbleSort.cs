using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbleSort : sortingAlgo
{
    public Color activeColor = Color.red;
    public Color currentColor = Color.green;
    public Color defaultColor = Color.white;
    audioManager am;
    bool sorted = false;
    bool started = false;

    float height1;
    float height2;
    int timer = 1;
    private void OnEnable()
    {
        graphManager.graphMngr.setMax(100);
        graphManager.graphMngr.setActive(activeColor);
        graphManager.graphMngr.setCurrent(currentColor);
        graphManager.graphMngr.setDefault(defaultColor);
        am = audioManager.main;
        started = false;

        sortingEvent.current.sortStarted += sort;
    }

    private void OnDisable()
    {
        started = false;

        sortingEvent.current.sortStarted -= sort;
    }
    // Start is called before the first frame update
    void sort()
    {
        if (!started && !graphManager.beingRando)
        {
            StartCoroutine(bubble());
        }
    }

    IEnumerator bubble()
    {

        started = true;
        yield return new WaitForSeconds(graphManager.time);

        for (int i = 0; i < bars.Count; i++)
        {
            sorted = true;
            am.play("barSound", getPitch(getIndex(i)));

            for (int j = 1; j < bars.Count - i; j++)
            {
                if (graphManager.beingRando)
                {
                    Debug.Log($"beingRando:{graphManager.beingRando}");
                    started = false;
                    sorted = false;
                    yield break;
                }
                setColor(j, activeColor);
                setColor(j - 1, currentColor);
                height1 = bars[j - 1].transform.localScale.y;
                height2 = bars[j].transform.localScale.y;
                am.play("barSound", getPitch(getIndex(j - 1)));
                if (timer == getTimer())
                {
                    timer = 1;
                    yield return new WaitForSeconds(graphManager.time);
                }
                timer++;
                if (height1 > height2)
                {
                    sorted = false;
                    swap(j - 1, j);

                }

                setColor(j - 1, defaultColor);
                setColor(j, defaultColor);

            }
            if (sorted)
            {
                break;
            }

        }
        yield return checkSort(activeColor, currentColor, defaultColor);

        sortingEvent.sorting = false;
        started = false;

    }
}
