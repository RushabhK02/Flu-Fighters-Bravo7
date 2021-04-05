﻿using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    public int powerupsToSpawn = 6;
    public GameObject batPrefab;
    public GameObject peoplePrefab;
    public float batChance = 0.2f;
    public float peopleChance = 0.4f;

    GroundSpawner groundSpawner;
    // Start is called before the first frame update
    private void Start()
    {
    	groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
    }

    private void OnTriggerExit (Collider other)
    {
    	groundSpawner.SpawnTile(true);
    	Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject obstaclePrefab;
    public int obstaclesToSpawn = 4;

    public void SpawnObstacle (int tileSpawnIndex)
    {
        //choose which obstacle to spawn
        GameObject obstacleToSpawn = obstaclePrefab;
        float random = Random.Range(0f, 1f);
        if (random < batChance)
        {
            obstacleToSpawn = batPrefab;
        }
        else if (random < peopleChance) {
            obstacleToSpawn = peoplePrefab;
        }

        int lowerBound = tileSpawnIndex*3 + 1;
        int upperBound = tileSpawnIndex*3 + 4;
        //Choosing rand point for obstacle
        int obstacleSpawnIndex = Random.Range(lowerBound, upperBound);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;
        Vector3 position = new Vector3(spawnPoint.position.x, 0.6f, spawnPoint.position.z);

        if (GameManager.inst.score < 10)
        {
            int probObstacle = Random.Range(0, 2);
            if (probObstacle >= 1)
                Instantiate(obstacleToSpawn, position, obstacleToSpawn.transform.rotation, transform);
        }
        else
        {
            Instantiate(obstacleToSpawn, position, obstacleToSpawn.transform.rotation, transform);
        }

        int obstacleLimit = Random.Range(1, this.obstaclesToSpawn);
        Vector3 secondPosition;
        if (obstacleLimit > 1 && GameManager.inst.score > 100)
        {
            int secondObstacleSpawnIndex = Random.Range(lowerBound, upperBound);
            Transform secondSpawnPoint = transform.GetChild(secondObstacleSpawnIndex).transform;
            secondPosition = new Vector3(secondSpawnPoint.position.x, 0.6f, secondSpawnPoint.position.z);
            if (position == secondPosition) return;
            // can make the obstacle random too
            Instantiate(batPrefab, secondPosition, obstacleToSpawn.transform.rotation, transform);

            Vector3 thirdPosition;
            if (obstacleLimit > 2)
            {
                int thirdObstacleSpawnIndex = Random.Range(lowerBound, upperBound);
                Transform thirdSpawnPoint = transform.GetChild(thirdObstacleSpawnIndex).transform;
                thirdPosition = new Vector3(thirdSpawnPoint.position.x, 0.6f, thirdSpawnPoint.position.z);
                if (position == thirdPosition || secondPosition == thirdPosition) return;
                // can make the obstacle random too
                Instantiate(batPrefab, thirdPosition, obstacleToSpawn.transform.rotation, transform);
            }
        }
    }

    public GameObject maskPrefab;
    public GameObject syringePrefab;
    public GameObject coinPrefab;
    public GameObject mysteryPrefab;
    public float syringeChance = 0.35f;
    public float maskChance = 0.2f;
    public float mysteryChance = 0.05f;
    public GameObject rightExtent;
    public GameObject leftExtent;
    public GameObject aheadExtent;
    public GameObject behindExtent;

    private List<Vector3> points;

    public void SpawnPowerUps(GameObject tile)
    {
        //choose which obstacle to spawn
        GameObject powerupsToSpawn = coinPrefab;
        float random = Random.Range(0f, 1f);
        if (random < mysteryChance)
        {
            powerupsToSpawn = mysteryPrefab;
        }
        else if (random < maskChance)
        {
            powerupsToSpawn = maskPrefab;
        }
        else if (random < syringeChance) 
        {
            powerupsToSpawn = syringePrefab;
        }

        int powerUpLimit = Random.Range(3, this.powerupsToSpawn);
        for (int i=0; i<powerUpLimit; i++)
        {
            GameObject temp = Instantiate(powerupsToSpawn, transform);
            temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
        }
    }

    Vector3 GetRandomPointInCollider(Collider collider)
    {
        Vector3 point = new Vector3(
            Random.Range(rightExtent.transform.position.x, leftExtent.transform.position.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(behindExtent.transform.position.z, aheadExtent.transform.position.z)
            );

        if (point == collider.ClosestPoint(point))
        {
            point = GetRandomPointInCollider(collider);
        }

        point.y = 1;
        return point;
    }
}