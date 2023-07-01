using System;
using System.Collections.Generic;
using UnityEngine;

public class Cake : MonoBehaviour
{

    public List<Candle> candles;

    private void Update()
    {
        var shouldLight = false;
        foreach (var candle in candles)
        {
            if (candle.candleLit)
            {
                shouldLight = true;
            }
        }

        if (shouldLight)
        {
            foreach (var candle in candles)
            {
                candle.candleLit = true;
            }
        }
    }
}