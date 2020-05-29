using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDisable : MonoBehaviour
{
    [Range(0.1f,2.0f)]
    public float Timerequied;
    private void OnEnable()
    {
        Invoke("DisableObject", Timerequied);
    }

    void DisableObject()
    {
        this.gameObject.SetActive(false);
    }
}
