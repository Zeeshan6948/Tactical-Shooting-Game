using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivacyPolicyDailog : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("PrivacyAccepted") == 1)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PrivacyAcceptedButton()
    {
        PlayerPrefs.SetInt("PrivacyAccepted", 1);
        PlayerPrefs.SetInt("TotalReward", PlayerPrefs.GetInt("TotalReward") + 3000);
        PlayerPrefs.SetInt("BulletHave", PlayerPrefs.GetInt("BulletHave") + 50);
        Destroy(this.gameObject);
    }

    public void PrivacyPolicyBtn()
    {
        Application.OpenURL("https://taptoaction.wixsite.com/privacypolicy");
    }

    public void TermAndConditionBtn()
    {
        Application.OpenURL("https://taptoaction.wixsite.com/privacypolicy/about");
    }
}
