using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blendOutScript : MonoBehaviour
{
    public Image Img;
        private bool isShowing = false;
    private float alpha =0f;
    void Start(){
        Img = GetComponent<Image>();
    }

    void Update(){
        if(isShowing){
            if(alpha>0f){
            alpha -= Time.deltaTime;
            Img.color = new Color(0f,0f,0f,alpha);}
            else{
                isShowing=false;
            }
        }
    }
    public void show(){
        alpha=1f;
        isShowing=true;
    }
}
