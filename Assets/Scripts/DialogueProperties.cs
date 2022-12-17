using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueProperties : MonoBehaviour
{

    //an array of strings contains all phrases
    public string[] phrases;

    //on collision with player, show dialogue
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            FindObjectOfType<UIScript>().dialogue(phrases[0]);
        }
    }

    //on collision exit with player, hide dialogue
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<UIScript>().dialogue("");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<UIScript>().dialogue("test");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
