using UnityEngine.AI;

namespace ArtFactory._Scripts.Managers
{
    public class Customer : PoolableObjects
    {
        public NavMeshAgent Agent;

        public override void OnDisable()
        {
            base.OnDisable();
            Agent.enabled = false;
        }
    }
}
