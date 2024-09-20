using UnityEngine;

public class Factor
{
    #region Transform
    public Vector3 PosBG { get => new Vector3(0f, 0f, 10f); }
    public Vector3 PosGridSpawn { get => new Vector3(0f, 1f, 2f); }
 
    public static Vector3 ScalePuzzleSmall { get=> new Vector3(0.2f, 0.2f, 1f); } 
    public static Vector3 ScalePuzzleNormal { get=> new Vector3(0.5f, 0.5f, 1f); } 
    #endregion

    #region Color
    public Color BGColorDefault { get=> new Color(215f / 255f, 210f / 255f, 210f / 255f); }
    
    public Color Grey1 { get=> new Color(204f / 255f, 204f / 255f, 204f / 255f); }
  //  public Color Grey2 { get=> new Color(153f / 255f, 153f / 255f, 153f / 255f, 30f/255f); }
  public Color Grey2 { get=> new Color(0f / 255f,0f / 255f, 0f / 255f, 45f/255f); }
    public Color Grey3 { get=> new Color(102f / 255f, 102f / 255f, 102f / 255f); }
    public Color Grey4 { get=> new Color(51f / 255f, 51f / 255f, 51f / 255f); }

    public Color Green1 { get=> new Color(207f / 255f, 227f / 255f, 155f / 255f); }
    public Color Green2 { get => new Color(167f / 255f, 198f / 255f, 101f / 255f); }
    public  Color Green3 { get=> new Color(119f / 255f, 141f / 255f, 72f / 255f); }
    public Color Green4 { get=> new Color(45f / 255f, 53f / 255f, 27f / 255f); }

    public Color LightPurple1 { get => new Color(204f / 255f, 204f / 255f, 255f / 255f); }
    public Color LightPurple2 { get=> new Color(204f / 255f, 153f / 255f, 204f / 255f); }
    public Color LightPurple3 { get=> new Color(153f / 255f, 102f / 255f, 153f / 255f); }
    public Color LightPurple4 { get=> new Color(93f / 255f, 75f / 255f, 113f / 255f); }

    public Color LightBlue1 { get=> new Color(197f / 255f, 234f / 255f, 247f / 255f); }
    public Color LightBlue2 { get => new Color(138f / 255f, 187f / 255f, 203f / 255f); }
    public Color LightBlue3 { get=> new Color(99f / 255f, 134f / 255f, 146f / 255f); }
    public Color LightBlue4 { get => new Color(59f / 255f, 80f / 255f, 87f / 255f); }

    public Color Pink1 { get=> new Color(248f / 255f, 223f / 255f, 248f / 255f); }
    public Color Pink2 { get => new Color(247f / 255f, 190f / 255f, 247f / 255f); }
    public Color Pink3 { get => new Color(131f / 255f, 85f / 255f, 131f / 255f); }
    public Color Pink4 { get => new Color(56f / 255f, 35f / 255f, 56f / 255f); }
    #endregion

} // end of class