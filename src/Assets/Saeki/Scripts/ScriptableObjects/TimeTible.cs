using UnityEngine;

[CreateAssetMenu(fileName ="timetible",menuName = "ScriptableObject/TimeTible", order = 1)]
public class TimeTible : ScriptableObject
{
    [Header("出現する時間")]
    [Tooltip("この値はキャラクターの出現する時間を設定します。")]
    [SerializeField]
    int spownTime;

    public int GetSpowntime => spownTime;//出現時間を取得

    [Header("出現位置")]
    [SerializeField]
    SpownInfo[] spownInfos;

    [System.Serializable]
    public struct SpownInfo
    {
        [Tooltip("この値はキャラクターの出現位置を定義します。")]
        [SerializeField]
        GameObject spownPoint;
        [Tooltip("この値は生成する敵オブジェクトを定義します。")]
        [SerializeField]
        GameObject EnemyObject;

        public readonly Transform Point => spownPoint.transform;//出現位置を取得
        public readonly GameObject Enemy => EnemyObject;//出現する敵を取得
    }

    public int GetInfomationLength => spownInfos.Length;//出現情報の量を取得
    public SpownInfo GetSpownInfomation(int number) => spownInfos[number];//出現情報を取得
}
