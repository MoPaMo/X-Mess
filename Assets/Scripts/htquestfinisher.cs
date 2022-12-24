using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class htquestfinisher : MonoBehaviour
{
    public Vector3 pos;
    private bool completed = false;
    //on trigger enter, award player with quest item, teleport out of the room
    void Start()
    {
        //read player prefs
        if (PlayerPrefs.HasKey("htquest_completed"))
        {
            completed = PlayerPrefs.GetInt("htquest_completed") == 1;
        }
        else
        {
            PlayerPrefs.SetInt("htquest_completed", 0);
        }
        
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (completed)
            {
                Object.Destroy(this.gameObject);
            }
            else
            {
                completed=true;
                GameObject.Find("Player").GetComponent<controller>().addCollected();
                PlayerPrefs.SetInt("htquest_completed", 1);
            }
                GameObject.Find("Player").GetComponent<controller>().isInHouse=false;
                collision.gameObject.transform.position = pos;
        }
    }

}
