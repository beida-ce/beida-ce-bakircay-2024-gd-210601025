using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Yerle�tirilecek nesneler
    public Vector3 spawnAreaSize = new Vector3(13, 0, 13); // Spawn alan� boyutlar�
    public float spawnHeight = 1f; // Nesnelerin sabit y�kseklik de�eri
    public float minimumSpacing = 2f; // Nesnelerin birbirine olan minimum mesafesi

    private List<Vector3> spawnPositions = new List<Vector3>(); // Kullan�lan pozisyonlar� takip eder

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
                Debug.LogWarning($"'{obj.name}' i�in uygun bir pozisyon bulunamad�!");
            }
        }
    }

    private Vector3 GenerateValidPosition()
    {
        int maxAttempts = 100; // �ak��ma kontrol� i�in maksimum deneme say�s�
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                spawnHeight, // Y�kseklik sabit
                Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
            );

            if (IsPositionValid(randomPosition))
            {
                spawnPositions.Add(randomPosition); // Pozisyonu kaydet
                return randomPosition;
            }
        }

        return Vector3.zero; // Ge�erli bir pozisyon bulunamazsa s�f�r d�ner
    }

    private bool IsPositionValid(Vector3 position)
    {
        foreach (Vector3 existingPosition in spawnPositions)
        {
            if (Vector3.Distance(existingPosition, position) < minimumSpacing)
            {
                return false; // �ak��ma varsa ge�ersiz
            }
        }

        return true; // �ak��ma yoksa ge�erli
    }
}


