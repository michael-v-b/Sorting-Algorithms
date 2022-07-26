using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resumeScript : MonoBehaviour
{
    GameObject testObject;
    exitScript exit;
    Button resume;
    private void Awake()
    {
        resume = this.gameObject.GetComponent<Button>();
        Debug.Log("test");
        exit = FindObjectOfType<exitScript>();
        resume.onClick.AddListener(delegate {
            exit.removeExit(); 
        });
    }

   
 
}
