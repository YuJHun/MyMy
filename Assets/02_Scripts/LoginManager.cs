using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoginManager : MonoBehaviour
{
    private string savePath => Path.Combine(Application.persistentDataPath, "signup.json");

    public bool TryLogin(string id, string password)
    {
        if (!File.Exists(savePath)) return false;

        string json = File.ReadAllText(savePath);
        UserListWrapper wrapper = JsonUtility.FromJson<UserListWrapper>(json);

        foreach (var user in wrapper.list)
        {
            if (user.id == id && user.password == password)
            {
                Debug.Log($" 로그인 성공: {id}");

                // 로그인한 유저를 게임 전역에 등록
                GameManager.Instance.userData = user;

                return true;
            }
        }

        Debug.Log(" 로그인 실패: ID 또는 비밀번호 불일치");
        return false;
    }
}
