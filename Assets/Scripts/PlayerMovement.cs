using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.0f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    private PhotonView view;
    private Camera playerCamera;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        view = GetComponent<PhotonView>();
        
        // Get the camera component
        playerCamera = GetComponentInChildren<Camera>();

        // Disable camera if this is not the local player
        if (!view.IsMine)
        {
            if (playerCamera != null)
            {
                playerCamera.enabled = false;
            }
            AudioListener listener = GetComponentInChildren<AudioListener>();
            if (listener != null)
            {
                listener.enabled = false;
            }
            enabled = false; // Disable this script for non-local players
        }
    }

    void Update()
    {
        if (!view.IsMine) return;

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
}
