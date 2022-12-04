using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    public CharacterController character;
    public int playerSpeed = 10;
    private Vector3 direction = Vector3.zero;
    private Vector3 lookDirection = Vector3.zero;
    public float rotTime = 5;
    public bool isInHouse = false;

    private bool isInteractable = false;
    private GameObject interactableObj = null;
    public sb_thrower thrower;
    // Start is called before the first frame update
    private GameObject cam;

    void Start()
    {
        cam = GameObject.Find("Main Camera");
    }

    void OnTriggerEnter(Collider other)
    {
        //check if the collider has the door tag
        if (other.gameObject.CompareTag("Door"))
        {
            //get the door properties
            DoorProperties doorProperties = other.gameObject.GetComponent<DoorProperties>();
            //set the teleport position
            character.Move(doorProperties.teleportPosition - transform.position);
            //set is inside house
            isInHouse = doorProperties.leadingInside;
        }
        if (other.gameObject.CompareTag("Interactable"))
        {

            isInteractable = true;
            interactableObj = other.gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            isInteractable = false;
            interactableObj = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //set direction
        direction = direction.normalized;

        character.SimpleMove(direction * playerSpeed);
        //define look direction, that's not set to 0 if the player is not moving
        if (new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) != Vector3.zero)
        {
            lookDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //set direction
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

        Vector3 charPos = character.transform.position; //current character postiioin
        charPos.y += 8; //correct postition to be diagonally above
        charPos.z -= 8;
        if (cam.transform.position != charPos)
        {

            cam.transform.position = charPos;

            /*Ray r = new Ray(cam.transform.position, cam.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit))
            {
                if (hit.collider.gameObject != gameObject)
                {
                    hit.collider.gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.5f);
                }
            }*/
        }
        if (Input.GetKeyDown(KeyCode.F) && isInteractable)
        {
            Debug.Log(interactableObj.GetComponent<DialogueProperties>().dialog);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) != Vector3.zero)
                thrower.Throw(direction);
            else
                thrower.Throw(transform.forward);
        }
    }
}