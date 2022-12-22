using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_collect : MonoBehaviour
{

    //an array of strings contains all phrases
    public string[] phrases_p1;
    public string[] noncollected;
    public string[] missingcollected;
    public string[] finishing_phrases;
    private int currentPhrase = 0;
    private bool isColliding = false;
    public string charName="Der Unbekannte";
    public Sprite avatar;
    public int phase = 1;
    public int collected=0;
    public int max_collected=6;
    private bool completed = false;
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
void Start(){
            if (PlayerPrefs.HasKey("collected_quest"))
        {
            completed = PlayerPrefs.GetInt("collected_quest") == 1;
        }
        if(completed){
            Object.Destroy(this.gameObject);
        }
}
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && isColliding&&!completed){
            FindObjectOfType<UIScript>().setName(charName);
            FindObjectOfType<UIScript>().setImage(avatar);
            FindObjectOfType<AudioManager>().Play("UI Click");

            if(phase==1){
                if (currentPhrase >= phrases_p1.Length)
                {
                    GameObject.Find("Player").GetComponent<controller>().dialogueMode = false;
                    currentPhrase = 0;
                    FindObjectOfType<UIScript>().hideDialogue();
                    phase = 2;

                }else
                {
                    GameObject.Find("Player").GetComponent<controller>().dialogueMode = true;
                    FindObjectOfType<UIScript>().dialogue(phrases_p1[currentPhrase]);
                    currentPhrase++;

                }
            }else if (phase==2){
                if(collected==0){
                    if (currentPhrase >= noncollected.Length)
                    {
                        GameObject.Find("Player").GetComponent<controller>().dialogueMode = false;
                        currentPhrase = 0;
                        FindObjectOfType<UIScript>().hideDialogue();

                    }else
                    {
                        GameObject.Find("Player").GetComponent<controller>().dialogueMode = true;
                        FindObjectOfType<UIScript>().dialogue(noncollected[currentPhrase]);
                        currentPhrase++;

                    }
                }else if(collected<max_collected){
                    if (currentPhrase >= missingcollected.Length)
                    {
                        GameObject.Find("Player").GetComponent<controller>().dialogueMode = false;
                        currentPhrase = 0;
                        FindObjectOfType<UIScript>().hideDialogue();

                    }else
                    {
                        GameObject.Find("Player").GetComponent<controller>().dialogueMode = true;
                        FindObjectOfType<UIScript>().dialogue(missingcollected[currentPhrase]);
                        currentPhrase++;

                    }
                }else{
                    if (currentPhrase >= finishing_phrases.Length)
                    {
                        GameObject.Find("Player").GetComponent<controller>().dialogueMode = false;
                        currentPhrase = 0;
                        FindObjectOfType<UIScript>().hideDialogue();
                        PlayerPrefs.SetInt("collected_quest", 1);
                        completed = true;
                        Object.Destroy(this.gameObject);

                        //award one joyful star

                    }else
                    {
                        GameObject.Find("Player").GetComponent<controller>().dialogueMode = true;
                        FindObjectOfType<UIScript>().dialogue(finishing_phrases[currentPhrase]);
                        currentPhrase++;

                    } 
                }
            }
        }
    }
}
