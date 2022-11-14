using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class fpscounter : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tmp.text = Mathf.Round(1f / Time.deltaTime)+"Hz";
    }
}
