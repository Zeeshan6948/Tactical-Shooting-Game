using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public int[] Challenges;
    public int currentChallenge;
    public Text ChallengeText;
    public Slider ChallengeSlider;
    public GameObject UnlockButton;
    public GameObject Characters;
    public Image CurrentCharacter;

    PlayerProperties PlayerProp;
    bool once;
    // Start is called before the first frame update
    void Start()
    {
        currentChallenge = PlayerPrefs.GetInt("currentChallenge");
    }

    // Update is called once per frame
    void Update()
    {
        ChallengeText.text = PlayerPrefs.GetInt("TotalReward") + " / " + Challenges[currentChallenge];
        ChallengeSlider.value = (float)PlayerPrefs.GetInt("TotalReward") / (float)Challenges[currentChallenge];
        if (PlayerPrefs.GetInt("TotalReward") >= Challenges[currentChallenge] && !once)
        {
            once = true;
            RewardReady();
        }
    }

    void RewardReady()
    {
        UnlockButton.GetComponent<GrowAndShrink>().enabled = true;
    }

    void RandomUnlockPlayer()
    {
        GameObject.FindObjectOfType<MainMenuManager>().BtnClickSound.Play();
        if (once == true)
        {
            int number = 0;
            PlayerProp = Characters.transform.GetChild(0).GetComponent<PlayerProperties>();
            while (!PlayerProp.Locked)
            {
                number = Random.Range(1, 6);
                PlayerProp = Characters.transform.GetChild(number).GetComponent<PlayerProperties>();
            }
            PlayerProp.Unlocked();
            PlayerPrefs.SetInt("TotalReward", PlayerPrefs.GetInt("TotalReward") - Challenges[currentChallenge]);
            PlayerPrefs.SetInt("currentChallenge", PlayerPrefs.GetInt("currentChallenge") + 1);
            currentChallenge = PlayerPrefs.GetInt("currentChallenge");
            UnlockButton.GetComponent<GrowAndShrink>().StopAllCoroutines();
            UnlockButton.GetComponent<GrowAndShrink>().enabled = false;
            once = false;
        }
    }

    public void StartWorkRandom()
    {
        GameObject.FindObjectOfType<MainMenuManager>().BtnClickSound.Play();
        if (once)
            StartCoroutine(RandomBlinker());
    }
    
    IEnumerator RandomBlinker()
    {
        PlayerProp = Characters.transform.GetChild(0).GetComponent<PlayerProperties>();
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < 6; i++)
            {
                PlayerProp = Characters.transform.GetChild(i).GetComponent<PlayerProperties>();
                if (PlayerProp.Locked)
                {
                    PlayerProp.GetComponent<Button>().interactable = true;
                    GameObject.FindObjectOfType<MainMenuManager>().BtnClickSound.Play();
                    yield return new WaitForSeconds(0.2f);
                    PlayerProp.GetComponent<Button>().interactable = false;
                }
            }
        }
        RandomUnlockPlayer();
    }
}
