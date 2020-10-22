using System.Collections.Generic;
using UnityEngine;

public class DropsPool : PoolingSystem 
{
    // A quantidade de drops criados vai de acordo com a sua raridade.
    protected override void CreatePools() 
    {
        for (int i = 0; i < objectsPrefab.Length; i++)
        {
            IPooledObject pooledObject = objectsPrefab[i].GetComponent<IPooledObject>();
            
            if (pooledObject == null)
            {
                Debug.LogError(objectsPrefab[i].name + " - This prefab doesn't implement 'IPooledObject'. " +
                                                       "Aren't you forgeting something?");
                return;
            }

            Queue<GameObject> pooledObjectsQueue = new Queue<GameObject>();

            if (pools.ContainsKey(pooledObject.GetType()))
                pooledObjectsQueue = pools[pooledObject.GetType()];

            int amountToSpawn = (int)objectsPrefab[i].GetComponent<IDrop>().ItemRarity;

            for (int j = 0; j < amountToSpawn; j++)
            {
                GameObject newObj = Instantiate(objectsPrefab[i]);
                newObj.transform.parent = this.transform;
                newObj.GetComponent<IPooledObject>().Pool = this;
                newObj.SetActive(false);

                listOfAllObjects.Add(newObj);
                pooledObjectsQueue.Enqueue(newObj);
            }

            pools[pooledObject.GetType()] = pooledObjectsQueue;
        }
    }

    public GameObject SpawnRandomDrop(Vector3 position, Quaternion rotation)
    {
        int randomIndex = Random.Range(0, listOfAllObjects.Count);
        if (listOfAllObjects[randomIndex].activeInHierarchy)
        {
            return null;
        }

        GameObject randomObjectToSpawn = listOfAllObjects[randomIndex];

        return SpawnAnyObject(randomObjectToSpawn, position, rotation);
    }
}
