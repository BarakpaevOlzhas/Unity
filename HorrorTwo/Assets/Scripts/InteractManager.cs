using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractManager : MonoBehaviour
{
    [SerializeField]
    private float interactDistance;

    [SerializeField]
    private Transform cameraPosition;

    [SerializeField]
    private FlashLight flashLight;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private Image interactImage;

    private bool haveKey = false;

    void Start()
    {
        interactImage.gameObject.SetActive(false);
    }
   
    void Update()
    {
        Ray ray = new Ray(cameraPosition.position, cameraPosition.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactDistance, layerMask))
        {
            interactImage.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E)) {               

                if (hit.collider.tag == "Battery")
                {
                    flashLight.AddEnergy(1.5f);
                    Destroy(hit.collider.gameObject);
                }
                else if (hit.collider.tag == "Candle")
                {
                    var candle = hit.collider.GetComponent<Candle>();
                    candle.SetActive();
                }
                else if(hit.collider.tag == "Key")
                {
                    haveKey = true;
                    Destroy(hit.collider.gameObject);
                }
                else if (hit.collider.tag == "Door")
                {
                    var door = hit.collider.GetComponentInParent<DoorOpen>();
                    if (haveKey)
                    {
                    door.Unlock();
                    }
                    door.Open();
                }
            }
        }    
        else
        {
            interactImage.gameObject.SetActive(false);
        }

    }
}
