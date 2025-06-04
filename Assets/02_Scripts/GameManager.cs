using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;// { get; private set; }
    [Header("User Data")]
    public UserData userData;

    private string savePath => Path.Combine(Application.persistentDataPath, "userdata.json");

    void Start()
    {
        SaveUserDataToPrefs();
        Debug.Log(savePath);
#if UNITY_EDITOR
        LoadUserData();
        Debug.Log("로드 성공!");
#endif





        string path = "";

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData)
               + @"\..\LocalLow\" + Application.companyName + @"\" + Application.productName;
#elif UNITY_ANDROID
    path = Application.persistentDataPath; // 내부 앱 저장소 (/data/data/패키지명/files)
#elif UNITY_IOS
    path = Application.persistentDataPath; // 예: /var/mobile/Containers/Data/Application/UUID/Documents
#else
    path = Application.persistentDataPath; // 기타 플랫폼
#endif

        Debug.Log(" PlayerPrefs 저장 위치 예상 경로:\n" + path);
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadUserData();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //userData = new UserData("Player", 555555555555550, 11000000);
    public void SaveUserData()
    {
        string json = JsonUtility.ToJson(userData, true);
        // C# 객체인 userData를 JSON 형식의 문자열로 변환하는 코드
        //userData라는 객체를 JSON 문자열로 바꾸고, 그 결과를 json이라는 문자열 변수에 저장
        //JsonUtility Unity에서 제공하는 JSON 직렬화 도구(Unity 전용)
        //ToJson(obj) C# 객체를 JSON 문자열로 변환함
        //userData 변환하고 싶은 대상 객체(UserData 클래스 인스턴스)
        //true 보기 좋게 줄 바꿈과 들여쓰기 포함해서 출력하도록 함(Pretty Print 옵션)
        //JsonUtility.ToJson(obj, true)   보기 좋게 정렬된 JSON(들여쓰기 포함)
        //JsonUtility.ToJson(obj, false)  한 줄로 된 JSON(용량 작고 전송 최적화에 좋음)
        File.WriteAllText(savePath, json);
        //json이라는 문자열 데이터를, savePath에 해당하는 파일에 텍스트 파일로 저장하는 코드
        //File System.IO 네임스페이스에 포함된 C# 기본 파일 클래스
        //WriteAllText(path, contents)    지정한 경로(path)에 문자열(contents)을 덮어쓰기로 저장
        //savePath    저장할 파일 경로(예: "C:/Users/유저/AppData/.../userdata.json")
        //json 실제 저장할 내용(보통 JSON 문자열)
    }
    //플레이어 프랩스를 통해 디버그 창에 저장후 디버그창에 출력
    public void SaveUserDataToPrefs()
    {
        PlayerPrefs.SetString("UserName", userData.name);
        PlayerPrefs.SetInt("Cash", userData.cash);
        PlayerPrefs.SetString("Balance", userData.balance.ToString()); // ulong은 string으로 저장

        PlayerPrefs.Save();

        Debug.Log(" Saved to PlayerPrefs");
        Debug.Log(" Name: " + PlayerPrefs.GetString("UserName"));
        Debug.Log(" Cash: " + PlayerPrefs.GetInt("Cash"));
        Debug.Log(" Balance: " + PlayerPrefs.GetString("Balance"));
    }
    public void LoadUserData()
    {
        if (File.Exists(savePath))
        //지정된 경로에 파일이 실제로 존재하는지를 확인하는 조건문
        {
            string json = File.ReadAllText(savePath);
            //Unity와 C#에서 파일 내용을 문자열로 읽어오는 기본 코드
            //savePath에 있는 파일(userdata.json)의 전체 내용을 문자열 형태로 읽어서 json 변수에 저장하는 코드
            //File.ReadAllText(...)=System.IO에 포함된 함수로, 텍스트 파일 전체를 문자열(string)로 읽어옴
            //savePath=	파일 경로 (Application.persistentDataPath + "userdata.json" 같이 완성된 경로)
            //json=읽은 문자열 결과를 담는 변수 예: {"name":"유재훈","balance":100000,"cash":300000}
            userData = JsonUtility.FromJson<UserData>(json);
            //Unity와 C#에서 객체를 JSON 문자열로 변환하는 기본 코드
            //이걸 통해 저장된 데이터를 다시 UserData 객체로 복원할 수 있다
        }
        else
        {
            Debug.Log("No save file found. Creating new data.");
            userData = new UserData("유재훈", 100000, 300000);
            SaveUserData(); // 기본 데이터 저장
        }
        //형식               문자열 기반.json 파일               Key - Value 저장소(Unity 내부)
        //저장 위치          실제 파일(디스크에 저장됨)          레지스트리 / 로컬 DB(플랫폼마다 다름)
        //데이터 크기        큼(객체, 리스트, 구조체 다 가능)    작음(문자열, int, float만 가능)
        //유지 기간          파일이 삭제되기 전까지 영구적       기본적으로 영구적(앱 삭제 시 사라짐)
        //복잡한 구조 저장   가능(객체, 배열 등) ✅	             불가능 ❌ (배열, 리스트 직접 저장 불가)
        //읽기 / 쓰기 속도   느림(파일 접근)                     빠름(내부 저장소)
        //플랫폼 간 공유     가능(파일 복사)                     어려움(내부 저장 위치 불투명)
        //암호화             수동으로 가능                       직접 암호화해야 함
    }
}
