using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class LogoAnim : MonoBehaviour
{
    private TextMeshProUGUI[] _tmpros = null;

    private void OnEnable()
    {
        _tmpros = this.GetComponentsInChildren<TextMeshProUGUI>();
        StartCoroutine(LogoAnimRepeating());
    }
    IEnumerator LogoAnimRepeating()
    {
        while (true)
        {
            for (int i = 0; i < _tmpros.Length; i++)
            {
                Vector3 letterPos = _tmpros[i].rectTransform.localPosition;
                letterPos.y =15f;
                _tmpros[i].rectTransform.localPosition = letterPos;

                yield return new WaitForSeconds(0.15f);

                SetPosOrigin(_tmpros[i]);
            }
        }
    }

    private void SetPosOrigin(TextMeshProUGUI tmpro)
    {
        if (tmpro == null && tmpro.rectTransform.localPosition.y == 0) return;
        Vector3 pos = tmpro.rectTransform.localPosition;
        pos.y = 0;
        tmpro.rectTransform.localPosition = pos;
    }
    private void OnDisable()
    {
        StopAllCoroutines();

        for (int i = 0; i < _tmpros.Length; i++)
        {
            if (_tmpros[i].rectTransform.localPosition.y == 0) continue;
            Vector3 letterPos = _tmpros[i].rectTransform.localPosition;
            letterPos.y = 0;
            _tmpros[i].rectTransform.localPosition = letterPos;
        }
    }
} // end of class
