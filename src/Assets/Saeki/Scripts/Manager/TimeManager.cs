using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;//�}�l�[�W���[�N���X

    [SerializeField] 
    TextMeshProUGUI timeText;//�\���e�L�X�g

    int time;//���ԕϐ�

    public int Gettime => time;//���Ԃ̃Q�b�^�[

    // Start is called before the first frame update
    void Start()
    {
        time = 0;//������
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�{�X���v���C���[�����ʂ܂�
        if(gameManager.IsMainGameState)
            time++;//���Z

        //���Ԃ�Text�ɏo��
        timeText.text = time.ToString();
    }
}
