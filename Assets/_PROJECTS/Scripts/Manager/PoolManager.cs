using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PoolManager
{
    //storage of all item
    public static Dictionary<string,List<object>> poolM = new Dictionary<string, List<object>>();
    //storage of all item current id
    public static Dictionary<string, int> poolIDM = new Dictionary<string, int>(); 

    /// <summary>
    /// function to setup
    /// </summary>
    /// <typeparam name="T"> name of prefab </typeparam>
    /// <param name="_prefab"> prefab to instantiate </param>
    /// <param name="poolSize"> size of the pool </param>
    public static void Setup<T>(T _prefab, int poolSize) where T : Component
    {
        //if pool already contain that prefab
        if (poolM.ContainsKey(_prefab.name))
        {
            //if that pool already reach maximum amount
            if (poolM[_prefab.name].Count >= poolSize)
            {
                //stop execute
                return;
            }
        }
        else //if not reached to maximum yet
        {
            //add create new qeue
            poolM.Add(_prefab.name, new List<object>());
            //add new pool managentment id
            poolIDM.Add(_prefab.name, 0);
        }

        //loop with following pool size
        for(int i = 0; i < poolSize; i++) 
        {
            //spawn object to scene
            T _poolInstance = Object.Instantiate(_prefab);
            //deactivate object in pool
            _poolInstance.gameObject.SetActive(false);
            //add that object into visa
            poolM[_prefab.name].Add(_poolInstance);
        }
        
    }

    /// <summary>
    /// function to get item
    /// </summary>
    /// <param name="_itemName"> item name </param>
    /// <returns></returns>
    public static T GetItem<T>(string _itemName) where T : Component
    {
        //increase item id
        poolIDM[_itemName]++;
        //return the item that found
        return poolM[_itemName][poolIDM[_itemName]] as T;
    }
}
