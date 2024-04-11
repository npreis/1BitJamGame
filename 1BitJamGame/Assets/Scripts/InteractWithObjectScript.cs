using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithObjectScript : MonoBehaviour
{
    [SerializeField]
    Camera cam;

    [SerializeField]
    GameObject interactUI;
    bool canInteract;
    // Start is called before the first frame update
    void Start()
    {
        canInteract = false;
        cam = Camera.main;
    }

    private void Update()
    {
        RaycastHit hit;
        float rayDistance = 1.5f;
        Ray ray = new Ray(origin: cam.transform.position, direction: cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance);
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.tag == "Interact")
            {
                canInteract = true;
                interactUI.SetActive(true);
            }
            if (hit.collider.tag == "Untagged")
            {
                canInteract = false;
                interactUI.SetActive(false);
            }
        }
        else
        {
            canInteract = false;
            interactUI.SetActive(false);
        }

        Interact();
    }

    void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(canInteract)
            {
                Debug.Log("Interacted");
            }
            else
            {
                Debug.Log("Not Interacted");
            }
        }
    }
}
