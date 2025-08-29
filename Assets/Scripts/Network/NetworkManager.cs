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
    //  WebAPI�̐ڑ����ݒ�
#if DEBUG
    //  �J�����Ŏg�p����l���Z�b�g
    const string API_BASE_URL = "http://localhost:8000/api/";
#else
    //  �{�Ԋ��Ŏg�p����l���Z�b�g
    const string API_BASE_URL = "http://azure.com/api/";
#endif

    //private int userID; //  �����̃��[�U�[ID
    private string userName;    //  ���͂����z��̎����̃��[�U�[��
    private int stage;    //  �����̃X�e�[�W�f�[�^
    private string apiToken;    //  API�g�[�N��
    private int[] tileX;
    private int[] tileY;
    private int[] tileType;
    private int[] eleX;
    private int[] eleY;
    private int[] eleType;
    private int[] paletteType;
    static public string nameData;
    static public int stageData;

    //  �v���p�e�B
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
    // �ʐM�p�̊֐�

    //���[�U�[�o�^����
    public IEnumerator RegistUser(string name, int stage, Action<bool> result)
    {
        //�T�[�o�[�ɑ��M����I�u�W�F�N�g���쐬
        RegistUserRepuest requestData = new RegistUserRepuest();
        requestData.Name = name;
        requestData.Stage = stage;
        //�T�[�o�[�ɑ��M����I�u�W�F�N�g��JSON�ɕϊ�
        string json = JsonConvert.SerializeObject(requestData);
        Debug.Log(json);
        //���M
        UnityWebRequest request = UnityWebRequest.Post(
                    API_BASE_URL + "users/store", json, "application/json");

        yield return request.SendWebRequest();
        bool isSuccess = false;
        if (request.result == UnityWebRequest.Result.Success
    && request.responseCode == 200)
        {
            //�ʐM�����������ꍇ�A�Ԃ��Ă���JSON���I�u�W�F�N�g�ɕϊ�
            string resultJson = request.downloadHandler.text;
            Debug.Log("�T�[�o�[����̃��X�|���X: " + resultJson);

            RegistUserResponse response =
                JsonConvert.DeserializeObject<RegistUserResponse>(resultJson);

            Debug.Log("id: " + response.Name);
            Debug.Log("APIToken: " + response.Token);


            if (response != null)
            {
                this.userName = response.Name;
                this.apiToken = response.Token;
                Debug.Log("�ϊ����APIToken: " + response.Token);
                SaveUserData();
                isSuccess = true;
            }
            else
            {
                Debug.Log("�f�V���A���C�Y���s");
            }
        }
        result?.Invoke(isSuccess); //�����ŌĂяo������result�������Ăяo��
    }

    //  ���[�U�[����ۑ�����
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

    // ���[�U�[����ǂݍ���
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

    // ���[�U�[����ǂݍ���
    public (string, int) IndexUserData()
    {
        
        //���M
        UnityWebRequest request = UnityWebRequest.Post(
                    API_BASE_URL + "users/index", "", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + this.apiToken);

        //yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success
         && request.responseCode == 200)
        {
            //�ʐM�����������ꍇ�A�Ԃ��Ă���JSON���I�u�W�F�N�g�ɕϊ�
            string resultJson = request.downloadHandler.text;

            IndexUserResponse response =
                JsonConvert.DeserializeObject<IndexUserResponse>(resultJson);

            // �����ɃA�N�Z�X�ł���悤�ɃQ�[�����ɏ���ێ����Ă���
            nameData = response.Name;
            stageData = response.Stage;
            Debug.Log("Token�ݒu����");
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

    // ���[�U�[�̃��x��(�N���A�X�e�[�W��)���ڍׂɓǂݍ���
    static public int LoadUserLvl()
    {
        return stageData;
    }
    // ���[�U�[�̃��x��(�N���A�X�e�[�W��)���ڍׂɓǂݍ���
    static public int LoadUserAchievement()
    {
        return 0;
    }
    static public string LoadUserName()
    {
        return nameData;
    }

    //���[�U�[���X�V
    public IEnumerator UpdateUser(string name, int stage, Action<bool> result)
    {
        //�T�[�o�[�ɑ��M����I�u�W�F�N�g���쐬
        UpdateUserRequest requestData = new UpdateUserRequest();
        requestData.Name = name;
        requestData.Stage = stage;
        //�T�[�o�[�ɑ��M����I�u�W�F�N�g��JSON�ɕϊ�
        string json = JsonConvert.SerializeObject(requestData);
        Debug.Log("���M����JSON�f�[�^ : "+json + "<-" + requestData.Name + "," + requestData.Stage);
        Debug.Log("APIToken : " + apiToken);
        //���M
        UnityWebRequest request = UnityWebRequest.Post(
                    API_BASE_URL + "users/update", json, "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + this.apiToken);

        yield return request.SendWebRequest();

        bool isSuccess = false;
        if (request.result == UnityWebRequest.Result.Success
         && request.responseCode == 200)
        {
            // �ʐM�����������ꍇ�A�t�@�C���ɍX�V�������[�U�[����ۑ�
            this.userName = name;
            nameData = userName;
            stageData = stage;
            SaveUserData();
            isSuccess = true;
        }

        result?.Invoke(isSuccess); //�����ŌĂяo������result�������Ăяo��
    }
    // �^�C������ǂݍ���
    public (int[], int[], int[]) GetTileData(int stage)
    {

        //���M
        UnityWebRequest request = UnityWebRequest.Post(
                    API_BASE_URL + "stages/get/" + stage, "", "application/json");

        //yield return request.SendWebRequest();

        TileLoadResponse response;

        if (request.result == UnityWebRequest.Result.Success
         && request.responseCode == 200)
        {
            //�ʐM�����������ꍇ�A�Ԃ��Ă���JSON���I�u�W�F�N�g�ɕϊ�
            string resultJson = request.downloadHandler.text;

            response =
                JsonConvert.DeserializeObject<TileLoadResponse>(resultJson);
            Debug.Log("���X�|���X : "+response.TileX);
            this.tileX = response.TileX;
            this.tileY = response.TileY;
            this.tileType = response.TileType;
        }
        return (tileX, tileY, tileType);
    }
}