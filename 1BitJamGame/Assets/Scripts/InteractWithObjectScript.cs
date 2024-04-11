using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithObjectScript : MonoBehaviour
{
    [SerializeField]
    Camera cam;

    [SerializeField]
    GameObject interactUI;

    [SerializeField]
    GameObject interactObject;
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
                interactObject = hit.collider.gameObject;
            }
            if (hit.collider.tag == "Untagged")
            {
                canInteract = false;
                interactUI.SetActive(false);
                interactObject = null;
            }
        }
        else
        {
            canInteract = false;
            interactUI.SetActive(false);
            interactObject = null;
        }

        Interact(interactObject);
    }

    void Interact(GameObject interact)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(canInteract)
            {
                Destroy(interact);
            }
            else
            {
                Debug.Log("Not Interacted");
            }
        }
    }
}
