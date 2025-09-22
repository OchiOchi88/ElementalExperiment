using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RegistUserRepuest;
using UnityEngine.Networking;
using System.IO;
using Unity.VisualScripting.Antlr3.Runtime;
using System.Linq;


public class NetworkManager : MonoBehaviour
{
    //  WebAPIの接続先を設定
#if DEBUG
    //  開発環境で使用する値をセット
    const string API_BASE_URL = "http://localhost:8000/api/";
#else
    //  本番環境で使用する値をセット
    const string API_BASE_URL = "http://localhost:8000/api/";
    //const string API_BASE_URL = "http://ge202403.japaneast.cloudapp.azure.com/api/";
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
    private int count;

    //  プロパティ
    public string Token
    {
        get
        {
            return this.apiToken;
        }
    }
    public int Count
    {
        get
        {
            return this.count;
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
        requestData.name = name;
        requestData.stage = stage;
        //サーバーに送信するオブジェクトをJSONに変換
        string json = JsonConvert.SerializeObject(requestData);
        Debug.Log(json);
        //送信
        UnityWebRequest request = UnityWebRequest.Post(
                    API_BASE_URL + "users/store", json, "application/json");
        yield return request.SendWebRequest();
        bool isSuccess = false;
        Debug.Log(request.responseCode);
        if (request.result == UnityWebRequest.Result.Success
    && request.responseCode == 201)
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
        else
        {
            Debug.Log("500!!!");
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
        this.apiToken = saveData.Token;
        nameData = userName;
        //stage = saveData.Stage;
        Debug.Log("APIToken : " + Token);
        return true;
    }

    // ユーザー情報を読み込む
    public IEnumerator IndexUserData(Action<bool> result)
    {
        
        //送信
        UnityWebRequest request = UnityWebRequest.Post(
                    API_BASE_URL + "users/index", "", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + this.apiToken);;
        yield return request.SendWebRequest();
        bool isSuccess = false;
        if (request.result == UnityWebRequest.Result.Success
         && request.responseCode == 200)
        {
            isSuccess = true;
            //通信が成功した場合、返ってきたJSONをオブジェクトに変換
            string resultJson = request.downloadHandler.text;

            IndexUserResponse response =
                JsonConvert.DeserializeObject<IndexUserResponse>(resultJson);

            // すぐにアクセスできるようにゲーム内に情報を保持しておく
            nameData = response.name;
            stageData = response.stage;
            Debug.Log("Token設置完了");
            Debug.Log("response.stage:" + response.stage);
        }

        if (!File.Exists(Application.persistentDataPath + "/saveData.json"))
        {
            isSuccess = false;
        }
        else
        {
            var reader =
                       new StreamReader(Application.persistentDataPath + "/saveData.json");
            Debug.Log(Application.persistentDataPath + "/saveData.json");
            Debug.Log(request.responseCode);
            string json = reader.ReadToEnd();
            reader.Close();
            SaveData saveData = JsonConvert.DeserializeObject<SaveData>(json);
            this.userName = saveData.Name;
            nameData = userName;
            apiToken = saveData.Token;
        }
        result?.Invoke(isSuccess);
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
    //ユーザーのステージ情報更新
    public IEnumerator LevelUp(string name, int stage, Action<bool> result)
    {
        //サーバーに送信するオブジェクトを作成
        UpdateUserRequest requestData = new UpdateUserRequest();
        requestData.name = name;
        requestData.stage = stage;
        Debug.Log("APIToken : " + this.apiToken);
        //送信
        UnityWebRequest request = new UnityWebRequest(API_BASE_URL + "users/update", "POST");
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + this.apiToken);

        yield return request.SendWebRequest();

        bool isSuccess = false;
        Debug.Log("レスポンス:" + request.responseCode);
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
    //ユーザー情報更新
    public IEnumerator UpdateUser(string name, int stage, Action<bool> result)
    {
        //サーバーに送信するオブジェクトを作成
        UpdateUserRequest requestData = new UpdateUserRequest();
        requestData.name = name;
        requestData.stage = stage;
        //サーバーに送信するオブジェクトをJSONに変換
        string json = JsonConvert.SerializeObject(requestData);
        Debug.Log("送信するJSONデータ : "+json + "<-" + requestData.name + "," + requestData.stage);
        Debug.Log("APIToken : " + apiToken);
        //送信
        UnityWebRequest request = UnityWebRequest.Post(
                    API_BASE_URL + "users/update", json, "application/json");
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Authorization", "Bearer " + this.apiToken);

        yield return request.SendWebRequest();

        bool isSuccess = false;
        Debug.Log("レスポンス:" + request.responseCode);
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
    public IEnumerator GetTileData(int stage, Action<bool> result)
    {

        //送信
        UnityWebRequest tileRequest = UnityWebRequest.Get(
                    API_BASE_URL + "tiles/get/" + stage);
        UnityWebRequest elementRequest = UnityWebRequest.Get(
            API_BASE_URL + "elements/get/" + stage);
        UnityWebRequest paletteRequest = UnityWebRequest.Get(
            API_BASE_URL + "palettes/get/" + stage);

        yield return tileRequest.SendWebRequest();
        bool isSuccess = false;
        Debug.Log(tileRequest.responseCode);
        if (tileRequest.result == UnityWebRequest.Result.Success
         && tileRequest.responseCode == 200)
        {
            //通信が成功した場合、返ってきたJSONをオブジェクトに変換
            string tileResultJson = tileRequest.downloadHandler.text;
            Debug.Log(tileResultJson);
            List<TileLoadResponse> tileResponse =
                JsonConvert.DeserializeObject<List<TileLoadResponse>>(tileResultJson);
            //Debug.Log("レスポンス : "+response);
            // 必要な大きさで配列を確保
            this.tileX = new int[tileResponse.Count];
            this.tileY = new int[tileResponse.Count];
            this.tileType = new int[tileResponse.Count];
            Debug.Log(tileResponse.Count);
            GameCtrler.InitTile(tileResponse.Count);
            for (int i = 0; i < tileResponse.Count; i++)
            {
                Debug.Log("レスポンス："+ tileResponse[i].X);  //  0になっていた
                this.tileX[i] = tileResponse[i].X;
                this.tileY[i] = tileResponse[i].Y;
                this.tileType[i] = tileResponse[i].Type;
                Debug.Log("変数：" +this.TileX[i]);           //  0になっていた
                GameCtrler.GetTileData(tileX[i], tileY[i], tileType[i], i);
            }
        }
        yield return elementRequest.SendWebRequest();
        Debug.Log(elementRequest.responseCode);
        if (elementRequest.result == UnityWebRequest.Result.Success
         && elementRequest.responseCode == 200)
        {
            //通信が成功した場合、返ってきたJSONをオブジェクトに変換
            string elementResultJson = elementRequest.downloadHandler.text;
            Debug.Log(elementResultJson);
            List<ElementLoadResponse> elementResponse =
                JsonConvert.DeserializeObject<List<ElementLoadResponse>>(elementResultJson);
            //Debug.Log("レスポンス : "+response);
            // 必要な大きさで配列を確保
            this.eleX = new int[elementResponse.Count];
            this.eleY = new int[elementResponse.Count];
            this.eleType = new int[elementResponse.Count];
            Debug.Log(elementResponse.Count);
            int startCount = 0;
            int goalCount = 0;
            for (int i = 0; i < elementResponse.Count; i++)
            {
                if (elementResponse[i].Type == 0)
                {
                    goalCount++;
                }
                else
                {
                    startCount++;
                }
            }
            GameCtrler.InitElement(startCount,goalCount);
            for (int i = 0; i < elementResponse.Count; i++)
            {
                Debug.Log("レスポンス：" + elementResponse[i].X+" " + elementResponse[i].Y + " " + elementResponse[i].Type );
                this.eleX[i] = elementResponse[i].X;
                this.eleY[i] = elementResponse[i].Y;
                this.eleType[i] = elementResponse[i].Type;
                Debug.Log("変数：" + this.TileX[i]);           //  0になっていた
                GameCtrler.GetElementData(eleX[i], eleY[i], eleType[i]);
            }
        }
        yield return paletteRequest.SendWebRequest();
        Debug.Log(paletteRequest.responseCode);
        if (paletteRequest.result == UnityWebRequest.Result.Success
         && paletteRequest.responseCode == 200)
        {
            isSuccess = true;
            //通信が成功した場合、返ってきたJSONをオブジェクトに変換
            string paletteResultJson = paletteRequest.downloadHandler.text;
            Debug.Log(paletteResultJson);
            List<PaletteLoadResponse> paletteResponse =
                JsonConvert.DeserializeObject<List<PaletteLoadResponse>>(paletteResultJson);
            //Debug.Log("レスポンス : "+response);
            // 必要な大きさで配列を確保
            this.paletteType = new int[paletteResponse.Count];
            Debug.Log(paletteResponse.Count);
            GameCtrler.InitPalette(paletteResponse.Count);
            for (int i = 0; i < paletteResponse.Count; i++)
            {
                this.paletteType[i] = paletteResponse[i].Type;
                Debug.Log("変数：" + this.TileX[i]);           //  0になっていた
                GameCtrler.GetPaletteData(paletteType[i]);
            }
        }
        result?.Invoke(isSuccess);
    }
    public (int[], int[], int[]) SendTileData()
    {
        return (tileX, tileY, tileType);
    }
    // 総ステージ数を読み込む
    public IEnumerator LoadStageCount(Action<bool> result)
    {
        //送信
        UnityWebRequest Request = UnityWebRequest.Get(
                    API_BASE_URL + "stages/count");
        yield return Request.SendWebRequest();
        bool isSuccess = false;
        Debug.Log(Request.responseCode);
        if (Request.result == UnityWebRequest.Result.Success
         && Request.responseCode == 200)
        {
            isSuccess = true;
            //通信が成功した場合、返ってきたJSONをオブジェクトに変換
            string ResultJson = Request.downloadHandler.text;
            Debug.Log(ResultJson);
            //StageCountResponse Response =
              //  JsonConvert.DeserializeObject<StageCountResponse>(ResultJson);
            //Debug.Log("レスポンス : "+response);
            //this.count = int.Parse(Response);
            count = int.Parse(ResultJson);
            GameCtrler.GetStageCount(count);
        }
        result?.Invoke(isSuccess);
    }
}