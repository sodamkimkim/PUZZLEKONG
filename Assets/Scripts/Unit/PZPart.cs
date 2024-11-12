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
    public string TriggeredGridPartIdxStr = string.Empty; // r,c ���·� ����

    private void Awake()
    {
        string[] idxStr = this.name.Split(',');
        idxStruct.IdxR = int.Parse(idxStr[0]);
        idxStruct.IdxC = int.Parse(idxStr[1]);
    }
    private void Update()
    {
        ShotRay();
    }
    private void ShotRay()
    {
        if (TriggeredGridPartIdxStr != string.Empty)
            TriggeredGridPartIdxStr = string.Empty;
        if (ParentPuzzle != TouchRaycast.TouchingPuzzle) return;

        RaycastHit hit;
        // Z�� �������� ���� ��� (transform.forward�� Z�� ����)
        if (Physics.Raycast(this.gameObject.transform.position, transform.forward, out hit, 100f))
        { 
            Debug.DrawRay(this.gameObject.transform.position, transform.forward, Color.green, 0.01f);
            if (hit.collider != null && hit.transform.gameObject.tag == "GridPart")
            {
                if (TriggeredGridPartIdxStr != hit.transform.gameObject.name)
                    TriggeredGridPartIdxStr = hit.transform.gameObject.name;
            }
            else TriggeredGridPartIdxStr = string.Empty;
        }
        else
            TriggeredGridPartIdxStr = string.Empty;
        //  Debug.Log($"Triggered GridPart : {TriggeredGridPartIdxStr}");
    }


} // end of class
