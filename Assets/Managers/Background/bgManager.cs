using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgManager : MonoBehaviour
{
    GameObject[] dots = new GameObject[50];
    [SerializeField] GameObject dot;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i< dots.Length; i++)
        {
            //Debug.Log($"test {i}");
            
            dots[i] = Instantiate(dot, Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f,1f),Random.Range(0f,1f),10)),Quaternion.identity);
            dots[i].transform.parent = this.transform;
        }
    }

}
