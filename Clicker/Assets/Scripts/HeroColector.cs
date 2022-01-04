using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroColector : MonoBehaviour
{
    [SerializeField]
    private GameObject _cZone;
    private List<Hero> _heroes;

    void Start()
    {
        _heroes = new List<Hero>(); 

        foreach (Transform child in transform)
        {
            if (child.gameObject.layer == 6)
            {
                _heroes.Add(child.gameObject.GetComponent<Hero>());
            }
        }

        _cZone.gameObject.GetComponent<ClicableZone>().Heroes = _heroes;
    }

    void Update()
    {

    }
}
