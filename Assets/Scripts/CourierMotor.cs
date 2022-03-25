using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CourierMotor : MonoBehaviour
{
    public NavMeshAgent agent;
    public LayerMask bridges;
    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public int lengthOfLineRenderer = 20;
    public Vector3 point;
    public bool draw = false;
    LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.2f;
        lineRenderer.positionCount = lengthOfLineRenderer;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;
        agent = GetComponent<NavMeshAgent>();


    }

    private void Update()
    {
        if (draw && point != new Vector3(0, 0, 0)) DrawPath(point);
    }

    // Update is called once per frame
    public void MoveToPoint(Vector3 poi)
    {
        agent.SetDestination(poi);
        point = poi;
    }

    public void DrawPath(Vector3 point)
    {
        lineRenderer.positionCount = 0;
        NavMeshPath path = new NavMeshPath();
        NavMesh.CalculatePath(agent.transform.position, point, agent.areaMask, path); //Saves the path in the path variable.
        Vector3[] corners = path.corners;
        lineRenderer.positionCount = corners.Length;
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
