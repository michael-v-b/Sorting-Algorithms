using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgScript : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 direction;
    float speed = .005f;

    [SerializeField] float opacity;
    SpriteRenderer rend;
    private void Awake()
    {
        rend = this.GetComponent<SpriteRenderer>();
        randomizeDirection();
        opacity = rend.color.a;
        //Debug.Log($"opacity {opacity}");
    }

    void randomizeDirection()
    {
        direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f),0);
        Vector3.Normalize(direction);
    }
    
    
    IEnumerator fadeIn()
    {

        for(float i = 0; i <= opacity; i += opacity/50f)
        {
            yield return null;
            rend.color = new Color(rend.color.r,rend.color.g,rend.color.b,i);

        }
    }

    private void Update()
    {

        this.transform.Translate(direction*speed);
           //when object is invisible teleport to middle
        if(!rend.isVisible)
        {
            rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 0);
            this.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 10));
            randomizeDirection();
            

           StartCoroutine(fadeIn());
            
        }
        
    }


}
