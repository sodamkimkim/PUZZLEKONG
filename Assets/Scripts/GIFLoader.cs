using UnityEngine;
using System.Collections;
using UnityEngine.UI;  // UI�� ����ϴ� ���

public class GIFLoader : MonoBehaviour
{
    public string gifFileName;  // ����� GIF ������ �̸�

    void Start()
    {
        // GIF ������ �ε��ϰ� ����ϴ� �޼ҵ� ȣ��
        StartCoroutine(LoadAndPlayGIF());
    }

    IEnumerator LoadAndPlayGIF()
    {
        // Resources �������� GIF ������ �ε�
        var gifSprite = Resources.Load<Sprite>($"Sprites/{gifFileName}");
        if (gifSprite)
            Debug.Log(gifSprite);
        // GIF�� �����ϸ� �̸� ����
        if (gifSprite != null)
        {
            
            // UI �̹����� GIF �����ϴ� ���
            GetComponent<Image>().sprite = gifSprite;
        }

        yield return null;
    }
}