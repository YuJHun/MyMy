using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[System.Serializable]
public class UserListWrapper
{
    public List<UserData> list = new List<UserData>();
}
public class SignupDataManager : MonoBehaviour
{
    private string savePath => Path.Combine(Application.persistentDataPath, "signup.json");

    private UserListWrapper LoadAllData()
    {
        if (!File.Exists(savePath))
            return new UserListWrapper(); // 빈 리스트 반환

        string json = File.ReadAllText(savePath);
        return JsonUtility.FromJson<UserListWrapper>(json);
    }
    public void SaveSignupData(UserData newUser)
    {
        //UserData newUser = new UserData(id, password, name, balance, cash);

        UserListWrapper wrapper;

        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            wrapper = JsonUtility.FromJson<UserListWrapper>(json);
        }
        else
        {
            wrapper = new UserListWrapper();
        }

        wrapper.list.Add(newUser);  // ← 여기서 이제 newUser로 넣어줌
        string newJson = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(savePath, newJson);
        Debug.Log(" 회원가입 저장 경로: " + savePath);
    }
    public bool CheckDuplicateID(string inputId)
    {
        UserListWrapper dataList = LoadAllData();
        return dataList.list.Exists(user => user.id == inputId);
    }

    public UserData GetUserData(string id)
    {
        UserListWrapper dataList = LoadAllData();
        return dataList.list.Find(user => user.id == id);
    }
    public bool IsDuplicateID(string inputId)
    {
        if (!File.Exists(savePath)) return false;

        string json = File.ReadAllText(savePath);
        UserListWrapper wrapper = JsonUtility.FromJson<UserListWrapper>(json);

        foreach (UserData user in wrapper.list)
        {
            if (user.id == inputId)
                return true;
        }
        return false;
    }

}
