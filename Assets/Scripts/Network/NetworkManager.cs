using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RegistUserRepuest;
using UnityEngine.Networking;
using System.IO;
using Unity.VisualScripting.Antlr3.Runtime;


public class NetworkManager : MonoBehaviour
{
    //  WebAPIの接続先を設定
#if DEBUG
    //  開発環境で使用する値をセット
    const string API_BASE_URL = "http://localhost:8000/api/";
#else
    //  本番環境で使用する値をセット
    const string API_BASE_URL = "http://azure.com/api/";
#endif

    //private int userID; //  自分のユーザーID
    private string userName;    //  入力される想定の自分のユーザー名
    private int stage;    //  自分のステージデータ
    private string apiToken;    //  APIトークン
    static public string nameData;
    static public int stageData;
    static public int achieveData;

    //  プロパティ
    public string APIToken
    {
        get
        {
            return this.apiToken;
        }
    }


    public string UserName
    {
        get
        {
            return this.userName;
        }
    }

    public int Stage
    {
        get
        {
            return this.stage;
        }
    }

    private static NetworkManager instance;
    public static NetworkManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameObj = new GameObject("NetworkManager");
                instance = gameObj.AddComponent<NetworkManager>();
                DontDestroyOnLoad(gameObj);
            }
            return instance;
        }
    }
    // 通信用の関数

    //ユーザー登録処理
    public IEnumerator RegistUser(string name, int stage, int achieve, Action<bool> result)
    {
        //サーバーに送信するオブジェクトを作成
        RegistUserRepuest requestData = new RegistUserRepuest();
        requestData.name = name;
        requestData.stage = stage;
        requestData.achievement = achieve;
        //サーバーに送信するオブジェクトをJSONに変換
        string json = JsonConvert.SerializeObject(requestData);
        Debug.Log(json);
        //送信
        UnityWebRequest request = UnityWebRequest.Post(
                    API_BASE_URL + "users/store", json, "application/json");

        yield return request.SendWebRequest();
        bool isSuccess = false;
        if (request.result == UnityWebRequest.Result.Success
    && request.responseCode == 200)
        {
            //通信が成功した場合、返ってきたJSONをオブジェクトに変換
            string resultJson = request.downloadHandler.text;
            Debug.Log("サーバーからのレスポンス: " + resultJson);

            RegistUserResponse response =
                JsonConvert.DeserializeObject<RegistUserResponse>(resultJson);

            Debug.Log("id: " + response.Id);
            Debug.Log("APIToken: " + response.APIToken);


            if (response != null)
            {
                this.userName = response.Id;
                this.apiToken = response.APIToken;
                Debug.Log("変換後のAPIToken: " + response.APIToken);
                SaveUserData();
                isSuccess = true;
            }
            else
            {
                Debug.Log("デシリアライズ失敗");
            }


            ////ファイルにユーザーIDを保存
            //this.userName = name;
            //nameData = userName;
            //this.apiToken = response.APIToken;
            //Debug.Log("取得したAPIToken : " + response.APIToken);
            //SaveUserData();
            //isSuccess = true;
        }
        result?.Invoke(isSuccess); //ここで呼び出し元のresult処理を呼び出す
    }

    //  ユーザー情報を保存する
    private void SaveUserData()
    {
        SaveData saveData = new SaveData();
        saveData.UserName = this.userName;
        nameData = userName;
        saveData.APIToken = this.apiToken;
        string json = JsonConvert.SerializeObject(saveData);
        var writer =
                new StreamWriter(Application.persistentDataPath + "/saveData.json");
        writer.Write(json);
        writer.Flush();
        writer.Close();
    }

    // ユーザー情報を読み込む
    public bool LoadUserData()
    {
        if (!File.Exists(Application.persistentDataPath + "/saveData.json"))
        {
            return false;
        }
        var reader =
                   new StreamReader(Application.persistentDataPath + "/saveData.json");
        Debug.Log(Application.persistentDataPath + "/saveData.json");
        string json = reader.ReadToEnd();
        reader.Close();
        SaveData saveData = JsonConvert.DeserializeObject<SaveData>(json);
        this.userName = saveData.UserName;
        nameData = userName;
        //stage = saveData.Stage;
        Debug.Log("APIToken : " + APIToken);
        return true;
    }

    // ユーザーのレベル(クリアステージ数)を詳細に読み込む
    static public int LoadUserLvl()
    {
        if (!File.Exists(Application.persistentDataPath + "/saveData.json"))
        {
            return 0;
        }
        var reader =
                   new StreamReader(Application.persistentDataPath + "/saveData.json");
        Debug.Log(Application.persistentDataPath + "/saveData.json");
        string json = reader.ReadToEnd();
        reader.Close();
        SaveData saveData = JsonConvert.DeserializeObject<SaveData>(json);
        //stageData = saveData.Stage;
        return stageData;
    }
    // ユーザーのレベル(クリアステージ数)を詳細に読み込む
    static public int LoadUserAchievement()
    {
        if (!File.Exists(Application.persistentDataPath + "/saveData.json"))
        {
            return 0;
        }
        var reader =
                   new StreamReader(Application.persistentDataPath + "/saveData.json");
        Debug.Log(Application.persistentDataPath + "/saveData.json");
        string json = reader.ReadToEnd();
        reader.Close();
        SaveData saveData = JsonConvert.DeserializeObject<SaveData>(json);
        //achieveData = saveData.Achievement;
        return achieveData;
    }

    //ユーザー情報更新
    public IEnumerator UpdateUser(string name, int stage,int achieve, Action<bool> result)
    {
        //サーバーに送信するオブジェクトを作成
        UpdateUserRequest requestData = new UpdateUserRequest();
        requestData.name = name;
        requestData.stage = stage;
        requestData.achievement = achieve;
        //サーバーに送信するオブジェクトをJSONに変換
        string json = JsonConvert.SerializeObject(requestData);
        //送信
        UnityWebRequest request = UnityWebRequest.Post(
                    API_BASE_URL + "users/update", json, "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + this.apiToken);

        yield return request.SendWebRequest();

        bool isSuccess = false;
        if (request.result == UnityWebRequest.Result.Success
         && request.responseCode == 200)
        {
            // 通信が成功した場合、ファイルに更新したユーザー名を保存
            this.userName = name;
            nameData = userName;
            SaveUserData();
            isSuccess = true;
        }

        result?.Invoke(isSuccess); //ここで呼び出し元のresult処理を呼び出す
    }
}