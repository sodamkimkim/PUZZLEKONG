using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameObject EffectPrefab_Complete_Hori { get; private set; }
    public GameObject EffectPrefab_Complete_Verti { get; private set; }
    public GameObject EffectPrefab_Complete_Area { get; private set; }
    public GameObject EffectPrefab_Celebration_Combo { get; private set; }
    public GameObject EffectPrefab_Celebration_Finish { get; private set; }
 
    public void LazyStart()
    {
        EffectPrefab_Complete_Hori = Resources.Load<GameObject>($"{Path.CompleteEffectPrefab}/{ThemaManager.Eeffect_Hori.ToString()}");
        EffectPrefab_Complete_Verti = Resources.Load<GameObject>($"{Path.CompleteEffectPrefab}/{ThemaManager.Eeffect_Verti.ToString()}");
        EffectPrefab_Complete_Area = Resources.Load<GameObject>($"{Path.CompleteEffectPrefab}/{ThemaManager.Eeffect_Area.ToString()}");
        EffectPrefab_Celebration_Combo = Resources.Load<GameObject>($"{Path.CelebrationEffectPrefab}/{ThemaManager.Eeffect_Combo.ToString()}");
        EffectPrefab_Celebration_Finish = EffectPrefab_Celebration_Combo;
    }
} // end of class