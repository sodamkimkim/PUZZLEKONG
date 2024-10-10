using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForCoroutines : CustomYieldInstruction
{
    public override bool keepWaiting => _remainCount > 0;

    private int _remainCount;

    public WaitForCoroutines(MonoBehaviour runner, params IEnumerator[] coroutines)
    {
        IEnumerator WaitingCoroutine(IEnumerator coroutine)
        {
            yield return coroutine;
            _remainCount--;
        }

        _remainCount = coroutines.Length;
        for (var i = 0; i < _remainCount; i++)
        {
            runner.StartCoroutine(WaitingCoroutine(coroutines[i]));
        }
    }
}// end of class
