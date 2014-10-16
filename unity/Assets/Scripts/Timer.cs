using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    int fen = 30;
    int miao = 0;
    float haomiao = 0;
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
        if (true)//Application.platform != RuntimePlatform.Android)
        {
            if (GUI.Button(new Rect(110, 10, 300, 200), "start/stop"))
            {
                ifStart = !ifStart;
            }
            if (GUI.Button(new Rect(420, 10, 300, 200), "reset"))
            {
                fen = 30;
                miao = 0;
                haomiao = 0;
                ifStart = false;
                Flash();
            }
            if (GUI.Button(new Rect(730, 10, 300, 200), "add"))
            {
                fen += 5;
                Flash();
            }
            if (GUI.Button(new Rect(1040, 10, 300, 200), "next"))
            {
                fen = 5;
                miao = 0;
                haomiao = 0;
                ifStart = false;
                Flash();
            }
            if (GUI.Button(new Rect(1350, 10, 300, 200), "hide/show"))
            {
                ifHide = !ifHide;
                Flash();
            }
            if (GUI.Button(new Rect(1620, 880, 300, 200), "quit"))
                OnQuit();
        }
    }
	// Update is called once per frame
	void Update () {
        if (ifStart)
        {
            //Debug.Log(Time.deltaTime);
            haomiao -= Time.deltaTime * 100;
            //Debug.Log(haomiao);
            if (haomiao < 0 )
            {
                miao--;
                haomiao = 100 + haomiao;
            }
            if (miao < 0)
            {
                if (fen - 1 < 0)
                {
                    ifStart = false;
                    return;
                }
                fen--;
                miao = 59;
            }
            Flash();
        }
	}

    public void startButton()
    {
        ifStart = !ifStart;
    }

    void OnQuit()
    {
        Application.Quit();
    }

    void Flash()
    {
        if (!ifHide)
            time.text = fen.ToString() + ":" +
                (miao < 10 ? "0" + miao.ToString() : miao.ToString()) + ":" +
                (haomiao < 10 ? "0" + ((int)haomiao).ToString() : ((int)haomiao).ToString());
        else
            time.text = fen.ToString() + ":" +
           (miao < 10 ? "0" + miao.ToString() : miao.ToString());
    }
}
