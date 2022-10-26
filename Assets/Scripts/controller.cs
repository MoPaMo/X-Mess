using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
           private CharacterController character;
            public int playerSpeed = 10;

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
                     var cam = GameObject.Find("Main Camera");

        Vector3 charPos=character.transform.position; //current character postiioin
        charPos.y+=8; //correct postition to be diagonally above
        charPos.z-=8;
        cam.transform.position=charPos;

    }
}
