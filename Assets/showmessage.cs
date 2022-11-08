using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class showmessage: MonoBehaviour
{
    public RectTransform rt;
    Vector2 pos = Vector2.zero;
    Vector2 offscreen = Vector2.zero;
    public bool shown = false;
    Vector2 vel = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        //get current position on canvas
        pos = rt.anchoredPosition;
        //set offscreen position
        offscreen.x = -pos.x;
        offscreen.y = pos.y;
        //log current position
        Debug.Log("current position: " + pos + offscreen);
    }
    // Update is called once per frame
    void Update()
    {
        if (shown)
        {
            rt.anchoredPosition = Vector2.SmoothDamp(rt.anchoredPosition, pos, ref vel, 0.5f);
        }
        else
        {
            rt.anchoredPosition = Vector2.SmoothDamp(rt.anchoredPosition, offscreen, ref vel, 0.5f);
        }
    }
    //create function message to change position
    public void message()
    {
        //if current position is offscreen, move to onscreen
        if (rt.anchoredPosition == offscreen)
        {
            shown = true;
        }
    }
}