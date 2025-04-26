using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBigHorming : ShotPatarnBase
{
    [SerializeField] 
    GameObject bulletSmall;//Bullet�̃I�u�W�F�N�g(�������e)

    [SerializeField] 
    GameObject bulletLarge;//Bullet�̃I�u�W�F�N�g(�傫���e)

    [SerializeField] 
    float aimValue = 560f;//�����v�Z���x

    [SerializeField] 
    float patarnReportTimeSpan = 0.2f;//���ˊԊu�̒x������

    [SerializeField] 
    int patarnReportValue = 5;//���˃p�^�[���̋N����

    //�v���C���[�̕����ȊO�̕����ɂ΂�T������3�`33���w��
    //O�`2�y��34�`36�̊Ԃ̓v���C���[�̕����ɂȂ邽�ߔ��˂����Ȃ�
    const int MinBackValue = 3,
              MaxBackValue = 33;

    //�S�̂̔��ˏ���
    public override void PatarnPlay(Transform enemyTransform)
    {
        //���ˑΏۂ̈ʒu���擾
        Vector3 target = GameManager.Getplayer.transform.position;

        //����Ɍ������Ē����ɔ��˂���p�^�[���N��
        MainPatarnPlay(target, enemyTransform);

        //UniTask�񓯊������N��
        //����ȊO�̕����ɐ�`�ɔ��˂���p�^�[���N��
        OtherPatarnDelay(target, enemyTransform);
    }

    //���ˏ���(�v���C���[��1����)
    void MainPatarnPlay(Vector3 target, Transform enemyTransform)
    {
        //�v���C���[�ɔ�΂��������v�Z
        Vector2 dirTarget = target - enemyTransform.position;

        //�I�u�W�F�N�g����
        GameObject bullet = Instantiate(bulletLarge, enemyTransform.position, Quaternion.identity);

        //Rigidbody2D�擾
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

        //�m�[�}���C�Y
        Vector2 rotate = Quaternion.Euler(Vector3.forward) * dirTarget.normalized;

        //���˕������
        bulletRB.velocity = rotate;
    }

    //���ˏ����̒x������
    async void OtherPatarnDelay(Vector3 target, Transform enemyTransform) 
    {
        //UniTask�p�g�[�N��
        var token = this.GetCancellationTokenOnDestroy();

        //����Delay��������ɁA���˃p�^�[�����N��
        for (int i = 0;i < patarnReportValue; i++)
        {
            //�C���^�[�o���x��
            await UniTask.Delay(TimeSpan.FromSeconds(patarnReportTimeSpan), cancellationToken: token);

            //���ˏ����N��
            OtherPatarnPlay(target, enemyTransform);
        }
    }

    //���ˏ���(�v���C���[����Ȃ������ɐ��ɔ���)
    void OtherPatarnPlay(Vector3 target, Transform enemyTransform)
    {
        //null�`�F�b�N
        if (enemyTransform == null)
            return;

        //�v���C���[�̕����ȊO�̕����ɂ΂�T������3�`33���w��
        for (int i = MinBackValue; i <= MaxBackValue; i++)
        {
            //��ƂȂ�������v�Z
            Vector2 dirTarget = target - enemyTransform.position;

            //�I�u�W�F�N�g����
            GameObject bullet = Instantiate(bulletSmall, enemyTransform.position, Quaternion.identity);

            //Rigidbody2D�擾
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

            //���˕����v�Z
            float angleRadians = (aimValue * i) * Mathf.Deg2Rad;

            //���˕����v�Z
            Vector2 rotate = Quaternion.Euler(Vector3.forward * angleRadians) * dirTarget.normalized;

            //���˕������
            bulletRB.velocity = rotate;
        }
    }
}
