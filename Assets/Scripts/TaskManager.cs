using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TaskType
{
    Target, Jems, Damage, Spoted, WatchAd, Level
}

[System.Serializable]
public struct Tasks
{
    public int Prize;
    public string TaskText;
    public int TaskTarget;
    public TaskType Type;
    public bool Ready;
}

public class TaskManager : MonoBehaviour
{
    public Tasks[] MyTask;
    int[] currentTasks = new int[3];
    public GameObject[] TaskButtons;


    //bool Added1, Added2, Added3, Added4, Added5;
    void Start()
    {
        if (PlayerPrefs.GetInt("FirstTime") == 0)
        {
            PlayerPrefs.SetInt("Damage", 1);
            PlayerPrefs.SetInt("Spoted", 1);
            PlayerPrefs.SetInt("FirstTime", 1);
        }
        RandomTaskAssigner();
        InvokeRepeating("TaskReady", 0.3f, 1);
    }

    void Update()
    {

    }

    public void TaskReady()
    {
        int temp;
        for (int i = 0; i < 3; i++)
        {
            temp = currentTasks[i];
            if (!MyTask[temp].Ready)
            {
                TaskButtons[i].transform.GetChild(0).GetComponent<Text>().text = MyTask[temp].TaskText;
                TaskButtons[i].transform.GetChild(2).GetChild(1).GetComponent<Text>().text = MyTask[temp].Prize.ToString();
                if (MyTask[temp].Type == TaskType.Target )//&& !Added1)
                {
                    TaskTarget(i);
                }
                if (MyTask[temp].Type == TaskType.Jems)
                {
                    TaskJems(i);
                }
                if (MyTask[temp].Type == TaskType.Damage)
                {
                    TaskDamage(i);
                }
                if (MyTask[temp].Type == TaskType.Spoted)
                {
                    TaskSpoted(i);
                }
                if (MyTask[temp].Type == TaskType.WatchAd)
                {
                    TaskWatchAd(i);
                }
                if (MyTask[temp].Type == TaskType.Level)
                {
                    TaskLevel(i);
                }
            }
        }
    }

