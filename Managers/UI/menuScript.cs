using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class menuScript : MonoBehaviour
{
    int ballColor = 0;
    bool exitOpen;
    Color[] colors = { Color.white, new Color(255/255f,92/255f,85/255f),new Color(255/255f,194/255f,71/255f), new Color(255/255f,255/255f,66/255f),
        new Color(109/255f,255/255f,112/255f), new Color(85/255f,255/255f,255/255f), new Color(91/255f,83/255f,255/255f),
        new Color(175/255f,52/255f,255/255f)};
    // Start is called before the first frame update
    GameObject[] dots = new GameObject[50];
    [SerializeField] GameObject dot;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI instruct;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    
        this.ballColor = Random.Range((int)0, (int)8);
        StartCoroutine(this.setBackground());
    }

    IEnumerator setBackground()
    {
        yield return null;
        for (int i = 0; i < dots.Length; i++)
        {
            

            //instantiate all balls off screen

            dots[i] = Instantiate(dot,new Vector3(50,50,10), Quaternion.identity);
            dots[i].transform.parent = this.transform;
            yield return null;
        }

        Debug.Log("happens");
        sortingEvent.current.changeColor(colors[ballColor]);
        yield return new WaitForSeconds(.5f);
        yield return StartCoroutine(fadeIn(title));
        yield return new WaitForSeconds(.75f);
        yield return StartCoroutine(fadeIn(instruct));
        //StartCoroutine(fadeIn(instruct));
    }

    IEnumerator fadeIn(TextMeshProUGUI text)
    {
        for (float i = 0; i <= 1; i += .01f)
        {
            yield return null;
            text.color = new Color(text.color.r, text.color.g, text.color.b, i);
        }
    }
    public Color getColor()
    {
        return colors[ballColor];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape) && !exitOpen)
        {
            SceneManager.LoadScene("Graph");
        }
    }
}