using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectTypes
{
    Enemy
}

public class Detector : MonoBehaviour
{
    public ObjectTypes ThisObjectIs;
    public GameObject SignalSender;
    bool disablework = false;
    public GameObject QuestionMark;
    public GameObject ExclimatryMark;
    GameObject temp;
    // Start is called before the first frame update
    void Start()
    {
        temp = transform.Find("MarkCanvas").gameObject;
        ExclimatryMark = temp.transform.GetChild(0).gameObject;
        QuestionMark = temp.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.transform.tag == "PlayerCar" && ThisObjectIs == ObjectTypes.Final && !disablework)
        //{
        //    LevelManager.m_instance.CanvasObject.MessngerText.text = LevelManager.m_instance.m_level[0].LevelObject.GetComponent<Level1>().Statements[1];
        //    LevelManager.m_instance.Messenger.SetActive(true);
        //    LevelManager.m_instance.m_level[0].LevelObject.GetComponent<Level1>().GetOutOfCar();
        //    ThisObjectIs = ObjectTypes.Reached;
        //    disablework = true;
        //    Invoke("wait", 1);
        //}
        if (other.transform.tag == "Player" && ThisObjectIs == ObjectTypes.Enemy && !disablework)
        {
            LevelManager.m_instance.KilledEnemy();
            this.GetComponent<CharacterDamage>().ApplyDamage(3000,transform.position, transform.position, transform,true,false);
            this.enabled = false;
            Instantiate(SignalSender, this.transform.position, Quaternion.identity);
        }
        if (other.transform.tag == "Signal" && ThisObjectIs == ObjectTypes.Enemy && !disablework)
        {
            this.GetComponent<RandomMotion>().notwork = true;
            this.GetComponent<AI>().GoToPosition(other.transform.position,true);
            Destroy(other.transform.gameObject);
            QuestionMark.SetActive(true);
        }
        //if (other.transform.tag == "Player" && ThisObjectIs == ObjectTypes.CheckPost && !disablework)
        //{
        //    //LevelManager.m_instance.CanvasObject.MessengerWork("Wait for Enemies to Come to You");
        //    //ActivateObject.SetActive(true);
        //    //Destroy(this.gameObject);
        //}
    }
    private void OnTriggerExit(Collider other)
    {
    }
    private void OnTriggerStay(Collider other)
    {
        
    }
    void wait()
    {
        disablework = false;
    }
}
