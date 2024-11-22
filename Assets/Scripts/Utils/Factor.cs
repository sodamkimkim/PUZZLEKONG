using UnityEngine;

public class Factor : MonoBehaviour
{
    #region GridPartData 
    /// <summary>
    /// 0
    /// </summary>
    public static int HasNoPuzzle { get => 0; }
    /// <summary>
    /// 1
    /// </summary>
    public static int HasPuzzle { get => 1; } 
    /// <summary>
    /// 2
    /// </summary>
    public static int Placable { get => 2; }
    /// <summary>
    /// 3
    /// </summary>
    public static int Completable { get => 3; }
    /// <summary>
    /// 4
    /// </summary>
    public static int UseItem1 { get => 4; }
    #endregion
    #region PuzzleStatusData
    /// <summary>
    /// 1
    /// </summary>
    public static int PuzzleStatus_ItemNormal { get => 0; }
    /// <summary>
    /// 1
    /// </summary>
    public static int PuzzleStatus_ItemUse { get => 1; }  
    #endregion
    #region Common Factor
    public static int CompleteScore { get => 5; }
    public static int IntInitialized { get => -99; }
    public static float CompleteCoroutineInterval { get => 0.07f; }
    public static float CompletableOffset { get => 0.8f; }
    public static Vector3 EffectPos_Celebration { get => new Vector3(0f, 1f, -0.2f); }
    #endregion

    #region Transform
    public static Vector3 PosBG { get => new Vector3(0f, 0f, 2f); }
    public static Vector3 PosGridSpawn { get => new Vector3(0f, 1f, 1f); }

    public static Vector3 PosPuzzleSpawn0 { get => new Vector3(-1.5f, -3.15f, 0f); }
    public static Vector3 PosPuzzleSpawn1 { get => new Vector3(0f, -3.15f, 0f); }
    public static Vector3 PosPuzzleSpawn2 { get => new Vector3(1.5f, -3.15f, 0f); }
    public static float PosEffectSpawnZ { get => -2f; }

    public static Vector3 ScalePuzzleSmall { get => new Vector3(0.2f, 0.2f, 1f); }
    public static Vector3 ScalePuzzleNormal { get => new Vector3(0.5f, 0.5f, 1f); }
    public static Vector3 TouchingObjOffset { get => new Vector3(0f, 3f, 0f); }
    #endregion

    #region Color
    public static Color BGColorDefault { get => new Color(215f / 255f, 210f / 255f, 210f / 255f); }

    public static Color Grey0 { get => new Color(204f / 255f, 204f / 255f, 204f / 255f); } // BG
    public static Color Grey1 { get => new Color(0f / 255f, 0f / 255f, 0f / 255f, 45f / 255f); } // HasNoPuzzle
    public static Color Grey2 { get => new Color(102f / 255f, 102f / 255f, 102f / 255f); } // Placable
    public static Color Grey3 { get => new Color(51f / 255f, 51f / 255f, 51f / 255f); } // HasPuzzle

    public static Color Green0 { get => new Color(207f / 255f, 227f / 255f, 155f / 255f); }
    public static Color Green1 { get => new Color(167f / 255f, 198f / 255f, 101f / 255f); }
    public static Color Green2 { get => new Color(119f / 255f, 141f / 255f, 72f / 255f); }
    public static Color Green3 { get => new Color(45f / 255f, 53f / 255f, 27f / 255f); }

    public static Color LightPurple0 { get => new Color(204f / 255f, 204f / 255f, 255f / 255f); }
    public static Color LightPurple1 { get => new Color(245f / 255f, 186f / 255f, 245f / 255f); }
    public static Color LightPurple2 { get => new Color(153f / 255f, 102f / 255f, 153f / 255f); }
    public static Color LightPurple3 { get => new Color(93f / 255f, 75f / 255f, 113f / 255f); }

    public static Color LightBlue0 { get => new Color(197f / 255f, 234f / 255f, 247f / 255f); }
    public static Color LightBlue1 { get => new Color(138f / 255f, 187f / 255f, 203f / 255f); }
    public static Color LightBlue2 { get => new Color(99f / 255f, 134f / 255f, 146f / 255f); }
    public static Color LightBlue3 { get => new Color(59f / 255f, 80f / 255f, 87f / 255f); }

    public static Color Pink0 { get => new Color(248f / 255f, 223f / 255f, 248f / 255f); }
    public static Color Pink1 { get => new Color(247f / 255f, 190f / 255f, 247f / 255f); }
    public static Color Pink2 { get => new Color(131f / 255f, 85f / 255f, 131f / 255f); }
    public static Color Pink3 { get => new Color(56f / 255f, 35f / 255f, 56f / 255f); }

    public static Color Yellow0 { get => new Color(223f / 255f, 221f / 255f, 196f / 255f); }
    public static Color Yellow1 { get => new Color(239f / 255f, 235f / 255f, 180f / 255f); }
    public static Color Yellow2 { get => new Color(252f / 255f, 205f / 255f, 126f / 255f); }
    public static Color Yellow3 { get => new Color(252f / 255f, 170f / 255f, 12f / 255f); }
    #endregion

} // end of class