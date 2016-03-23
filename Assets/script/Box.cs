using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var s = new Vector3(-Time.deltaTime * 0.5f,0,0);
       // transform.Translate(s);
	}

    void OnCollisionEnter(Collision collision) 
    {
        Debug.Log("collison=>"+collision.collider.name);
    }


    void OnTriggerEnter(Collider other)
    {
       // Destroy(other.gameObject);
        Debug.Log("trigger=>" + other.name); 
    }
}
