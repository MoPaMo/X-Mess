using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sb_thrower : MonoBehaviour
{
    public float angle = 45f;
    public float force = 1000f;
    private int snowballs = 3;
    public int maxSnowballs = 3;
  
    public GameObject projectile;
   
    public void Throw(Vector3 direction)
    {
        if (snowballs > 0)
        {
            snowballs--;
            FindObjectOfType<AudioManager>().Play("Snowball Throw");
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            GameObject newProjectile = Instantiate(projectile, pos, Quaternion.identity);
            direction = direction * force;
            direction = new Vector3(direction.x, Mathf.Sqrt(Mathf.Pow(direction.x, 2f) + Mathf.Pow(direction.z, 2f)) * Mathf.Tan(angle), direction.z);

            newProjectile.GetComponent<Rigidbody>().AddForce(direction);
            Debug.Log("Throwing");
        }
        else{
            snowballs=maxSnowballs;
        }
    }


}
