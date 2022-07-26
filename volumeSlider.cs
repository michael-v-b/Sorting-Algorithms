using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class volumeSlider : MonoBehaviour
{
    public audioManager am;
    AudioSource audioSource;
    [SerializeField] TextMeshProUGUI volNum;
    [SerializeField] Slider volSlider;
    [SerializeField]
    // Start is called before the first frame update
    void Awake()
    {

        am = audioManager.main;
        audioSource = am.gameObject.GetComponent<AudioSource>();
        volSlider.value = audioSource.volume;
        volNum.text = "" + (((float)volSlider.value * 100) - (((float)volSlider.value * 100) % 1));

    }
    public void changeVolume()
    {
        audioSource.volume = volSlider.value;
        volNum.text = "" + (((float)volSlider.value * 100) - (((float)volSlider.value * 100) % 1));
    }


}
