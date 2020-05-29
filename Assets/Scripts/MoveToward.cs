using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToward : MonoBehaviour
{
    public Transform pointB;
    public float speed = 1.0f;
    public float After;
    Vector3 heading, Direction;
    float Distance, delta;
    private Transform myTransform;
    bool Now;
    void Start()
    {
        myTransform = transform;
        Invoke("afterTime", After);
        Invoke("DestoryIt", After+0.5f);
    }

    private void Update()
    {
        if (Now)
        {
            heading = pointB.position - myTransform.position;
            Distance = heading.magnitude;
            Direction = heading / Distance;
            delta = Time.deltaTime * 60;
            myTransform.Translate(Direction * speed * delta);
            myTransform.localScale = myTransform.localScale * (speed);
        }
    }
    void afterTime()
    {
        Now = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 11 && Now)
        {
            Destroy(gameObject);
        }
    }

    void DestoryIt()
    {
        Destroy(gameObject);
    }
}