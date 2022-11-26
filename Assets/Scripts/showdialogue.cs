using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class showdialogue : MonoBehaviour
{
    public RectTransform rt;
    float pos = 0f;
    float offscreen = 0f;
    public bool shown = false;
    float vel = 0f;
    // Start is called before the first frame update
    void Start()
    {
        //get current position on canvas
        pos = rt.anchoredPosition.y;
        //set offscreen position
        offscreen = -1.6f * pos;
        //log current position
        Debug.Log("current position: " + pos + offscreen);
        if (!shown)
        {
            //move offscreen
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, offscreen);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (shown)
        {
            if (rt.anchoredPosition.y / pos < 0.995f)
            {
                rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, Mathf.SmoothDamp(rt.anchoredPosition.y, pos, ref vel, 0.5f));
            }
            else
            {
                rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, pos);
            }
        }
        else
        {
            if (rt.anchoredPosition.y / offscreen < 0.975f)
            {
                rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, Mathf.SmoothDamp(rt.anchoredPosition.y, offscreen, ref vel, 0.5f));
            }
            else
            {
                rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, offscreen);
            }
        }
    }
    //create function message to change position
    public void message()
    {
        //if current position is offscreen, move to onscreen
        if (rt.anchoredPosition.y == offscreen)
        {
            shown = true;
        }
    }
}