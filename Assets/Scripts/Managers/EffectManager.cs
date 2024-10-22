using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameObject completeEffect1 { get; private set; }
    public GameObject completeEffect2 { get; private set; }
    public GameObject completeEffect3 { get; private set; }
 
    public void LazyStart()
    {
        completeEffect1 = Resources.Load<GameObject>($"{Path.CompleteEffectPrefab}/{ThemaManager.Eeffect1.ToString()}");
        completeEffect2 = Resources.Load<GameObject>($"{Path.CompleteEffectPrefab}/{ThemaManager.Eeffect2.ToString()}");
        completeEffect3 = Resources.Load<GameObject>($"{Path.CompleteEffectPrefab}/{ThemaManager.Eeffect3.ToString()}");
    }
} // end of class