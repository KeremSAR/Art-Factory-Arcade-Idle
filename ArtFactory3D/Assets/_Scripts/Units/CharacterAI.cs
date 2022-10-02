using System;
using UnityEngine;
using UnityEngine.AI;

namespace ArtFactory._Scripts.Units
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class CharacterAI : MonoBehaviour
    { 
        private NavMeshAgent Agent;
        private Vector3 target;
       private CharacterState _characterState;

       private void Awake()
       {
           target = new Vector3(12.61f, 1.22f, -13f);
           Agent = GetComponent<NavMeshAgent>();
       }

       void Start()
       {
           
           _characterState = CharacterState.movement;
       }

        private void DesicionAI()
        {
            switch (_characterState)
            {
                case CharacterState.idle:
                    Debug.Log("do nothing");
                    break;
                case CharacterState.movement: 
                    Agent.SetDestination(target);
                    
                    if (Agent.remainingDistance-Agent.stoppingDistance < 1f && Agent.remainingDistance != 0f )
                    {
                        Agent.isStopped = true;
                        this.gameObject.SetActive(false);
                        this.transform.position = new Vector3(0f, 0f, 0f);
                    }
                    break;
            }
        }
        
        void Update()
        {
            DesicionAI();
        }
        
        
        private enum CharacterState
        {
            idle,
            movement,
            Oncomlete,
            finish
        }
    }
}
