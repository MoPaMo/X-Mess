using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sb_hitreceiver : MonoBehaviour
{

    public bool is_enabled;
    void Start()
    {
        is_enabled = true;
    }
    public void Hit()
    {
        Debug.Log("Hit detection");
    }
}
