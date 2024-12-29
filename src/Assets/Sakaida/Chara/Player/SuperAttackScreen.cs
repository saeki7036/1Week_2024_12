using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAttackScreen : SuperAttackBase
{
    [SerializeField] GameObject Black;
    [SerializeField] int screenValue = 20;
    [SerializeField] float interval = 0.03f;
    Vector2 rabdomPos => new Vector2(UnityEngine.Random.Range(-8.5f, 0), UnityEngine.Random.Range(-5, 5));
    public override void PlaySuperAttack()
    {
        TimeToAttack();
        //’ÊíUŒ‚‚ÌScreen‚ğ‚Ô‚Á”ò‚Î‚·ˆ—
    } 
    async void TimeToAttack()
    {
        var token = this.GetCancellationTokenOnDestroy();      
        for (int i = 0; i < screenValue; i++)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(interval), cancellationToken: token);
            GameObject CL_Black = Instantiate(Black);
            CL_Black.transform.position = rabdomPos;
            Destroy(CL_Black, 0.8f);
        }
    }
}
