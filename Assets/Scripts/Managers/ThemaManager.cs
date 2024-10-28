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
    //   public static Enum.eTheme ETheme = Enum.eTheme.Grey;
    //  public static Enum.eTheme ETheme = Enum.eTheme.Green;
    // public static Enum.eTheme ETheme = Enum.eTheme.LightBlue;
      public static Enum.eTheme ETheme = Enum.eTheme.LightPurple;
   // public static Enum.eTheme ETheme = Enum.eTheme.Pink;
    //
    //public static Enum.eTheme ETheme = Enum.eTheme.Yellow;
    // public static Enum.eTheme ETheme = Enum.eTheme.Mint;

    public static Enum.eEffect_Complete Eeffect_Hori = Enum.eEffect_Complete.C1_RoundStar2;
    public static Enum.eEffect_Complete Eeffect_Verti = Enum.eEffect_Complete.C1_Spark1;
    public static Enum.eEffect_Complete Eeffect_Area = Enum.eEffect_Complete.C2_Love1;
    public static Enum.eEffect_Celebration Eeffect_Combo = Enum.eEffect_Celebration.Celebration1;
    public static Enum.eEffect_Celebration Eeffect_Finish = Enum.eEffect_Celebration.Celebration2;
} // end of class

