
public struct IdxRCStruct
{
    public int IdxR { get; set; }
    public int IdxC { get; set; }

    public IdxRCStruct(int idxR, int idxC)
    {
        this.IdxR = idxR;
        this.IdxC = idxC;
    }
    public override string ToString() => $"{IdxR},{IdxC}";

    public bool Equals(int idxR, int idxC)
    {
        return (IdxR == idxR && IdxC == idxC) ? true : false;
    }
    public void Clear()
    {
        IdxR = 0;
        IdxC = 0;
    }
} // end of struct