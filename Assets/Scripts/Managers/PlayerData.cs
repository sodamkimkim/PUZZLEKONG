using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Data
{
    public int MyBestScore;
    public int PlayerTotalScore;
    public int NowScore;

    public static Dictionary<string, int> ItemDic;
    public static Dictionary<string, string> ItemPosDic;
    public Data(int myBest, int playerTotalScore, int nowScore)
    {
        MyBestScore = myBest;
        PlayerTotalScore = playerTotalScore;
        NowScore = nowScore;


        ItemDic = new Dictionary<string, int>();
        ItemPosDic = new Dictionary<string, string>();
        
    }

    public override string ToString()
    {
        return $"MyBest: {MyBestScore}, PlayerTotalScore: {PlayerTotalScore}, NowScore: {NowScore}";
    }
}
public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance = null;
    public static Data Data = null;
    public static string filePath = string.Empty;
    public static readonly string encryptionKey = "BBUNIKONG_PUZZLEKONG_0512";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        Data = new Data(0, 0, 0);
        //  Btn_save.onClick.AddListener(SaveData);
        filePath = Application.persistentDataPath + "/gamedata.dat";
        LoadData();


    }
    private void Start()
    {
        // test data
        PlayerPrefs.DeleteAll();
        SetData();
    }
    private void SetData()
    {
        Util.AddOrChangeDictinaryValue(Data.ItemDic, "Item_a_Mushroom", 9999);
        Util.AddOrChangeDictinaryValue(Data.ItemDic, "Item_b_Wandoo", 9999);
        Util.AddOrChangeDictinaryValue(Data.ItemDic, "Item_c_Reset", 9999);
        Util.AddOrChangeDictinaryValue(Data.ItemDic, "Item_d_SwitchRows", 9999);
        Util.AddOrChangeDictinaryValue(Data.ItemDic, "Item_e_SwitchColumns", 9999);
        Util.AddOrChangeDictinaryValue(Data.ItemDic, "Item_f_Bumb", 9999);
        Util.AddOrChangeDictinaryValue(Data.ItemDic, "Item_g_Eraser", 9999);
        Util.AddOrChangeDictinaryValue(Data.ItemDic, "Item_h_PushLeft", 9999);
        Util.AddOrChangeDictinaryValue(Data.ItemDic, "Item_i_PushUp", 9999);

        Util.AddOrChangeDictinaryValue(Data.ItemPosDic, "ItemSlot0", "Item_c_Reset");
        Util.AddOrChangeDictinaryValue(Data.ItemPosDic, "ItemSlot1", "Item_a_Mushroom");
        Util.AddOrChangeDictinaryValue(Data.ItemPosDic, "ItemSlot2", "Item_f_Bumb");
        Util.AddOrChangeDictinaryValue(Data.ItemPosDic, "ItemSlot3", "Item_g_Eraser");
        Util.AddOrChangeDictinaryValue(Data.ItemPosDic, "ItemSlot4", "Item_h_PushLeft");
        Util.AddOrChangeDictinaryValue(Data.ItemPosDic, "ItemSlot5", "Item_i_PushUp");
        Util.AddOrChangeDictinaryValue(Data.ItemPosDic, "ItemSlot6", "Item_d_SwitchRows");
        Util.AddOrChangeDictinaryValue(Data.ItemPosDic, "ItemSlot7", "Item_e_SwitchColumns");
        SaveData();

        //PlayerPrefs.SetInt("Item_a_Mushroom", 9999);
        //PlayerPrefs.SetInt("Item_b_Wandoo", 9999);
        //PlayerPrefs.SetInt("Item_c_Reset", 9999);
        //PlayerPrefs.SetInt("Item_d_SwitchRows", 9999);
        //PlayerPrefs.SetInt("Item_e_SwitchColumns", 9999);
        //PlayerPrefs.SetInt("Item_f_Bumb", 9999);
        //PlayerPrefs.SetInt("Item_g_Eraser", 9999);
        //PlayerPrefs.SetInt("Item_h_PushLeft", 9999);
        //PlayerPrefs.SetInt("Item_i_PushUp", 9999);
        //PlayerPrefs.Save();

        //// Player가 Slot에 Item 지정
        //PlayerPrefs.SetString("ItemSlot0", "Item_c_Reset");
        //PlayerPrefs.SetString("ItemSlot1", "Item_a_Mushroom");
        ////PlayerPrefs.SetString("ItemSlot3", "Item_d_SwitchRows");
        ////PlayerPrefs.SetString("ItemSlot4", "Item_e_SwitchColumns");
        //PlayerPrefs.SetString("ItemSlot2", "Item_f_Bumb");
        //PlayerPrefs.SetString("ItemSlot3", "Item_g_Eraser");
        //PlayerPrefs.SetString("ItemSlot4", "Item_h_PushLeft");
        //PlayerPrefs.SetString("ItemSlot5", "Item_i_PushUp");
        //PlayerPrefs.SetString("ItemSlot6", "Item_d_SwitchRows");
        //PlayerPrefs.SetString("ItemSlot7", "Item_e_SwitchColumns");
        //PlayerPrefs.Save();
    }
    // 데이터 저장
    // 데이터 불러오기
    public void LoadData()
    {
        if (File.Exists(filePath))
        {
            string encryptedData = File.ReadAllText(filePath);
            string jsonData = Decrypt(encryptedData, encryptionKey);

            Data = JsonUtility.FromJson<Data>(jsonData);
            Data.NowScore = 0;
        }
        else
        {
            SaveData();
            LoadData();
        }
    }
    public static void SaveData()
    {
        string jsonData = JsonUtility.ToJson(Data, true);
        string encryptedData = Encrypt(jsonData, encryptionKey);

        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close(); // 파일 생성 후 닫기
        }

        File.WriteAllText(filePath, encryptedData);
    }

    private static string Encrypt(string plainText, string key)
    {
        byte[] keyBytes = GetAesKey(key);
        byte[] iv = new byte[16]; // 16바이트 IV 사용 (0으로 채워진 초기화 벡터)

        using (Aes aes = Aes.Create())
        {
            aes.Key = keyBytes;
            aes.IV = iv;
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    }

    private string Decrypt(string cipherText, string key)
    {
        byte[] keyBytes = GetAesKey(key);
        byte[] iv = new byte[16]; // 16바이트 IV 사용 (0으로 채워진 초기화 벡터)

        using (Aes aes = Aes.Create())
        {
            aes.Key = keyBytes;
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText)))
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
    }

    private static byte[] GetAesKey(string key)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);

        // 키 크기를 16, 24 또는 32바이트로 조정
        if (keyBytes.Length < 16)
            Array.Resize(ref keyBytes, 16); // 16바이트로 패딩
        else if (keyBytes.Length > 16 && keyBytes.Length < 24)
            Array.Resize(ref keyBytes, 24); // 24바이트로 패딩
        else if (keyBytes.Length > 24 && keyBytes.Length < 32)
            Array.Resize(ref keyBytes, 32); // 32바이트로 패딩
        return keyBytes;
    }
} // end of class