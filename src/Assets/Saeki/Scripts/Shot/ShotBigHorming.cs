using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ShotBigHorming : ShotPatarnBase
{
    [SerializeField] 
    GameObject bulletSmall;
    [SerializeField] 
    GameObject bulletLarge;

    [SerializeField] 
    float aimValue = 10f;

    [SerializeField] 
    float patarnReportTimeSpan = 0.2f;

    [SerializeField] 
    int patarnReportValue = 5;

    public override void PatarnPlay(Transform enemyTransform)
    {
        Vector3 target = GameManager.Getplayer.transform.position;

        //����Ɍ������Ē����ɔ��˂���p�^�[��
        MainPatarnPlay(target, enemyTransform);
        //����ȊO�̕����ɐ�`�ɔ��˂���p�^�[��
        OtherPatarnDelay(target, enemyTransform);
    }

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



    async void OtherPatarnDelay(Vector3 target, Transform enemyTransform) 
    {
        var token = this.GetCancellationTokenOnDestroy();

        //����Delay��������ɁA���˃p�^�[�����N��
        for (int i = 0;i < patarnReportValue; i++)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(patarnReportTimeSpan), cancellationToken: token);
            OtherPatarnPlay(target, enemyTransform);
        }
    }

    void OtherPatarnPlay(Vector3 target, Transform enemyTransform)
    {
        //�v���C���[�̕����ȊO�̕����ɂ΂�T������3�`33���w��
        for (int i = 3; i <= 33; i++)
        {
            if (enemyTransform == null)
                break;

            //��ƂȂ�������v�Z
            Vector2 dirTarget = target - enemyTransform.position;

            //�I�u�W�F�N�g����
            GameObject bullet = Instantiate(bulletSmall, enemyTransform.position, Quaternion.identity);

            //Rigidbody2D�擾
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

            //���˕����v�Z
            float angleRadians = (aimValue * i) * Mathf.Deg2Rad;
            Vector2 rotate = Quaternion.Euler(Vector3.forward * angleRadians) * dirTarget.normalized;

            //���˕������
            bulletRB.velocity = rotate;
        }
    }
}
