using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class Big1Shot : ShotPatarnBase
{
    [SerializeField] GameObject bulletPrehab;//Bullet�̃I�u�W�F�N�g

    //�P�̃e�X�gOK
    //���ˏ���
    public override void PatarnPlay(Transform enemyTransform)
    {
        //null�`�F�b�N
        if (enemyTransform == null)
            return;

        //���ˑΏۂ̈ʒu���擾
        Vector3 target = GameManager.Getplayer.transform.position;

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
