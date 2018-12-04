using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour {
    float m_time = 8.0f / 12.0f;
    float m_age = 0.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        m_age += Time.deltaTime;
        if (m_age > m_time)
        {
            Destroy(gameObject);
        }
	}
}
