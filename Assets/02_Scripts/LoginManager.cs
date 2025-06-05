using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoginManager : MonoBehaviour
{
    private string savePath => Path.Combine(Application.persistentDataPath, "signup.json");

    public bool TryLogin(string id, string password)
    {
        if (!File.Exists(savePath)) return false;// 저장된 회원 정보 파일이 없으면 실패 처리

        string json = File.ReadAllText(savePath);// JSON 파일에서 회원 정보 읽기
        UserListWrapper wrapper = JsonUtility.FromJson<UserListWrapper>(json);

        foreach (var user in wrapper.list) // 모든 회원 데이터 중에서 입력한 ID/비밀번호를 찾기
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
    public void Logout()
    {
        if (GameManager.Instance.userData != null)
        {
            Debug.Log($"로그아웃: {GameManager.Instance.userData.id}");
        }

        GameManager.Instance.userData = null;
    }
}
