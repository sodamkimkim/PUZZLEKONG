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

    public string TriggeredGridPartIdxStr = string.Empty; // r,c ���·� ����
    private void OnEnable()
    {
        LocalScaleSmall = transform.localScale;
        //  Debug.Log($"{this.name} || {LocalScaleSmall}");
        Animator = Util.CheckAndAddComponent<Animator>(this.gameObject);
    }
    public void SetPos(bool isWorldPos, Vector3 pos)
    {
        if (isWorldPos)
            transform.position = pos;
        else
            transform.localPosition = pos;
    }
    public void SetScale(Enum.eItemScale eScale, float multiply)
    {
        switch (eScale)
        {
            case Enum.eItemScale.Small:
                this.transform.localScale = LocalScaleSmall* multiply;
                break;
            case Enum.eItemScale.Big:
                this.transform.localScale = LocalScaleSmall * 3f* multiply;
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
    private void ShotRay()
    {
        if (TriggeredGridPartIdxStr != string.Empty)
            TriggeredGridPartIdxStr = string.Empty;
        if (this != TouchRaycast2D_Item.TouchingItem) return;

        RaycastHit hit;
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
        else TriggeredGridPartIdxStr = string.Empty;
    }
} // end of class