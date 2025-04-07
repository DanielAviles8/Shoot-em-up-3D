using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider controlVolume;


    public GameObject[] Audios;

   private void Start()
    {
        Audios = GameObject.FindGameObjectsWithTag("audio");
        controlVolume.value = PlayerPrefs.GetFloat("VolumeSave", 1f);
    }
    

     private void Update()
    {
        foreach(GameObject au in Audios) 
            au.GetComponent<AudioSource>().volume = controlVolume.value;
    }
   

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("volumeSave", controlVolume.value);
    }


}
