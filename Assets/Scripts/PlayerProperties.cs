using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PlayerProperties : MonoBehaviour
{
    public bool Locked;
    public int Index;
    public float Speed;
    public float Health;

    Button MyButton;
    NavMeshAgent MyAgent;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Button>() != null)
            MyButton = GetComponent<Button>();

        if (GetComponent<NavMeshAgent>() != null)
            MyAgent = GetComponent<NavMeshAgent>();

        if (PlayerPrefs.GetInt("Player"+Index)==0)
        {
            Locked = true;
            if(MyButton)
                Onlocked();
        }
        else
        {
            Locked = false;
            if (MyButton)
                NotLocked();
        }

        if (MyAgent)
        {
            ApplyProperties();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ApplyProperties()
    {
        MyAgent.speed = Speed;
        GetComponent<AIDamage>().hitPoints = Health;
        MyAgent.acceleration += 10;
    }

    void Onlocked()
    {
        MyButton.interactable = false;
    }

    void NotLocked()
    {
        MyButton.interactable = true;
        MyButton.onClick.AddListener(PlayerSelectionBtn);
        if(PlayerPrefs.GetInt("SelectedChar") == Index)
        {
            MyButton.Select();
        }
    }

    public void Unlocked()
    {
        PlayerPrefs.SetInt("Player" + Index,1);
        Locked = false;
        NotLocked();
    }

    public void PlayerSelectionBtn()
    {
        PlayerPrefs.SetInt("SelectedChar", Index);
        GameObject.FindObjectOfType<MainMenuManager>().BtnClickSound.Play();
        //GameObject.FindObjectOfType<MainMenuManager>().BackBtnFun();
    }
}
