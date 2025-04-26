using UnityEngine;

[CreateAssetMenu(fileName ="timetible",menuName = "ScriptableObject/TimeTible", order = 1)]
public class TimeTible : ScriptableObject
{
    [Header("�o�����鎞��")]
    [Tooltip("���̒l�̓L�����N�^�[�̏o�����鎞�Ԃ�ݒ肵�܂��B")]
    [SerializeField]
    int spownTime;

    public int GetSpowntime => spownTime;//�o�����Ԃ��擾

    [Header("�o���ʒu")]
    [SerializeField]
    SpownInfo[] spownInfos;

    [System.Serializable]
    public struct SpownInfo
    {
        [Tooltip("���̒l�̓L�����N�^�[�̏o���ʒu���`���܂��B")]
        [SerializeField]
        GameObject spownPoint;
        [Tooltip("���̒l�͐�������G�I�u�W�F�N�g���`���܂��B")]
        [SerializeField]
        GameObject EnemyObject;

        public readonly Transform Point => spownPoint.transform;//�o���ʒu���擾
        public readonly GameObject Enemy => EnemyObject;//�o������G���擾
    }

    public int GetInfomationLength => spownInfos.Length;//�o�����̗ʂ��擾
    public SpownInfo GetSpownInfomation(int number) => spownInfos[number];//�o�������擾
}
