using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class buttonColor : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IPointerExitHandler
{
    Color[] altColors = { Color.green, /*orange*/new Color(255/255,153/255f,45/255f),/*yellow*/ new Color(1,1,76/255f),/*pink*/ new Color(1,166f/255,126/255f),
    new Color(1,113f/255,175f/255), new Color(1,88f/255,189f/255), new Color(1,88f/255,189f/255), new Color(121f/255,1,121f/255)};

    [SerializeField] float time = .1f;
    TextMeshProUGUI buttonText;
    Button button;
    [SerializeField] Image im;
    SpriteRenderer buttonSprite;

    private void Start()
    {

        buttonText = this.GetComponentInChildren<TextMeshProUGUI>();

        buttonSprite = this.GetComponentInChildren<SpriteRenderer>();
        //im = this.GetComponentInChildren<Image>();
        button = this.GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (buttonText != null)
        {
            StartCoroutine(fadeIn(buttonText));
        }
        if (buttonSprite != null)
        {
            StartCoroutine(fadeIn(buttonSprite));
        }
        if (im != null)
        {
            StartCoroutine(fadeIn(im));
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {


        if (buttonText != null)
        {
            StartCoroutine(fadeOut(buttonText));
        }
        if (buttonSprite != null)
        {
            StartCoroutine(fadeOut(buttonSprite));
        }
        if (im != null)
        {
            StartCoroutine(fadeOut(im));
        }
    }

    //here for to satisfy interface
    public void OnSelect(BaseEventData eventData)
    {
        //do your stuff when selected

    }






    IEnumerator fadeIn(TextMeshProUGUI bt)
    {
        Color fadeColor;
        for (float i = 0; i <= 1; i += time)
        {


            yield return null;

            if (altColors[AlgoUIManager.main.getIndex()] == null)
            {
                Debug.Log("altColors is null");
            }
            fadeColor = Color.Lerp(AlgoUIManager.main.getColor(), altColors[AlgoUIManager.main.getIndex()], i);
            bt.color = fadeColor;


        }


    }
    IEnumerator fadeIn(SpriteRenderer bs)
    {
        Color fadeColor;
        for (float i = 0; i <= 1; i += time)
        {


            yield return null;

            if (altColors[AlgoUIManager.main.getIndex()] == null)
            {
                Debug.Log("altColors is null");
            }
            fadeColor = Color.Lerp(AlgoUIManager.main.getColor(), altColors[AlgoUIManager.main.getIndex()], i);
            bs.color = fadeColor;


        }


    }
    IEnumerator fadeIn(Image buttonImage)
    {
        Color fadeColor;
        for (float i = 0; i <= 1; i += time)
        {


            yield return null;

            if (altColors[AlgoUIManager.main.getIndex()] == null)
            {
                Debug.Log("altColors is null");
            }
            fadeColor = Color.Lerp(AlgoUIManager.main.getColor(), altColors[AlgoUIManager.main.getIndex()], i);
            buttonImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, buttonImage.color.a);


        }


    }
    IEnumerator fadeOut(TextMeshProUGUI bt)
    {
        Color fadeColor;
        for (float i = 0; i <= 1; i += time)
        {

            yield return null;
            fadeColor = Color.Lerp(altColors[AlgoUIManager.main.getIndex()], AlgoUIManager.main.getColor(), i);
            bt.color = fadeColor;
        }


    }
    IEnumerator fadeOut(SpriteRenderer bs)
    {
        Color fadeColor;
        for (float i = 0; i <= 1; i += time)
        {
            yield return null;
            fadeColor = Color.Lerp(altColors[AlgoUIManager.main.getIndex()], AlgoUIManager.main.getColor(), i);
            bs.color = fadeColor;
        }
    }
    IEnumerator fadeOut(Image buttonImage)
    {
        Color fadeColor;
        for (float i = 0; i <= 1; i += time)
        {
            yield return null;
            fadeColor = Color.Lerp(altColors[AlgoUIManager.main.getIndex()], AlgoUIManager.main.getColor(), i);
            buttonImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, buttonImage.color.a);
        }
    }
}
