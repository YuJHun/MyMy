using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UserDataService
{
    private string savePath => Path.Combine(Application.persistentDataPath, "signup.json");

    public UserListWrapper LoadAllUsers()
    {
        if (!File.Exists(savePath)) return new UserListWrapper();
        string json = File.ReadAllText(savePath);
        return JsonUtility.FromJson<UserListWrapper>(json);
    }

    public void SaveAllUsers(UserListWrapper wrapper)
    {
        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(savePath, json);
    }

    public bool Transfer(string toId, ulong amount)
    {
        var users = LoadAllUsers();

        var sender = users.list.Find(u => u.id == GameManager.Instance.userData.id);
        var receiver = users.list.Find(u => u.id == toId);

        if (sender == null || receiver == null || sender.balance < amount)
        {
            Debug.Log("송금 실패 조건 발생");
            return false;
        }

        sender.balance -= amount;
        receiver.balance += amount;

        // 현재 로그인된 유저도 업데이트
        GameManager.Instance.userData = sender;

        SaveAllUsers(users);
        return true;
    }
}