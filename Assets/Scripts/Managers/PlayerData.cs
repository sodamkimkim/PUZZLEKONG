using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    //  public static int NowScore = 0;
    public static readonly string EncryptionKey = "BBUNIKONG_PUZZLEKONG_0512";
    #region Properties
    public static int NowScore { get => GetInt(Str.NowScore); set => SetString(Str.NowScore, value.ToString()); }
    public static int PlayerTotalScore { get => GetInt(Str.PlayerTotalScore); set => SetString(Str.PlayerTotalScore, value.ToString()); }
    public static int MyBestScore { get => GetInt(Str.MyBestScore); set => SetString(Str.MyBestScore, value.ToString()); }

    public static int Item_a_Mushroom { get => GetInt(Str.Item_a_Mushroom); set => SetString(Str.Item_a_Mushroom, value.ToString()); }
    public static int Item_b_Wandoo { get => GetInt(Str.Item_b_Wandoo); set => SetString(Str.Item_b_Wandoo, value.ToString()); }
    public static int Item_c_Reset { get => GetInt(Str.Item_c_Reset); set => SetString(Str.Item_c_Reset, value.ToString()); }
    public static int Item_d_SwitchRows { get => GetInt(Str.Item_d_SwitchRows); set => SetString(Str.Item_d_SwitchRows, value.ToString()); }
    public static int Item_e_SwitchColumns { get => GetInt(Str.Item_e_SwitchColumns); set => SetString(Str.Item_e_SwitchColumns, value.ToString()); }
    public static int Item_f_Bumb { get => GetInt(Str.Item_f_Bumb); set => SetString(Str.Item_f_Bumb, value.ToString()); }
    public static int Item_g_Eraser { get => GetInt(Str.Item_g_Eraser); set => SetString(Str.Item_g_Eraser, value.ToString()); }
    public static int Item_h_PushLeft { get => GetInt(Str.Item_h_PushLeft); set => SetString(Str.Item_h_PushLeft, value.ToString()); }
    public static int Item_i_PushUp { get => GetInt(Str.Item_i_PushUp); set => SetString(Str.Item_i_PushUp, value.ToString()); }

    public static string ItemSlot0 { get => GetString(Str.ItemSlot0); set => SetString(Str.ItemSlot0, value); }
    public static string ItemSlot1 { get => GetString(Str.ItemSlot1); set => SetString(Str.ItemSlot1, value); }
    public static string ItemSlot2 { get => GetString(Str.ItemSlot2); set => SetString(Str.ItemSlot2, value); }
    public static string ItemSlot3 { get => GetString(Str.ItemSlot3); set => SetString(Str.ItemSlot3, value); }
    public static string ItemSlot4 { get => GetString(Str.ItemSlot4); set => SetString(Str.ItemSlot4, value); }
    public static string ItemSlot5 { get => GetString(Str.ItemSlot5); set => SetString(Str.ItemSlot5, value); }
    public static string ItemSlot6 { get => GetString(Str.ItemSlot6); set => SetString(Str.ItemSlot6, value); }
    public static string ItemSlot7 { get => GetString(Str.ItemSlot7); set => SetString(Str.ItemSlot7, value); }
    #endregion
    public static string ToString_Score()
    {
        return $"{Str.MyBestScore} : {MyBestScore} | {Str.PlayerTotalScore}: {PlayerTotalScore} | {Str.NowScore}: {NowScore}";
    }

    public static new string ToString()
    {
        return $"{Str.NowScore} : {NowScore}\n" +
            $"{Str.MyBestScore} : {MyBestScore}\n" +
            $"{Str.PlayerTotalScore} : {PlayerTotalScore}\n\n" +

            $"{Str.Item_a_Mushroom} : {Item_a_Mushroom}\n" +
            $"{Str.Item_b_Wandoo} : {Item_b_Wandoo}\n" +
            $"{Str.Item_c_Reset} : {Item_c_Reset}\n" +
            $"{Str.Item_d_SwitchRows} : {Item_d_SwitchRows}\n" +
            $"{Str.Item_e_SwitchColumns} : {Item_e_SwitchColumns}\n" +
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
            $"{Str.ItemSlot6} : {ItemSlot6}\n" +
            $"{Str.ItemSlot7} : {ItemSlot7}";
    }

    private void Awake()
    {
        NowScore = 0;
    }
    private void Start()
    {
        //// test data
       //  PlayerPrefs.DeleteAll();
       //  SetTestData_Ecrypt(); 
    }
    public static void Save()
    {
        PlayerPrefs.Save();
    }
    private void SetTestData_Ecrypt()
    {
        NowScore = 0;
        MyBestScore = 0;
        PlayerTotalScore = 0;

        Item_a_Mushroom = 99;
        Item_b_Wandoo = 99;
        Item_c_Reset = 99;
        Item_d_SwitchRows = 99;
        Item_e_SwitchColumns = 99;
        Item_f_Bumb = 99;
        Item_g_Eraser = 99;
        Item_h_PushLeft = 99;
        Item_i_PushUp = 99;

        // Player가 Slot에 Item 지정
        ItemSlot0 = Str.Item_c_Reset;
        ItemSlot1 = Str.Item_a_Mushroom;
        ItemSlot2 = Str.Item_f_Bumb;
        ItemSlot3 = Str.Item_g_Eraser;
        ItemSlot4 = Str.Item_h_PushLeft;
        ItemSlot5 = Str.Item_i_PushUp;
        ItemSlot6 = Str.Item_d_SwitchRows;
        ItemSlot7 = Str.Item_e_SwitchColumns;
    }
    public static void SetString(string key, string value)
    {
        PlayerPrefs.SetString(key, Encrypt(value, EncryptionKey));
        PlayerPrefs.Save();
    }
    public static string GetString(string key)
    {
        return Decrypt(PlayerPrefs.GetString(key), EncryptionKey);
    }
    public static int GetInt(string key)
    {
        int newInt = 0;
        int.TryParse(PlayerData.GetString(key), out newInt);
        return newInt;
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