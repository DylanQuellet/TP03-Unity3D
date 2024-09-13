using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Player;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float mouseSensibility = 100f;
    public Transform groundCheck;
    public Transform player;
    public Transform playerBody;
    public Transform playerNeck;
    public Camera playerCam;
    public float groundCheckRadius = 0.2f;
    public Rigidbody rb;
    private Collider coll;
    public LayerMask groundLayer;
    public bool isGrounded;
    private float xRotation = 0f;
    private float yRotation = 0f;
    private float yHeadRotation = 0f;
    public float maxZoom = -20f;
    public float minZoom = -2f;
    private bool camHead;

    private bool playing;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playing = true;
        camHead = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        float moveInputHorizontal = Input.GetAxis("Horizontal");
        float moveInputVertical = Input.GetAxis("Vertical");

        Vector3 moveVelocity = new Vector3(0f, rb.velocity.y, 0f);
        rb.velocity = moveVelocity;

        Vector3 moveDirection = new Vector3(moveInputHorizontal * moveSpeed, 0f, moveInputVertical * moveSpeed);
        Vector3 moveGlobal = transform.TransformDirection(moveDirection);
        rb.MovePosition(rb.position + moveGlobal);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (playing)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensibility * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensibility * Time.deltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -75f, 75f);
            yRotation += mouseX;
            yHeadRotation += mouseX;

            if (!camHead)
            {
                playerNeck.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                player.transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
            }
            else
            {
                playerNeck.transform.localRotation = Quaternion.Euler(xRotation, yHeadRotation, 0f);
            }
            float mouseScrollWheel = Input.GetAxis("Mouse ScrollWheel"); // +=^ ; -=v


            if (mouseScrollWheel != 0f)
            {
                Debug.Log("Scroll");
                playerCam.transform.Translate(Vector3.forward * mouseScrollWheel, Space.Self);

                float currentZoom = Mathf.Clamp(playerCam.transform.localPosition.z, -maxZoom, -minZoom);
                playerCam.transform.localPosition = new Vector3(playerCam.transform.localPosition.x, playerCam.transform.localPosition.y, currentZoom);
            }


            ///Pour basculer entre les 2 vues de manières fluide et cohérente :
            if (Input.GetMouseButtonDown(0)) // Clic Gauche
            {
                Debug.Log("Clic gauche");
                if (!camHead)
                {
                    yHeadRotation = 0f;
                }
                camHead = true;
            }
            if (Input.GetMouseButtonDown(1)) // Clic Droit
            {
                Debug.Log("Clic Droit");

                //playerBody.transform.localRotation = Quaternion.Euler(0f, player.transform.localRotation.y, 0f); // ça change R
                
                camHead = false;
            }


        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            playing = false;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            playing = true;
        }
    }
}
