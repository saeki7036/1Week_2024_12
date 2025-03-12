using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shot3Ways : ShotPatarnBase
{
    [SerializeField] 
    GameObject bulletPrehab;

    [SerializeField] 
    float aimValue = 15f;

    public override void PatarnPlay(Transform enemyTransform)
    {
        if (enemyTransform == null)
            return;

        Vector3 target = GameManager.Getplayer.transform.position;

        //3Wayのため、補正を、-1,0,1の数値でかける
        for (int i = -1; i <= 1; i++)
        {
            //基準となる方向を計算
            Vector2 dirTarget =  target - enemyTransform.position;

            //オブジェクト生成
            GameObject bullet = Instantiate(bulletPrehab, enemyTransform.position, Quaternion.identity);

            //Rigidbody2D取得
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

            //発射方向計算
            float angleRadians = (aimValue * i)* Mathf.Deg2Rad;
            Vector2 rotate = Quaternion.Euler(Vector3.forward * angleRadians) * dirTarget.normalized;

            //発射方向代入
            bulletRB.velocity = rotate;
        }
    }
}
