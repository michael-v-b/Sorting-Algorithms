using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class exitScript : MonoBehaviour
{
    static bool open = false;
    static bool moving = false;
    [SerializeField] GameObject exitMenu;
     GameObject exit;
    [SerializeField] Canvas canvas;
    [SerializeField] float growRate;
    float scaleX;
    float scaleY;
    float growthRate = .05f;

    //create menu object, which will then grow to full size
    public void summonExit()
    {
 
        if (!open)
        {
            Debug.Log("opens");
            open = true;
            exit = Instantiate(exitMenu, new Vector3(0, 2.2f, 0), Quaternion.identity);
            canvas = exit.GetComponentInChildren<Canvas>();
            canvas.worldCamera = Camera.main;

            scaleX = exit.transform.localScale.x;
            scaleY = exit.transform.localScale.y;
            exit.transform.parent = canvas.transform;
            exit.transform.localScale = new Vector3(0, 0, 1);
            exit.transform.position = new Vector3(0, .15f, 0);
            StartCoroutine(grow());
        }
    }
    //slowly grows menu to proper size
    IEnumerator grow()
    {
   
        
        for(float i = 0; i <= 1; i+=growthRate)
        {
            yield return null;
            exit.transform.localScale = new Vector3(scaleX * i, scaleY * i, i);
        }
        moving = false;

    }

    public void removeExit()
    {
        if (open)
        {

            StartCoroutine(shrink());
        }
    }

    //shrinks menu
    IEnumerator shrink()
    {
        open = false;

        scaleX = exit.transform.localScale.x;
        scaleY = exit.transform.localScale.y;
       

        for (float i = 1; i >= 0; i-=growthRate){
            yield return null;
            
            exit.transform.localScale = new Vector3(scaleX * i, scaleY * i, i);

        }

        
        Destroy(exit);
        moving = false;
    }

    //function gets called every frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !moving)
        {
            moving = true;
            if (!open)
            {

                summonExit();
            }
            else
            {

                removeExit();
            }

        }

    }

}
