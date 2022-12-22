using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Image BlendOutImg;
    private bool isFading = false;
    private float alpha =0f;
    public showdialogue showDialogue;
    public showmessage showMessage;

    public GameObject hint;
    public OpenHintScript hintText;
    void Start(){

    }

    void Update(){

        if(isFading){
            if(alpha>0f){
            alpha -= Time.deltaTime;
            BlendOutImg.color = new Color(0f,0f,0f,alpha);}
            else{
                isFading=false;
            }
        }


    }
    public void fadeOut(){
        alpha=1f;
        isFading=true;
    }
    public void setHint(string s){
        hint.SetActive(true);
        hintText.text(s);
    }
    public void hideHint(){
        hint.SetActive(false);
    }
    public void dialogue(string s){
        showDialogue.message(s);
    }
     public void hideDialogue(){
        showDialogue.hide();
    }
    public void message(string s){
        showMessage.message(s);
    }
    public void setName(string name){
        showDialogue.setName(name);
    }
    public void setImage(Sprite img){
        showDialogue.setImage(img);
    }
}
