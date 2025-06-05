using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UserDataService
{
    private string savePath => Path.Combine(Application.persistentDataPath, "signup.json");

    /// <summary>
    /// 저장된 모든 사용자 데이터를 불러오는 함수
    /// </summary>
    /// <returns>모든 사용자 데이터 리스트를 담은 래퍼</returns>
    public UserListWrapper LoadAllUsers()
    {
        if (!File.Exists(savePath)) return new UserListWrapper();// 파일이 없으면 빈 리스트 반환

        string json = File.ReadAllText(savePath);// JSON을 읽어서 UserListWrapper로 역직렬화
        return JsonUtility.FromJson<UserListWrapper>(json);
    }

    /// <summary>
    /// 모든 사용자 데이터를 JSON으로 저장하는 함수
    /// </summary>
    /// <param name="wrapper">저장할 사용자 데이터 리스트</param>
    public void SaveAllUsers(UserListWrapper wrapper)
    {
        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(savePath, json);
    }

    /// <summary>
    /// 송금 기능: toId에게 amount만큼 송금
    /// </summary>
    /// <param name="toId">받는 사람 ID</param>
    /// <param name="amount">송금할 금액</param>
    /// <returns>성공 여부</returns>
    public bool Transfer(string toId, ulong amount)
    {
        var users = LoadAllUsers();// 모든 사용자 데이터를 불러오기

        var sender = users.list.Find(u => u.id == GameManager.Instance.userData.id);// 보낸 사람과 받는 사람 찾기
        var receiver = users.list.Find(u => u.id == toId);

        if (sender == null || receiver == null || sender.balance < amount)// 조건 검사: 송금 불가 상황
        {
            Debug.Log("송금 실패 조건 발생");
            return false;
        }

        sender.balance -= amount;
        receiver.balance += amount;

        // 게임 내 로그인된 유저 데이터도 최신화
        GameManager.Instance.userData = sender;

        SaveAllUsers(users);// 변경된 사용자 데이터를 저장
        return true;
    }

    /// <summary>
    /// 특정 ID의 사용자가 존재하는지 확인
    /// </summary>
    /// <param name="inputId">확인할 사용자 ID</param>
    /// <returns>존재 여부</returns>
    public bool IsExistingUser(string inputId)
    {
        var users = LoadAllUsers();
        return users.list.Exists(user => user.id == inputId);
    }
}