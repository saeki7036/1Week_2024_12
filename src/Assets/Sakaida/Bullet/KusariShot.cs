using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class KusariShot : ShotPatarnBase
{
    [SerializeField] GameObject bulletPrehab;
    [SerializeField] GameObject BigBulletPrehab;
    [SerializeField] float aimValue = 0f;
    public override void PatarnPlay(Transform enemyTransform)
    {
        
        Vector3 target = GameManager.Getplayer.transform.position;

       
            Vector2 dirTarget = target - enemyTransform.position;

            GameObject bullet = Instantiate(BigBulletPrehab, enemyTransform.position, Quaternion.identity);
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

            float angleRadians = aimValue* Mathf.Deg2Rad;
            Vector2 rotate = Quaternion.Euler(Vector3.forward * angleRadians) * dirTarget.normalized;

            bullet.transform.up = rotate;

            bulletRB.velocity = rotate;

        Wait(15, enemyTransform, rotate);

       

       
        
    }
    async void Wait(int WaitTime , Transform enemyTransform, Vector2 rotate) 
    {var token = this.GetCancellationTokenOnDestroy();

        for (int i = 0; i < 3; i++)
        {
            await UniTask.DelayFrame(WaitTime, cancellationToken: token);

            GameObject smallbullet = Instantiate(bulletPrehab, enemyTransform.position, Quaternion.identity);
            smallbullet.transform.up = rotate;
            Rigidbody2D smallbulletRB = smallbullet.GetComponent<Rigidbody2D>();
            smallbulletRB.velocity = rotate;
            
        }
        
        
    }
}
