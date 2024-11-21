using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private GameObject _initialItemSlotPos = null;
    private Vector3 _localScaleSmall = Vector3.zero;
    private Animator _animator = null;
    private bool _isForEffect = true;
    public GameObject InitialItemSlotPos { get => _initialItemSlotPos; set => _initialItemSlotPos = value; }
    public Vector3 LocalScaleSmall { get => _localScaleSmall; private set => _localScaleSmall = value; }
    public Animator Animator { get => _animator; private set => _animator = value; }
    public bool IsForEffect { get => _isForEffect; set => _isForEffect = value; }

    public int TriggeredGridPartIdxR = 0; // GridPart Idx
    public int TriggeredGridPartIdxC = 0; // GridPart Idx
    public Puzzle TriggeredPuzzle = null;
    private GameObject _childParticleGo = null;
    private void OnEnable()
    {
        LocalScaleSmall = transform.localScale;
        //  Debug.Log($"{this.name} || {LocalScaleSmall}");
        Animator = Util.CheckAndAddComponent<Animator>(this.gameObject);
        ParticleSystem ps = this.GetComponentInChildren<ParticleSystem>();
        if (ps != null)
        {
            _childParticleGo = ps.gameObject;
            _childParticleGo.SetActive(false);
        }
    }
    public void ChildParticleSetActive(bool isActive)
    {
        if (_childParticleGo != null)
            _childParticleGo.SetActive(isActive);
    }
    public void SetPos(bool isWorldPos, Vector3 pos)
    {
        if (isWorldPos)
            transform.position = pos;
        else
            transform.localPosition = pos;
    }
    public void SetScale(Str.eItemScale eScale, float multiply)
    {
        switch (eScale)
        {
            case Str.eItemScale.Small:
                this.transform.localScale = LocalScaleSmall * multiply;
                break;
            case Str.eItemScale.Big:
                this.transform.localScale = LocalScaleSmall * 3f * multiply;
                break;
        }
    }
    public void Anim(string anim, bool isAnim)
    {
        if (Animator == null)
            Animator = Util.CheckAndAddComponent<Animator>(this.gameObject);

        Animator.SetBool(anim, isAnim);
    }
    private void Update()
    {
        ShotRay();
    }
    private void SetTriggeredIdx(int r, int c)
    {
        if (TriggeredGridPartIdxR != r)
            TriggeredGridPartIdxR = r;
        if (TriggeredGridPartIdxC != c)
            TriggeredGridPartIdxC = c;
    }
    private void ShotRay()
    {
        SetTriggeredIdx(Factor.IntInitialized, Factor.IntInitialized);
        if (TriggeredPuzzle != null)
            TriggeredPuzzle = null;

        if (this != TouchRaycast_Item.TouchingItem) return;

        RaycastHit hit;
        if (Physics.Raycast(this.gameObject.transform.position, transform.forward, out hit, 1000f))
        {
            Debug.DrawRay(this.gameObject.transform.position, transform.forward, Color.green, 0.01f);

            // # GridPart
            if (hit.collider != null && hit.transform.gameObject.tag == "GridPart")
            {
                TriggeredPuzzle = null;
                GridPart gp = hit.transform.gameObject.GetComponent<GridPart>();
                if (gp != null)
                    SetTriggeredIdx(gp.IdxRow, gp.IdxCol);
            }
            // # Puzzle
            else if (hit.transform.gameObject.tag == "PuzzlePart")
            {
                SetTriggeredIdx(Factor.IntInitialized, Factor.IntInitialized);
                TriggeredPuzzle = hit.transform.gameObject.GetComponentInParent<Puzzle>();
                //     Debug.Log($"{this.name} || {IsPuzzleTriggered}");
            }
            else
            {
                SetTriggeredIdx(Factor.IntInitialized, Factor.IntInitialized);
                if (TriggeredPuzzle != null)
                    TriggeredPuzzle = null;
            }
        }
        else
        {
            SetTriggeredIdx(Factor.IntInitialized, Factor.IntInitialized);
            if (TriggeredPuzzle != null)
                TriggeredPuzzle = null;
        }
    }
} // end of class