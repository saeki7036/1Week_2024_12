using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
public class AllForwardShot : ShotPatarnBase
{
    [SerializeField] 
    GameObject bulletSmall;

    [SerializeField] 
    float aimValue = 10f;

    [SerializeField]
    float patarnReportTimeSpan = 0.3f;
    
    [SerializeField]
    int patarnReportValue = 3;

    public override void PatarnPlay(Transform enemyTransform)
    {
        Vector3 target = GameManager.Getplayer.transform.position;
        OtherPatarnDelay(target, enemyTransform);
    }

    async void OtherPatarnDelay(Vector3 target, Transform enemyTransform)
    {
        var token = this.GetCancellationTokenOnDestroy();

        //����Delay��������ɁA���˃p�^�[�����N��
        for (int i = 0; i < patarnReportValue; i++)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(patarnReportTimeSpan), cancellationToken: token);
            OtherPatarnPlay(target, enemyTransform);
        }
    }

    void OtherPatarnPlay(Vector3 target, Transform enemyTransform)
    {
        //10�x������
        for (int i = 0; i <= 36; i++)
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
