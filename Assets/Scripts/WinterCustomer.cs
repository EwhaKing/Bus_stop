﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinterCustomer : MonoBehaviour
{
    public GameObject person;   //손님 오브젝트
    private List<GameObject> passengers = new List<GameObject>();   //손님 오브젝트 배열

    int[] ListOfNumPass;        //정류장 손님수 배열
    int NumOfPass;              //각 정류장마다 손님수를 저장할 변수

    float timeCount;
    string wheel;
    bool wheel1, wheel2, wheel3, wheel4;
    private bool eachtaken;     //손님 탑승 체크 변수
    private bool insign;        //버스 스탑 점선 안에 있는지 체크할 변수

    // Start is called before the first frame update
    void Start()
    {
        WinterAssign Cus = GameObject.Find("Map_Winter").GetComponent<WinterAssign>();
        ListOfNumPass = Cus.EachPass;        //정류장 랜덤 손님 수 배열 가져오기
        string name = this.gameObject.name;     //오브젝트 이름
        Vector3 basePos = transform.position;   //오브젝트 위치

        if (name == "BusStopSign1")
            NumOfPass = ListOfNumPass[0];
        else if (name == "BusStopSign2")
            NumOfPass = ListOfNumPass[1];
        else if (name == "BusStopSign3")
            NumOfPass = ListOfNumPass[2];
        else if (name == "BusStopSign4")
            NumOfPass = ListOfNumPass[3];

        timeCount = 4 * NumOfPass;      //각 바퀴 콜라이더마다 계산해서 4 곱해야 함


        for (int i = 0; i < NumOfPass; i++)
        {
            GameObject per = Instantiate(person, this.transform.position, Quaternion.identity);
            per.transform.parent = this.gameObject.transform;
            per.transform.localScale = new Vector3(0.03f, 0.006f, 0.03f);
            per.transform.localRotation = Quaternion.Euler(0, 90, 90);
            per.transform.localPosition = new Vector3(0.005f - 0.002f * i, 0.0065f, 0.0002f);
            //나중에 크기 변환 조절해야 함.
            passengers.Add(per);
        }

        wheel1 = false;
        wheel2 = false;
        wheel3 = false;
        wheel4 = false;
        eachtaken = true;
        insign = false;
    }


    void OnTriggerStay(Collider coll)
    {
        wheel = coll.gameObject.name;
        if (wheel == "BUS_wheelLB")
            wheel1 = true;
        else if (wheel == "BUS_wheelLF")
            wheel2 = true;
        else if (wheel == "BUS_wheelRB")
            wheel3 = true;
        else if (wheel == "BUS_wheelRF")
            wheel4 = true;

        if (wheel1 && wheel2 && wheel3 && wheel4)   // 네 바퀴가 모두 점선과 접촉해있을 때
        {
            insign = true;
            if (car.speed == 0)          // 버스 속도가 0이어야        
                if (timeCount > 0)
                    timeCount -= Time.deltaTime;
                else
                    timeCount = 4 * NumOfPass;

        }


        if (timeCount <= 0 && eachtaken)
        {
            eachtaken = false;

            //손님 오브젝트 삭제
            foreach (var child in passengers)
                Destroy(child.gameObject);

        }

    }

    void OnTriggerExit(Collider coll)
    {
        wheel = coll.gameObject.name;
        if (wheel == "BUS_wheelLB")
            wheel1 = false;
        else if (wheel == "BUS_wheelLF")
            wheel2 = false;
        else if (wheel == "BUS_wheelRB")
            wheel3 = false;
        else if (wheel == "BUS_wheelRF")
            wheel4 = false;

        if (!(wheel1 && wheel2 && wheel3 && wheel4))
        {
            insign = false;
            if (eachtaken)
                timeCount = 4 * NumOfPass;
        }
    }

    public bool Taken()     //손님 탑승 완료 반환 함수
    {
        return eachtaken;
    }

    public bool InSign()    //정류장 점선 안에 버스가 있는지를 반환하는 함수
    {
        return insign;
    }
}
