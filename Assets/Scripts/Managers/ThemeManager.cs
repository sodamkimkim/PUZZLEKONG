using UnityEngine;

public class ThemeManager : MonoBehaviour
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
   // public static Enum.eTheme ETheme = Enum.eTheme.Grey;
   public static Enum.eTheme ETheme = Enum.eTheme.Green;
   //  public static Enum.eTheme ETheme = Enum.eTheme.LightBlue;
  //   public static Enum.eTheme ETheme = Enum.eTheme.LightPurple;
   // public static Enum.eTheme ETheme = Enum.eTheme.Mint;
  //  public static Enum.eTheme ETheme = Enum.eTheme.Pink;
} // end of class

