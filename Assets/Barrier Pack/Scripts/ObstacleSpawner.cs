using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public List<GameObject> obstaclePrefabs; // Engellerin prefabları
    public float spawnInterval = 3f; // Engeller arası spawn aralığı
    public float roadWidth = 10f; // Yol genişliği

    private float nextSpawnTime; // Sonraki spawn zamanı
    private RoadSpawner roadSpawner; // RoadSpawner referansı
    private SpawnManager spawnManager; // SpawnManager referansı

    void Start()
    {
        nextSpawnTime = Time.time + spawnInterval; // İlk spawn zamanı
        roadSpawner = FindObjectOfType<RoadSpawner>(); // RoadSpawner'i bul
        spawnManager = FindObjectOfType<SpawnManager>(); // SpawnManager'i bul
    }

    void Update()
    {
        // Belirli aralıklarla engel spawnlamak için kontrol
        if (Time.time >= nextSpawnTime)
        {
            SpawnObstacle();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnObstacle()
    {
        // Yolun içine, rastgele bir x pozisyonunda bir grup engel spawnla
        float xPos = Random.Range(-roadWidth / 2f, roadWidth / 2f);

        // Yol parçalarını kontrol et
        foreach (GameObject road in roadSpawner.roads)
        {
            // Yolun z konumunu al
            float roadZ = road.transform.position.z;

            // Engellerin spawn pozisyonunu belirle
            Vector3 spawnPosition = new Vector3(xPos, transform.position.y, roadZ);

            // Engelleri spawnla
            int randomIndex = Random.Range(0, obstaclePrefabs.Count);
            GameObject obstaclePrefab = obstaclePrefabs[randomIndex];
            Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        }
    }

    // Bu metod spawn trigger alanına giriş yapıldığında çağrılacak
    public void OnSpawnTriggerEntered()
    {
        // Yol parçalarını hareket ettir
        roadSpawner.MoveRoad();
    }
}
