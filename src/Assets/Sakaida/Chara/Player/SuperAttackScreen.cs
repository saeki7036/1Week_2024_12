using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAttackScreen : SuperAttackBase
{
    [SerializeField] 
    GameObject Black;//��������I�u�W�F�N�g

    [SerializeField] 
    int screenValue = 20;//�X�N���[����

    [SerializeField] 
    float interval = 0.03f;//�x���C���^�[�o��

    [SerializeField]
    float DestroyTime = 0.8f;//�j�󎞊�

    [SerializeField]
    float minPosX = -8.5f;//x�͈͍ŏ��l

    [SerializeField]
    float maxPosX = 0f;//x�͈͍ő�l

    [SerializeField]
    float minPosY = -5f;//y�͈͍ŏ��l

    [SerializeField]
    float maxPosY = 5f;//y�͈͍ő�l

    //�����_���ɓ���͈͂���擾
    Vector2 randomPos => new Vector2(
        UnityEngine.Random.Range(minPosX, maxPosX),
        UnityEngine.Random.Range(minPosY, maxPosY));
    
    //�K�E�Z�N��
    public override void PlaySuperAttack()
    {
        //UniTask�񓯊������N��
        //�ʏ�U����Screen���Ԃ���΂�����
        TimeToAttack();
    }

    //�K�E�Z�̏���
    async void TimeToAttack()
    {
        //UniTask�p�g�[�N��
        var token = this.GetCancellationTokenOnDestroy();

        //����������X�N���[��������
        for (int i = 0; i < screenValue; i++)
        {
            //�C���^�[�o���x��
            await UniTask.Delay(TimeSpan.FromSeconds(interval), cancellationToken: token);

            //�I�u�W�F�N�g����
            GameObject CL_Black = Instantiate(Black);

            //�I�u�W�F�N�g�ʒu�ύX
            CL_Black.transform.position = randomPos;

            //Destroy�ō폜����
            Destroy(CL_Black, DestroyTime);
        }
    }
}
