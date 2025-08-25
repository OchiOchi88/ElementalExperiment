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
    static public string nameData;
    static public int stageData;
    static public int achieveData;

    //  �v���p�e�B
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
    // �ʐM�p�̊֐�

    //���[�U�[�o�^����
    public IEnumerator RegistUser(string name, int stage, int achieve, Action<bool> result)
    {
        //�T�[�o�[�ɑ��M����I�u�W�F�N�g���쐬
        RegistUserRepuest requestData = new RegistUserRepuest();
        requestData.name = name;
        requestData.stage = stage;
        requestData.achievement = achieve;
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

            Debug.Log("id: " + response.Id);
            Debug.Log("APIToken: " + response.APIToken);


            if (response != null)
            {
                this.userName = response.Id;
                this.apiToken = response.APIToken;
                Debug.Log("�ϊ����APIToken: " + response.APIToken);
                SaveUserData();
                isSuccess = true;
            }
            else
            {
                Debug.Log("�f�V���A���C�Y���s");
            }


            ////�t�@�C���Ƀ��[�U�[ID��ۑ�
            //this.userName = name;
            //nameData = userName;
            //this.apiToken = response.APIToken;
            //Debug.Log("�擾����APIToken : " + response.APIToken);
            //SaveUserData();
            //isSuccess = true;
        }
        result?.Invoke(isSuccess); //�����ŌĂяo������result�������Ăяo��
    }

    //  ���[�U�[����ۑ�����
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
        this.userName = saveData.UserName;
        nameData = userName;
        //stage = saveData.Stage;
        Debug.Log("APIToken : " + APIToken);
        return true;
    }

    // ���[�U�[�̃��x��(�N���A�X�e�[�W��)���ڍׂɓǂݍ���
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
    // ���[�U�[�̃��x��(�N���A�X�e�[�W��)���ڍׂɓǂݍ���
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

    //���[�U�[���X�V
    public IEnumerator UpdateUser(string name, int stage,int achieve, Action<bool> result)
    {
        //�T�[�o�[�ɑ��M����I�u�W�F�N�g���쐬
        UpdateUserRequest requestData = new UpdateUserRequest();
        requestData.name = name;
        requestData.stage = stage;
        requestData.achievement = achieve;
        //�T�[�o�[�ɑ��M����I�u�W�F�N�g��JSON�ɕϊ�
        string json = JsonConvert.SerializeObject(requestData);
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
            SaveUserData();
            isSuccess = true;
        }

        result?.Invoke(isSuccess); //�����ŌĂяo������result�������Ăяo��
    }
}