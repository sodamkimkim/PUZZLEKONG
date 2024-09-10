using UnityEngine;
using System.Collections;
using UnityEngine.UI;  // UI를 사용하는 경우

public class GIFLoader : MonoBehaviour
{
    public string gifFileName;  // 재생할 GIF 파일의 이름

    void Start()
    {
        // GIF 파일을 로드하고 재생하는 메소드 호출
        StartCoroutine(LoadAndPlayGIF());
    }

    IEnumerator LoadAndPlayGIF()
    {
        // Resources 폴더에서 GIF 파일을 로드
        var gifSprite = Resources.Load<Sprite>($"Sprites/{gifFileName}");
        if (gifSprite)
            Debug.Log(gifSprite);
        // GIF가 존재하면 이를 적용
        if (gifSprite != null)
        {
            
            // UI 이미지에 GIF 적용하는 경우
            GetComponent<Image>().sprite = gifSprite;
        }

        yield return null;
    }
}