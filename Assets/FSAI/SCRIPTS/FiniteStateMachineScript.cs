using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

namespace FSAIScripts
{
    public class FiniteStateMachineScript : MonoBehaviour
    {
        public CollisionDetector eyesDetector;

        public SoundDetector earDetector;
        
        public State currentState = State.PickDestination;
        public int reachDistance = 2;
        private GameObject[] _destinations;
        private GameObject _currentDestination;
        private GameObject _currentTarget;
        
        public int rangedAttackDistance = 5;
        public bool canRangedAttack;
        public float cooldown;

        private NavMeshAgent _navMeshAgent;

        public ParticleSystem distanceAttack;

        private void Start()
        {
            _destinations = GameObject.FindGameObjectsWithTag("Destination");
        }

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            canRangedAttack = true;
        }

        private void Update()
        {
           UpdateState();
        }

        private void UpdateState()
        {
            switch (currentState)
            {
                case State.PickDestination:
                    PickDestination();
                    break;
                case State.MoveToDestination:
                    MoveToDestination();
                    break;
                case State.Chase:
                    Chase();
                    break;
                case State.Attack:
                    Attack();
                    break;
                case State.DistanceAttack:
                    DistanceAttack();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            ShootCoolDown();
        }
        
        public enum State
        {
            PickDestination,
            MoveToDestination,
            Chase,
            Attack,
            DistanceAttack
        }

        void PickDestination()
        {
            int rndIndex = Random.Range(0, _destinations.Length);
            _currentDestination = _destinations[rndIndex];
            if (_currentDestination != null) currentState = State.MoveToDestination;
        }

        private void MoveToDestination()
        {
            _navMeshAgent.SetDestination(_currentDestination.transform.position);
            if (Vector3.Distance(transform.position, _currentDestination.transform.position) <= reachDistance)
            {
                currentState = State.PickDestination;
            }

            GameObject firstWithTag = eyesDetector.firstWithTag("Player");
            if (firstWithTag != null)
            {
                _currentTarget = firstWithTag;
                currentState = State.Chase;
            }

            GameObject soundWithTag = earDetector.SoundWithTag("PlayerSound");
            if (soundWithTag != null)
            {
                Debug.Log("J'ai entendu quelque chose");
                _currentTarget = soundWithTag;
            }
        }

        void Chase()
        {
            _navMeshAgent.SetDestination(_currentTarget.transform.position);
            if (_currentTarget = null)
                PickDestination();

            if (Vector3.Distance(transform.position, _currentTarget.transform.position) <= rangedAttackDistance && canRangedAttack)
                currentState = State.DistanceAttack;
            
            if (Vector3.Distance(transform.position, _currentTarget.transform.position) <= reachDistance)
                currentState = State.Attack;

            if (eyesDetector.firstWithTag("Player") != _currentTarget)
            {
                _currentTarget = null;
                currentState = State.PickDestination;
            }
        }

        void Attack()
        {
            Debug.Log("Attack!!!");
            if (Vector3.Distance(transform.position, _currentDestination.transform.position) <= reachDistance)
                _currentTarget = null;
                currentState = State.Chase;
        }

        void DistanceAttack()
        {
            distanceAttack.Play();
            canRangedAttack = false;
            
            currentState = State.Chase;
        }

        void ShootCoolDown()
        {
            if (!canRangedAttack)
            {
                cooldown -= Time.deltaTime;
                if (cooldown <= 0)
                {
                    canRangedAttack = true;
                    cooldown = 3;
                }
            }
        }
    }
}
