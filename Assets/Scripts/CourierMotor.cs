using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CourierMotor : MonoBehaviour
{
    public NavMeshAgent agent;
    public LayerMask bridges;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    public void MoveToPoint(Vector3 point, bool draw = false)
    {
        agent.SetDestination(point);
        //if (draw) DrawPath(point);
    }

    public void DrawPath(Vector3 point)
    {
        NavMeshPath path = new NavMeshPath();
        NavMesh.CalculatePath(agent.transform.position, point, agent.areaMask, path); //Saves the path in the path variable.
        Vector3[] corners = path.corners;
        LineRenderer lineRenderer = new LineRenderer();
        lineRenderer.SetPositions(corners);
    }

    public bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, agent.areaMask))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
