using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public string name;
    public ulong balance;
    public int cash;
    public long cash1;

    public UserData(string name1, ulong balance1, int cash1)
    {
        name = name1;
        balance = balance1;
        cash = cash1;
    }

}
