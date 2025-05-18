using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;
    
    public Sprite[] portraitArr;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] { "안녕?:0", "이 곳에 처음 왔구나?:1" });
        talkData.Add(2000, new string[] { "여어:1", "이 곳에 처음 왔구나?:0" });
        talkData.Add(100, new string[] { "평범한 꽃이다" });
        talkData.Add(200, new string[] { "평범한 나무다" });
        talkData.Add(300, new string[] { "평범한 집이다" });
        
        //Quest Talk
        talkData.Add(10 + 1000, new string[] { "어서와:0", "이 마을에는 놀라운 전설이 있다는데:1", "오른쪽 호수쪽에 루도가 알려줄꺼야.:0" });
        talkData.Add(11 + 2000, new string[] { "여어...:1", "이 이 호수의 전설을 들으러 온거야?:0", "그럼 일 좀 하나 해주면 좋을텐데.....:1","내 집 근처에 떨어진 동전 좀 주워줬으면 하는데:0" });
        talkData.Add(20 + 1000, new string[] {
            "루도의 동전?:1",
            "돈을 흘리고 다니면 못쓰지!:3",
            "나중에 루도에게 한마디 해야겠어..:3"
        });

        talkData.Add(20 + 2000, new string[] {
            "찾으면 꼭 좀 가져다 줘.:1"
        });

        talkData.Add(20 + 5000, new string[] {
            "근처에서 동전을 찾았다."
        });

        talkData.Add(21 + 2000, new string[] {
            "엇, 찾아줘서 고마워.:2"
        });

        
        //Portrait Data
        //0:Normal, 1:Speak, 2:Happy, 3:Angry
        portraitData.Add (1000 + 0, portraitArr[0]);
        portraitData.Add (1000 + 1, portraitArr[1]);
        portraitData.Add (1000 + 2, portraitArr[2]);
        portraitData.Add (1000 + 3, portraitArr[3]);
        portraitData.Add (2000 + 0, portraitArr[4]);
        portraitData.Add (2000 + 1, portraitArr[5]);
        portraitData.Add (2000 + 2, portraitArr[6]);
        portraitData.Add (2000 + 3, portraitArr[7]);
        
        
        
        
        
    }
    public string GetTalk(int id, int talkIndex)
    {
        if (!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10)) 
                return GetTalk(id - id % 100, talkIndex); //Get First Talk

            else
                return GetTalk(id - id % 10, talkIndex); //Get First Quest Talk
        }

        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    
    public Sprite GetPortrait(int id, int portraitIndex)

    {
        return portraitData[id + portraitIndex];
    }


}
