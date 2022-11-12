using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller: MonoBehaviour
{
	public CharacterController character;
	public int playerSpeed = 10;
	private Vector3 direction = Vector3.zero;
	private Vector3 lookDirection = Vector3.zero;
	public float rotTime = 5;

	// Start is called before the first frame update
	void Start() {}

	// Update is called once per frame
	void Update()
	{
		direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));	//set direction
		direction = direction.normalized;

		character.SimpleMove(direction *playerSpeed);
		var cam = GameObject.Find("Main Camera");
		//define look direction, that's not set to 0 if the player is not moving
		if (new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) != Vector3.zero)
		{
			lookDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));	//set direction
		}

		if (lookDirection != Vector3.zero)
		{
			// if the difference between the current rotation and the look direction is greater than 0.1, rotate the player
			if (Vector3.Angle(transform.forward, lookDirection) > 1)
			{
				//rotate the player
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), rotTime * Time.deltaTime);
			}
			//set to current rotation if difference is less than 1 but exists
			else if (transform.rotation != Quaternion.LookRotation(lookDirection))
			{
				//rotate the player
				transform.rotation = Quaternion.LookRotation(lookDirection);
			}
			
		}

		Vector3 charPos = character.transform.position;	//current character postiioin
		charPos.y += 8;	//correct postition to be diagonally above
		charPos.z -= 8;
		cam.transform.position = charPos;
	}
}