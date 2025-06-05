using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class BtnUI : MonoBehaviour
{
    public GameObject closeButton;
    public GameObject homePanel;
    public GameObject deposit_Withdrawal;
    public GameObject depositPanel;
    public GameObject withdrawPanel;
    public GameObject signUpPopup;
    public GameObject sendPopup;

    [Header("송금 관련")]
    public TMP_InputField receiverInput;
    public TMP_InputField amountInput;
    public UserDataService userService;

    public void sendMoney()
    {
        string toId = receiverInput.text;
        ulong amount = ulong.Parse(amountInput.text);

        if (userService.Transfer(toId, amount))
            Debug.Log("송금 성공!");
        else
            Debug.Log("송금 실패.");
    }
    public void CloseButton()
    {
        closeButton.SetActive(false);
        FindObjectOfType<UserUI>().UpdateUserUI();
    }
    public void FromDeposit()
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
        homePanel.SetActive(true);

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
