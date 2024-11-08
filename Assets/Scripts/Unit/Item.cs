using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private GameObject _initialITemSlotPos;
    private Vector3 _localScaleSmall;
    public GameObject InitialITemSlotPos { get => _initialITemSlotPos; set => _initialITemSlotPos = value; }
    public Vector3 LocalScaleSmall { get => _localScaleSmall; private set => _localScaleSmall = value; }

    private void Awake()
    {
        LocalScaleSmall = transform.localScale;
        Debug.Log($"{this.name} || {LocalScaleSmall}");
    }
} // end of class