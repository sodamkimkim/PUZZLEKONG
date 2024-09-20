using UnityEngine;

public class ThemeManager : MonoBehaviour
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
    public static Enum.eTheme ETheme = Enum.eTheme.Green;
 
} // end of class

