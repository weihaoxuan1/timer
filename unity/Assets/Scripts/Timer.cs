using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    int fen = 30;
    int miao = 0;
    int haomiao = 0;
    float delta=0;
    bool ifStart = false;
    bool ifHide = false;
    UILabel time;
	// Use this for initialization
	void Start () {
        time = GetComponent<UILabel>();
	}

    void OnGUI()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            if (GUI.Button(new Rect(110, 10, 150, 100), "start"))
                ifStart = true;
            if (GUI.Button(new Rect(310, 10, 150, 100), "stop"))
                ifStart = false;
            if (GUI.Button(new Rect(510, 10, 150, 100), "add"))
                fen += 5;
            if (GUI.Button(new Rect(710, 10, 150, 100), "next"))
            {
                fen = 5;
                miao = 0;
            }
            if (GUI.Button(new Rect(110, 110, 150, 100), "hide/show"))
                ifHide = !ifHide;
        }
    }
	// Update is called once per frame
	void Update () {
        if (ifStart)
        {
            
            haomiao -= (int)(Time.deltaTime * 100);
            if (haomiao < 0 )
            {
                miao--;
                haomiao = 100 + haomiao;
            }
            if (miao < 0)
            {
                fen--;
                miao = 59;
            }
            if (!ifHide)
                time.text = fen.ToString() + ":" +
                    (miao < 10 ? "0" + miao.ToString() : miao.ToString()) + ":" +
                    (haomiao < 10 ? "0" + haomiao.ToString() : haomiao.ToString());
            else
                time.text = fen.ToString() + ":" +
               (miao < 10 ? "0" + miao.ToString() : miao.ToString());
        }
	}

    public void startButton()
    {
        Debug.Log("a");
        ifStart = !ifStart;
    }
}
