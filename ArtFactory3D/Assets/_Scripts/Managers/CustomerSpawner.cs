using System;
using System.Collections;
using System.Collections.Generic;
using ArtFactory._Scripts.Units;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace ArtFactory._Scripts.Managers
{
    public class CustomerSpawner : MonoBehaviour
    {
        public int NumberofCustomerToSpawn = 5;
        public float spawnDelay = 1f;
        public List<Customer> customerPrefabs = new List<Customer>();
        
        private Dictionary<int, ObjectPool> CustomerObjectPools = new Dictionary<int, ObjectPool>();
        private NavMeshTriangulation Triangulation;
        private void Awake()
        {
            for (int i = 0; i < customerPrefabs.Count; i++)
            {
                CustomerObjectPools.Add(i,ObjectPool.CreateInstance(customerPrefabs[i], NumberofCustomerToSpawn));
            }
        }

        private void Start()
        {
            Triangulation = NavMesh.CalculateTriangulation();
            StartCoroutine(SpawnCustormer());
        }

        private void DoSpawnCustomer(int SpawnIndex)
        {
            PoolableObjects poolableObjects = CustomerObjectPools[SpawnIndex].GetObjects();

            if (poolableObjects != null)
            {
                Customer customer = poolableObjects.GetComponent<Customer>();
                int VertexIndex = Random.Range(0, Triangulation.vertices.Length);
                
                NavMeshHit Hit;
                if (NavMesh.SamplePosition(Triangulation.vertices[VertexIndex], out Hit, 2f, -1))
                {
                    customer.Agent.Warp(Hit.position);
                    customer.Agent.enabled = true;
                }
                else
                {
                    Debug.LogError($"Unable to place NavMeshAgent on NavMesh. Tried to use {Triangulation.vertices[VertexIndex]}");
                }

                
            }
            else
            {
                Debug.LogError("dospawncustomer error");
            }
        }

        private IEnumerator SpawnCustormer()
        {
            WaitForSeconds Wait = new WaitForSeconds(spawnDelay);
            int SpawnedCustomer = 0;

            while (SpawnedCustomer < NumberofCustomerToSpawn)
            {
                DoSpawnCustomer(0);
                SpawnedCustomer++;
                yield return Wait;
            }
        }
    }
}
