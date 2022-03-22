using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMotor : MonoBehaviour
{
        private NavMeshAgent agent;
        private Transform startPosition;

        private void Start()
        {
                agent = this.gameObject.GetComponent<NavMeshAgent>();
                startPosition = this.gameObject.transform;
        }

        public void moveToDropHouse(Transform enemyHouse)
        {
                agent.SetDestination(enemyHouse.position);
        }

        public void moveToStartPosition()
        {
                agent.SetDestination(startPosition.position);
        }
}
