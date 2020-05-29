using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(AI))]
public class RandomMotion : MonoBehaviour
{
    public int range;

    AI MyAI;
    Vector3 point;
    [HideInInspector]
    public bool mover;
    [HideInInspector]
    public bool reached;

    Renderer cubeRenderer;
    Color First;
    Color Second;

    NavMeshAgent MyAgent;

    bool rotatenow;
    bool once;
    [HideInInspector]
    public bool notwork;
    // Start is called before the first frame update
    void Start()
    {
        MyAI = GetComponent<AI>();
        MyAgent = GetComponent<NavMeshAgent>();
        cubeRenderer = transform.Find("V-Light 0").GetChild(0).GetComponent<Renderer>();
        First = cubeRenderer.material.color;
        Second = Color.red;
        Second.a = First.a;
        Invoke("NextRandomPoint", 1);
        if (PlayerPrefs.GetInt("SpotedOn") == 1)
            PlayerPrefs.SetInt("Spoted", 0);
        InvokeRepeating("Check", 3, 2);
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                if (Vector3.Distance(transform.position, hit.position) > 10)
                {
                    result = hit.position;
                return true;
                }
            }
        }
        result = Vector3.zero;
        return false;
    }


    void Update()
    {
        //MyAI.GoToPosition(this.transform.localPosition + new Vector3(0, 0, 5), false);
        //if (MyAI.targetVisible == false && !notwork)
        //    RayCastHandle();
        if (rotatenow)
        {
            if (MyAI.targetVisible == false)
                transform.localEulerAngles += new Vector3(0, 0.5f, 0);
        }
        if (reached)
        {
            reached = false;
            notwork = false;
            MovetoPosition();
        }
        if (MyAI.targetVisible == true)
        {
            GetComponent<Detector>().ExclimatryMark.SetActive(true);
            cubeRenderer.material.SetColor("_Color", Second);
            if (PlayerPrefs.GetInt("SpotedOn") == 1)
                PlayerPrefs.SetInt("Spoted", 1);
            reached = true;
            mover = false;
        }
        if (MyAI.targetVisible == false)
        {
            cubeRenderer.material.SetColor("_Color", First);
        }
    }

    public void MovetoPosition()
    {
        if (!once)
        {
            if (Random.Range(0, 10) <= 9)
            {
                once = false;
                NextRandomPoint();
            }
            else
            {
                once = true;
                ActivateRotation();
            }
        }
    }

    void NextRandomPoint()
    {
        mover = true;
        if (RandomPoint(transform.position, range, out point))
        {
            mover = true;
            MyAI.GoToPosition(point, false);
        }
    }

    void ActivateRotation()
    {
        rotatenow = true;
        Invoke("wait", 2);
    }

    void wait()
    {
        rotatenow = false;
        once = false;
    }

    void Check()
    {
        if (MyAgent.isStopped == true || MyAgent.speed == 0)
        {
            MovetoPosition();
        }
    }
}
