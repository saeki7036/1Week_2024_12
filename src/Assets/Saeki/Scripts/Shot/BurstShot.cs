using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstShot : ShotPatarnBase
{
    [SerializeField] 
    GameObject bulletPrehab;//Bullet�̃I�u�W�F�N�g

    [SerializeField]
    float patarnReportTimeSpan = 0.2f;//���ˊԊu�̒x������

    [SerializeField]
    int patarnReportValue = 3;//���˃p�^�[���̋N����

    //���ˏ���
    public override void PatarnPlay(Transform enemyTransform)
    {
        //���ˑΏۂ̈ʒu���擾
        Vector3 target = GameManager.Getplayer.transform.position;

        //UniTask�񓯊������N��
        //���ˏ����̒x�������̋N��
        PatarnDelay(target, enemyTransform);
    }

    //���ˏ����̒x������
    async void PatarnDelay(Vector3 target, Transform enemyTransform)
    {
        //UniTask�p�g�[�N��
        var token = this.GetCancellationTokenOnDestroy();

        //����Delay��������ɁA���˃p�^�[�����N��
        for (int i = 0; i < patarnReportValue; i++)
        {
            //�C���^�[�o���x��
            await UniTask.Delay(TimeSpan.FromSeconds(patarnReportTimeSpan), cancellationToken: token);

            //������̔��ˏ����N��
            RepeatPatarnPlay(target, enemyTransform);
        }
    }

    //������̔��ˏ���
    void RepeatPatarnPlay(Vector3 target, Transform enemyTransform)
    {
        //null�`�F�b�N
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
