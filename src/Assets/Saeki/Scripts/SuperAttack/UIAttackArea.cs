using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAttackArea : MonoBehaviour
{
    [SerializeField]
    float magnifyScaleX = 23;//スケール量

    [SerializeField]
    float magnifyTime = 2;//スケール変更時間

    [SerializeField] 
    int loopCount = 2;//ループ回数

    [SerializeField]
    float delayTime = 0.5f;//遅延時間

    [SerializeField]
    float takeDamageValue = 200f;//敵に与えるダメージ量

    const LoopType loopType = LoopType.Yoyo;//ループタイプ

    Player player;//Prehubから呼び出すのでSerializeしない

    void Start()
    {
        //playerがnullなら探索を行う(PlayerのTagはSceneに一つだけなのでO(1)。)
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");

            //PlayerObjectにあるplayerクラスをTryGetで代入(基本成功する)
            if (playerObject != null && playerObject.TryGetComponent<Player>(out var p))
                player = p;
        }
    }

    /// <summary>
    /// 攻撃判定のあるエリアを動かす
    /// </summary>
    public void MoveArea()
    {
        //このオブジェクトをアクティブ状態に
        this.gameObject.SetActive(true);

        //DOTweenを使ってXのスケールを変形
        this.transform
            .DOScaleX(magnifyScaleX, magnifyTime)//指定した値にスケール
            .SetLoops(loopCount, loopType)//動作をループさせる
            .SetDelay(delayTime).//アニメーションの開始を遅らせる
        OnComplete(() =>//完了後
        {
            //完了後非アクティブ状態に
            this.gameObject.SetActive(false);

            //オブジェクト削除
            Destroy(this.gameObject);
        });
    }

    //攻撃判定処理
    void OnTriggerEnter2D(Collider2D collision)
    {
        //playerがnullなら止める
        if (player == null) return;

        //プレイヤーの必殺攻撃側の当たり判定処理
        //Bulletの基底クラスを取得
        if (collision.TryGetComponent<BulletBase>(out BulletBase bullet))
        {
            //必殺技貯める
            player.AddPoint(bullet.GetAddPoint);

            //弾の削除を行う
            bullet.KillBullet();
            
            return;
        }
        //Enemyの基底クラスを取得
        else if (collision.TryGetComponent<EnemyBase>(out EnemyBase enemy))
        {
            //必殺技貯める
            player.AddPoint(enemy.GetAddPoint);

            //ダメージ処理
            enemy.TakeDamage(takeDamageValue);

            return;
        }
    }
}
