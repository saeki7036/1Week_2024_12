using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShotPatarnBase : MonoBehaviour
{
    [SerializeField]
    int shotInterval = 200;//���ˊԊu�p�����[�^

    /// <summary>
    /// ���˂̊Ԋu�̏�������
    /// </summary>
    /// <param name="time">�J�E���g�p�����[�^</param>
    /// <returns>�J�E���g���C���^�[�o���ȏ�Ȃ�true</returns>
    public bool PatarnCeangeLimit(int time) => time >= shotInterval;
    
    //���˃p�^�[��
    public virtual void PatarnPlay(Transform enemyTransform)
    {
        return;//���N���X
    }
}
