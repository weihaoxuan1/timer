using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    struct stat
    {
        public int et;
        public int ets;
        public int m;
        public int s;
        public float ms;
        public float de;
        public int cg;
        public bool ifst;
        public bool ifhi;
        public bool ifti;
        public bool ifse;
        public string mt;
        public string dt;
        public string gr;
        public void set(int zet, int zets, int zm, int zs, float zms, float zde, int zcg, bool zifst, bool zifhi, bool zifti, bool zifse, string zmt, string zdt, string zgr)
        {
            et = zet; ets = zets; m = zm; s = zs; ms = zms; de = zde; cg = zcg; ifst = zifst; ifhi = zifhi; ifti = zifti; ifse = zifse; mt = zmt; dt = zdt; gr = zgr;
        }
    };

    stat[] done;
    int pDone = 0;

    public int eachTime;
    public int eachTimeSec;
    int mainMin = 10;
    int mainSec = 0;
    float mSec = 0;
    float delta = 0;
    int currGroup = 1;
    bool ifStart = false;
    bool ifHide = false;
    bool ifTimeout = false;
    bool ifSetting = false;
    UILabel mainTime;
    UILabel deltaTime;
    UILabel group;

    public AudioClip buttSound;
    public AudioClip ringSound;

    GameObject zoomIn;
    GameObject zoomOut;
    GameObject settingUI;
	// Use this for initialization
	void Start () {
        mainTime = transform.Find("MainTime").GetComponent<UILabel>();
        deltaTime = transform.Find("DeltaTime").GetComponent<UILabel>();
        group = transform.Find("Group").GetComponent<UILabel>();
        zoomIn = transform.Find("Zoomin").gameObject;
        zoomOut = transform.Find("Zoomout").gameObject;
        settingUI = transform.Find("SettingUI").gameObject;
        mainMin = eachTime;
        done = new stat[10];

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
                    audio.PlayOneShot(ringSound);
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
        if (ifSetting) return;
        audio.PlayOneShot(buttSound);
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
        if (ifSetting) return;
        Remember();
        delta -= 300;
        mainMin += 5;
        ifTimeout = false;
        audio.PlayOneShot(buttSound);
        Flash();
    }

    public void OnNextGroup()
    {
        if (ifSetting) return;
        Remember();
        delta += mainMin * 60 + mainSec;
        mainMin = eachTime;
        mainSec = eachTimeSec;
        mSec = 0;
        ifStart = false;
        ifTimeout = false;
        
        currGroup++;
        group.text = "第" + currGroup.ToString() + "组";
        audio.PlayOneShot(buttSound);
        /*Debug.Log("eachTimer =" + eachTime +
            "\n eachTimeSec =" + eachTimeSec +
            "\n mainMin =" + mainMin +
            "\n mainSec =" + mainSec +
            "\n mSec =" + mSec +
            "\n delta =" + delta +
            "\n currGroup =" + currGroup +
            "\n ifStart =" + ifStart +
            "\n ifHide =" + ifHide +
            "\n ifTimeout =" + ifTimeout +
            "\n ifSetting =" + ifSetting +
            "\n mainTime =" + mainTime.text +
            "\n deltaTime =" + deltaTime.text +
            "\n group =" + group.text);*/
        Flash();
    }

    public void OnChange_mSec()
    {
        if (ifSetting) return;
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
        audio.PlayOneShot(buttSound);
        Flash();
    }

    public void OnReset()
    {
        if (ifSetting) return;
        Remember();
        mainMin = eachTime;
        mainSec = eachTimeSec;
        mSec = 0;
        delta = 0;
        currGroup = 1;
        ifStart = false;
        ifTimeout = false;
        audio.PlayOneShot(buttSound);
        group.text = "第" + currGroup.ToString() + "组";
        Flash();
    }

    public void OnInputSubmit()
    {
        //audio.PlayOneShot(buttSound);
        //Debug.Log(eachTime);
        Remember();
        int s = int.Parse(GameObject.Find("InputEachTime_Sec/Label").gameObject.GetComponent<UILabel>().text);
        int m = int.Parse(GameObject.Find("InputEachTime_Min/Label").gameObject.GetComponent<UILabel>().text);
        if (m > 59) m = 59;
        GameObject.Find("InputEachTime_Min/Label").gameObject.GetComponent<UILabel>().text = m.ToString();
        if (s > 59) s = 59;
        GameObject.Find("InputEachTime_Sec/Label").gameObject.GetComponent<UILabel>().text = s<10?"0" + s.ToString():s.ToString();
        eachTime = m;
        eachTimeSec = s;
        //Debug.Log(eachTime);
        if (!ifStart && !ifTimeout && mSec == 0)
        {
            mainMin = eachTime;
            mainSec = eachTimeSec;
        }
        Flash();
    }

    public void OnSetting()
    {
        audio.PlayOneShot(buttSound);
        if (ifSetting)
        {
            ifSetting = false;
            settingUI.SetActive(false);
        }
        else
        {
            ifSetting = true;
            settingUI.SetActive(true);
        }
    }

    public void OnSettingCancel()
    {
        audio.PlayOneShot(buttSound);
        settingUI.SetActive(false);
    }

    void Remember()
    {
        /*Debug.Log("before remember , pDone = " + pDone%10);*/
        //Debug.Log(done[0].gr);
        done[pDone % 10].set(
            eachTime,
            eachTimeSec,
            mainMin,
            mainSec,
            mSec,
            delta,
            currGroup,
            ifStart, 
            ifHide, 
            ifTimeout, 
            ifSetting,
            mainTime.text,
            deltaTime.text, 
            group.text);
        pDone++;
        /*Debug.Log("remembered , pDone = " + pDone);
        Debug.Log("eachTimer =" + eachTime +
            "\n eachTimeSec =" + eachTimeSec +
            "\n mainMin =" + mainMin +
            "\n mainSec =" + mainSec +
            "\n mSec =" + mSec +
            "\n delta =" + delta +
            "\n currGroup =" + currGroup +
            "\n ifStart =" + ifStart +
            "\n ifHide =" + ifHide +
            "\n ifTimeout =" + ifTimeout +
            "\n ifSetting =" + ifSetting +
            "\n mainTime =" + mainTime.text +
            "\n deltaTime =" + deltaTime.text +
            "\n group =" + group.text);*/
    }

    public void OnUndo()
    {
        if (pDone <= 0) return;
        pDone--;
        Debug.Log("after undo , pDone = " + pDone % 10);
        audio.PlayOneShot(buttSound);
        eachTime = done[pDone % 10].et;
        eachTimeSec = done[pDone % 10].ets;
        mainMin = done[pDone % 10].m;
        mainSec = done[pDone % 10].s;
        mSec = done[pDone % 10].ms;
        delta = done[pDone % 10].de;
        currGroup = done[pDone % 10].cg;
        ifStart = done[pDone % 10].ifst;
        ifHide = done[pDone % 10].ifhi;
        ifTimeout = done[pDone % 10].ifti;
        ifSetting = done[pDone % 10].ifse;
        mainTime.text = done[pDone % 10].mt;
        deltaTime.text = done[pDone % 10].dt;
        group.text = done[pDone % 10].gr;
        /*Debug.Log("eachTimer =" + eachTime +
            "\n eachTimeSec =" + eachTimeSec +
            "\n mainMin =" + mainMin +
            "\n mainSec =" + mainSec +
            "\n mSec =" + mSec +
            "\n delta =" + delta +
            "\n currGroup =" + currGroup +
            "\n ifStart =" + ifStart +
            "\n ifHide =" + ifHide +
            "\n ifTimeout =" + ifTimeout +
            "\n ifSetting =" + ifSetting +
            "\n mainTime =" + mainTime.text +
            "\n deltaTime =" + deltaTime.text +
            "\n group =" + group.text);*/
        Flash();
    }
}
