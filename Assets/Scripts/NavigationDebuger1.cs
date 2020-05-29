using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer))]
public class NavigationDebuger1 : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agentToDebug;
    private LineRenderer linerenderer;
    public Transform Arrow;
    public int vertexCount = 12;
    List<Vector3> point;
    // Start is called before the first frame update
    void Start()
    {
        linerenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        point = new List<Vector3>();
        if (agentToDebug.hasPath)
        {
            for (int i = 0; i < agentToDebug.path.corners.Length - 2; i += 2)
            {
                for (float ratio = 0; ratio <= 1; ratio += 1.0f / vertexCount)
                {
                    var TangentlineVertex1 = Vector3.Lerp(agentToDebug.path.corners[i], agentToDebug.path.corners[i + 1], ratio);
                    var TangentlineVertex2 = Vector3.Lerp(agentToDebug.path.corners[i + 1], agentToDebug.path.corners[i + 2], ratio);
                    var BezierPoint = Vector3.Lerp(TangentlineVertex1, TangentlineVertex2, ratio);
                    point.Add(BezierPoint);
                }
            }
            if (agentToDebug.path.corners.Length % 3 == 2)
            {
                point.Add(agentToDebug.path.corners[agentToDebug.path.corners.Length - 2]);
                point.Add(agentToDebug.path.corners[agentToDebug.path.corners.Length - 1]);
            }
            if (agentToDebug.path.corners.Length % 3 == 1)
            {
                point.Add(agentToDebug.path.corners[agentToDebug.path.corners.Length - 1]);
            }
            linerenderer.positionCount = point.Count;
            linerenderer.SetPositions(point.ToArray());
            //linerenderer.positionCount = agentToDebug.path.corners.Length;
            //linerenderer.SetPositions(agentToDebug.path.corners);
            linerenderer.enabled = true;
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
