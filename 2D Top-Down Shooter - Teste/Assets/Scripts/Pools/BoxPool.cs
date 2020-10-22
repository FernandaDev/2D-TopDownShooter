using System;
using UnityEngine;

public class BoxPool : PoolingSystem
{
    float amountToPool;
    float boxCounter;

    private void Start()
    {
        SpawnInRandomPlaces<Box>(true);
        amountToPool = FindObjectOfType<Box>().AmountToPool;
        boxCounter = amountToPool;
    }

    public override void DespawnObject(Type objectType, GameObject objectToDespawn)
    {
        if (!pools.ContainsKey(objectType))
        {
            Debug.LogWarning("Couldnt find this type of object on dictionary.");
            return;
        }
        objectToDespawn.SetActive(false);
        objectToDespawn.transform.position = Vector2.one * 1000f;
        pools[objectType].Enqueue(objectToDespawn);

        boxCounter--;
        if (boxCounter <= 0) 
            Respawn();
    }

    void Respawn()
    {
        boxCounter = amountToPool;
        SpawnInRandomPlaces<Box>(true);
    }
}