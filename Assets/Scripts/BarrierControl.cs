using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierControl : MonoBehaviour
{
    [SerializeField] Transform barrierTransform;

    [System.NonSerialized] public bool active;
    //[SerializeField] GameObject barrier;
    // Start is called before the first frame update
    void Start()
    {
        active = true;
    }




    // Update is called once per frame
    void Update()
    {
        
        if (!active && barrierTransform.position.y > -0.5)
        {
            barrierTransform.Translate(0, -0.01f, 0);
        }
        
    }
}
