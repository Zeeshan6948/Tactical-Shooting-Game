using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Firebase.Analytics;

public class CanvasControl : MonoBehaviour
{
    public GameObject[] m_Panels;
    public int m_CurrentPanel;
    public Image TimerStatus;
    public Text TimerText;
    public Text JemsText;
    public Text TaskText;
    public Text Targettext;
    public GameObject Messenger;
    public Text CurrentLevelText;
    public Text RewardText;
    public GameObject DoubleRewardBtn;



    int i = 0;
    bool Paused;
    double levelrecord;
    int waitnumber;
    void Start()
    {
    }
    

    void Update()
    {
        //JemsText.text = PlayerPrefs.GetInt("TotalReward").ToString();
    }

    public void OpenSpecficPanel(int number)
    {
        m_Panels[m_CurrentPanel].SetActive(false);
        m_CurrentPanel = number;
        m_Panels[m_CurrentPanel].SetActive(true);
    }

    public void MessengerWork(string Task)
    {
        Messenger.SetActive(true);
        TaskText.GetComponent<Typer>().ValueSeter(Task);
    }

    public void PausedBtnFun()
    {
        Paused = !Paused;
        if (Paused)
        {
            OpenSpecficPanel(1);
            levelrecord = (float)LevelManager.m_currentLevelNo;
            Time.timeScale = 0;
            AudioListener.volume = 0;
            //PlayerPosition();
            //if (AdsManager.Instance)
            //{
            //    FirebaseAnalytics.LogEvent("Level", "LevelPaused", levelrecord);
            //}
        }
        else
        {
            OpenSpecficPanel(0);
            Time.timeScale = 1;
            AudioListener.volume = 1;
        }
    }

    public void ReloadBtnFun()
    {
        bl_SceneLoaderUtils.GetLoader.LoadLevel("Game");
        Time.timeScale = 1;
        AudioListener.volume = 1;
    }

    public void NextBtnFun()
    {
        PlayerPrefs.SetInt("LevelSelected", PlayerPrefs.GetInt("LevelSelected") + 1);
        if (PlayerPrefs.GetInt("LevelSelected") == LevelManager.m_instance.m_level.Length)
        {
            AudioListener.volume = 1;
            bl_SceneLoaderUtils.GetLoader.LoadLevel("Main");
            Time.timeScale = 1;
        }
        else
        {
            AudioListener.volume = 1;
            bl_SceneLoaderUtils.GetLoader.LoadLevel("Game");
            Time.timeScale = 1;
        }
    }

    public void RewardFun()
    {
        AudioListener.volume = 1;
        Time.timeScale = 1;
        Invoke("GeneralWait", 1);
        waitnumber = 0;
    }

    public void Reward2Ad()
    {
        //if (AdsManager.Instance)
        //{
        //    AdsManager.Instance.functioncalling(gameObject, "RewardTextIncremental");
        //    RewardText.text = (LevelManager.m_instance.m_level[LevelManager.m_currentLevelNo].TargetsNo * 15 * 2).ToString();
        //    AdsManager.Instance.TappedReward();
        //}
    }

    public void RewardTextIncremental()
    {
        RewardText.GetComponent<IncrementalText>().Calling = gameObject;
        RewardText.GetComponent<IncrementalText>().FunctionName = "AfterDoubleRewarded";
        RewardText.GetComponent<IncrementalText>().Inceamental();
    }


    public void AfterDoubleRewarded()
    {
        AudioListener.volume = 1;
        Invoke("GeneralWait", 1);
        waitnumber = 1;
        Time.timeScale = 1;
    }

    public void GeneralWait()
    {
        switch (waitnumber)
        {
            case 0:
                PlayerPrefs.SetInt("TotalReward", PlayerPrefs.GetInt("TotalReward") + LevelManager.m_instance.m_level[LevelManager.m_currentLevelNo].TargetsNo * 15);
                if (PlayerPrefs.GetInt("JemsOn") == 1)
                    PlayerPrefs.SetInt("SpecialJems", PlayerPrefs.GetInt("SpecialJems") + LevelManager.m_instance.m_level[LevelManager.m_currentLevelNo].TargetsNo * 15);
                bl_SceneLoaderUtils.GetLoader.LoadLevel("Main");
                break;
            case 1:
                PlayerPrefs.SetInt("TotalReward", PlayerPrefs.GetInt("TotalReward") + LevelManager.m_instance.m_level[LevelManager.m_currentLevelNo].TargetsNo * 15 * 2);
                if (PlayerPrefs.GetInt("JemsOn") == 1)
                    PlayerPrefs.SetInt("SpecialJems", PlayerPrefs.GetInt("SpecialJems") + LevelManager.m_instance.m_level[LevelManager.m_currentLevelNo].TargetsNo * 15);
                bl_SceneLoaderUtils.GetLoader.LoadLevel("Main");
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }

    public void ContinueBtnFun()
    {
        //if (AdsManager.Instance)
        //{
        //    AdsManager.Instance.functioncalling(gameObject, "afterContinue");
        //    AdsManager.Instance.TappedReward();
        //}
    }

    public void afterContinue()
    {
        Time.timeScale = 1;
        LevelManager.m_instance.PlayerObject.GetComponent<AIDamage>().hitPoints = 100;
        LevelManager.m_instance.ActivationofObjects();
        OpenSpecficPanel(0);
    }
    //public void PlayerPosition()
    //{
    //    Transform Record = LevelManager.m_instance.GetComponent<ControlConverter>().PlayerObjects[0].transform;
    //    PlayerPrefs.SetInt("PositonSaved", 1);
    //    PlayerPrefs.SetFloat("PositonSavedx", Record.position.x);
    //    PlayerPrefs.SetFloat("PositonSavedy", Record.position.y);
    //    PlayerPrefs.SetFloat("PositonSavedz", Record.position.z);
    //}
}
