using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerSmoothBoundary : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    float timeOffset;

    [SerializeField]
    float LeftLimit;
    [SerializeField]
    float RightLimit;
    [SerializeField]
    float bottomLimit;
    [SerializeField]
    float topLimit;

    [SerializeField]
    Vector3 posOffset;

    private Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 startPos = transform.position;

        Vector3 endPos = player.transform.position;

        endPos.x += posOffset.x;
        endPos.y += posOffset.y;
        endPos.z += posOffset.z;

        transform.position = Vector3.Lerp(startPos, endPos, timeOffset + Time.deltaTime);

        transform.position = new Vector3
            (
            Mathf.Clamp(transform.position.x, LeftLimit,RightLimit),
            transform.position.y,
            Mathf.Clamp(transform.position.z, bottomLimit, topLimit)
            );
    }
}
