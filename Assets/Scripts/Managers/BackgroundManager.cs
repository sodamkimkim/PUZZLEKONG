using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    private GameObject[] BGArr = new GameObject[2];
    public GameObject BGgo { get; private set; }
    private void Start()
    {
        LazyStart();
    }
    private void LazyStart()
    {
        Path path = new Path();
        BGArr[0] = Resources.Load<GameObject>($"{path.Backgrounds}BG_Default");
        BGArr[1] = Resources.Load<GameObject>($"{path.Backgrounds}BG_MintChoco");
        BGgo = GetBGGo(ThemeManager.ETheme);
        foreach(GameObject go in BGArr)
        {
        Debug.Log(go.name); 
        }
    }
    public GameObject GetBGGo(Enum.eTheme eTheme)
    {
        GameObject bgGo = null;
        Factor factor = new Factor();
        switch (eTheme)
        { 
            case Enum.eTheme.Grey:
                bgGo = Instantiate(BGArr[0], Vector3.zero, Quaternion.identity);
                bgGo.GetComponent<SpriteRenderer>().color = factor.Grey1;
                break;
            case Enum.eTheme.Green:
                bgGo = Instantiate(BGArr[0], Vector3.zero, Quaternion.identity);
                bgGo.GetComponent<SpriteRenderer>().color = factor.Green1;
                break;
            case Enum.eTheme.LightPurple:
                bgGo = Instantiate(BGArr[0], Vector3.zero, Quaternion.identity);
                bgGo.GetComponent<SpriteRenderer>().color = factor.LightPurple1;
                break;
            case Enum.eTheme.LightBlue:
                bgGo = Instantiate(BGArr[0], Vector3.zero, Quaternion.identity);
                bgGo.GetComponent<SpriteRenderer>().color = factor.LightBlue1;
                break;
            case Enum.eTheme.Pink:
                bgGo = Instantiate(BGArr[0], Vector3.zero, Quaternion.identity);
                bgGo.GetComponent<SpriteRenderer>().color = factor.Pink1;
                break;
            case Enum.eTheme.Mint:
                bgGo = Instantiate(BGArr[1], Vector3.zero, Quaternion.identity);
                bgGo.GetComponent<SpriteRenderer>().color = factor.BGColorDefault;
                break;
        }
        bgGo.transform.position = factor.PosBG;
        return bgGo;
    } 
} // end of class