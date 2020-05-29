using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IncrementalText : MonoBehaviour
{
    [Range(0.01f,0.4f)]
    public float Speed;
    public int score;
    [HideInInspector]
    public int displayScore=0;
    public Text scoreUI;
    public GameObject DisableOb;
    public GameObject Calling;
    public string FunctionName;
    public bool TimeBased;
    public float TimetoComplete;
    void Start()
    {

    }

    private IEnumerator ScoreIncrease()
    {
        bool working = true;
        while (working)
        {
            if (displayScore < score)
            {
                displayScore++; //Increment the display score by 1
                scoreUI.text = "+"+displayScore.ToString(); //Write it to the UI
                LevelManager.m_instance.JemCollectionSound();
            }
            else
            {
                working = false;
            }
            yield return new WaitForSeconds(Speed); // I used .2 secs but you can update it as fast as you want
        }
        if (DisableOb)
            DisableOb.SetActive(false);
        if (Calling)
            Calling.SendMessage(FunctionName, SendMessageOptions.DontRequireReceiver);
    }

    public void Inceamental()
    {
        if (TimeBased)
        {
            Speed = TimetoComplete / score;
        }
        StartCoroutine(ScoreIncrease());
    }

    public void Decremental ()
    {
        score = int.Parse(scoreUI.text);
        displayScore = score;
        if (TimeBased)
        {
            Speed = TimetoComplete / score;
        }
        StartCoroutine(ScoreDecrease());
    }

    private IEnumerator ScoreDecrease()
    {
        bool working = true;
        while (working)
        {
            if (displayScore > 0)
            {
                displayScore--; //Increment the display score by 1
                scoreUI.text = displayScore.ToString(); //Write it to the UI
                LevelManager.m_instance.JemCollectionSound();
            }
            else
            {
                working = false;
            }
            yield return new WaitForSeconds(Speed); // I used .2 secs but you can update it as fast as you want
        }
        if(DisableOb)
            DisableOb.SetActive(false);
        if (Calling)
            Calling.SendMessage(FunctionName, SendMessageOptions.DontRequireReceiver);
    }
}
