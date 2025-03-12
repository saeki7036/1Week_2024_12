using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class Big1Shot : ShotPatarnBase
{
    [SerializeField] GameObject bulletPrehab;

    //単体テストOK

    public override void PatarnPlay(Transform enemyTransform)
    {
        if (enemyTransform == null)
            return;

        Vector3 target = GameManager.Getplayer.transform.position;

        //プレイヤーに飛ばす方向を計算
        Vector2 dirTarget = target - enemyTransform.position;

        //オブジェクト生成
        GameObject bullet = Instantiate(bulletPrehab, enemyTransform.position, Quaternion.identity);

        //Rigidbody2D取得
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

        //ノーマライズ
        Vector2 rotate = dirTarget.normalized;

        //発射方向代入
        bulletRB.velocity = rotate;
    }
}
