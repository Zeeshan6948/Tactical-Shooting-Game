using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    Animator mAnimator;
    NavMeshAgent mNavMeshAgent;
    bool mRunning;
    public Camera My;
    public LayerMask MyMask;
    public Transform Target;
    public ParticleSystem Locked;
    public float raycastdis;
    public Transform RayCastTrans;


    Vector3 heading, Direction;
    float Distance;
    RaycastHit hit;
    Ray ray;
    float TimetoNext;
    // Start is called before the first frame update
    void Start()
    {
        TimetoNext = Time.time;
        mAnimator = GetComponent<Animator>();
        mNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        ray = My.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            if (TimetoNext + 1f > Time.time)
                return;
            Target = null;
            if (Physics.Raycast(ray,out hit, 100, MyMask))
            {
                if(hit.collider.gameObject.layer == 13)
                {
                    Target = hit.collider.gameObject.transform;
                    GameObject ob = Instantiate(Locked.gameObject);
                    ob.transform.parent = Target;
                    ob.transform.localPosition = Vector3.zero;
                    ob.GetComponent<ParticleSystem>().Play();
                }
                else
                {
                    mNavMeshAgent.destination = hit.collider.transform.position;
                }
            }
        }
        if (Target != null)
        {
            heading = Target.position - transform.position;
            Distance = heading.magnitude;
            Direction = heading / Distance;
            mNavMeshAgent.destination = Target.position - Direction;
        }
        if(mNavMeshAgent.remainingDistance <= mNavMeshAgent.stoppingDistance)
        {
            mRunning = false;
        }
        else
        {
            mRunning = true;
        }
        mAnimator.SetBool("running", mRunning);
        RayCastHandle();
    }
    int CoverdAngle=0;
    Quaternion targetRotation;
    void RayCastHandle()
    {
        CoverdAngle = 0;
        if (!mRunning)
        {
            if (Physics.Raycast(RayCastTrans.position, RayCastTrans.forward, out hit, raycastdis))
            {
                CoverdAngle++;
                //print("front");
                if (Physics.Raycast(RayCastTrans.position, RayCastTrans.right, out hit, raycastdis))
                {
                   // print("right");
                    Debug.DrawRay(RayCastTrans.position, RayCastTrans.right * raycastdis, Color.green);
                    CoverdAngle++;
                }
                else
                {
                    //print("yehcheez");
                    targetRotation = Quaternion.LookRotation(RayCastTrans.right);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2 * Time.deltaTime);
                }
                if (Physics.Raycast(RayCastTrans.position, -RayCastTrans.right, out hit, raycastdis))
                {
                    CoverdAngle++;
                }
                else
                {
                    targetRotation = Quaternion.LookRotation(-RayCastTrans.right);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2 * Time.deltaTime);
                }
                if (Physics.Raycast(RayCastTrans.position, -RayCastTrans.forward, out hit, raycastdis))
                {
                    CoverdAngle++;
                }
                else
                {
                    targetRotation = Quaternion.LookRotation(-RayCastTrans.forward);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2 * Time.deltaTime);
                }
                if(CoverdAngle == 4)
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2 * Time.deltaTime);
            }
        }
        //print(CoverdAngle);
    }
}
