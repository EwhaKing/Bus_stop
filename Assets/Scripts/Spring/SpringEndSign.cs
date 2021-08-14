using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SpringEndSign : MonoBehaviour
{
    public GameObject result;  //��� �˾�â
    public Timer timer;        //�ð� ��������
    public TextMeshProUGUI NumOfCus;      //�˾�â �մ� ��
    public TextMeshProUGUI Time;          //�˾�â �ð�
    public Image Face;         //�˾�â ������ �̹���
    
    int comfortNum;     //������ 1(����), 0(����), -1(����)

    string wheel;
    bool wheel1, wheel2, wheel3, wheel4;

    void Start()
    { 
        wheel1 = false;
        wheel2 = false;
        wheel3 = false;
        wheel4 = false;
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

        if (wheel1 && wheel2 && wheel3 && wheel4)
        {
            NumOfCus.text = SpringTotal.SumOfCus.ToString();      //���� ��ũ��Ʈ��
            timer.TimerPause();         //�ð� ����
            Time.text = timer.GetTime();    //�ð� �˾�â
            SetResultFace();
            result.SetActive(true);     //���� �˾�â ��Ÿ��

            //��� ������Ʈ
            BestScore.UpdateSpring(comfortNum, timer.GetMin(), timer.GetSec(), SpringTotal.SumOfCus);
        }
    }

    void SetResultFace()
    {
        int comfort = SpringComfort.comfort;

        if (comfort >= 80)
        {
            Face.sprite = Resources.Load<Sprite>("UI/���_����");
            comfortNum = 1;
        }
        else if (comfort >= 40)
        {
            Face.sprite = Resources.Load<Sprite>("UI/���_����");
            comfortNum = 0;
        }
        else
        {
            Face.sprite = Resources.Load<Sprite>("UI/���_����");
            comfortNum = -1;
        }
    }
}