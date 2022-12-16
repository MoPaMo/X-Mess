using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class showdialogue : MonoBehaviour
{
    public RectTransform rt_d;
    float pos_d = 0f;
    float offscreen_d = 0f;
    public bool shown_d = false;
    float vel_d = 0f;
    // Start is called before the first frame update
    void Start()
    {
        //get current position on canvas
        pos_d = rt_d.anchoredPosition.y;
        //set offscreen position
        offscreen_d = -1.6f * pos_d;
        //log current position
        Debug.Log("current position: " + pos_d + offscreen_d);
        if (!shown_d)
        {
            //move offscreen
            rt_d.anchoredPosition = new Vector2(rt_d.anchoredPosition.x, offscreen_d);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (shown_d)
        {
            if (rt_d.anchoredPosition.y / pos_d < 0.995f)
            {
                rt_d.anchoredPosition = new Vector2(rt_d.anchoredPosition.x, Mathf.SmoothDamp(rt_d.anchoredPosition.y, pos_d, ref vel_d, 0.5f));
            }
            else
            {
                rt_d.anchoredPosition = new Vector2(rt_d.anchoredPosition.x, pos_d);
            }
        }
        else
        {
            if (rt_d.anchoredPosition.y / offscreen_d < 0.975f)
            {
                rt_d.anchoredPosition = new Vector2(rt_d.anchoredPosition.x, Mathf.SmoothDamp(rt_d.anchoredPosition.y, offscreen_d, ref vel_d, 0.5f));
            }
            else
            {
                rt_d.anchoredPosition = new Vector2(rt_d.anchoredPosition.x, offscreen_d);
            }
        }
    }
    //create function message to change position
    public void message()
    {
        //if current position is offscreen, move to onscreen
        if (rt_d.anchoredPosition.y == offscreen_d)
        {
            shown_d = true;
        }
    }
}