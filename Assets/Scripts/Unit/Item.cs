using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private GameObject _initialITemSlotPos;
    private Vector3 _localScaleSmall;
    private Animator _animator;

    public GameObject InitialITemSlotPos { get => _initialITemSlotPos; set => _initialITemSlotPos = value; }
    public Vector3 LocalScaleSmall { get => _localScaleSmall; private set => _localScaleSmall = value; }
    public Animator Animator { get => _animator; private set => _animator = value; }
    public string TriggeredGridPartIdxStr = string.Empty; // r,c 형태로 저장
    private void Awake()
    {
        LocalScaleSmall = transform.localScale;
        Debug.Log($"{this.name} || {LocalScaleSmall}");
        Animator = Util.CheckAndAddComponent<Animator>(this.gameObject);
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