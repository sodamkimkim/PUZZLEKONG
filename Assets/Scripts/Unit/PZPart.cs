using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PZPart : MonoBehaviour
{
    #region Hidden Private Variables 
    private Puzzle _parentPuzzle;
    private SpriteRenderer _spr;
    #endregion
    public IdxRCStruct idxStruct = new IdxRCStruct(0, 0); 
    public Puzzle ParentPuzzle { get => _parentPuzzle; set => _parentPuzzle = value; }
    public SpriteRenderer Spr { get => _spr; set => _spr = value; }
    public string TriggeredGridPartIdxStr = string.Empty; // r,c 형태로 저장  
    private void Awake()
    {
        string[] idxStr = this.name.Split(',');
        idxStruct.IdxR = int.Parse(idxStr[0]);
        idxStruct.IdxC = int.Parse(idxStr[1]);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
         
        if (collision.tag == "GridPart")
        {
            if (TriggeredGridPartIdxStr != collision.name)
            {
                TriggeredGridPartIdxStr = collision.name;

            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "GridPart")
            TriggeredGridPartIdxStr = string.Empty;
 
    }
} // end of class
