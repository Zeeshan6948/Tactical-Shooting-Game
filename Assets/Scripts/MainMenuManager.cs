using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Firebase.Analytics;
//using Firebase.Messaging;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject[] m_Panels;
    public int m_CurrentPanel = 0;
    public List<int> PanelFlow;

    [Header("Music")]
    public Button SettingSoundBtn;
    public AudioSource SoundSource;
    public AudioSource BtnClickSound;
    

    public Text CurrentLevel;
    public GameObject Premium;
    void Start()
    {
        OpenSpecficPanel(0);
        if (PlayerPrefs.GetInt("First") == 0)
        {
            PlayerPrefs.SetInt("Player0", 1);
            PlayerPrefs.SetInt("First", 1);
        }
        //if (AdsManager.Instance)
        //{
        //    AdsManager.Instance.TapdaqBannerShow();
        //    FirebaseAnalytics.LogEvent("GameOpened");
        //    FirebaseMessaging.TokenReceived += OnTokenReceived;
        //    FirebaseMessaging.MessageReceived += OnMessageReceived;
        //}
    }

    //public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    //{
    //    UnityEngine.Debug.Log("Received Registration Token: " + token.Token);
    //}

    //public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    //{
    //    UnityEngine.Debug.Log("Received a new message from: " + e.Message.From);
    //}

    void Update()
    {
        CurrentLevel.text = "Level " + (PlayerPrefs.GetInt("LevelCompleted") + 1).ToString();
    }

    public void BackBtnFun()
    {
        int temp = PanelFlow.IndexOf(m_CurrentPanel);
        if (temp == 0)
        {
            m_Panels[8].SetActive(false);
            OpenSpecficPanel(0);
            PanelFlow.Clear();
            PanelFlow.Add(0);
            return;
        }
        OpenSpecficPanel(PanelFlow[temp - 1]);
        BtnClickSound.Play();
        if (m_CurrentPanel == 0)
        {
            PanelFlow.Clear();
            PanelFlow.Add(0);
        }
    }

    void OpenSpecficPanel(int number)
    {
        m_Panels[m_CurrentPanel].SetActive(false);
        m_CurrentPanel = number;
        m_Panels[m_CurrentPanel].SetActive(true);
        PanelFlow.Add(number);
        //if (AdsManager.Instance)
        //    AdsManager.Instance.MediationAd();
    }
    
    public void SettingVibration()
    {
        Handheld.Vibrate();
    }

    public void ShareBtnFun()
    {
        BtnClickSound.Play();
    }
    public void SettingSound()
    {
        if (SoundSource.volume == 0)
        {
            SettingSoundBtn.GetComponent<Image>().color = SettingSoundBtn.colors.normalColor;
            SoundSource.volume = 1;
            SoundSource.Play();
        }
        else
        {
            SettingSoundBtn.GetComponent<Image>().color = SettingSoundBtn.colors.disabledColor;
            SoundSource.volume = 0;
            SoundSource.Stop();
        }
        BtnClickSound.Play();
    }
    public void RestoreGame()
    {
        PlayerPrefs.DeleteAll();
        BtnClickSound.Play();
    }
    public void PrivacyPolicyBtn()
    {
        Application.OpenURL("https://taptoaction.wixsite.com/privacypolicy");
        BtnClickSound.Play();
    }
    public void TermConditionBtn()
    {
        Application.OpenURL("https://taptoaction.wixsite.com/privacypolicy");
        BtnClickSound.Play();
    }
    public void RateUSBtn()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
        BtnClickSound.Play();
    }

    public void ReviewbtnFun()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
        BtnClickSound.Play();
    }
    public void SettingsBtnFun()
    {
        OpenSpecficPanel(1);
        BtnClickSound.Play();
    }
    public void NoAdsBtnFun()
    {
        BtnClickSound.Play();
    }
    public void VIPBtnFun()
    {
        OpenSpecficPanel(3);
        BtnClickSound.Play();
    }
    public void UpgradeAssassinBtn()
    {
        OpenSpecficPanel(2);
        BtnClickSound.Play();
    }

    public void StartGameBtn()
    {
        PlayerPrefs.SetInt("LevelSelected", PlayerPrefs.GetInt("LevelCompleted")); 
        bl_SceneLoaderUtils.GetLoader.LoadLevel("Game");
        BtnClickSound.Play();
    }

    public void Ad500Reward()
    {
        BtnClickSound.Play();
        //if (AdsManager.Instance && AdsManager.Instance.RewardAdAvaible)
        //{
        //    AdsManager.Instance.functioncalling(gameObject, "After500reward");
        //    AdsManager.Instance.TappedReward();
        //}
    }

    public void After500reward()
    {
        PlayerPrefs.SetInt("TotalReward", PlayerPrefs.GetInt("TotalReward") + 500);
    }
    int Adcount;
    public void WatchAdWithCounter(int AdCount)
    {
        Adcount = AdCount;
        BtnClickSound.Play();
        if (AdCount == 2)
        {
            //if (AdsManager.Instance && AdsManager.Instance.RewardAdAvaible && PlayerPrefs.GetInt("AdCount2") < 2)
            //{
            //    AdsManager.Instance.functioncalling(gameObject, "AfterAdCounter");
            //    AdsManager.Instance.TappedReward();
            //}
        }
        if (AdCount == 3)
        {
            //if (AdsManager.Instance && AdsManager.Instance.RewardAdAvaible && PlayerPrefs.GetInt("AdCount3") < 3)
            //{
            //    AdsManager.Instance.functioncalling(gameObject, "AfterAdCounter");
            //    AdsManager.Instance.TappedReward();
            //}
        }
        if (AdCount == 4)
        {
            //if (AdsManager.Instance && AdsManager.Instance.RewardAdAvaible && PlayerPrefs.GetInt("AdCount4") < 4)
            //{
            //    AdsManager.Instance.functioncalling(gameObject, "AfterAdCounter");
            //    AdsManager.Instance.TappedReward();
            //}
        }
    }

    public void AfterAdCounter()
    {
        if (Adcount == 2)
        {
            if (PlayerPrefs.GetInt("AdCount2") < 1)
            {
                PlayerPrefs.SetInt("AdCount2", PlayerPrefs.GetInt("AdCount2") + 1);
            }
            else
            {
                Premium.transform.GetChild(1).GetChild(0).GetComponent<Button>().interactable = true;
            }
        }
        if (Adcount == 3)
        {
            if (PlayerPrefs.GetInt("AdCount3") < 2)
            {
                PlayerPrefs.SetInt("AdCount3", PlayerPrefs.GetInt("AdCount3") + 1);
            }
            else
            {
                Premium.transform.GetChild(2).GetChild(0).GetComponent<Button>().interactable = true;
            }
        }
        if (Adcount == 4)
        {
            if (PlayerPrefs.GetInt("AdCount4") < 3)
            {
                PlayerPrefs.SetInt("AdCount4", PlayerPrefs.GetInt("AdCount4") + 1);
            }
            else
            {
                Premium.transform.GetChild(3).GetChild(0).GetComponent<Button>().interactable = true;
            }
        }
    }

    public void SpecialCharacterUnlock(int number)
    {
        PlayerPrefs.SetInt("SelectedChar", number);
        BtnClickSound.Play();
    }

}
