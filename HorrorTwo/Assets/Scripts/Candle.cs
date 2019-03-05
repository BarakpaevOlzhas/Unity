using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    [SerializeField]
    private Light candle;

    public void SetActive()
    {
        candle.enabled = !candle.enabled;
    }
}
