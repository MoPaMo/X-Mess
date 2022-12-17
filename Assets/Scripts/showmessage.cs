using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class showmessage: MonoBehaviour
{
    public RectTransform rt;
    float pos = 0f;
    float offscreen = 0f;
    public bool shown = false;
    float vel = 0f;
    public TextMeshProUGUI text;
  public float timeShown = 10f;
    private float timeShownCounter = -69f;
    // Start is called before the first frame update
    void Start()
    {
        //get current position on canvas
        pos = rt.anchoredPosition.x;
        //set offscreen position
        offscreen = -1.2f*pos;
        //log current position
        Debug.Log("current position: " + pos + offscreen);
        if(!shown){
            //move offscreen
            rt.anchoredPosition = new Vector2(offscreen, rt.anchoredPosition.y);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (shown)
        {
            if(rt.anchoredPosition.x/pos<0.995f){
                rt.anchoredPosition = new Vector2(Mathf.SmoothDamp(rt.anchoredPosition.x, pos, ref vel, 0.5f), rt.anchoredPosition.y);
            }
           
            else{
                if (timeShownCounter ==-69f)
                {

                    rt.anchoredPosition = new Vector2(pos, rt.anchoredPosition.y);
                    timeShownCounter = timeShown;
                }
                else if(timeShownCounter>0)
                    {
                    timeShownCounter -= Time.deltaTime;

                }else{
                    shown = false;
                    timeShownCounter = -69f;
                }
            }
        }
        else
        {
            if(rt.anchoredPosition.x/offscreen<0.975f){
            rt.anchoredPosition = new Vector2(Mathf.SmoothDamp(rt.anchoredPosition.x, offscreen, ref vel, 0.5f), rt.anchoredPosition.y);}
            else{
                rt.anchoredPosition = new Vector2(offscreen, rt.anchoredPosition.y);
            }
        }
    }
    //create function message to change position
    public void message(string s, float time=10f)
    {

        //if current position is offscreen, move to onscreen
        if (rt.anchoredPosition.x == offscreen)
        {
            shown = true;
        }
        text.text=s;
    }
}