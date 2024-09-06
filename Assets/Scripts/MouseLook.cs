using UnityEngine;
using Photon.Pun;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;

    private PhotonView view;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        view = GetComponentInParent<PhotonView>();  // Get the PhotonView from the parent object

        // Disable this script if this is not the local player
        if (!view.IsMine)
        {
            enabled = false;
        }
    }

    void Update()
    {

        if (!view.IsMine) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
