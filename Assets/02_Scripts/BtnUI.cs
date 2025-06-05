using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class BtnUI : MonoBehaviour
{
    public GameObject closeButton;          // 닫기 버튼
    public GameObject homePanel;            // 홈 화면 패널
    public GameObject deposit_Withdrawal;   // 입출금 패널
    public GameObject depositPanel;         // 입금 패널
    public GameObject withdrawPanel;        // 출금 패널
    public GameObject signUpPopup;          // 회원가입 팝업
    public GameObject sendPopup;            // 송금 팝업

    [Header("송금 관련")]
    public TMP_InputField receiverInput;    // 송금 받을 사람의 ID 입력 필드
    public TMP_InputField amountInput;      // 송금 금액 입력 필드
    public UserDataService userService;     // 사용자 데이터 서비스

    public void sendMoney()// 송금 버튼 클릭 시 호출
    {
        string toId = receiverInput.text;            // 송금 받을 사람의 ID
        ulong amount = ulong.Parse(amountInput.text); // 송금 금액

        if (userService.Transfer(toId, amount))
            Debug.Log("송금 성공!");
        else
            Debug.Log("송금 실패.");
    }
    public void FromDeposit()// 입,출금 -> 홈
    {
        depositPanel.SetActive(false);
        withdrawPanel.SetActive(false);

        deposit_Withdrawal.SetActive(false);
        homePanel.SetActive(true);
    }

   
    public void ToDepositFromHome()//홈->입금
    {
        homePanel.SetActive(false);
        depositPanel.SetActive(true);
        deposit_Withdrawal.SetActive(true);
    }
    public void ToWithdrawFromHome()//홈->출금
    {
        homePanel.SetActive(false);
        withdrawPanel.SetActive(true);
        deposit_Withdrawal.SetActive(true);
    }
    public void ToSendpFromHome()//홈->송금
    {
        homePanel.SetActive(false);
        sendPopup.SetActive(true);
    }
    public void onClickClose(GameObject popup)
    {
        popup.SetActive(false);

    }
    public void onClickOpen(GameObject popup)
    {
        popup.SetActive(true);
    }
    public void OnClickExit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
