using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Image BlendOutImg;
    private bool isFading = false;
    private float alpha =0f;
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
}
