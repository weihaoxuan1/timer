using UnityEngine;
using System.Collections;

public class ResetTween : MonoBehaviour {

    TweenAlpha ta;
	// Use this for initialization
	void Start () {
        ta = GetComponent<TweenAlpha>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Reset()
    {
        ta.ResetToBeginning();
        gameObject.SetActive(false);
    }
}
