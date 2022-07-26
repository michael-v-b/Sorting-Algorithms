using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class graphManager : MonoBehaviour
{


    // wait for i seconds


    public static graphManager graphMngr;
    public static float time = 0;
    public static bool beingRando = false;
    bool ascending = false;
    bool descending = false;

    [SerializeField] Color activeColor = Color.red;
    [SerializeField] Color currentColor = Color.green;
    [SerializeField] Color defaultColor = Color.red;
    Color oldDefault = Color.white;
    audioManager am;
    [SerializeField] GameObject bar;
    [SerializeField] GameObject barHolder;
    GameObject temp;
    [SerializeField] Slider amountSlider;
    [SerializeField] TMP_InputField amountText;
    [SerializeField] GameObject graphLine;
    [SerializeField] int max;
    [SerializeField] int min;
    int timer = 1;


    float width = 0;
    float xpos = 0;
    float ypos = 0;
    float height = 2;




    void Start()
    {
        Application.targetFrameRate = 60;
        graphMngr = this;
        am = audioManager.main;
        /*GameObject temp = Instantiate(bar, new Vector3(2, 0, 0),Quaternion.identity);
        temp.transform.localScale = new Vector2(12, 8);
        
        bars.Add(temp);
        originalColor = bars[0].GetComponent<SpriteRenderer>().color;*/
        fillList(5);
        max = (int)amountSlider.maxValue;
        min = (int)amountSlider.minValue;



    }


    //fills list upon slider change
    public void fillList(float amount)
    {
        StartCoroutine(changeListKill());
        ResetBarList();

        // this is probably not the real solution, but it should work 

        width = 12 / amount;
        for (int i = 0; i < amount; i++)
        {
            xpos = (2 - (width / 2 * (amount - 1)) + (i * width));
            height = 8 * (i + 1) / amount;
            ypos = -4 + (height / 2);

            temp = Instantiate(bar, new Vector3(xpos, ypos, 0), Quaternion.identity);
            temp.transform.localScale = new Vector2(width, height);
            temp.GetComponent<SpriteRenderer>().color = defaultColor;
            temp.transform.parent = barHolder.transform;
            sortingAlgo.bars.Add(temp);

        }

    }

    IEnumerator changeListKill()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForEndOfFrame();
            beingRando = true;
        }
        beingRando = false;

    }


    private void ResetBarList()
    {
        for (int i = 0; i < sortingAlgo.bars.Count; i++)
        {
            Destroy(sortingAlgo.bars[i]);

            //if slider changes while bars are being made halt function
        }
        sortingAlgo.bars.Clear();
    }

    //a method available to the "Randomize" button that starts the coroutine to randomize the list 

    public void randomizeList()
    {
        StartCoroutine(tempRando());
    }
    //randomizes button, randomizes the list of bars
    public IEnumerator tempRando()
    {

        int randomBar;

        GameObject tempBar;
        for (int i = sortingAlgo.bars.Count - 1; i >= 0; i--)
        {
            beingRando = true;

            //set variables
            randomBar = Random.Range(0, i);
            sortingAlgo.setColor(i, activeColor);
            sortingAlgo.setColor(randomBar, activeColor);
            am.play("barSound", sortingAlgo.getPitch(i));
            am.play("barSound", sortingAlgo.getPitch(sortingAlgo.getIndex(randomBar)));


            if (timer >= sortingAlgo.getTimer())
            {
                timer = 1;

                yield return null;
            }
            timer++;


            tempBar = sortingAlgo.bars[randomBar];

            //iterate through barList and randomize

            sortingAlgo.setColor(i, defaultColor);
            sortingAlgo.setColor(randomBar, defaultColor);
            sortingAlgo.swap(randomBar, i);

        }
        beingRando = false;
    }




    //changes inputfield values and fillList if slider is changed
    public void sliderChange(float amount)
    {

        amountText.text = ("" + (int)amount);

        fillList(amount);
    }

    //changes slider value and fillList if inputField is changed
    public void fieldChange(string amount)
    {
        float tempAmount;

        if (int.Parse(amount) >= max)
        {
            tempAmount = (float)max;
            amountText.text = max.ToString();
        }
        else
        {
            tempAmount = float.Parse(amount);
        }
        if (int.Parse(amount) <= min)
        {
            tempAmount = (float)min;
            amountText.text = min.ToString();
        }
        amountSlider.value = (int)tempAmount;
        fillList(tempAmount);
    }



    //Changes graph color from left to right
    public void changeGraphColor()
    {
        ascending = true;
        for (int i = 0; i < sortingAlgo.bars.Count; i++)
        {
            StartCoroutine(fadeColor(oldDefault, defaultColor, i));
        }
    }



    IEnumerator fadeColor(Color color, Color newColor, int index)
    {

        Color fadeColor;
        for (float i = 0; i <= 1; i += .05f)
        {
            fadeColor = Color.Lerp(color, newColor, i);
            yield return null;

            sortingAlgo.setColor(index, fadeColor);

        }

    }





    public void setCurrent(Color curr)
    {
        currentColor = curr;
    }
    public void setActive(Color active)
    {

        activeColor = active;
    }
    public void setDefault(Color def)
    {
        oldDefault = defaultColor;
        defaultColor = def;
    }
    public static void setTime(float temp)
    {
        time = temp;
    }

    public void setMax(int tempMax)
    {
        max = tempMax;
        amountSlider.maxValue = max;
        if (amountSlider.value > max)
        {
            fieldChange("" + max);
        }
    }




    //given index return proper x position in array
    public float getPos(int index)
    {

        int amount = sortingAlgo.bars.Count;
        width = 12f / amount;
        float xpos = (2 - (width / 2f * (amount - 1)) + (index * width));

        return xpos;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            randomizeList();
        }
    }

}
