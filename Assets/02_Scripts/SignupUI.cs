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
    public TMP_InputField idInput;            // ID 입력 필드
    public TMP_InputField nameInput;          // 이름 입력 필드
    public TMP_InputField pwInput;            // 비밀번호 입력 필드
    public TMP_InputField pwConInput;         // 비밀번호 확인 입력 필드
    public TextMeshProUGUI idCheckText;       // ID 중복 확인 결과를 보여줄 텍스트

    public SignupDataManager dataManager; // 회원가입 데이터 매니저 (데이터 저장/로드 담당)

    public GameObject signUpValidatorPopup;// 입력값 누락 시 경고 팝업
    private string savePath => Path.Combine(Application.persistentDataPath, "signup.json");

    /// <summary>
    /// 시작할 때, ID 입력 필드에 실시간 중복 확인 이벤트 추가
    /// </summary>
    private void Start()
    {
        idInput.onValueChanged.AddListener(CheckDuplicateID);
    }

    /// <summary>
    /// ID 중복 여부를 실시간으로 확인하여 결과를 idCheckText에 표시
    /// </summary>
    /// <param name="inputId">입력된 ID 문자열</param>
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

    /// <summary>
    /// 회원가입 버튼 클릭 시 호출되는 함수
    /// </summary>
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

        if (dataManager.IsDuplicateID(id))// 중복된 ID면 가입 처리 중단
        {
            idCheckText.text = "<color=red>중복된 ID입니다.</color>";
            return;
        }

        UserData newUser = new UserData(id, pw, name, balance, cash);// 새로운 사용자 데이터 생성 및 저장
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
