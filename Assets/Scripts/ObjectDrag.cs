using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    private float zAxisOffset;

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera bulunamadý! Kamerayý 'MainCamera' olarak ayarlayýn.");
        }
    }

    void OnMouseDown()
    {
        if (mainCamera == null) return;

        // Z ekseni mesafesini al
        zAxisOffset = mainCamera.WorldToScreenPoint(transform.position).z;

        // Fare pozisyonunu hesapla
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = zAxisOffset;

        offset = transform.position - mainCamera.ScreenToWorldPoint(mousePosition);

        Debug.Log($"Nesne týklandý: {gameObject.name}");
    }

    void OnMouseDrag()
    {
        if (mainCamera == null) return;

        // Fare pozisyonunu hesapla
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = zAxisOffset;

        // Yeni pozisyonu uygula
        transform.position = mainCamera.ScreenToWorldPoint(mousePosition) + offset;
    }
}
