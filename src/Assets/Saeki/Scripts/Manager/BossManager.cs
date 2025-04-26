using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField]
    BossEnemyLeader bossEnemy;//�{�X�̖{��

    [SerializeField]
    GameObject bossEnemyObject;//�{�X�̑匳�̃I�u�W�F�N�g

    [SerializeField]
    BossEnemySub[] bossSub;//�{�X�̘r

    [SerializeField]
    float downParameter = -11f;//��ʊO����̈ړ���

    [SerializeField]
    float moveTime = 5f;//�ړ�����

    // Start is called before the first frame update
    void Start()
    {
        //��ʊO�����ʏ㕔�܂ňړ�
        this.transform.DOMove(new Vector3(
            transform.position.x, //�ړ�����
            transform.position.y +downParameter, //y���ňړ�
            0f), moveTime);//z����2D�̂���0��OK
            
        //�{�X�̌`�ԕω�UniTask�N��
        SetUpBossLeader();

        //�{�X�̎��S����UniTask�N��
        bossDeadCheck();
    }

    //�{�X�̌`�ԕω�(UniTask)
    async void SetUpBossLeader()
    {
        //UniTask�p�g�[�N��
        var token = this.GetCancellationTokenOnDestroy();

        //�S�Ă̘r�̔j��܂őҋ@
        for (int i = 0; i < bossSub.Length;i++)//���ꂼ��̘r��HP��0�ɂȂ����珈���̐i�s
        await UniTask.WaitUntil(() => bossSub[i].IsDestroyed(), cancellationToken: token);

        //�{�̂̓����蔻��̕ύX(������悤��)
        bossEnemyObject.layer = LayerMask.NameToLayer("Enemy");

        //�`�ԕω�
        bossEnemy.SetSuperMode();
    }

    //�{�X�̎��S����(UniTask)
    async void bossDeadCheck()
    {
        //UniTask�p�g�[�N��
        var token = this.GetCancellationTokenOnDestroy();

        //�{�̂̎��S�܂őҋ@
        await UniTask.WaitUntil(() => bossEnemyObject.activeSelf == false, cancellationToken: token);

        //�N���A�t���O�ݒ�
        GameClear();
    }

    //�N���A�t���O�ݒ�
    void GameClear()
    {
        //GameManager���擾
        GameManager manager = GameObject.FindWithTag("Manager").GetComponent<GameManager>();

        //GameManager�Ƀt���O���M
        if (manager != null)
            manager.BosskillFlag();
    }
}
