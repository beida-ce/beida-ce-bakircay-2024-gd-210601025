using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementArea : MonoBehaviour
{
    private GameObject placedObject = null;

    private void OnTriggerEnter(Collider other)
    {
        if (placedObject == null && other.CompareTag("Draggable"))
        {
            placedObject = other.gameObject;
            other.transform.position = transform.position; // Nesneyi yerle�tirme alan�na hizalar.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (placedObject == other.gameObject)
        {
            placedObject = null;
        }
    }
}
