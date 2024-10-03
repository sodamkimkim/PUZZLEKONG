
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
} // end of struct