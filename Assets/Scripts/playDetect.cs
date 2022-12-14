using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playDetect : MonoBehaviour
{
    public string musicName;
    //when the player enters the trigger
    void OnTriggerEnter(Collider other)
    {
        //play music
        FindObjectOfType<AudioManager>().Play(musicName);
    }
}
