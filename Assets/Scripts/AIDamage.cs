using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIDamage : MonoBehaviour
{
    public float hitPoints;
    //public GameObject Fire;
    public Renderer cubeRenderer;
    public AudioSource DamageSound;
    Color abc;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("DamageOn") == 1)
            PlayerPrefs.SetInt("Damage", 0);
        abc = cubeRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
    }
    public void ApplyDamage()
    {
        Color abc = cubeRenderer.material.GetColor("_Color");
        if (hitPoints <= 0.0f)
        {
            return;
        }
        hitPoints -= 25;
        abc.g = Mathf.Abs(((100 - hitPoints) / 100)-1);
        abc.b = abc.g;
        cubeRenderer.material.SetColor("_Color", abc);
        DamageSound.Play();
        if (PlayerPrefs.GetInt("DamageOn") == 1)
            PlayerPrefs.SetInt("Damage", 1);
        if (hitPoints <= 5)
        {
            //Fire.SetActive(true);
            cubeRenderer.material.SetColor("_Color", abc);
            //Invoke("InvokeAfter", 10);
            GameObject.FindObjectOfType<NavigationDebuger>().gameObject.SetActive(false);
            LevelManager.m_instance.LevelFailed();
        }
    }
    public void ApplyDamage(float Value)
    {
        if (hitPoints <= 0.0f)
        {
            return;
        }
        hitPoints -= Value;
        DamageSound.Play();
        if (hitPoints <= 25)
        {
            //Fire.SetActive(true);
            cubeRenderer.material.SetColor("_Color", Color.black);
            Invoke("InvokeAfter", 2);
        }
    }
    private void OnDisable()
    {
        //Fire.SetActive(false);
        hitPoints = 100;
    }

    void InvokeAfter()
    {
        CancelInvoke("InvokeAfter");
        cubeRenderer.material.SetColor("_Color", abc);
    }
}
