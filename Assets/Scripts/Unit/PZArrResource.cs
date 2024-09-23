using System.Text;

public class PZArrResource
{
    public static int[][,] PZArrArr = new int[][,] {
            new int[,] // ∆€¡Ò000
            {
                { 1, 1, 1, 0 },
                { 1, 0, 0, 0 },
                { 1, 0, 0 ,0 },
                { 0, 0, 0 ,0 }
            },
            new int[,] // ∆€¡Ò001
            {
                { 0, 0, 1, 0 },
                { 0, 0, 1, 0 },
                { 1, 1, 1, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // ∆€¡Ò002
            {
                { 1, 1, 1, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // ∆€¡Ò003
            {
                { 1, 1, 1, 1 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // ∆€¡Ò004
            {
                { 1, 0, 0, 0 },
                { 1, 0, 0, 0 },
                { 1, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // ∆€¡Ò005
            {
                { 1, 0, 0, 0 },
                { 1, 0, 0, 0 },
                { 1, 0, 0, 0 },
                { 1, 0, 0, 0 }
            },
            new int[,] // ∆€¡Ò006
            {
                { 1, 1, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 1, 1, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // ∆€¡Ò007
            {
                { 0, 1, 1, 0 },
                { 0, 1, 0, 0 },
                { 1, 1, 0, 0 },
                { 0, 0, 0, 0 }
            }
            ,
            new int[,] // ∆€¡Ò008
            {
                { 1, 1, 1, 0 },
                { 0, 1, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // ∆€¡Ò009
            {
                { 0, 1, 0, 0 },
                { 0, 1, 0, 0 },
                { 1, 1, 1, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // ∆€¡Ò010
            {
                { 1, 0, 0, 0 },
                { 1, 1, 0, 0 },
                { 1, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // ∆€¡Ò011
            {
                { 0, 1, 0, 0 },
                { 1, 1, 1, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // ∆€¡Ò012
            {
                { 1, 1, 0, 0 },
                { 1, 1, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            }
        };
    // πËø≠¿ª πÆ¿⁄ø≠∑Œ ∫Ø»Ø«œ¥¬ ∏ﬁº≠µÂ
    public static string ConvertPuzzleArrayToString(int[,] puzzleArray)
    {
        StringBuilder sb = new StringBuilder();

        int rows = puzzleArray.GetLength(0);
        int cols = puzzleArray.GetLength(1);
        sb.AppendLine();
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                sb.Append(puzzleArray[r, c].ToString());
                if (c < cols - 1)
                    sb.Append(", "); // ∞¢ º˝¿⁄ ªÁ¿Ãø° Ω∞«• √ﬂ∞°
            }
            sb.AppendLine(); // ∞¢ «‡ ≥°ø° ¡ŸπŸ≤ﬁ √ﬂ∞°
        }

        return sb.ToString();
    }
} // end of class