using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RandomShot : ShotPatarnBase
{
    [SerializeField]
    GameObject bulletPrehab;//Bullet�̃I�u�W�F�N�g

    [SerializeField] 
    float aimValue = 560f;//�����v�Z���x

    [SerializeField] 
    float patarnReportTimeSpan = 0.1f;//���ˊԊu�̒x������

    [SerializeField]
    int patarnReportValue = 50;//���˃p�^�[���̋N����

    [SerializeField]
    int oneSetBulletCount = 3;//�p�^�[�����̔��ˉ�

    //�����_���p�����[�^�̂��߂̏���A�����B
    //���l1���Ƃ�10�x��z��
    const int RandomMinValue = 0, 
              RandomMaxValue = 37;

    //�����_���ȕ������擾(0�`36�̊�)
    int RandomAngle => UnityEngine.Random.Range(RandomMinValue, RandomMaxValue);

    //���ˏ���
    public override void PatarnPlay(Transform enemyTransform)
    {
        //UniTask�񓯊������N��
        //���ˏ����̒x�������̋N��
        PatarnDelay(enemyTransform);
    }

    //���ˏ����̒x������
    async void PatarnDelay(Transform enemyTransform)
    {
        //UniTask�p�g�[�N��
        var token = this.GetCancellationTokenOnDestroy();

        //����Delay��������ɁA���˃p�^�[�����N��
        for (int i = 0; i < patarnReportValue; i++)
        {
            //�C���^�[�o���x��
            await UniTask.Delay(TimeSpan.FromSeconds(patarnReportTimeSpan), cancellationToken: token);

            //������̔��ˏ����N��
            RepeatPatarnPlay(enemyTransform);
        }
    }

    //������̔��ˏ���
    void RepeatPatarnPlay(Transform enemyTransform)
    {
        //�v���C���[��null�`�F�b�N
        if (GameManager.Getplayer == null) 
            return;

        //null�`�F�b�N
        if (enemyTransform == null)
            return;

        //���ˑΏۂ̈ʒu���擾
        Vector3 target = GameManager.Getplayer.transform.position;

        //���ˉ񐔕����[�v
        for (int i = 0; i < oneSetBulletCount; i++)
        {
            //��ƂȂ�������v�Z
            Vector2 dirTarget = target - enemyTransform.position;

            //�I�u�W�F�N�g����
            GameObject bullet = Instantiate(bulletPrehab, enemyTransform.position, Quaternion.identity);

            //Rigidbody2D�擾
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

            //�����_���ɕ������w��
            float angleRadians = (aimValue * RandomAngle) * Mathf.Deg2Rad;

            //���˕����v�Z
            Vector2 rotate = Quaternion.Euler(Vector3.forward * angleRadians) * dirTarget.normalized;

            //���˕������
            bulletRB.velocity = rotate;
        }
    }
}
