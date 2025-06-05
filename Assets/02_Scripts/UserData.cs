using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public string id;
    public string password;
    public string name;
    public ulong balance;
    public int cash;

    public UserData(string id, string password, string name, ulong balance, int cash)
    {
        this.id = id;
        this.password = password;
        this.name = name;
        this.balance = balance;
        this.cash = cash;
    }

}
