using UnityEngine;

[DefaultExecutionOrder(-10)]
public class BackgroundManager : MonoBehaviour
{
    [SerializeField]
    private Transform _bgParentTr = null;
    private GameObject[] BGArr = new GameObject[2];
    public GameObject BGgo { get; private set; }
    private void Awake()
    {
        Path path = new Path();
        BGArr[0] = Resources.Load<GameObject>($"{Path.Backgrounds}/BG_Default");
        BGArr[1] = Resources.Load<GameObject>($"{Path.Backgrounds}/BG_MintChoco");
    }
    private void Start()
    {
        BGgo = GetBGGo(ThemaManager.ETheme);  
    } 
    public GameObject GetBGGo(Str.eTheme eTheme)
    {
        GameObject bgGo = null; 
        switch (eTheme)
        {
            case Str.eTheme.Grey:
                bgGo = Instantiate(BGArr[0], Vector3.zero, Quaternion.identity, _bgParentTr);
                bgGo.GetComponent<SpriteRenderer>().color = Factor.Grey0;
                bgGo.name = "BG_Default";
                break;
            case Str.eTheme.Green:
                bgGo = Instantiate(BGArr[0], Vector3.zero, Quaternion.identity, _bgParentTr);
                bgGo.GetComponent<SpriteRenderer>().color = Factor.Green0;
                bgGo.name = "BG_Default";
                break;
            case Str.eTheme.LightPurple:
                bgGo = Instantiate(BGArr[0], Vector3.zero, Quaternion.identity, _bgParentTr);
                bgGo.GetComponent<SpriteRenderer>().color = Factor.LightPurple0;
                bgGo.name = "BG_Default";
                break;
            case Str.eTheme.LightBlue:
                bgGo = Instantiate(BGArr[0], Vector3.zero, Quaternion.identity, _bgParentTr);
                bgGo.GetComponent<SpriteRenderer>().color = Factor.LightBlue0;
                bgGo.name = "BG_Default";
                break;
            case Str.eTheme.Pink:
                bgGo = Instantiate(BGArr[0], Vector3.zero, Quaternion.identity, _bgParentTr);
                bgGo.GetComponent<SpriteRenderer>().color = Factor.Pink0;
                bgGo.name = "BG_Default";
                break;
            case Str.eTheme.Yellow:
                bgGo = Instantiate(BGArr[0], Vector3.zero, Quaternion.identity, _bgParentTr);
                bgGo.GetComponent<SpriteRenderer>().color = Factor.Yellow0;
                bgGo.name = "BG_Default";
                break;
            case Str.eTheme.Mint:
                bgGo = Instantiate(BGArr[1], Vector3.zero, Quaternion.identity, _bgParentTr);
                bgGo.GetComponent<SpriteRenderer>().color = Factor.BGColorDefault;
                bgGo.name = "BG_MintChoco";
                break;
        } 
        bgGo.transform.position = Factor.PosBG;
        return bgGo;
    }
} // end of class