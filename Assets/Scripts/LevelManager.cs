using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
//using Firebase.Analytics;

[System.Serializable]
public class Level
{
    public Transform PlayerPos = null;
    public int TargetsNo = 0;
    public GameObject LevelObject = null;
}





public class LevelManager : MonoBehaviour
{
    public static LevelManager m_instance;
    public Level[] m_level;
    public GameObject[] Players;
    public GameObject PlayerObject;
    public CanvasControl CanvasObject;
    public int Timer;
    public static int m_currentLevelNo;
    public int targeteliminated = 0;
    public NPCRegistry NPCReg;
    public GameObject jemsEmitter;
    int waitnumber;
    public AudioSource JemCollection;
    double levelrecord;
    public static Vector3 playercordinate;
    public IncrementalText PlayerScore;
    public GameObject DeathIndication;
    public GameObject StartIndication;
    public PlayerCameraControl CamScript;
    public NavigationDebuger Path;
    AI[] many;
    void Start()
    {
        m_instance = this;
        m_currentLevelNo = PlayerPrefs.GetInt("LevelSelected");
        targeteliminated = 0;
        ActivationofObjects();
        levelrecord = (float)m_currentLevelNo;
        //if (AdsManager.Instance)
        //{
        //    AdsManager.Instance.TapdaqBannerHide();
        //    FirebaseAnalytics.LogEvent("Level", "LevelOpened", levelrecord);
        //}
        //Invoke("After2sec", 1);
    }

    void Update()
    {
        playercordinate = PlayerObject.transform.position;
    }

    public void ActivationofObjects()
    {
        if (PlayerObject == null)
        {
            Players[PlayerPrefs.GetInt("SelectedChar")].SetActive(true);
            PlayerObject = Players[PlayerPrefs.GetInt("SelectedChar")];
            PlayerScore = PlayerObject.transform.GetChild(0).GetChild(1).GetComponent<IncrementalText>();
            CamScript.player = PlayerObject;
            Path.agentToDebug = PlayerObject.GetComponent<NavMeshAgent>();
            jemsEmitter.GetComponent<MoveToward>().pointB = PlayerObject.transform;
        }
        PlayerObject.transform.position = m_level[m_currentLevelNo].PlayerPos.position;
        PlayerObject.transform.rotation = m_level[m_currentLevelNo].PlayerPos.rotation;
        PlayerObject.SetActive(true);
        GameObject ob = Instantiate(StartIndication);
        ob.transform.position = PlayerObject.transform.position + new Vector3(0, 5, 0);
        ob.GetComponent<ParticleSystem>().Play();
        m_level[m_currentLevelNo].LevelObject.SetActive(true);
        CanvasObject.Targettext.text = (targeteliminated + "/" + m_level[m_currentLevelNo].TargetsNo).ToString();
        Destroy(ob, 3);
        many = m_level[m_currentLevelNo].LevelObject.GetComponentsInChildren<AI>();
        After2sec();
    }

    void After2sec()
    {
        print("chala");
        if (CamScript.m_Targets.Count > 1)
        {
            CamScript.m_Targets.Clear();
            CamScript.m_Targets.Add(PlayerObject.transform);
        }
        for (int i = 0; i < many.Length; i++)
        {
            CamScript.m_Targets.Add(many[i].transform);
        }
    }
    void TickTick()
    {
        Timer--;
        CanvasObject.TimerText.text = (Timer / 60).ToString() + ":" + (Timer % 60).ToString();
        if (Timer == 0)
        {
            LevelFailed();
            CancelInvoke("TickTick");
        }
    }

    public void LevelFailed()
    {
        for (int i = 0; i < NPCReg.Npcs.Count; i++)
        {
            NPCReg.Npcs[i].StopAllCoroutines();
            NPCReg.Npcs[i].AnimatorComponent.SetInteger("AnimState", 0);
        }
        PlayerObject.SetActive(false);
        GameObject ob;
        if (targeteliminated != 0)
        {
            for (int i = 0; i < targeteliminated * 15; i++)
            {
                ob = Instantiate(jemsEmitter);
                value = Random.Range(0, 2);
                ob.transform.position = PlayerObject.transform.position + new Vector3(value, 0.5f, value);
            }
        }
        ob = Instantiate(DeathIndication);
        ob.transform.position = PlayerObject.transform.position + new Vector3(0, 5, 0);
        ob.GetComponent<ParticleSystem>().Play();
        Destroy(ob, 3);
        Invoke("GeneralWait", 2);
        waitnumber = 2;
    }

