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
        switch (eTheme)
        { 
            case Enum.eTheme.Grey:
                bgGo = Instantiate(BGArr[0], Vector3.zero, Quaternion.identity);
                bgGo.GetComponent<SpriteRenderer>().color = new Factor().Grey1;
                break;
            case Enum.eTheme.Green:
                bgGo = Instantiate(BGArr[0], Vector3.zero, Quaternion.identity);
                bgGo.GetComponent<SpriteRenderer>().color = new Factor().Green1;
                break;
            case Enum.eTheme.LightPurple:
                bgGo = Instantiate(BGArr[0], Vector3.zero, Quaternion.identity);
                bgGo.GetComponent<SpriteRenderer>().color = new Factor().LightPurple1;
                break;
            case Enum.eTheme.LightBlue:
                bgGo = Instantiate(BGArr[0], Vector3.zero, Quaternion.identity);
                bgGo.GetComponent<SpriteRenderer>().color = new Factor().LightBlue1;
                break;
            case Enum.eTheme.Pink:
                bgGo = Instantiate(BGArr[0], Vector3.zero, Quaternion.identity);
                bgGo.GetComponent<SpriteRenderer>().color = new Factor().Pink1;
                break;
            case Enum.eTheme.Mint:
                bgGo = Instantiate(BGArr[1], Vector3.zero, Quaternion.identity);
                bgGo.GetComponent<SpriteRenderer>().color = new Factor().BGColorDefault;
                break;
        }
        bgGo.transform.position = new Factor().PosBG;
        return bgGo;
    } 
} // end of class