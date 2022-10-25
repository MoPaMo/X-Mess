using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
           private CharacterController character;
            public int playerSpeed = 69;
    // Start is called before the first frame update
    void Start()
    {
           character = GetComponent(typeof(CharacterController)) as CharacterController;

    }
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //set direction
        direction=direction.normalized;
        character.SimpleMove(direction*playerSpeed);
    }
}
