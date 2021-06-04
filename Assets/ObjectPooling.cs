using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{
    [SerializeField] GameObject platformPrefabs;
    [SerializeField] List<GameObject> platforms = new List<GameObject>();

    public GameObject GetPlattformPrefabs()
    {
        foreach (GameObject b in platforms ){
            if (b.activeSelf)
            {
                continue;
            }
            return b;  
        }
        GameObject b2 = Instantiate(platformPrefabs, this.transform.position, Quaternion.identity);
        return b2;
        
        
    }
}
