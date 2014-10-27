using UnityEngine;
using System.Collections;

public class FormalUI : MonoBehaviour {

    public int mainMin = 0;
    public int mainSec = 5;
    bool ifNext = false;
    float mSec = 0;
    bool ifStart = false;
    bool ifHide = false;

    UILabel hide;
    UILabel mainTime;
    UILabel stage;
    GameObject next;

    public AudioClip buttSound;
    public AudioClip ringSound;

	// Use this for initialization
	void Start () {
        mainTime = transform.Find("MainTime").GetComponent<UILabel>();
        stage = transform.Find("Stage").GetComponent<UILabel>();
        hide = transform.Find("Hide").GetComponent<UILabel>();
        next = transform.Find("Next").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if(mainMin == 2 && mainSec == 0 && mSec <= 2 && stage.text.Equals("说明阶段"))
            audio.PlayOneShot(ringSound);

        if (ifStart)
        {
            //Debug.Log(Time.deltaTime);
            mSec -= Time.deltaTime * 100;
            //Debug.Log(mSec);
            if (mSec < 0)
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
                    mSec = 0;
                    ifStart = false;
                    audio.PlayOneShot(ringSound);
                    return;
                }
                mainMin--;
                mainSec = 59;
            }
            Flash();
        }
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
    }

    public void OnReset()
    {
        next.SetActive(true);
        ifNext = false;
        ifStart = false;
        mainMin = 10;
        mainSec = 0;
        mSec = 0;
        audio.PlayOneShot(buttSound);
        stage.text = "说明阶段";
        Flash();
    }

    public void OnStart()
    {
        audio.PlayOneShot(buttSound);
        ifStart = !ifStart;
    }

    public void OnNext()
    {
        next.SetActive(false);
        ifNext = true;
        stage.text = "提问阶段";
        ifStart = false;
        mainMin = 10;
        mainSec = 0;
        mSec = 0;
        audio.PlayOneShot(buttSound);
        Flash();
    }

    public void OnChange_mSec()
    {
        ifHide = !ifHide;
        if (ifHide)
            hide.text = "显示毫秒";
        else
            hide.text = "隐藏毫秒";
        audio.PlayOneShot(buttSound);
        Flash();
    }
}
