using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlgoUIManager : MonoBehaviour
{
    public static AlgoUIManager main;
    //list of sorting algo names for the ui
     string[] algoNames = { "Selection Sort","Bubble Sort","Insertion Sort","Cocktail Sort", "Merge Sort", "Quick Sort",
        "Heap Sort", "Radix Sort"};
    Color[] colors = { Color.white, new Color(255/255f,92/255f,85/255f),new Color(255/255f,194/255f,71/255f), new Color(255/255f,255/255f,66/255f),
        new Color(109/255f,255/255f,112/255f), new Color(85/255f,255/255f,255/255f), new Color(91/255f,83/255f,255/255f), 
        new Color(175/255f,52/255f,255/255f)};

    //field filled with empty objects that apply the various sorting algos
    [SerializeField] GameObject[] algos = new GameObject[10];
    int index = 0;
    Color currentColor = Color.white;
    [SerializeField]TextMeshProUGUI indexText;
    [SerializeField]TextMeshProUGUI algoText;

    private void Start()
    {
        if(main == null)
        {
            main = this;
        }
    }



    public void increment()
    {

        algos[index].SetActive(false);
        if (index < algos.Length-1)
        {
            index++;
        }
        else
        {
            algos[index].SetActive(false);
            index = 0;
        }
        changeText();
        algos[index].SetActive(true);
        //changesColors
        currentColor = colors[index];
        sortingEvent.current.changeColor(currentColor);
       

    }
    public void decrement()
    {
        algos[index].SetActive(false);
        if(index == 0)
        {
            index = algos.Length - 1;
        } else
        {
            index--;
        }
        changeText();
        
        algos[index].SetActive(true);
        // and changesColor
        currentColor = colors[index];
        sortingEvent.current.changeColor(currentColor);
    }

    private void changeText()
    {
        indexText.text = (index+1).ToString();
        algoText.text = algoNames[index];
    }
    public void setIndex(int i)
    {
        index = i;
    }
    public Color getColor()
    {
        return currentColor;
    }
    public int getIndex()
    {
        return index;
    }
   

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.decrement();
            graphManager.graphMngr.changeGraphColor();
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.increment();
            graphManager.graphMngr.changeGraphColor();
        }
    }

}
