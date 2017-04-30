using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy; //Enemy prefab.
    public float spawnRate; //Rate at which enemies spawn.

    private bool spawnEnemy = true; //When true an enemy needs to be spawned.
    private bool waiting = false; //When true the time until the next spawn is being measured.

    private GameObject[] spawnPoints;
    private GameObject[] homePoints;
	
    void Start()
    {

        spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");

        homePoints = GameObject.FindGameObjectsWithTag("HomePoint");

    }

	// Update is called once per frame
	void Update () {
	
        if (spawnEnemy)
        {

            SpawnEnemy();
            spawnEnemy = false;

        }
        else if (!waiting) //If not already waiting start waiting.
        {

            StartCoroutine("SpawnDelay");

        }

	}

    void SpawnEnemy ()
    {

        Vector3 spawnLocation = GetRandomPoint(spawnPoints);
        GameObject newEnemy = Instantiate(enemy, spawnLocation, Quaternion.identity) as GameObject;
        TankAI enemyAI = newEnemy.GetComponent<TankAI>();

        Vector3 target = GetRandomPoint(homePoints);
        enemyAI.SetTarget(target);

    }


    IEnumerator SpawnDelay()
    {

        waiting = true;

        yield return new WaitForSeconds(spawnRate);

        spawnEnemy = true;
        waiting = false;

    }

    Vector3 GetRandomPoint(GameObject[] points)
    {

        int pointIndex = Random.Range(0, points.Length);

        Vector3 pointLocation = points[pointIndex].transform.position;

        return pointLocation;

    }

}
