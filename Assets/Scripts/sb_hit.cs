using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sb_hit : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponents<sb_hitreceiver>().Length != 0)
        {
            sb_hitreceiver hitreceiver = collision.transform.GetComponent<sb_hitreceiver>();
            if (hitreceiver.is_enabled)
            {
                hitreceiver.Hit();
                Object.Destroy(this.gameObject);
            }
        }
    }
}
