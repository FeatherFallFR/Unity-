using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float time = 2;

    void Start()
    {
        Destroy(this.gameObject,time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
