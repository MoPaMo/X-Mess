using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenHintScript : MonoBehaviour
{
    public TextMeshProUGUI textM;
    public void text(string s)
    {
        textM.text = s;
    }
}
