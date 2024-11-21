public class Str
{
    public enum eTheme { Grey, Green, LightPurple, LightBlue, Pink, Yellow, Mint }
    public enum eTouchFunc { TouchBegin, TouchMoved, TouchEnd }
    public enum eEffect_Complete
    {
        C1_Dust_blue,
        C1_Flash1, C1_Flash2, C1_Flash3, C1_Flash4, C1_Flash5, C1_Flash6, C1_Flash7,
        C1_RoundStar1, C1_RoundStar2,
        //   C1_Shine_Blue, C1_Shine_Green, C1_Shine_Grey, C1_Shine_LightPurple, C1_Shine_Mint, C1_Shine_Pink, C1_Shine_Yellow,
        C1_Spark1, C1_Spark2,
        C1_Water_blast_blue, C1_Water_blast_green,
        C2_Fire, C2_Ice, C2_Lighting, C2_Love, C2_Mint,
        C2_Love1
    }
    public enum eEffect_Celebration
    {
        Celebration1, Celebration2
    }
    public enum eItem
    {
        Item_a_Mushroom, Item_b_Wandoo, Item_c_Reset, Item_d_SwitchHori, Item_e_SwitchVerti
    }
    public enum eItemScale
    {
        Small, Big
    }
    public enum eStage { Classic, Item, Event }

    #region PlayerData
    public static string NowScore = "NowScore";
    public static string MyBestScore_Item = "MyBestScore_Item";
    public static string PlayerTotalScore_Item = "PlayerTotalScore_Item";
    public static string MyBestScore_Classic = "MyBestScore_Classic";
    public static string PlayerTotalScore_Classic = "PlayerTotalScore_Classic";

    public static string Item_a_Mushroom = "Item_a_Mushroom";
    public static string Item_b_Wandoo = "Item_b_Wandoo";
    public static string Item_c_Reset = "Item_c_Reset";
    public static string Item_d_SwitchRows = "Item_d_SwitchRows";
    public static string Item_e_SwitchColumns = "Item_e_SwitchColumns";
    public static string Item_f_Bumb = "Item_f_Bumb";
    public static string Item_g_Eraser = "Item_g_Eraser";
    public static string Item_h_PushLeft = "Item_h_PushLeft";
    public static string Item_i_PushUp = "Item_i_PushUp";

    public static string ItemSlot0 = "ItemSlot0";
    public static string ItemSlot1 = "ItemSlot1";
    public static string ItemSlot2 = "ItemSlot2";
    public static string ItemSlot3 = "ItemSlot3";
    public static string ItemSlot4 = "ItemSlot4";
    public static string ItemSlot5 = "ItemSlot5";
    public static string ItemSlot6 = "ItemSlot6";
    public static string ItemSlot7 = "ItemSlot7";
    #endregion
} // end of class