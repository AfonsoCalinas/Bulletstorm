using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks
{
    // Movement variables
    public float speed = 6.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.0f;

    // Mouse look variables
    public float mouseSensitivity = 100f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private Camera playerCamera;
    private Transform soldierModel;  // Reference to the player model (Soldier_demo)

    void Start()
    {
        // Reference to the Soldier model where PhotonView is attached
        soldierModel = transform.Find("Soldier_demo");

        // CharacterController is expected to be on the Player GameObject (the camera in this case)
        controller = GetComponent<CharacterController>();

        AudioListener listener = playerCamera.GetComponent<AudioListener>();
        if (listener != null)
        {
            listener.enabled = true;
        }
    }

    void Update()
    {
        if (!photonView.IsMine) return;

        HandleMovement();
    }

    void HandleMovement()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Move relative to the camera's forward direction
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move.y = 0f;  // Ensure the player doesn't move vertically when moving forward/backward
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}



/* 
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    // Movement variables
    public float speed = 6.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.0f;

    // Mouse look variables
    public float mouseSensitivity = 100f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float xRotation = 0f;

    private PhotonView view;
    private Camera playerCamera;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        view = GetComponent<PhotonView>();
        playerCamera = GetComponentInChildren<Camera>();

        // Disable camera and script for non-local players
        if (!view.IsMine)
        {
            if (playerCamera != null)
            {
                playerCamera.enabled = false;
                AudioListener listener = playerCamera.GetComponent<AudioListener>();
                if (listener != null)
                {
                    listener.enabled = false;
                }
            }
            enabled = false;
            return;
        }

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!view.IsMine) return;

        HandleMovement();
        HandleMouseLook();
    }

    void HandleMovement()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
 */