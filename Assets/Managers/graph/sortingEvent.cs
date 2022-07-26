using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class sortingEvent : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI sortButton;
    public static sortingEvent current;
    public static bool sorting = false;
    [SerializeField]

    private void Awake()

    {
        current = this;
    }
    public event Action sortStarted;
    public event Action<Color> colorChanged;

    public void startSort()
    {
        if (sortStarted != null && !sorting)
        {
            sortButton.text = "Stop";
            sorting = true;
            sortStarted();
        }
        else if (sorting)
        {
            StartCoroutine("tempRando");
        }
    }
    IEnumerator tempRando()
    {
        graphManager.beingRando = true;
        yield return null;
        graphManager.beingRando = false;
        sorting = false;
    }
    public void changeColor(Color c)
    {
        if (colorChanged != null)
        {
            colorChanged(c);
        }
    }

    public void Update()
    {
        if (sorting == false)
        {
            sortButton.text = "Sort";
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            startSort();
        }
    }
}
