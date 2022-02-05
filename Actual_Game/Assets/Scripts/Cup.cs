using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    internal List<Transform> _segments;
    public Transform cupsPrefub;
    void Start()
    {
        _segments = new List<Transform>();
        _segments.Add(transform);

        for (int i = 1; i < 3; i++)
        {
            Transform segment = Instantiate(cupsPrefub);
            segment.transform.parent = transform.parent;

            Vector3 pos = _segments[0].position;
            pos.x += i;
            segment.position = pos;

            _segments.Add(segment);
            Debug.Log(_segments.Count);
        }
        
    }
    
}
