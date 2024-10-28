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

