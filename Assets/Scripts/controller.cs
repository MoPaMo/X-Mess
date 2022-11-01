using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller: MonoBehaviour {
  private CharacterController character;
  public int playerSpeed = 10;
  private Vector3 direction = Vector3.zero;
  private Vector3 lookDirection = Vector3.zero;
  public float rotTime = 5;
  // Start is called before the first frame update
  void Start() {
    character = GetComponent(typeof(CharacterController)) as CharacterController;

  }
  // Update is called once per frame
  void Update() {    
    direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //set direction
    direction = direction.normalized;
    
    character.SimpleMove(direction * playerSpeed);
    var cam = GameObject.Find("Main Camera");
//define look direction, that's not set to 0 if the player is not moving
    if (new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) != Vector3.zero) {
      lookDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //set direction
    }
    if (lookDirection != Vector3.zero) {
      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * rotTime);
    }
    

    /*
     var rot = Quaternion.LookRotation(direction);
    rot = Quaternion.Slerp(cam.transform.rotation, rot, Time.deltaTime * rotTime);
    rot = Quaternion.Euler(0, rot.eulerAngles.y, 0);
    transform.rotation = rot;

        if (direction!=Vector3.zero) {
      Quaternion rotation = Quaternion.LookRotation(direction);
      transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotTime);
    }*/

    
    Vector3 charPos = character.transform.position; //current character postiioin
    charPos.y += 8; //correct postition to be diagonally above
    charPos.z -= 8;
    cam.transform.position = charPos;

  }
}