using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Yerleþtirilecek nesneler
    public Vector3 spawnAreaSize = new Vector3(13, 0, 13); // Spawn alaný boyutlarý
    public float spawnHeight = 1f; // Nesnelerin sabit yükseklik deðeri
    public float minimumSpacing = 2f; // Nesnelerin birbirine olan minimum mesafesi

    private List<Vector3> spawnPositions = new List<Vector3>(); // Kullanýlan pozisyonlarý takip eder

    private void Start()
    {
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        foreach (GameObject obj in objectsToSpawn)
        {
            Vector3 spawnPosition = GenerateValidPosition();

            if (spawnPosition != Vector3.zero)
            {
                Instantiate(obj, spawnPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning($"'{obj.name}' için uygun bir pozisyon bulunamadý!");
            }
        }
    }

    private Vector3 GenerateValidPosition()
    {
        int maxAttempts = 100; // Çakýþma kontrolü için maksimum deneme sayýsý
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                spawnHeight, // Yükseklik sabit
                Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
            );

            if (IsPositionValid(randomPosition))
            {
                spawnPositions.Add(randomPosition); // Pozisyonu kaydet
                return randomPosition;
            }
        }

        return Vector3.zero; // Geçerli bir pozisyon bulunamazsa sýfýr döner
    }

    private bool IsPositionValid(Vector3 position)
    {
        foreach (Vector3 existingPosition in spawnPositions)
        {
            if (Vector3.Distance(existingPosition, position) < minimumSpacing)
            {
                return false; // Çakýþma varsa geçersiz
            }
        }

        return true; // Çakýþma yoksa geçerli
    }
}


