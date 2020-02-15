using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple class that spawns obstacles within a set radius
/// around the obj this is attached to
/// </summary>
public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;    //Array of obstacles to spawn. Randomly chooses between objects in the array
    public float spawnRadius;   //Radius from this classes obj to spawn obstacles
    public float spawnRate; //How fast in seconds to spawn obstacles

    private void Start()
    {
        StartSpawningObstacles();
    }

    public void StartSpawningObstacles()
    {
        StartCoroutine(SpawnRoutine());
    }

    /// <summary>
    /// Coroutine that continuously spawns obstacles
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnRoutine()
    {
        while(true)
        {
            //Random angle to determine where the obstacle will spawn around this gameobject
            float randAngle = Random.Range(0, 2 * Mathf.PI);

            Vector3 spawnPos = new Vector3
            {
                /**
                 * Got that calc
                 * x = r * cos(theta)
                 * y = r * sin(theta)
                 * Wait was that calc?
                 */
                x = transform.position.x + spawnRadius * Mathf.Cos(randAngle),
                y = transform.position.y,
                z = transform.position.z + spawnRadius * Mathf.Sin(randAngle)
            };

            //Spawns object and waits to spawn a new random one
            Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], spawnPos, transform.rotation);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
