using System.Text;

public class PZArrResource
{
    public static int[][,] PZArrArr = new int[][,] {
            new int[,] // ����000
            {
                { 1, 1, 1, 0 },
                { 1, 0, 0, 0 },
                { 1, 0, 0 ,0 },
                { 0, 0, 0 ,0 }
            },
            new int[,] // ����001
            {
                { 0, 0, 1, 0 },
                { 0, 0, 1, 0 },
                { 1, 1, 1, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // ����002
            {
                { 1, 1, 1, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // ����003
            {
                { 1, 1, 1, 1 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // ����004
            {
                { 1, 0, 0, 0 },
                { 1, 0, 0, 0 },
                { 1, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // ����005
            {
                { 1, 0, 0, 0 },
                { 1, 0, 0, 0 },
                { 1, 0, 0, 0 },
                { 1, 0, 0, 0 }
            },
            new int[,] // ����006
            {
                { 1, 1, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 1, 1, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // ����007
            {
                { 0, 1, 1, 0 },
                { 0, 1, 0, 0 },
                { 1, 1, 0, 0 },
                { 0, 0, 0, 0 }
            }
            ,
            new int[,] // ����008
            {
                { 1, 1, 1, 0 },
                { 0, 1, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // ����009
            {
                { 0, 1, 0, 0 },
                { 0, 1, 0, 0 },
                { 1, 1, 1, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // ����010
            {
                { 1, 0, 0, 0 },
                { 1, 1, 0, 0 },
                { 1, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // ����011
            {
                { 0, 1, 0, 0 },
                { 1, 1, 1, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },
            new int[,] // ����012
            {
                { 1, 1, 0, 0 },
                { 1, 1, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            }
        };
    // �迭�� ���ڿ��� ��ȯ�ϴ� �޼���
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
                    sb.Append(", "); // �� ���� ���̿� ��ǥ �߰�
            }
            sb.AppendLine(); // �� �� ���� �ٹٲ� �߰�
        }

        return sb.ToString();
    }
} // end of class