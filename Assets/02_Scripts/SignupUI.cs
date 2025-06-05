using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SignupUI : MonoBehaviour
{
    [Header("회원가입관련")]
    public TMP_InputField idInput;
    public TMP_InputField nameInput;
    public TMP_InputField pwInput; 
    public TMP_InputField pwConInput;
    public TextMeshProUGUI idCheckText;

    public SignupDataManager dataManager;

    public GameObject signUpValidatorPopup;
    private string savePath => Path.Combine(Application.persistentDataPath, "signup.json");
    private void Start()
    {
        idInput.onValueChanged.AddListener(CheckDuplicateID);
    }
    private void CheckDuplicateID(string inputId)
    {
        if (string.IsNullOrWhiteSpace(inputId))
        {
            idCheckText.text = "";
            return;
        }

        if (dataManager.IsDuplicateID(inputId))
        {
            idCheckText.text = "<color=red> 중복된 ID입니다</color>";
        }
        else
        {
            idCheckText.text = "<color=green> 사용 가능한 ID입니다</color>";
        }
    }

    public void OnClickSave()
    {
        if (string.IsNullOrWhiteSpace(idInput.text) ||
            string.IsNullOrWhiteSpace(nameInput.text) ||
            string.IsNullOrWhiteSpace(pwInput.text) ||
            string.IsNullOrWhiteSpace(pwConInput.text))
        {
            signUpValidatorPopup.SetActive(true);
            return;
        }

        string id = idInput.text;
        string pw = pwInput.text;
        string name = nameInput.text;
        ulong balance = 123456789;
        int cash = 100000;

        if (dataManager.IsDuplicateID(id))
        {
            idCheckText.text = "<color=red>중복된 ID입니다.</color>";
            return;
        }

        UserData newUser = new UserData(id, pw, name, balance, cash);
        dataManager.SaveSignupData(newUser); // 리스트에 추가 저장
        Debug.Log($"회원가입 완료: {id}");
    }
    //public void OnClickSave()
    //{
    //    if (string.IsNullOrWhiteSpace(idInput.text) ||
    //        string.IsNullOrWhiteSpace(nameInput.text) ||
    //        string.IsNullOrWhiteSpace(pwInput.text) ||
    //        string.IsNullOrWhiteSpace(pwConInput.text))
    //    {
    //        signUpValidatorPopup.SetActive(true);
    //    }
    //    else
    //    {
    //        string id = idInput.text;
    //        string pw = pwInput.text;
    //        string name = nameInput.text;
    //        ulong balance = 123456789; // 초기 잔액
    //        int cash = 100000; // 초기 캐시
    //        dataManager.SaveSignupData(id, pw,name,balance,cash);
    //        Debug.Log("회원가입 정보 저장 완료: " + id + ", " + pw+","+name);
    //    }
    //}

    //public void OnClickLoad()
    //{
    //    UserData loaded = dataManager.LoadSignupData();
    //    if (loaded != null)
    //    {
    //        idInput.text = loaded.id;
    //        pwInput.text = loaded.password;
    //    }
    //}

    //public void closeSignupValidatorPopup()
    //{
    //    signUpValidatorPopup.SetActive(false);
    //}
}
