using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTracker : MonoBehaviour
{
    public static SceneTracker Instance { get; private set; }
    private HashSet<string> _visitedScenes = new HashSet<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    public bool IsFirstVisit(string sceneName)
    {
        if (_visitedScenes.Contains(sceneName))
            return false;
        else
        {
            _visitedScenes.Add(sceneName);
            return true;
        }
    }
} // end of class
