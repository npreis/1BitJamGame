using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class InteractWithObjectScript : MonoBehaviour
{
    [SerializeField]
    Camera cam;

    [SerializeField]
    GameObject interactUI;

    [SerializeField]
    GameObject interactObject;

    [SerializeField]
    GameObject[] interactObjects;

    [SerializeField]
    GameObject[] interactChangeObjects;
    public bool canInteract;
    public bool canInteractChange;

    public AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        canInteract = false;
        cam = Camera.main;
        interactObjects = GameObject.FindGameObjectsWithTag("Interact");
        interactChangeObjects = GameObject.FindGameObjectsWithTag("InteractChange");
        SearchForLayer_(9);
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
                canInteractChange = false;
                interactUI.SetActive(true);
                interactObject = hit.collider.gameObject;
            }
            if (hit.collider.tag == "InteractChange")
            {
                canInteract = false;
                canInteractChange = true;
                interactUI.SetActive(true);
                interactObject = hit.collider.gameObject;
            }
            else
            {
                canInteract = false;
                canInteractChange = false;
                interactUI.SetActive(false);
                interactObject = null;
            }
        }
        else
        {
            canInteract = false;
            canInteractChange = false;
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
            if(canInteractChange)
            {
                ChangeWorldLayerScript world = interactObject.GetComponent<ChangeWorldLayerScript>();
                SearchForLayer(world.worldLayer);
            }
            else
            {
                Debug.Log("Not Interacted");
            }
        }
    }

    //Layers to change:
    //7 to activate black and white world
    //8 for black and red
    //9 for black and blue
    //10 for black and yellow
    void SearchForLayer(int layer_)
    {
        for (int i = 0; i < interactObjects.Length; i++)
        {
            if (interactObjects[i].layer == layer_ || interactObjects[i].layer == 0)
            {
                interactObjects[i].SetActive(true);
            }
            else
            {
                interactObjects[i].SetActive(false);
            }
        }

        for(int i = 0; i < interactChangeObjects.Length; i++)
        {
            if (interactChangeObjects[i].layer == layer_)
            {
                interactChangeObjects[i].SetActive(false);
            }
            else
            {
                interactChangeObjects[i].SetActive(true);
            }
        }

        audio.Play();
    }

    //ONLY USE ONCE AT THE BEGINNING!!!!!
    void SearchForLayer_(int layer_)
    {
        for (int i = 0; i < interactObjects.Length; i++)
        {
            if (interactObjects[i].layer == layer_ || interactObjects[i].layer == 0)
            {
                interactObjects[i].SetActive(true);
            }
            else
            {
                interactObjects[i].SetActive(false);
            }
        }

        for (int i = 0; i < interactChangeObjects.Length; i++)
        {
            if (interactChangeObjects[i].layer == layer_)
            {
                interactChangeObjects[i].SetActive(false);
            }
            else
            {
                interactChangeObjects[i].SetActive(true);
            }
        }
    }
}
