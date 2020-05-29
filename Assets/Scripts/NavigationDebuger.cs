using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using PathCreation;

[RequireComponent(typeof(LineRenderer))]
public class NavigationDebuger : MonoBehaviour
{
    [SerializeField]
    public NavMeshAgent agentToDebug;
    private LineRenderer linerenderer;
    public Transform Arrow;
    public PathCreator pathCreator;

    BezierPath bezierPath;
    // Start is called before the first frame update
    void Start()
    {
        linerenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agentToDebug.hasPath)
        {
            if (agentToDebug.path.corners.Length > 2)
            {
                bezierPath = new BezierPath(agentToDebug.path.corners, false, PathSpace.xyz);
                pathCreator.bezierPath = bezierPath;
                linerenderer.positionCount = pathCreator.path.localPoints.Length;
                linerenderer.SetPositions(pathCreator.path.localPoints);
                linerenderer.enabled = true;
            }
            else
            {
                linerenderer.positionCount = agentToDebug.path.corners.Length;
                linerenderer.SetPositions(agentToDebug.path.corners);
                linerenderer.enabled = true;
            }
            Arrow.position = agentToDebug.path.corners[agentToDebug.path.corners.Length - 1];
            Arrow.position += new Vector3(0, 1, 0);
            Arrow.rotation = Quaternion.LookRotation(agentToDebug.path.corners[agentToDebug.path.corners.Length - 1] - agentToDebug.path.corners[agentToDebug.path.corners.Length - 2]);
        }
        else
        {
            linerenderer.enabled = false;
        }
    }
}
