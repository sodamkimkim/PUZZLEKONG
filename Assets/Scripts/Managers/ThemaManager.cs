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
    //   public static Enum.eTheme ETheme = Enum.eTheme.LightPurple;
    public static Enum.eTheme ETheme = Enum.eTheme.Pink;
    //
    //public static Enum.eTheme ETheme = Enum.eTheme.Yellow;
    // public static Enum.eTheme ETheme = Enum.eTheme.Mint;

    public static Enum.eEffect_Complete Eeffect1 = Enum.eEffect_Complete.C1_RoundStar2;
    public static Enum.eEffect_Complete Eeffect2 = Enum.eEffect_Complete.C1_Dust_blue;
    public static Enum.eEffect_Complete Eeffect3 = Enum.eEffect_Complete.C1_Water_blast_green;
} // end of class

