using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    //  public static int NowScore = 0;
    public static readonly string EncryptionKey = "BBUNIKONG_PUZZLEKONG_0512";
    #region Properties
    public static int Level { get => GetInt(Str.Level); 
        set  {
            if (value > 10000)
                value = 10000;

            SetStr(Str.Level, value.ToString()); } }
    public static int Kong { get => GetInt(Str.Kong); set => SetStr(Str.Kong, value.ToString()); }
    public static int NowScore { get => GetInt(Str.NowScore); set => SetStr(Str.NowScore, value.ToString()); }
    public static int PlayerTotalScore
    {
        get
        {
            if (StageManager.Stage.Equals(Str.eStage.Item)) return GetInt(Str.PlayerTotalScore_Item);
            else return GetInt(Str.PlayerTotalScore_Classic);
        }
        set
        {
            int val = value;
            if (val >= Factor.LevelUpPlayerTotalScore)
            {
                while (val >= Factor.LevelUpPlayerTotalScore)
                {
                    Level++;
                    val -= Factor.LevelUpPlayerTotalScore;
                }
            }

            if (StageManager.Stage.Equals(Str.eStage.Item)) SetStr(Str.PlayerTotalScore_Item, val.ToString());
            else SetStr(Str.PlayerTotalScore_Classic, val.ToString());
        }
    }
    public static int MyBestScore
    {
        get
        {
            if (StageManager.Stage.Equals(Str.eStage.Item)) return GetInt(Str.MyBestScore_Item);
            else return GetInt(Str.MyBestScore_Classic);
        }
        set
        {
            if (StageManager.Stage.Equals(Str.eStage.Item)) SetStr(Str.MyBestScore_Item, value.ToString());
            else SetStr(Str.MyBestScore_Classic, value.ToString());
        }
    }
    public static int Item_a_Mushroom { get => GetInt(Str.Item_a_Mushroom); set => SetStr(Str.Item_a_Mushroom, value.ToString()); }
    public static int Item_b_Wandoo { get => GetInt(Str.Item_b_Wandoo); set => SetStr(Str.Item_b_Wandoo, value.ToString()); }
    public static int Item_c_Reset { get => GetInt(Str.Item_c_Reset); set => SetStr(Str.Item_c_Reset, value.ToString()); }
    public static int Item_d_SwitchRows { get => GetInt(Str.Item_d_SwitchRows); set => SetStr(Str.Item_d_SwitchRows, value.ToString()); }
    public static int Item_e_SwitchColumns { get => GetInt(Str.Item_e_SwitchColumns); set => SetStr(Str.Item_e_SwitchColumns, value.ToString()); }
    public static int Item_f_Bumb { get => GetInt(Str.Item_f_Bumb); set => SetStr(Str.Item_f_Bumb, value.ToString()); }
    public static int Item_g_Eraser { get => GetInt(Str.Item_g_Eraser); set => SetStr(Str.Item_g_Eraser, value.ToString()); }
    public static int Item_h_PushLeft { get => GetInt(Str.Item_h_PushLeft); set => SetStr(Str.Item_h_PushLeft, value.ToString()); }
    public static int Item_i_PushUp { get => GetInt(Str.Item_i_PushUp); set => SetStr(Str.Item_i_PushUp, value.ToString()); }

    public static string ItemSlot0 { get => GetStr(Str.ItemSlot0); set => SetStr(Str.ItemSlot0, value); }
    public static string ItemSlot1 { get => GetStr(Str.ItemSlot1); set => SetStr(Str.ItemSlot1, value); }
    public static string ItemSlot2 { get => GetStr(Str.ItemSlot2); set => SetStr(Str.ItemSlot2, value); }
    public static string ItemSlot3 { get => GetStr(Str.ItemSlot3); set => SetStr(Str.ItemSlot3, value); }
    public static string ItemSlot4 { get => GetStr(Str.ItemSlot4); set => SetStr(Str.ItemSlot4, value); }
    public static string ItemSlot5 { get => GetStr(Str.ItemSlot5); set => SetStr(Str.ItemSlot5, value); }
    public static string ItemSlot6 { get => GetStr(Str.ItemSlot6); set => SetStr(Str.ItemSlot6, value); }
    #endregion
    public static string ToString_Score()
    {
        return $"{Str.Level}: {Level} | \n{Str.Kong}: {Kong}\n | {Str.NowScore}: {NowScore}\n" +
            $"{Str.MyBestScore_Item} : {GetStr(Str.MyBestScore_Item)} | {Str.PlayerTotalScore_Item}: {GetStr(Str.PlayerTotalScore_Item)}\n" +
            $"{Str.MyBestScore_Classic} : {GetStr(Str.MyBestScore_Classic)} | {Str.PlayerTotalScore_Classic}: {GetStr(Str.PlayerTotalScore_Classic)}";
    }

    public static string PlayerDataToString()
    {
        return $"{Str.Level} : {Level}\n" +
            $"{Str.Kong} : {Kong}\n" +
            $"{Str.NowScore} : {NowScore}\n" +
            $"{Str.MyBestScore_Item} : {GetStr(Str.MyBestScore_Item)}\n" +
            $"{Str.PlayerTotalScore_Item} : {GetStr(Str.PlayerTotalScore_Item)}\n" +

            $"{Str.MyBestScore_Classic} : {GetStr(Str.MyBestScore_Classic)}\n" +
            $"{Str.PlayerTotalScore_Classic} : {GetStr(Str.PlayerTotalScore_Classic)}\n\n" +

            $"{Str.Kong} : {Kong}\n" +
            $"{Str.Item_a_Mushroom} : {Item_a_Mushroom}\n" +
            $"{Str.Item_b_Wandoo} : {Item_b_Wandoo}\n" +
            $"{Str.Item_c_Reset} : {Item_c_Reset}\n" +
            $"{Str.Item_f_Bumb} : {Item_f_Bumb}\n" +
            $"{Str.Item_g_Eraser} : {Item_g_Eraser}\n" +
            $"{Str.Item_h_PushLeft} : {Item_h_PushLeft}\n\n" +
            $"{Str.Item_i_PushUp} : {Item_i_PushUp}\n\n" +

            $"{Str.ItemSlot0} : {ItemSlot0}\n" +
            $"{Str.ItemSlot1} : {ItemSlot1}\n" +
            $"{Str.ItemSlot2} : {ItemSlot2}\n" +
            $"{Str.ItemSlot3} : {ItemSlot3}\n" +
            $"{Str.ItemSlot4} : {ItemSlot4}\n" +
            $"{Str.ItemSlot5} : {ItemSlot5}\n" +
            $"{Str.ItemSlot6} : {ItemSlot6}";
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        NowScore = 0;

    }
    private void Start()
    {
        //// test data
        PlayerPrefs.DeleteAll();
        NowScore = 0;
        SetTestData_Ecrypt();
        Debug.Log(PlayerDataToString());
    }
    public static void Save()
    {
        PlayerPrefs.Save();
    }
    private void SetTestData_Ecrypt()
    {
        NowScore = 0;
        Level = 10000;
        Kong = 9999;
        // Item전 vs classic 데이터는 따로 저장
        SetStr(Str.MyBestScore_Item, 0.ToString());
        SetStr(Str.MyBestScore_Classic, 0.ToString());

        StageManager.Stage = Str.eStage.Item;
        PlayerTotalScore = 99999999;
        StageManager.Stage = Str.eStage.Classic;
        PlayerTotalScore = 99999999;
        //  SetStr(Str.PlayerTotalScore_Item, 99999999.ToString());
        //  SetStr(Str.PlayerTotalScore_Classic, 99999999.ToString());

        Item_a_Mushroom = 99;
        Item_b_Wandoo = 99;
        Item_c_Reset = 99;

        Item_f_Bumb = 99;
        Item_g_Eraser = 99;
        Item_h_PushLeft = 99;
        Item_i_PushUp = 99;

        // Player가 Slot에 Item 지정
        ItemSlot0 = Str.Item_a_Mushroom;
        ItemSlot1 = Str.Item_b_Wandoo;
        ItemSlot2 = Str.Item_c_Reset;
        ItemSlot3 = Str.Item_f_Bumb;
        ItemSlot4 = Str.Item_g_Eraser;
        ItemSlot5 = Str.Item_h_PushLeft;
        ItemSlot6 = Str.Item_i_PushUp;

    }
    public static void SetStr(string key, string value)
    {
        PlayerPrefs.SetString(key, Encrypt(value, EncryptionKey));
        PlayerPrefs.Save();
    }
    public static string GetStr(string key)
    {
        if (PlayerPrefs.HasKey(key))
            return Decrypt(PlayerPrefs.GetString(key), EncryptionKey);
        else return string.Empty;
    }
    public static int GetInt(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            int newInt = 0;
            int.TryParse(PlayerData.GetStr(key), out newInt);
            return newInt;
        }
        else return 0;
    }

    public static string Encrypt(string plainText, string key)
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

    public static string Decrypt(string cipherText, string key)
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