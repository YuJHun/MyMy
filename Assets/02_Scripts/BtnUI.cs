using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BtnUI : MonoBehaviour
{
    public GameObject closeButton;
    public GameObject homePanel;
    public GameObject deposit_Withdrawal;
    public GameObject depositPanel;
    public GameObject withdrawPanel;
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
    //public void FromWithdraw()
    //{
    //    withdrawPanel.SetActive(false);
    //    deposit_Withdrawal.SetActive(false);
    //    homePanel.SetActive(true);
    //}
    public void ToDepositFromHome()
    {
        homePanel.SetActive(false);
        depositPanel.SetActive(true);
        deposit_Withdrawal.SetActive(true);
    }
    public void ToWithdrawFromHome()
    {
        homePanel.SetActive(false);
        withdrawPanel.SetActive(true);
        deposit_Withdrawal.SetActive(true);
    }
    public void OnClickSave()
    {
        GameManager.Instance.SaveUserData();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }

    public void OnClickLoad()
    {
        GameManager.Instance.LoadUserData();
        FindObjectOfType<UserUI>().UpdateUserUI(); // UI 갱신
    }
}
