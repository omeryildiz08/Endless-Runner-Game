using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    RoadSpawner roadSpawner;
    public GameObject player;
    public List<GameObject> obstaclePrefab;
    public GameObject goldPrefab; // Altın prefab'ı
    public float minX = -3f; // Sol sınır
    public float maxX = 3f; // Sağ sınır
    public float spawnY = 1f; // Yükseklik (yerden ne kadar yüksekte)
    int minObstacle = 1;
    int maxObstacle = 3;

    public float obstacleSpawnChance = 0.6f; // Obstacle'ların spawnlanma şansı (%60)
    public float destroyDistance = 10f;

    private List<GameObject> spawnedObstacles = new List<GameObject>();

    private void Start()
    {
        roadSpawner = GetComponent<RoadSpawner>();
        
        
    }

    private void Update()
    {
        CheckAndDestroyObstacles();
    }
    public void SpawnGoldTrigger()
    {
        SpawnGold();
        SpawnObstacles();
    }
    public void SpawnTriggerEntered()
    {
        roadSpawner.MoveRoad();
        
        
    }

    private void SpawnGold()
    {
        float lastRoadZ = roadSpawner.GetLastRoadZ(); // En son yolun z pozisyonunu al
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, spawnY, lastRoadZ - 5f); // Yeni yol parçasının z konumu ile ilişkili

        // Altın prefab'ını spawnla
        Instantiate(goldPrefab, spawnPosition, Quaternion.identity);
    }

    private void SpawnObstacles()
    {
        float lastRoadZ = roadSpawner.GetLastRoadZ();
        if (Random.value < obstacleSpawnChance)
        {
            int numObstacles = Random.Range(minObstacle, maxObstacle );
            

            for (int i = 0; i < numObstacles; i++)
            {
                float randomX = Random.Range(-1, maxX);
                
                float ironX = 3.58f;
                
                // Rastgele bir obstacle prefab'ı seç
                GameObject obstaclePref = obstaclePrefab[Random.Range(0, obstaclePrefab.Count)];
                GameObject spawnedObstacle = null;

                if (obstaclePref==obstaclePrefab[0])
                {
                    Vector3 spawnPos0 = new Vector3(randomX, spawnY+3f, lastRoadZ - 3f);
                  spawnedObstacle =  Instantiate(obstaclePref, spawnPos0, Quaternion.identity);
                                   
                }
                else if (obstaclePref== obstaclePrefab[1])
                {
                    Vector3 spawnPos1 = new Vector3(ironX, spawnY+0.7f, lastRoadZ - 3f);
                   spawnedObstacle= Instantiate(obstaclePref, spawnPos1, Quaternion.identity);
                   
                }

                if (spawnedObstacle != null)
                {
                    spawnedObstacles.Add(spawnedObstacle);
                }
            }
        }
        

    }
    private void CheckAndDestroyObstacles()
    {
        // Spawnlanan engelleri kontrol etmek için
        for (int i = spawnedObstacles.Count - 1; i >= 0; i--)
        {
            if (player.transform.position.z - spawnedObstacles[i].transform.position.z > destroyDistance)
            {
                Destroy(spawnedObstacles[i]); // Engeli yok et
                Debug.Log("obstacle destroyed");
                spawnedObstacles.RemoveAt(i); // Listeden çıkar
            }
        }
    }


}
