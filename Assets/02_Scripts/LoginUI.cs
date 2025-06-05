using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoginUI : MonoBehaviour
{
    public TMP_InputField idInput;              // ID 입력 필드
    public TMP_InputField pwInput;              // 비밀번호 입력 필드
    public GameObject loginFailPopup;           // 로그인 실패 시 표시할 팝업
    public GameObject bankPopup;                // 로그인 성공 후 표시할 은행 메인 UI
    public GameObject loginPopup;               // 로그인 UI
    public LoginManager loginManager;           // 로그인 관리 스크립트

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
    public void onClickLogout()
    {
        loginManager.Logout();
        bankPopup.SetActive(false);
        loginPopup.SetActive(true);
        FindObjectOfType<UserUI>().UpdateUserUI();
    }
}
