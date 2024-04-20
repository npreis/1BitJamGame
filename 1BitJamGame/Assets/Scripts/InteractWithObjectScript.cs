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
    GameObject[] interactChangeObjects;

    [SerializeField]
    GameObject[] otherObjects;
    public bool canInteractChange;

    public AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        interactChangeObjects = GameObject.FindGameObjectsWithTag("InteractChange");
        otherObjects = GameObject.FindGameObjectsWithTag("Other");
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
            if (hit.collider.tag == "InteractChange")
            {
                canInteractChange = true;
                interactUI.SetActive(true);
                interactObject = hit.collider.gameObject;
            }
            else
            {
                canInteractChange = false;
                interactUI.SetActive(false);
                interactObject = null;
            }
        }
        else
        {
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
        for(int i  = 0; i < otherObjects.Length; i++)
        {
            if (otherObjects[i].layer == layer_ || otherObjects[i].layer == 0)
            {
                otherObjects[i].SetActive(true);
            }
            else
            {
                otherObjects[i].SetActive(false);
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
        for (int i = 0; i < otherObjects.Length; i++)
        {
            if (otherObjects[i].layer == layer_ || otherObjects[i].layer == 0)
            {
                otherObjects[i].SetActive(true);
            }
            else
            {
                otherObjects[i].SetActive(false);
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
