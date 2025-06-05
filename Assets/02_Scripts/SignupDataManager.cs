using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// 사용자 데이터 리스트를 JSON으로 감싸는 래퍼 클래스
/// </summary>
[System.Serializable]
public class UserListWrapper
{
    public List<UserData> list = new List<UserData>();
}

/// <summary>
/// 회원가입 데이터 관리 스크립트
/// </summary>
public class SignupDataManager : MonoBehaviour
{
    private string savePath => Path.Combine(Application.persistentDataPath, "signup.json");// 회원 정보가 저장된 JSON 경로


    /// <summary>
    /// 모든 회원 데이터를 불러오기
    /// </summary>
    /// <returns>저장된 사용자 리스트</returns>
    private UserListWrapper LoadAllData()
    {
        if (!File.Exists(savePath))                // 파일이 없으면
            return new UserListWrapper();          // 빈 리스트 반환

        string json = File.ReadAllText(savePath);  // JSON 읽기
        return JsonUtility.FromJson<UserListWrapper>(json);//JSON 형식의 문자열(json)을 UserListWrapper 타입의 객체로 변환(역직렬화, deserialize) 해서 반환
    }

    /// <summary>
    /// 새 사용자 정보를 저장하기
    /// </summary>
    /// <param name="newUser">새로 가입할 사용자 정보</param>
    public void SaveSignupData(UserData newUser)
    {
        UserListWrapper wrapper;

        if (File.Exists(savePath))// 기존 데이터가 있으면 불러오고, 없으면 새로 생성
        {
            string json = File.ReadAllText(savePath);
            wrapper = JsonUtility.FromJson<UserListWrapper>(json);
        }
        else
        {
            wrapper = new UserListWrapper();
        }

        wrapper.list.Add(newUser);  // 새로운 사용자 데이터를 리스트에 추가

        string newJson = JsonUtility.ToJson(wrapper, true);// JSON으로 직렬화 후 저장
        File.WriteAllText(savePath, newJson);

        Debug.Log(" 회원가입 저장 경로: " + savePath);
    }

    /// <summary>
    /// 중복 ID가 있는지 확인
    /// </summary>
    /// <param name="inputId">확인할 ID</param>
    /// <returns>true: 중복됨, false: 중복 아님</returns>
    public bool CheckDuplicateID(string inputId)
    {
        UserListWrapper dataList = LoadAllData();
        return dataList.list.Exists(user => user.id == inputId);
    }

    /// <summary>
    /// 특정 ID의 사용자 정보 가져오기
    /// </summary>
    /// <param name="id">찾을 사용자 ID</param>
    /// <returns>해당 사용자 정보 (없으면 null)</returns>
    public UserData GetUserData(string id)
    {
        UserListWrapper dataList = LoadAllData();
        return dataList.list.Find(user => user.id == id);
    }

    /// <summary>
    /// ID 중복 여부 확인 (중복이면 true)
    /// </summary>
    /// <param name="inputId">확인할 ID</param>
    /// <returns>true: 중복됨, false: 중복 아님</returns>
    public bool IsDuplicateID(string inputId)
    {
        if (!File.Exists(savePath)) return false;

        string json = File.ReadAllText(savePath);
        UserListWrapper wrapper = JsonUtility.FromJson<UserListWrapper>(json);

        foreach (UserData user in wrapper.list)
        {
            if (user.id == inputId)
                return true;// 중복된 ID 발견
        }
        return false;// 중복되지 않음
    }

}
