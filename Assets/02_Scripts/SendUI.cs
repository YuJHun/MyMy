using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SendUI : MonoBehaviour
{
    public TMP_InputField receiverInput;         // 받는 사람의 ID 입력 필드
    public TMP_InputField amountInput;           // 송금할 금액 입력 필드
    public GameObject sendNotFoundPopup;         // 송금 대상 ID가 없을 때 띄울 팝업
    public GameObject sendFailPopup;             // 송금 실패(금액 비어있음, 0원, 잔액 부족 등) 시 띄울 팝업
    public UserDataService userService;          // 사용자 데이터 서비스
    private void Start()
    {
        // UserDataService 초기화
        userService = new UserDataService();
    }
    public void onClickSend()
    {
        string receiverId = receiverInput.text.Trim();// 입력한 받는 사람 ID (공백 제거)
        if (!userService.IsExistingUser(receiverId))//"해당 아이디(receiverId)를 가진 유저가 존재하지 않으면" 이라는 조건
        {
            sendNotFoundPopup.SetActive(true);
            return;
        }
        if (string.IsNullOrWhiteSpace(amountInput.text))//금액 입력란이 비었는지 확인하는 코드
        {
            sendFailPopup.SetActive(true);
            return;
        }

        ulong amount = ulong.Parse(amountInput.text); // 입력한 금액
        if (amount == 0)
        {
            Debug.Log(" 0원은 송금할 수 없습니다.");
            sendFailPopup.SetActive(true);
            return;
        }
        bool success = userService.Transfer(receiverId, amount);
        if (success)
        {
            FindObjectOfType<UserUI>().UpdateUserUI();
            Debug.Log(" 송금 성공");
        }
        else
        {
            sendFailPopup.SetActive(true);
            Debug.Log(" 송금 실패: 잔액 부족 등");
        }
    }

}
