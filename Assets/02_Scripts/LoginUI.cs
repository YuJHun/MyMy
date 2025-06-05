using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoginUI : MonoBehaviour
{
    public TMP_InputField idInput;
    public TMP_InputField pwInput;
    public GameObject loginFailPopup;
    public GameObject bankPopup;
    public GameObject loginPopup;
    public LoginManager loginManager;

    public void OnClickLogin()
    {
        string id = idInput.text;
        string pw = pwInput.text;

        if (loginManager.TryLogin(id, pw))
        {
            bankPopup.SetActive(true);
            loginPopup.SetActive(false);
            FindObjectOfType<UserUI>().UpdateUserUI();
        }
        else
        {
            loginFailPopup.SetActive(true);
        }
    }

    public void ClosePopup(GameObject popup)
    {
        popup.SetActive(false);
    }
}
