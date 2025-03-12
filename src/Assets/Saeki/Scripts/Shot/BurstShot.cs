using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BurstShot : ShotPatarnBase
{
    [SerializeField] 
    GameObject bulletPrehab;

    [SerializeField]
    float patarnReportTimeSpan = 0.2f;

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
        if (enemyTransform == null)
            return;

        //�v���C���[�ɔ�΂��������v�Z
        Vector2 dirTarget = target - enemyTransform.position;

        //�I�u�W�F�N�g����
        GameObject bullet = Instantiate(bulletPrehab, enemyTransform.position, Quaternion.identity);

        //Rigidbody2D�擾
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

        //�m�[�}���C�Y
        Vector2 rotate = dirTarget.normalized;

        //���˕������
        bulletRB.velocity = rotate;

    }
}
