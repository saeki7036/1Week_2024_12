using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;
    [SerializeField] TextMeshProUGUI timeText;
    int time;

    public int Gettime => time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�{�X���v���C���[�����ʂ܂�
        if(gameManager.IsMainGameState)
            time++;

        //���Ԃ�Text�ɏo��
        timeText.text = time.ToString();
       
    }
}
