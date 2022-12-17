using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueProperties : MonoBehaviour
{

    //an array of strings contains all phrases
    public string[] phrases;
    private int currentPhrase = 0;
    private bool isColliding = false;
    public string charName="Der Unbekannte";
    //on collision with player, show dialogue
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isColliding = true;
            
        }
    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isColliding = false;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && isColliding){
            FindObjectOfType<UIScript>().setName(charName);
            if (currentPhrase >= phrases.Length)
            {
                GameObject.Find("Player").GetComponent<controller>().dialogueMode = false;
                currentPhrase = 0;
                FindObjectOfType<UIScript>().hideDialogue();

            }else
            {
                GameObject.Find("Player").GetComponent<controller>().dialogueMode = true;
                FindObjectOfType<UIScript>().dialogue(phrases[currentPhrase]);
                currentPhrase++;

            }

        }
    }
}
