using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    [Header("\tSpeed Controller")]
    [Space(6)]
    [SerializeField] private float speedCloud = 2f;
    
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speedCloud);
        if (transform.position.x > 25.0f)
        {
            Destroy(gameObject);
        }
    }

}
