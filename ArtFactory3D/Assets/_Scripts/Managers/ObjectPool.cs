using System.Collections.Generic;
using UnityEngine;

namespace ArtFactory._Scripts.Managers
{
    public class ObjectPool
    {
        private PoolableObjects Prefab;
        private int size;
        private List<PoolableObjects> AvailableObjectPool;

        private ObjectPool(PoolableObjects Prefab, int size)
        {
            this.Prefab = Prefab;
            this.size = size;
            AvailableObjectPool = new List<PoolableObjects>(size);
        }

        public static ObjectPool CreateInstance(PoolableObjects Prefab, int size)
        {
            ObjectPool pool = new ObjectPool(Prefab, size);

            GameObject poolGameobject = new GameObject("Object Pool");
            pool.CreateObject(poolGameobject);
            
            return pool;
        }

        private void CreateObject(GameObject parent)
        {
            for (int i = 0; i < size; i++)
            {
                PoolableObjects poolableObjects =
                    GameObject.Instantiate(Prefab, Vector3.zero, Quaternion.identity, parent.transform);
                poolableObjects.Parent = this;
                poolableObjects.gameObject.SetActive(false);
            }
        }

        public PoolableObjects GetObjects()
        {
            PoolableObjects instance = AvailableObjectPool[0];
            AvailableObjectPool.RemoveAt(0);
            instance.gameObject.SetActive(true);

            return instance;
        }

        public void ReturnObjectToPool(PoolableObjects objects)
        {
            AvailableObjectPool.Add(objects);
        }
        
        
    }
}
