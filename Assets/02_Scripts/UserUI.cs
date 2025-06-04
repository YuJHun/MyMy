using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI balanceText;
    public TextMeshProUGUI cashText;
    void Start()
    {
        UpdateUserUI();
    }
    public void UpdateUserUI()
    {
        var userData = GameManager.Instance.userData;

        nameText.text = userData.name;
        balanceText.text = "Balance      " + userData.balance.ToString("N0");

        cashText.text = userData.cash.ToString("N0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
