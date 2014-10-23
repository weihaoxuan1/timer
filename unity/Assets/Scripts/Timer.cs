using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    public int eachTime;
    int mainMin = 10;
    int mainSec = 0;
    float mSec = 0;
    float delta = 0;
    int currGroup = 1;
    bool ifStart = false;
    bool ifHide = false;
    bool ifTimeout = false;
    UILabel mainTime;
    UILabel deltaTime;
    UILabel group;

    GameObject zoomIn;
    GameObject zoomOut;
	// Use this for initialization
	void Start () {
        mainTime = transform.Find("MainTime").GetComponent<UILabel>();
        deltaTime = transform.Find("DeltaTime").GetComponent<UILabel>();
        group = transform.Find("Group").GetComponent<UILabel>();
        zoomIn = transform.Find("Zoomin").gameObject;
        zoomOut = transform.Find("Zoomout").gameObject;
        mainMin = eachTime;

        Flash();
	}

    /*void OnGUI()
    {
        if (true)//Application.platform != RuntimePlatform.Android)
        {
            if (GUI.Button(new Rect(110, 10, 300, 200), "start/stop"))
            {
                ifStart = !ifStart;
            }
            if (GUI.Button(new Rect(420, 10, 300, 200), "reset"))
            {
                mainMin = eachTime;
                mainSec = 0;
                mSec = 0;
                ifStart = false;
                Flash();
            }
            if (GUI.Button(new Rect(730, 10, 300, 200), "add"))
            {
                mainMin += 5;
                Flash();
            }
            if (GUI.Button(new Rect(1040, 10, 300, 200), "next"))
            {
                mainMin = 5;
                mainSec = 0;
                mSec = 0;
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
    }*/
	// Update is called once per frame
	void Update () {
        if (ifStart && !ifTimeout)
        {
            //Debug.Log(Time.deltaTime);
            mSec -= Time.deltaTime * 100;
            //Debug.Log(mSec);
            if (mSec < 0 )
            {
                mainSec--;
                mSec = 100 + mSec;
            }
            if (mainSec < 0)
            {
                if (mainMin - 1 < 0)
                {
                    mSec = 0;
                    mainSec = 0;
                    ifStart = false;
                    ifTimeout = true;
                    return;
                }
                mainMin--;
                mainSec = 59;
            }
            Flash();
        }
        if (ifTimeout)
        {
            Timeout();
        }
	}

    public void OnStartTimer()
    {
        audio.Play();
        ifStart = !ifStart;
    }

    void Flash()
    {
        if (!ifHide)
            mainTime.text = mainMin.ToString() + ":" +
                (mainSec < 10 ? "0" + mainSec.ToString() : mainSec.ToString()) + ":" +
                (mSec < 10 ? "0" + ((int)mSec).ToString() : ((int)mSec).ToString());
        else
            mainTime.text = mainMin.ToString() + ":" +
           (mainSec < 10 ? "0" + mainSec.ToString() : mainSec.ToString());

        int d = delta >= 0 ? (int)delta : (int)(0 - delta);
        deltaTime.text = (delta >= 0 ? "+" : "-") + 
            (d / 60).ToString() + ":" + 
            ((d % 60) < 10 ? "0" + ((d % 60)).ToString() : ((d % 60)).ToString());
        if (delta >= 0)
            deltaTime.color = new Color(0, 255, 0);
        else
            deltaTime.color = new Color(255, 0, 0);
    }

    void Timeout()
    {
        delta -= Time.deltaTime;
        Flash();
    }

    public void OnIncreaseTime()
    {
        delta -= 300;
        mainMin += 5;
        ifTimeout = false;
        audio.Play();
        Flash();
    }

    public void OnNextGroup()
    {
        delta += mainMin * 60 + mainSec;
        mainMin = eachTime;
        mainSec = 0;
        mSec = 0;
        ifStart = false;
        ifTimeout = false;
        
        currGroup++;
        group.text = "第" + currGroup.ToString() + "组";
        audio.Play();
        Flash();
    }

    public void OnChange_mSec()
    {
        ifHide = !ifHide;
        if (ifHide)
        {
            zoomIn.SetActive(true);
            zoomOut.SetActive(false);
        }
        else
        {
            zoomIn.SetActive(false);
            zoomOut.SetActive(true);
        }
        audio.Play();
        Flash();
    }

    public void OnReset()
    {
        mainMin = eachTime;
        mainSec = 0;
        mSec = 0;
        delta = 0;
        currGroup = 1;
        ifStart = false;
        ifTimeout = false;
        audio.Play();
        group.text = "第" + currGroup.ToString() + "组";
        Flash();
    }

    public void OnInputSubmit()
    {
        Debug.Log(eachTime);
        eachTime = int.Parse(GameObject.Find("InputEachTime/Label").gameObject.GetComponent<UILabel>().text);
        Debug.Log(eachTime);
        if(!ifStart && !ifTimeout)
            mainMin = eachTime;
        Flash();
    }

    
}
