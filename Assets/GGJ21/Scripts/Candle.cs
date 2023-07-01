using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{

    public bool candleLit = false;
    public float finalIntensity = 70;

    public new Light light;
    public MeshRenderer capsule;
    
    // Start is called before the first frame update
    void Start()
    {
        light.intensity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        light.gameObject.SetActive(candleLit);
        capsule.gameObject.SetActive(candleLit);
        light.intensity = Mathf.Lerp(candleLit ? light.intensity : 0, finalIntensity, Time.deltaTime);
    }
}
