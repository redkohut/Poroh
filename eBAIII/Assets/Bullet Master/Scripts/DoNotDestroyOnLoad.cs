using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroyOnLoad : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        GameObject[] mmusicObj = GameObject.FindGameObjectsWithTag("GameMusic");
        if (mmusicObj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
