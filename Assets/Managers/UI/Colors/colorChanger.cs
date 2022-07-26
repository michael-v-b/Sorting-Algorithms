using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class colorChanger : MonoBehaviour
{
    [SerializeField]Color oldColor = Color.white;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Image im;


    

    private void Start()
    {
        sortingEvent.current.colorChanged += fadeColor;
       
    }
    private void Awake()
    {
        menuScript menu = FindObjectOfType<menuScript>();
        Color tempColor = Color.white;
        //if on menu
        if(menu!= null)
        {
            tempColor = menu.getColor();
        }
        //if during graph
        if(AlgoUIManager.main != null)
        {
            tempColor = AlgoUIManager.main.getColor();         
        }

        //change text with various cases
        if (text != null)
        {
            text.color = new Color(tempColor.r, tempColor.g,tempColor.b,text.color.a);
        }
        if (sprite != null)
        {
            sprite.color = new Color(tempColor.r, tempColor.g, tempColor.b, sprite.color.a);
        }
        if (im != null)
        {
            im.color = new Color(tempColor.r, tempColor.g, tempColor.b, im.color.a);
        }
    }
    void fadeColor(Color newColor)
    {
     

        if (text != null)
        {

            
            oldColor = text.color;
            newColor.a = text.color.a;
            oldColor.a = text.color.a;
            
            StartCoroutine(tempFade(oldColor, newColor,text));
        } else if(sprite != null)
        {
            
            oldColor = sprite.color;
            newColor.a = sprite.color.a;
            oldColor.a = sprite.color.a;
            StartCoroutine(tempFade(oldColor, newColor, sprite));
        } else if(im != null) {
          
            oldColor = im.color;
            oldColor.a = im.color.a;
            newColor.a = im.color.a;
            
            StartCoroutine(tempFade(oldColor, newColor, im));
        }
        
    }
    //fades all text
    IEnumerator tempFade(Color old, Color newC,TextMeshProUGUI t)
    {
        
        
        Color fadeColor;
        for (float i = 0; i <= 1; i += .05f)
        {
            fadeColor = Color.Lerp(old, newC, i);
            yield return null;
            
            t.color = fadeColor;
        }
        t.color = newC;

    }

    //fades all sprites
    IEnumerator tempFade(Color old, Color newC, SpriteRenderer s)
    {
        Color fadeColor = old;
        for (float i = 0; i <= 1; i += .05f)
        {
            fadeColor = Color.Lerp(old, newC, i);
            yield return null;
            
            s.color = fadeColor;
        }
    }
    //fades all images
    IEnumerator tempFade(Color old, Color newC, Image ima)
    {
        Color fadeColor = old;
        for (float i = 0; i <= 1; i += .05f)
        {
            fadeColor = Color.Lerp(old, newC, i);
            yield return null;
            
            ima.color = fadeColor;
        }
    }

    

}
