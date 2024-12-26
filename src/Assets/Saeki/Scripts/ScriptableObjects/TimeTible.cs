using UnityEngine;

[CreateAssetMenu(fileName ="timetible",menuName = "ScriptableObject/TimeTible", order = 1)]
public class TimeTible : ScriptableObject
{
    [Header("出現する時間")]
    [Tooltip("この値はキャラクターの出現する時間を設定します。")]
    [SerializeField]
    int spownTime;

    public int GetSpowntime => spownTime;

    [Header("出現位置")]
    [SerializeField]
    SpownInfo[] spownInfos;

    [System.Serializable]
    public struct SpownInfo
    {
        [Tooltip("この値はキャラクターの出現位置を定義します。")]
        [SerializeField]
        Transform spownPoint;
        [Tooltip("この値は生成する敵オブジェクトを定義します。")]
        [SerializeField]
        GameObject EnemyObject;

        public readonly Transform Point => spownPoint;
        public readonly GameObject Enemy => EnemyObject;
    }

    public int GetInfomationLength => spownInfos.Length;
    public SpownInfo GetSpownInfomation(int number) => spownInfos[number];
}