    public void LevelCompleted()
    {
        if (PlayerPrefs.GetInt("LevelCompleted") <= PlayerPrefs.GetInt("LevelSelected"))
            PlayerPrefs.SetInt("LevelCompleted", PlayerPrefs.GetInt("LevelSelected") + 1);
        CanvasObject.CurrentLevelText.text = "Level " + (PlayerPrefs.GetInt("LevelSelected") + 1).ToString();
        PlayerPrefs.SetInt("SpecialLevel", PlayerPrefs.GetInt("SpecialLevel") + 1);
        CanvasObject.RewardText.text = (targeteliminated * 15).ToString();
        Invoke("GeneralWait", 3);
        waitnumber = 0;

    }

    public void GeneralWait()
    {
        switch (waitnumber)
        {
            case 0:
                //AudioListener.volume = 0;
                //bl_SceneLoaderUtils.GetLoader.LoadLevel("Main");
                //if (AdsManager.Instance && AdsManager.Instance.RewardAdAvaible)
                //{
                //    CanvasObject.DoubleRewardBtn.SetActive(true);
                //}
                CanvasObject.OpenSpecficPanel(2);
                //if (AdsManager.Instance)
                //{
                //    AdsManager.Instance.MediationAd();
                //    FirebaseAnalytics.LogEvent("Level", "LevelCompleted", levelrecord);
                //}
                break;
            case 1:
                break;
            case 2:
                //AudioListener.volume = 0;
                //if (AdsManager.Instance && AdsManager.Instance.RewardAdAvaible)
                //    CanvasObject.OpenSpecficPanel(3);
                //else if (AdsManager.Instance)
                //{
                //    AdsManager.Instance.MediationAd();
                //    FirebaseAnalytics.LogEvent("Level", "LevelFailed", levelrecord);
                //}
                bl_SceneLoaderUtils.GetLoader.LoadLevel("Main");
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }
    int value;
    public void KilledEnemy()
    {
        targeteliminated++;
        if (PlayerPrefs.GetInt("TargetOn") == 1)
            PlayerPrefs.SetInt("SpecialTarget", PlayerPrefs.GetInt("SpecialTarget") + 1);
        CanvasObject.Targettext.text = (targeteliminated + "/" + m_level[m_currentLevelNo].TargetsNo).ToString();
        //GameObject ob = Instantiate(jemsEmitter);
        //ob.transform.position = PlayerObject.transform.position + new Vector3(0,2,0);
        Invoke("After05", 0.5f);
        for (int i = 0; i < 15; i++)
        {
            GameObject ob = Instantiate(jemsEmitter);
            value = Random.Range(0, 2);
            ob.transform.position = PlayerObject.transform.position + new Vector3(value, 0.5f, value);
            ob.GetComponent<MoveToward>().enabled = true;
        }
        //CamScript.m_Targets.Clear();
        //CamScript.m_Targets.Add(PlayerObject.transform);
        //Invoke("After2sec", 2);
        if (targeteliminated >= 2)
        {
            for (int i = 0; i < NPCReg.Npcs.Count; i++)
            {
                NPCReg.Npcs[i].walkSpeed += 1;
            }
        }
        if (targeteliminated == m_level[m_currentLevelNo].TargetsNo)
        {
            LevelCompleted();
        }
    }
    void After05()
    {
        PlayerScore.transform.parent.gameObject.SetActive(true);
        PlayerScore.score = targeteliminated * 15;
        PlayerScore.displayScore = (targeteliminated - 1) * 15;
        PlayerScore.Inceamental();
    }
    public void PlayerStatus()
    {
        if (PlayerPrefs.GetInt("Healthpack") == 1)
        {
            PlayerObject.GetComponent<FPSPlayer>().hitPoints = PlayerObject.GetComponent<FPSPlayer>().hitPoints + PlayerPrefs.GetInt("Healthpack");
        }
    }

    public void JemCollectionSound()
    {
        JemCollection.PlayOneShot(JemCollection.clip);
    }
}
