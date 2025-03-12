using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RandomShot : ShotPatarnBase
{
    [SerializeField] 
    GameObject bulletSmall;

    [SerializeField] 
    float aimValue = 560f;

    [SerializeField] 
    float patarnReportTimeSpan = 0.1f;

    [SerializeField]
    int patarnReportValue = 50;
    
    [SerializeField]
    int oneSetBulletCount = 3;

    public override void PatarnPlay(Transform enemyTransform)
    {
        OtherPatarnDelay(enemyTransform);
    }

    async void OtherPatarnDelay(Transform enemyTransform)
    {
        var token = this.GetCancellationTokenOnDestroy();

        //����Delay��������ɁA���˃p�^�[�����N��
        for (int i = 0; i < patarnReportValue; i++)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(patarnReportTimeSpan), cancellationToken: token);
            OtherPatarnPlay(enemyTransform);
        }
    }

    void OtherPatarnPlay(Transform enemyTransform)
    {
        if (GameManager.Getplayer == null) 
            return;

        if (enemyTransform == null)
            return;

        Vector3 target = GameManager.Getplayer.transform.position;

        //���ˉ񐔕����[�v
        for (int i = 0; i < oneSetBulletCount; i++)
        {
            //��ƂȂ�������v�Z
            Vector2 dirTarget = target - enemyTransform.position;

            //�I�u�W�F�N�g����
            GameObject bullet = Instantiate(bulletSmall, enemyTransform.position, Quaternion.identity);

            //Rigidbody2D�擾
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

            //�����_���ɕ������w��
            float angleRadians = (aimValue * UnityEngine.Random.Range(0,37)) * Mathf.Deg2Rad;

            //���˕����v�Z
            Vector2 rotate = Quaternion.Euler(Vector3.forward * angleRadians) * dirTarget.normalized;

            //���˕������
            bulletRB.velocity = rotate;
        }
    }
}
