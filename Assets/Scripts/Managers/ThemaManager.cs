using UnityEngine;

public class ThemaManager : MonoBehaviour
{
    /// <summary>
    /// # Theme �������
    /// - ThemeManager static Enum Factor ���� => �� Manager/SpawnerŬ�������� �ݿ�
    /// - Theme ������ ���� Lazy Start ����!
    /// 
    /// 1. Background ���� - BG_Default ���󺯰�/BG_${eTheme}
    /// 2. Grid�� ����
    /// 3. Grid-Placable����
    /// 4. Puzzle�� ����
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

