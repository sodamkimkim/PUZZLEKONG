using UnityEngine;

public class ThemaManager : MonoBehaviour
{
    /// <summary>
    /// # Theme 변경로직
    /// - ThemeManager static Enum Factor 변경 => 각 Manager/Spawner클래스에서 반영
    /// - Theme 적용은 게임 Lazy Start 시점!
    /// 
    /// 1. Background 변경 - BG_Default 색상변경/BG_${eTheme}
    /// 2. Grid색 변경
    /// 3. Grid-Placable변경
    /// 4. Puzzle색 변경
    /// </summary>
    /// 
    // public static Str.eTheme _eTheme = Str.eTheme.Grey;
    //  public static Str.eTheme _eTheme = Str.eTheme.Green;
    private static Str.eTheme _eTheme = Str.eTheme.LightBlue;
    //   public static Str.eTheme _eTheme = Str.eTheme.LightPurple;
    //public static Str.eTheme _eTheme = Str.eTheme.Pink;
    //
    // public static  Str.eTheme _eTheme = Str.eTheme.Yellow;
    // public static Str.eTheme _eTheme = Enum.eTheme.Mint;
    public static Str.eTheme ETheme { get => _eTheme; set => _eTheme = value; }

    public static Str.eEffect_Complete Eeffect_Hori = Str.eEffect_Complete.C2_Love1;
    public static Str.eEffect_Complete Eeffect_Verti = Str.eEffect_Complete.C2_Love1;
    public static Str.eEffect_Complete Eeffect_Area = Str.eEffect_Complete.C2_Love1;

    public static Str.eEffect_Celebration Eeffect_Combo = Str.eEffect_Celebration.Celebration2;
    public static Str.eEffect_Celebration Eeffect_Finish = Str.eEffect_Celebration.Celebration2;
} // end of class

