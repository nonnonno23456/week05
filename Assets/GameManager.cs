using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction;
    public int talkIndex;
    public Image portraitImg;

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj; // ✅ 변수명 대소문자 일치
        ObjData objData = scanObject.GetComponent<ObjData>();

        isAction = true; // ✅ 패널을 열겠다는 플래그를 먼저 true로 설정
        Talk(objData.id, objData.isNpc);
    
        talkPanel.SetActive(isAction); // ✅ 패널 열기
    }


    void Talk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);
        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            return;
        }

        if (isNpc)
        {

            talkText.text = talkData.Split(':')[0];
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkText.text = talkData;
            portraitImg.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;
    }
    


}