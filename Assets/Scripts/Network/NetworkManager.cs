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
    private int[] tileX;
    private int[] tileY;
    private int[] tileType;
    private int[] eleX;
    private int[] eleY;
    private int[] eleType;
    private int[] paletteType;
    static public string nameData;
    static public int stageData;

    //  プロパティ
    public string Token
    {
        get
        {
            return this.apiToken;
        }
    }


    public string Name
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

    public int[] TileX
    {
        get
        {
            return this.tileX;
        }
    }
    public int[] TileY
    {
        get
        {
            return this.tileY;
        }
    }
    public int[] TileType
    {
        get
        {
            return this.tileType;
        }
    }
    public int[] EleX
    {
        get
        {
            return this.eleX;
        }
    }
    public int[] EleY
    {
        get
        {
            return this.eleY;
        }
    }
    public int[] EleType
    {
        get
        {
            return this.eleType;
        }
    }

    public int[] PaletteType
    {
        get
        {
            return this.paletteType;
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
    public IEnumerator RegistUser(string name, int stage, Action<bool> result)
    {
        //サーバーに送信するオブジェクトを作成
        RegistUserRepuest requestData = new RegistUserRepuest();
        requestData.Name = name;
        requestData.Stage = stage;
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

            Debug.Log("id: " + response.Name);
            Debug.Log("APIToken: " + response.Token);


            if (response != null)
            {
                this.userName = response.Name;
                this.apiToken = response.Token;
                Debug.Log("変換後のAPIToken: " + response.Token);
                SaveUserData();
                isSuccess = true;
            }
            else
            {
                Debug.Log("デシリアライズ失敗");
            }
        }
        result?.Invoke(isSuccess); //ここで呼び出し元のresult処理を呼び出す
    }

    //  ユーザー情報を保存する
    private void SaveUserData()
    {
        SaveData saveData = new SaveData();
        saveData.Name = this.userName;
        nameData = userName;
        saveData.Token = this.apiToken;
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
        this.userName = saveData.Name;
        nameData = userName;
        //stage = saveData.Stage;
        Debug.Log("APIToken : " + Token);
        return true;
    }

    // ユーザー情報を読み込む
    public (string, int) IndexUserData()
    {
        
        //送信
        UnityWebRequest request = UnityWebRequest.Post(
                    API_BASE_URL + "users/index", "", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + this.apiToken);

        //yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success
         && request.responseCode == 200)
        {
            //通信が成功した場合、返ってきたJSONをオブジェクトに変換
            string resultJson = request.downloadHandler.text;

            IndexUserResponse response =
                JsonConvert.DeserializeObject<IndexUserResponse>(resultJson);

            // すぐにアクセスできるようにゲーム内に情報を保持しておく
            nameData = response.Name;
            stageData = response.Stage;
            Debug.Log("Token設置完了");
        }

        if (!File.Exists(Application.persistentDataPath + "/saveData.json"))
        {
            return (null,0);
        }
        var reader =
                   new StreamReader(Application.persistentDataPath + "/saveData.json");
        Debug.Log(Application.persistentDataPath + "/saveData.json");
        string json = reader.ReadToEnd();
        reader.Close();
        SaveData saveData = JsonConvert.DeserializeObject<SaveData>(json);
        this.userName = saveData.Name;
        nameData = userName;
        apiToken = saveData.Token;
        return (nameData, stageData);
    }

    // ユーザーのレベル(クリアステージ数)を詳細に読み込む
    static public int LoadUserLvl()
    {
        return stageData;
    }
    // ユーザーのレベル(クリアステージ数)を詳細に読み込む
    static public int LoadUserAchievement()
    {
        return 0;
    }
    static public string LoadUserName()
    {
        return nameData;
    }

    //ユーザー情報更新
    public IEnumerator UpdateUser(string name, int stage, Action<bool> result)
    {
        //サーバーに送信するオブジェクトを作成
        UpdateUserRequest requestData = new UpdateUserRequest();
        requestData.Name = name;
        requestData.Stage = stage;
        //サーバーに送信するオブジェクトをJSONに変換
        string json = JsonConvert.SerializeObject(requestData);
        Debug.Log("送信するJSONデータ : "+json + "<-" + requestData.Name + "," + requestData.Stage);
        Debug.Log("APIToken : " + apiToken);
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
            stageData = stage;
            SaveUserData();
            isSuccess = true;
        }

        result?.Invoke(isSuccess); //ここで呼び出し元のresult処理を呼び出す
    }
    // タイル情報を読み込む
    public (int[], int[], int[]) GetTileData(int stage)
    {

        //送信
        UnityWebRequest request = UnityWebRequest.Post(
                    API_BASE_URL + "stages/get/" + stage, "", "application/json");

        //yield return request.SendWebRequest();

        TileLoadResponse response;

        if (request.result == UnityWebRequest.Result.Success
         && request.responseCode == 200)
        {
            //通信が成功した場合、返ってきたJSONをオブジェクトに変換
            string resultJson = request.downloadHandler.text;

            response =
                JsonConvert.DeserializeObject<TileLoadResponse>(resultJson);
            Debug.Log("レスポンス : "+response.TileX);
            this.tileX = response.TileX;
            this.tileY = response.TileY;
            this.tileType = response.TileType;
        }
        return (tileX, tileY, tileType);
    }
}