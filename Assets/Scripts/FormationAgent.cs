using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FormationAgent : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;

    public Transform toFollow;

    private void Update()
    {
        if(animator != null)
        animator.SetFloat("Move", agent.velocity.magnitude);

        if (toFollow != null)
        agent.SetDestination(toFollow.position);
    }
}
