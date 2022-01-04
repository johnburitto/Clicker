using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColector : MonoBehaviour
{
    [SerializeField]
    private GameObject _cZone;

    void Start()
    {
        _cZone.gameObject.GetComponent<ClicableZone>().Enemy = transform.gameObject.GetComponent<Enemy>();
    }

    void Update()
    {
        
    }
}
