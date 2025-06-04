using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BankUI : MonoBehaviour
{
    public UserUI userUI;
    public GameObject no_Balance;
    public GameObject no_Cash;
    public TMP_InputField inputField;

    public void DepositByInput()
    {
        if (int.TryParse(inputField.text, out int valueToAdd))
        {
            if (valueToAdd <= 0)
            {
                Debug.Log("입력값이 0 이하입니다.");
                return;
            }
            if (GameManager.Instance.userData.cash < valueToAdd)
            {
                no_Cash.SetActive(true);
                return;
            }
            GameManager.Instance.userData.balance += (ulong)valueToAdd;
            GameManager.Instance.userData.cash -= valueToAdd;
            userUI.UpdateUserUI();
        }
        else
        {
            Debug.Log("잘못된 입력값입니다.");
        }
    }
    public void WithdrawByInput()
    {
        if (ulong.TryParse(inputField.text, out ulong valueToAdd))
        {
            if (valueToAdd <= 0)
            {
                Debug.Log("입력값이 0 이하입니다.");
                return;
            }
            if (GameManager.Instance.userData.balance < valueToAdd)
            {
                no_Balance.SetActive(true);
                return;
            }
            GameManager.Instance.userData.balance -= valueToAdd;
            GameManager.Instance.userData.cash += (int)valueToAdd;
            userUI.UpdateUserUI();
        }
        else
        {
            Debug.Log("잘못된 입력값입니다.");
        }
    }
    public void Up10000()
    {
        if (GameManager.Instance.userData.cash < 10000)
        {
            no_Cash.SetActive(true);
            return;
        }
        GameManager.Instance.userData.balance += 10000;
        GameManager.Instance.userData.cash -= 10000;
        FindObjectOfType<UserUI>().UpdateUserUI();
    }
    public void Up30000()
    {
        if (GameManager.Instance.userData.cash < 30000)
        {
            no_Cash.SetActive(true);
            return;
        }
        GameManager.Instance.userData.balance += 30000;

        GameManager.Instance.userData.cash -= 30000;
        userUI.UpdateUserUI();
    }
    public void Up50000()
    {
        if (GameManager.Instance.userData.cash < 50000)
        {
            no_Cash.SetActive(true);
            return;
        }
        GameManager.Instance.userData.balance += 50000;
        GameManager.Instance.userData.cash -= 50000;
        FindObjectOfType<UserUI>().UpdateUserUI();
    }
    public void Down10000()
    {
        if (GameManager.Instance.userData.balance < 10000)
        {
            no_Balance.SetActive(true);
            return;
        }
        GameManager.Instance.userData.balance -= 10000;
        GameManager.Instance.userData.cash += 10000;
        FindObjectOfType<UserUI>().UpdateUserUI();
    }
    public void Down30000()
    {
        if (GameManager.Instance.userData.balance < 30000)
        {
            no_Balance.SetActive(true);
            return;
        }
        GameManager.Instance.userData.balance -= 30000;
        GameManager.Instance.userData.cash += 30000;
        FindObjectOfType<UserUI>().UpdateUserUI();
    }
    public void Down50000()
    {
        if (GameManager.Instance.userData.balance < 50000)
        {
            no_Balance.SetActive(true);
            return;
        }
        GameManager.Instance.userData.balance -= 50000;
        GameManager.Instance.userData.cash += 50000;
        FindObjectOfType<UserUI>().UpdateUserUI();
    }
}
