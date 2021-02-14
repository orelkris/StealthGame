using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorRayCast : MonoBehaviour
{

    [SerializeField] private int rayLength = 5;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludeLayerName = null;
    //GameObject frame;
    private MyDoorController rayCastedObject;

    [SerializeField] private KeyCode openDoorKey = KeyCode.Mouse0;

    [SerializeField] private Image crosshair = null;
    private bool isCrosshairActive;
    private bool doOnce;

    private const string interactableTag = "InteractiveObject";
    void Start()
    {
        //frame = GameObject.Find("DoorV4");

    }
    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, mask))
        {
            if (hit.collider.CompareTag(interactableTag))
            {
                if (!doOnce)
                {
                    rayCastedObject = hit.collider.gameObject.GetComponent<MyDoorController>();
                    CrosshairChange(true);
                }

                isCrosshairActive = true;
                doOnce = true;

                if (Input.GetKeyDown(openDoorKey))
                {
                    rayCastedObject.PlayAnimation();
                }
            }
        }
        else
        {
            if(isCrosshairActive)
            {
                CrosshairChange(false);
                doOnce = false;
            }
        }
    }

    void CrosshairChange(bool on)
    {
        if(on && !doOnce)
        {
            crosshair.color = Color.red;
            //frame.GetComponent<Renderer>().material.color = Color.red;
            
        }
        else
        {
            crosshair.color = Color.white;
            //frame.GetComponent<Renderer>().material.color = Color.clear;
            isCrosshairActive = false;
        }
    }

}
