using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameData
{
    public int MyBestScore;
    public int PlayerTotalScore;
    public int NowScore;

    public GameData(int myBest, int playerTotalScore, int nowScore)
    {
        MyBestScore = myBest;
        PlayerTotalScore = playerTotalScore;
        NowScore = nowScore;
    }

    public override string ToString()
    {
        return $"MyBest: {MyBestScore}, PlayerTotalScore: {PlayerTotalScore}, NowScore: {NowScore}";
    }
}
public class PlayerDataManager : MonoBehaviour
{
    #region Dependency Injection
    [SerializeField]
    private EffectManager _effectManager = null;
    #endregion

    #region UI
    [SerializeField]
    private Button Btn_save = null;
    #endregion

    public static GameData GameData = new GameData(0, 0, 0);
    private string filePath = string.Empty;
    private readonly string encryptionKey = "BBUNIKONG_PUZZLEKONG_0512";

    private void Awake()
    {
        Btn_save.onClick.AddListener(SaveData);
        filePath = Application.persistentDataPath + "/gamedata.dat";
        LoadData();
    }

    public void UpdateData(UIManager uiManager)
    {
        GameData.PlayerTotalScore += GameData.NowScore;
        if(GameData.MyBestScore<GameData.NowScore)
        {
            Debug.Log("BestScore 갱신");
            uiManager.SetTMPText(uiManager.UITMP_TempText_Large, "YOUR\nBEST SCORE!", Color.white, false);
            Instantiate(_effectManager.EffectPrefab_Celebration_Finish, Factor.EffectPos_Celebration, Quaternion.identity);
            GameData.MyBestScore = GameData.NowScore;
        }
        uiManager.GameOver($"Score: {GameData.NowScore}\nTotal: {GameData.PlayerTotalScore}\nBest: {GameData.MyBestScore}");

        GameData.NowScore = 0;
    }
    // 데이터 저장
    public void SaveData()
    {
        string jsonData = JsonUtility.ToJson(GameData, true);
        string encryptedData = Encrypt(jsonData, encryptionKey);

        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close(); // 파일 생성 후 닫기
        }

        File.WriteAllText(filePath, encryptedData);
    }

    // 데이터 불러오기
    public void LoadData()
    {
        if (File.Exists(filePath))
        {
            string encryptedData = File.ReadAllText(filePath);
            string jsonData = Decrypt(encryptedData, encryptionKey);

            GameData = JsonUtility.FromJson<GameData>(jsonData);
            GameData.NowScore = 0;
        }
        else
        {
            SaveData();
            LoadData();
        }
    }
    private string Encrypt(string plainText, string key)
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

    private byte[] GetAesKey(string key)
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