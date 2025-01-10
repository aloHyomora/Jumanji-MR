using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOLObject : MonoBehaviour
{    
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
