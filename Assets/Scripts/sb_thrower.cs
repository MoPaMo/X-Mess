using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sb_thrower : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject projectile;
    public void Throw(Vector3 direction)
    {

        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        GameObject newProjectile = Instantiate(projectile, pos, Quaternion.identity);
        Debug.Log(direction * 1000);
        newProjectile.GetComponent<Rigidbody>().AddForce(direction * 1000);
        Debug.Log("Throwing");
    }


}
