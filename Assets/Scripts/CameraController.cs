using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // 플레이어 오브젝트를 찾기 위해서 멤버 변수 선언
    GameObject m_Player = null;
    // 플레이어가  이동할 때마다 카메라가 따라다니도록 플레이어 X,Y좌표 저장 변수
    [SerializeField]
    Vector3 vPlayerPositon = Vector3.zero;

    void Start()
    {
        // 플레이어 오브젝트를 찾아서 멤버 변수에 저장 
        m_Player = GameObject.Find("Pink");
    }

    void Update()
    {
        // 플레이어의 위치를 가져옴
        vPlayerPositon = m_Player.transform.position;
        // 카메라의 위치를 플레이어의 X와 Y 위치에 맞추고, 카메라의 원래 Z 위치를 유지
        transform.position = new Vector3(vPlayerPositon.x, vPlayerPositon.y, transform.position.z);
    }
}