using System;
using UnityEngine;

namespace ArtFactory._Scripts.Managers
{
    public abstract class PoolableObjects : MonoBehaviour
    {
        public ObjectPool Parent;

        public virtual void OnDisable()
        {
            Parent.ReturnObjectToPool(this);
        }
    }
}
