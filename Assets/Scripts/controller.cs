using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    public Rigidbody character;
    public int playerSpeed = 10;
    private Vector3 direction = Vector3.zero;
    private Vector3 lookDirection = Vector3.zero;
    public float rotTime = 5;
    public bool isInHouse = false;
    public bool hasSetHint = false;

    public bool dialogueMode = false;
    public Animator anim;

    //interactable objects
    private GameObject cam;
    private GameObject OpenHint;
    private OpenHintScript OpenHintText;
    private Collider doorCollider = null;

    public int collected=0;
    public int maxCollected=1;

    //scripts
    public sb_thrower thrower;
    private UIScript uis;

    void Start()
    {
        //load player prefs
        if (PlayerPrefs.HasKey("isInHouse"))
        {
            isInHouse = PlayerPrefs.GetInt("isInHouse") == 1;
        }
        else
        {
            PlayerPrefs.SetInt("isInHouse", 0);
        }
        if (PlayerPrefs.HasKey("PlayerX")&& PlayerPrefs.HasKey("PlayerY") && PlayerPrefs.HasKey("PlayerZ"))
        {
            transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"));
        }else
            {
                PlayerPrefs.SetFloat("PlayerX", transform.position.x);
                PlayerPrefs.SetFloat("PlayerY", transform.position.y);
                PlayerPrefs.SetFloat("PlayerZ", transform.position.z);
        }
        if (PlayerPrefs.HasKey("collected"))
        {
            collected = PlayerPrefs.GetInt("collected");
        }
        else
        {
            PlayerPrefs.SetInt("collected", 0);
        }


        if(!isInHouse){
            FindObjectOfType<AudioManager>().Play("Snow Background");

        }
        if(isInHouse){
            FindObjectOfType<ParticleSystem>().Stop();
        }
        cam = GameObject.Find("Main Camera");
        OpenHint = GameObject.Find("Open Hint");
        OpenHintText = GameObject.Find("Open Hint Text").GetComponent<OpenHintScript>();
        OpenHint.SetActive(false);
        uis = GameObject.Find("UI").GetComponent<UIScript>();


    }

    void OnTriggerEnter(Collider other)
    {
        //check if the collider has the door tag
        if (other.gameObject.CompareTag("Door"))
        {
            doorCollider = other;

            
                if (!hasSetHint)
                {
                    hasSetHint = true;
                    OpenHint.SetActive(true);
                    OpenHintText.text("ÖFFNEN [F]");
                }

        
        }
        //else if other game object has dialogue properties script
            else if (other.gameObject.CompareTag("Dialogue"))
            {
            if (!hasSetHint)
            {
                hasSetHint = true;
                OpenHint.SetActive(true);
                OpenHintText.text("REDEN [F]");
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
         if (other.gameObject.CompareTag("Door"))
        {
        doorCollider = null;
            hasSetHint = false;
            //hide ui object open hint
            OpenHint.SetActive(false);

        }
            else if (other.gameObject.CompareTag("Dialogue"))

        {
            hasSetHint = false;
            OpenHint.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!dialogueMode){
            direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //set direction
            direction = direction.normalized;

            if (direction != Vector3.zero && FindObjectOfType<AudioManager>().getSound("Footstep Snow").source.isPlaying == false && FindObjectOfType<AudioManager>().getSound("Footstep House").source.isPlaying == false)
            {
                if (isInHouse)
                    FindObjectOfType<AudioManager>().Play("Footstep House");
                else
                    FindObjectOfType<AudioManager>().Play("Footstep Snow");
            }
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
            charPos.y += 6; //correct postition to be diagonally above
            charPos.z -= 6;
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
            if(Input.GetKeyDown(KeyCode.H)){
                //teleport home
                transform.position = new Vector3(0, 10, 0);
            }
            if (Input.GetKeyDown(KeyCode.F) && hasSetHint && doorCollider != null)
            {
                //fade in
                uis.fadeOut();

                //get the door properties
                DoorProperties doorProperties = doorCollider.gameObject.GetComponent<DoorProperties>();
                transform.position = doorProperties.teleportPosition;
                FindObjectOfType<AudioManager>().Play("Door Enter");

                //set is inside house
                isInHouse = doorProperties.leadingInside;
                if(isInHouse){
                    FindObjectOfType<ParticleSystem>().Stop();
                    FindObjectOfType<AudioManager>().Stop("Snow Background");
                    }
                else{
                    FindObjectOfType<ParticleSystem>().Play();
                    FindObjectOfType<AudioManager>().Play("Snow Background");}

                     

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

    public void addCollected(){
        collected++;
        if(collected >=maxCollected){
            //win
            uis.fadeOut();
            dialogueMode = true;
        }
    }
    void FixedUpdate()
    {
        if (direction != Vector3.zero && dialogueMode == false)
        {
            anim.SetBool("isRunning", true);
            character.MovePosition(transform.position + direction * playerSpeed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }



    //on quit, store the current position in playerprefs
    void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("PlayerX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", transform.position.z);
        PlayerPrefs.SetInt("isInHouse", isInHouse ? 1 : 0);
        PlayerPrefs.SetInt("collected", collected);
    }
}