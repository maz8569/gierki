using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CourierMotor : MonoBehaviour
{
    NavMeshAgent agent;

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
}
