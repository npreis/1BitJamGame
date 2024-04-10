using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateScript : MonoBehaviour
{
    public Transform orientation;

    public float mouseSensitivityX;
    public float mouseSensitivityY;

    float cameraXRotation;
    float cameraYRotation;

    bool lockedCursor = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        float inputX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * mouseSensitivityX;
        float inputY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * mouseSensitivityY;

        cameraYRotation += inputX;
        cameraXRotation -= inputY;

        cameraXRotation = Mathf.Clamp(cameraXRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(cameraXRotation, cameraYRotation, 0);
        orientation.rotation = Quaternion.Euler(0, cameraYRotation, 0);
    }
}
