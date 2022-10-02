using System;
using UnityEngine;
using UnityEngine.AI;

namespace ArtFactory._Scripts.Units
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class CharacterAI : MonoBehaviour
    {
        private NavMeshAgent Agent;
       [SerializeField] private Transform target;
       private CharacterState _characterState;

       private void Awake()
       {
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
                    Agent.SetDestination(target.position);
                   
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
