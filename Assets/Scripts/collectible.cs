using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectible : MonoBehaviour
{
    private bool isColliding=false;
    public Quest_collect npc;
    // Start is called before the first frame update

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player")&&npc.phase==2)
        {
            GameObject.Find("UI").GetComponent<UIScript>().setHint("SAMMELN [F]");
            isColliding = true;
            
        }
    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player")&&npc.phase==2)
        {
            GameObject.Find("UI").GetComponent<UIScript>().hideHint();
            isColliding = false;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && isColliding){
            npc.collected++;
            Object.Destroy(this.gameObject);
            GameObject.Find("UI").GetComponent<UIScript>().hideHint();
       }  
    }
}
