using UnityEngine;
using System.Collections;

public class ChangeMode : MonoBehaviour {

    public GameObject formalMode;
    public GameObject gang4Mode;
    bool ifFormal = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnChangeMode()
    {
        if (ifFormal)
        {
            formalMode.SetActive(false);
            gang4Mode.SetActive(true);
            ifFormal = !ifFormal;
        }
        else
        {
            formalMode.SetActive(true);
            gang4Mode.SetActive(false);
            ifFormal = !ifFormal;
        }
    }
}
