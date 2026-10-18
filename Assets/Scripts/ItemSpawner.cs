using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject hazardPrefab;
    [SerializeField] private GameObject healthPackPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private int minimumItems = 2;
    [SerializeField] private int maximumItems = 10;
    [SerializeField] private float safeDistance = 2f;

    private GameObject[] spawnedItems;
    private float timer;

    private void Start()
    {
        spawnedItems = new GameObject[spawnPoints.Length];
        ResetTimer();
    }

    private void Update()
    {
        int count = CountItems();

        // Si faltan objetos, intentamos reponerlos sin esperar.
        while (count < minimumItems && count < maximumItems)
        {
            if (!TrySpawn())
            {
                break;
            }

            count++;
        }

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            if (count < maximumItems)
            {
                TrySpawn();
            }

            ResetTimer();
        }
    }

    private int CountItems()
    {
        int count = 0;

        foreach (GameObject item in spawnedItems)
        {
            if (item != null)
            {
                count++;
            }
        }

        return count;
    }

    private bool TrySpawn()
    {
        List<int> availablePoints = new List<int>();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnPoints[i] == null || spawnedItems[i] != null)
            {
                continue;
            }

            // Medimos la distancia sobre el suelo, usando X y Z.
            Vector3 difference = spawnPoints[i].position - player.position;
            difference.y = 0f;

            if (difference.magnitude >= safeDistance)
            {
                availablePoints.Add(i);
            }
        }

        if (availablePoints.Count == 0)
        {
            return false;
        }

        int index = availablePoints[
            Random.Range(0, availablePoints.Count)
        ];

        GameObject prefab = Random.Range(0, 2) == 0
            ? hazardPrefab
            : healthPackPrefab;

        spawnedItems[index] = Instantiate(
            prefab,
            spawnPoints[index].position,
            Quaternion.identity
        );

        return true;
    }

    private void ResetTimer()
    {
        timer = Random.Range(5f, 10f);
    }
}