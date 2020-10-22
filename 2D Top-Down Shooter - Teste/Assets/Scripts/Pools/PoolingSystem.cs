using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolingSystem : MonoBehaviour
{
    [SerializeField] protected GameObject[] objectsPrefab;
    protected Dictionary<Type, Queue<GameObject>> pools = new Dictionary<Type, Queue<GameObject>>();
    public List<GameObject> listOfAllObjects = new List<GameObject>();

    protected virtual void Awake()
    {
        CreatePools();
    }

    protected virtual void CreatePools()
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

            for (int j = 0; j < pooledObject.AmountToPool; j++)
            {
                GameObject newObject = Instantiate(objectsPrefab[i]);
                newObject.transform.parent = this.transform;
                newObject.GetComponent<IPooledObject>().Pool = this;
                newObject.SetActive(false);

                listOfAllObjects.Add(newObject);
                pooledObjectsQueue.Enqueue(newObject);
            }
            pools[pooledObject.GetType()] = pooledObjectsQueue;
        }
    }

    public virtual T SpawnObject<T>(Vector3 position, Quaternion rotation)
    {
        GameObject objectToSpawn = pools[typeof(T)].Dequeue();

        objectToSpawn.transform.position = position;
        objectToSpawn.transform.GetChild(0).rotation = rotation;

        objectToSpawn.SetActive(true);
        T gameobj = objectToSpawn.GetComponent<T>();

        return gameobj;
    }

    public virtual GameObject SpawnObject(Type type, Vector3 position, Quaternion rotation)
    {
        GameObject objectToSpawn = pools[type].Dequeue();

        objectToSpawn.transform.position = position;
        objectToSpawn.transform.GetChild(0).rotation = rotation;

        objectToSpawn.SetActive(true);

        return objectToSpawn;
    }

    public virtual GameObject SpawnAnyObject(GameObject objectToSpawn, Vector3 position, Quaternion rotation)
    {
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.GetChild(0).rotation = rotation;

        objectToSpawn.SetActive(true);

        return objectToSpawn;
    }

    public virtual void SpawnInRandomPlaces<T>(bool useRandomRotation)
    {
        for (int i = 0; i < listOfAllObjects.Count; i++)
        {
            Vector2 randomPlace = Level.GetInstance().GetRandomPointInMap();

            Vector3 desiredRotation = Vector3.zero;

            if (useRandomRotation)
                desiredRotation = new Vector3(0, 0, UnityEngine.Random.Range(0, 360));

            SpawnObject<T>(randomPlace, Quaternion.Euler(desiredRotation));
        }
    }

    public virtual void DespawnObject(Type objectType, GameObject objectToDespawn)
    {
        if (!pools.ContainsKey(objectType))
        {
            Debug.Log("Couldnt find this type of object on dictionary.");
            return;
        }

        objectToDespawn.SetActive(false);
        objectToDespawn.transform.parent = this.transform;
        objectToDespawn.transform.position = Vector2.one * 1000f;
        objectToDespawn.transform.rotation = this.transform.rotation;

        pools[objectType].Enqueue(objectToDespawn);
    }
}