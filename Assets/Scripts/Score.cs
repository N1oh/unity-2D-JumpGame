using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text text;              // 텍스트 컴포넌트를 참조하기 위한 변수
    public static int coinAmount;    // 동전 개수를 저장하기 위한 변수

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();    // 자신의 게임 오브젝트에서 Text 컴포넌트를 가져옴
    }

    // Update is called once per frame
    void Update()
    {
        text.text = coinAmount.ToString();    // 동전 개수를 문자열로 변환하여 텍스트에 표시
    }
}