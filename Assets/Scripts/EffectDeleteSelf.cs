using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDeleteSelf : MonoBehaviour
{
    public bool remove = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (remove)
        {
            Destroy(gameObject);
        }
    }
}