    void TaskTarget(int Value)
    {
        int temp = currentTasks[Value];
        PlayerPrefs.SetInt("TargetOn", 1);
        if (PlayerPrefs.GetInt("SpecialTarget") >= MyTask[temp].TaskTarget )//&& !Added1)
        {
            TaskButtons[Value].transform.GetChild(1).GetComponent<Text>().text = MyTask[temp].TaskTarget + " / " + MyTask[temp].TaskTarget;
            TaskButtons[Value].transform.GetChild(2).GetComponent<Button>().interactable = true;
            TaskButtons[Value].transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate { GainReward(MyTask[temp].Prize, 1, Value); });
            TaskButtons[Value].transform.GetChild(2).GetComponent<GrowAndShrink>().enabled = true;
            MyTask[temp].Ready = true;
            //Added1 = true;
        }
        else
        {
            TaskButtons[Value].transform.GetChild(2).GetComponent<Button>().interactable = false;
            TaskButtons[Value].transform.GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetInt("SpecialTarget") + " / " + MyTask[temp].TaskTarget;
        }
    }
    void TaskJems(int Value)
    {
        int temp = currentTasks[Value];
        PlayerPrefs.SetInt("JemsOn", 1);
        if (PlayerPrefs.GetInt("SpecialJems") >= MyTask[temp].TaskTarget) //&& !Added2)
        {
            TaskButtons[Value].transform.GetChild(1).GetComponent<Text>().text = MyTask[temp].TaskTarget + " / " + MyTask[temp].TaskTarget;
            TaskButtons[Value].transform.GetChild(2).GetComponent<Button>().interactable = true;
            TaskButtons[Value].transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate { GainReward(MyTask[temp].Prize, 2, Value); });
            TaskButtons[Value].transform.GetChild(2).GetComponent<GrowAndShrink>().enabled = true;
            MyTask[temp].Ready = true;
            //Added2 = true;
        }
        else
        {
            TaskButtons[Value].transform.GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetInt("SpecialJems") + " / " + MyTask[temp].TaskTarget;
            TaskButtons[Value].transform.GetChild(2).GetComponent<Button>().interactable = false;
        }
    }
    void TaskDamage(int Value)
    {
        int temp = currentTasks[Value];
        PlayerPrefs.SetInt("DamageOn", 1);
        TaskButtons[Value].transform.GetChild(1).GetComponent<Text>().text = "";
        if (PlayerPrefs.GetInt("Damage") == 0 )//&& !Added3)
        {
            TaskButtons[Value].transform.GetChild(2).GetComponent<Button>().interactable = true;
            TaskButtons[Value].transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate { GainReward(MyTask[temp].Prize, 3, Value); });
            TaskButtons[Value].transform.GetChild(2).GetComponent<GrowAndShrink>().enabled = true;
            MyTask[temp].Ready = true;
            //Added3 = true;
        }
        else
        {
            TaskButtons[Value].transform.GetChild(2).GetComponent<Button>().interactable = false;
        }
    }
    void TaskSpoted(int Value)
    {
        int temp = currentTasks[Value];
        PlayerPrefs.SetInt("SpotedOn", 1);
        TaskButtons[Value].transform.GetChild(1).GetComponent<Text>().text = "";
        if (PlayerPrefs.GetInt("Spoted") == 0 )//&& !Added4)
        {
            TaskButtons[Value].transform.GetChild(2).GetComponent<Button>().interactable = true;
            TaskButtons[Value].transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate { GainReward(MyTask[temp].Prize, 4, Value); });
            TaskButtons[Value].transform.GetChild(2).GetComponent<GrowAndShrink>().enabled = true;
            MyTask[temp].Ready = true;
            //Added4 = true;
        }
        else
        {
            TaskButtons[Value].transform.GetChild(2).GetComponent<Button>().interactable = false;
        }
    }
    void TaskWatchAd(int Value)
    {
        int temp = currentTasks[Value];
        TaskButtons[Value].transform.GetChild(1).GetComponent<Text>().text = "";
        //if (!Added5)
        //{
            TaskButtons[Value].transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate { WatchAdbutton(MyTask[temp].Prize, Value); });
            TaskButtons[Value].transform.GetChild(2).GetComponent<GrowAndShrink>().enabled = true;
            MyTask[temp].Ready = true;
            //Added5 = true;
        //}
    }
    void TaskLevel(int Value)
    {
        int temp = currentTasks[Value];
        PlayerPrefs.SetInt("LevelOn", 1);
        if (PlayerPrefs.GetInt("SpecialLevel") >= MyTask[temp].TaskTarget) //&& !Added2)
        {
            TaskButtons[Value].transform.GetChild(1).GetComponent<Text>().text = MyTask[temp].TaskTarget + " / " + MyTask[temp].TaskTarget;
            TaskButtons[Value].transform.GetChild(2).GetComponent<Button>().interactable = true;
            TaskButtons[Value].transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate { GainReward(MyTask[temp].Prize, 5, Value); });
            TaskButtons[Value].transform.GetChild(2).GetComponent<GrowAndShrink>().enabled = true;
            MyTask[temp].Ready = true;
            //Added2 = true;
        }
        else
        {
            TaskButtons[Value].transform.GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetInt("SpecialLevel") + " / " + MyTask[temp].TaskTarget;
            TaskButtons[Value].transform.GetChild(2).GetComponent<Button>().interactable = false;
        }
    }
    int adreward, adpos;
    public void WatchAdbutton(int Reward, int pos)
    {
        adreward = Reward;
        adpos = pos;
        GameObject.FindObjectOfType<MainMenuManager>().BtnClickSound.Play();
        //if (AdsManager.Instance && AdsManager.Instance.RewardAdAvaible)
        //{
        //    AdsManager.Instance.functioncalling(gameObject, "AfterWatchAd");
        //    AdsManager.Instance.TappedReward();
        //}
    }
    public void AfterWatchAd()
    {
        print("Ads" + adreward);
        PlayerPrefs.SetInt("TotalReward", PlayerPrefs.GetInt("TotalReward") + adreward);
        //Added5 = false;
        AdNewTask(adpos);
    }
    public void GainReward(int Reward, int type, int pos)
    {
        GameObject.FindObjectOfType<MainMenuManager>().BtnClickSound.Play();
        if (type == 1)
        {
            PlayerPrefs.SetInt("SpecialTarget", 0);
            PlayerPrefs.SetInt("TargetOn", 0);
            //Added1 = false;
        }
        if (type == 2)
        {
            PlayerPrefs.SetInt("SpecialJems", 0);
            PlayerPrefs.SetInt("JemsOn", 0);
            //Added2 = false;
        }
        if (type == 3)
        {
            PlayerPrefs.SetInt("Damage", 1);
            PlayerPrefs.SetInt("DamageOn", 0);
            //Added3 = false;
        }
        if (type == 4)
        {
            PlayerPrefs.SetInt("Spoted", 1);
            PlayerPrefs.SetInt("SpotedOn", 0);
            //Added4 = false;
        }
        if (type == 5)
        {
            PlayerPrefs.SetInt("SpecialLevel", 0);
            PlayerPrefs.SetInt("LevelOn", 0);
            //Added4 = false;
        }
        AdNewTask(pos);
        TaskButtons[pos].transform.GetChild(2).GetComponent<GrowAndShrink>().enabled = false;
        PlayerPrefs.SetInt("TotalReward", PlayerPrefs.GetInt("TotalReward") + Reward);
    }


    public void RandomTaskAssigner()
    {
        if (PlayerPrefs.GetInt("GotTasks") == 1)
        {
            currentTasks[0] = PlayerPrefs.GetInt("A");
            currentTasks[1] = PlayerPrefs.GetInt("B");
            currentTasks[2] = PlayerPrefs.GetInt("C");
        }
        else
        {
            while (currentTasks[0] == currentTasks[1] || currentTasks[0] == currentTasks[2])
                currentTasks[0] = Random.Range(0, 10);
            while (currentTasks[1] == currentTasks[0] || currentTasks[1] == currentTasks[2])
                currentTasks[1] = Random.Range(0, 10);
            while (currentTasks[2] == currentTasks[0] || currentTasks[2] == currentTasks[1])
                currentTasks[2] = Random.Range(0, 10);
            PlayerPrefs.SetInt("A", currentTasks[0]);
            PlayerPrefs.SetInt("B", currentTasks[1]);
            PlayerPrefs.SetInt("C", currentTasks[2]);
            PlayerPrefs.SetInt("GotTasks", 1);
        }
    }

    public void AdNewTask(int Pos)
    {
        int temp = currentTasks[Pos];
        MyTask[temp].Ready = false;
        TaskButtons[Pos].transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
        TaskButtons[Pos].transform.GetChild(2).GetComponent<GrowAndShrink>().StopAllCoroutines();
        TaskButtons[Pos].transform.GetChild(2).localScale = Vector3.one;
        TaskButtons[Pos].transform.GetChild(2).GetComponent<Button>().interactable = false;
        print(Pos);
        while (Pos == 0 && (currentTasks[0] == currentTasks[1] || currentTasks[0] == currentTasks[2] || currentTasks[0] == temp))
            currentTasks[0] = Random.Range(0, 10);
        while (Pos == 1 && (currentTasks[1] == currentTasks[0] || currentTasks[1] == currentTasks[2] || currentTasks[1] == temp))
            currentTasks[1] = Random.Range(0, 10);
        while (Pos == 2 && (currentTasks[2] == currentTasks[0] || currentTasks[2] == currentTasks[1] || currentTasks[2] == temp))
            currentTasks[2] = Random.Range(0, 10);
        if (Pos == 0)
        {
            PlayerPrefs.SetInt("A", currentTasks[Pos]);
        }
        if (Pos == 1)
        {
            PlayerPrefs.SetInt("B", currentTasks[Pos]);
        }
        if (Pos == 2)
        {
            PlayerPrefs.SetInt("C", currentTasks[Pos]);
        }
    }
}
