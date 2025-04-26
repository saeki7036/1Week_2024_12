using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    TimeManager timeManager;//���ԊǗ��N���X

    [SerializeField]
    TimeTible[] timeTibles;//�^�C���e�[�u��

    int maxIndex;//�ݒ�^�C���e�[�u����

    int currentIndex;//���݂̃^�C���e�[�u��

    int nextSpownTime;//���̃X�|�[������

    void Start()
    {
        //�ݒ�^�C���e�[�u����
        maxIndex = timeTibles.Length;

        //�����l�ɐݒ�
        currentIndex = 0;

        //�^�C���e�[�u������łȂ����
        if (timeTibles.Length != 0)
            nextSpownTime = timeTibles[0].GetSpowntime;//�^�C���e�[�u���̋N�����Ԃ̍ŏ�������
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�N������
        if(SpownCheck(timeManager.Gettime) == true)
        {
            //��������
            EnemySpown();

            //���^�C���e�[�u����ݒ�
            SetNextTimeTible();
        }
               
    }

    //�X�|�[������
    bool SpownCheck(int time)
    {
        //�Ō�̃^�C���e�[�u���Ȃ�
        if (currentIndex >= maxIndex) return false;

        //���Ԃ��X�|�[�����Ԃ�菬�����Ȃ�
        if (time <= nextSpownTime) return false;

        //����ȊO�Ȃ�
        return true;
    }

    //��������
    void EnemySpown()
    {
        //�����񐔎擾
        int length = timeTibles[currentIndex].GetInfomationLength;

        for(int i = 0; i < length; i++)
        {
            //�\���̏��擾
            var tible = timeTibles[currentIndex].GetSpownInfomation(i);
            //�G����
            Instantiate(tible.Enemy, tible.Point.position, Quaternion.identity);
        } 
    }

    //�^�C���e�[�u���X�V
    void SetNextTimeTible()
    {
        //���̃^�C���e�[�u����
        currentIndex++;

        //�Ō�̃^�C���e�[�u���Ȃ�return
        if (currentIndex >= maxIndex) return;

        //�X�|�[�����Ԃ��擾
        nextSpownTime = timeTibles[currentIndex].GetSpowntime;
    }
}
