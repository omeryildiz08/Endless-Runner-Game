using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public List<GameObject> roads;

    private float offset = 10f;

    private void Start()
    {
        if (roads != null && roads.Count > 0)
        {
            roads = roads.OrderBy(r => r.transform.position.z).ToList();
        }
    }
    public float GetLastRoadZ()
    {
        return roads[roads.Count - 1].transform.position.z; // Listenin sonundaki yol parçasının z konumunu döner
    }
    public void MoveRoad()
    {
        GameObject movedRoad = roads[0];
        roads.Remove(movedRoad);
        float newZ = roads[roads.Count - 1].transform.position.z + offset;
        movedRoad.transform.position = new Vector3(0, 0, newZ);
        roads.Add(movedRoad);
    }

    public void ResetRoads()
    {
        // Roads listesini baştaki orijinal konumlarına sıfırlıyoruz
        float initialZ = 69f; 
        for (int i = 0; i < roads.Count; i++)
        {
            roads[i].transform.position = new Vector3(0, 0, initialZ + (i * offset));
        }

        roads = roads.OrderBy(r => r.transform.position.z).ToList();
    }
}
