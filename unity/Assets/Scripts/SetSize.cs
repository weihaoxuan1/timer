using UnityEngine;
using System.Collections;

public class SetSize : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UITexture bg = transform.GetComponent<UITexture>();
        bg.width = Screen.width;
        bg.height = Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
