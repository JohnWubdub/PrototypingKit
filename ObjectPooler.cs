using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages multiple pools of commonly instantiated objects.
/// This system prevents calling Instantiate() and Destroy() a lot and wrecking performance, by spawning a bunch of objects at once,
/// and keeping them in the scene, but enabling/disabling them when they need to be spawned and despawned. Works great for things like bullets.
/// This also means that wherever you would Destroy() an object that will be pooled, you can just SetActive(false) instead.
/// </summary>
public class ObjectPooler : MonoBehaviour
{
    public Dictionary<string, ObjectPool> Pools = new Dictionary<string, ObjectPool>();

    //For use with a Services manager class, if you're not using one, just delete this.
    void Awake()
    {
        Services.ObjectPools = this;
    }

    /// <summary>
    /// Creates a pool of [objectToPool], and adds [initialAmount] of them into the pool.
    /// </summary>
    public void Create(GameObject objectToPool, int initialAmount)
    {
        if (!Pools.ContainsKey(objectToPool.name))
            Pools.Add(objectToPool.name, new ObjectPool(objectToPool, initialAmount));
        else
          Debug.Log("There's already an object pool for " + objectToPool.name + "dummy");
    }

    /// <summary>
    /// Gets a GameObject from its pool then sets it as active, "spawning" it into the scene.
    /// </summary>
    public GameObject Spawn(GameObject objectToSpawn) => Spawn(objectToSpawn, Vector3.zero, Quaternion.identity);

    //Overloaded version of the method, spawns the object and places it at a specified position and rotation. Used like Instantiate() for pooled objects.
    public GameObject Spawn(GameObject objectToSpawn, Vector3 position, Quaternion rotation)
    {
        try
        {
            GameObject obj = Pools[objectToSpawn.name].Get();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
            return obj;
        }
        catch (KeyNotFoundException e)
        {
            throw new Exception("There is no object pool with the object " + objectToSpawn.name + "! Try making a pool first with Create().");
        }
    }

}


/// <summary>
/// The actual pool class. One ObjectPool manages the spawning and despawning of a single type of GameObject.
/// You will usually never have to use this class in a script outside of the ObjectPooler class at all
/// </summary>
public class ObjectPool
{
    private List<GameObject> objectList;
    private GameObject pooledObject;
    private GameObject parentObject;

    /// <summary>
    /// Creates the object pool by instiating the amount needed, sticking them under an empty parent (for oranization),
    /// and making a list to keep track of 'em all.
    /// </summary>
    public ObjectPool(GameObject objectToPool, int initialAmount)
    {
        pooledObject = objectToPool;
        objectList = new List<GameObject>(initialAmount);
        parentObject = new GameObject("-" + objectToPool.name + " Pool-");
        AddToPool(initialAmount);
    }

    /// <summary>
    /// Instantiates and adds certain amount of objects to the pool
    /// </summary>
    public void AddToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            AddToPool();
        }
    }

    public GameObject AddToPool()
    {
        GameObject newObj = GameObject.Instantiate(pooledObject, parentObject.transform);
        newObj.SetActive(false);
        objectList.Add(newObj);

        return newObj;
    }

    public GameObject Get()
    {
        foreach (GameObject obj in objectList)
        {
            if (!obj.activeSelf)
                return obj;
        }

        return AddToPool();
    }
}
