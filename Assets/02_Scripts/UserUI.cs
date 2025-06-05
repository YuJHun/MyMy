using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserUI : MonoBehaviour
{
    // 이름, 잔액, 캐시를 표시할 TextMeshProUGUI 컴포넌트
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI balanceText;
    public TextMeshProUGUI cashText;

    /// <summary>
    /// 게임 시작 시 사용자 UI 갱신
    /// </summary>
    void Start()
    {
        UpdateUserUI();
    }

    /// <summary>
    /// 현재 로그인된 사용자의 정보를 UI에 표시
    /// </summary>
    public void UpdateUserUI()
    {
        var userData = GameManager.Instance.userData;

        if (userData == null)// 로그아웃 상태일 경우, UI 초기화
        {
            // 로그아웃 상태일 경우, UI 초기화 또는 숨김
            nameText.text = "";
            balanceText.text = "잔액: -";
            cashText.text = "보유 캐시: -";
            return;
        }

        // 로그인 상태라면 이름, 잔액, 캐시를 UI에 표시
        nameText.text = userData.name;
        balanceText.text = "Balance      " + userData.balance.ToString("N0");
        cashText.text = userData.cash.ToString("N0");
    }
}
