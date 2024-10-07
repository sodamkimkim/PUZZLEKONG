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
    private void Update()
    {
        ShotRay();
    }
    private void ShotRay()
    {
        TriggeredGridPartIdxStr = string.Empty;
        if (ParentPuzzle != TouchRaycast2D.TouchingPuzzle)
        {
            return;
        }
        RaycastHit hit;
        // Z축 방향으로 레이 쏘기 (transform.forward는 Z축 방향)
        if (Physics.Raycast(this.gameObject.transform.position, transform.forward, out hit, 100f))
        {
            // Physics.Raycast는 out 키워드를 통해 hit 변수에 충돌 정보를 넣어줍니다. 
            Debug.DrawRay(this.gameObject.transform.position, transform.forward, Color.green, 10f);
            if (hit.collider != null && hit.transform.gameObject.tag == "GridPart")
            {
                if (TriggeredGridPartIdxStr != hit.transform.gameObject.name)
                {
                    TriggeredGridPartIdxStr = hit.transform.gameObject.name;
                }
            }
            else TriggeredGridPartIdxStr = string.Empty;
        }
        else
        {
            TriggeredGridPartIdxStr = string.Empty;
        }
        Debug.Log($"Triggered GridPart : {TriggeredGridPartIdxStr}");
    }
     

} // end of class
