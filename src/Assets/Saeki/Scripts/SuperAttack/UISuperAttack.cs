using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using static Unity.Collections.AllocatorManager;
using System;
using Unity.VisualScripting.Antlr3.Runtime;
public class UISuperAttack : SuperAttackBase
{
    [SerializeField] GameObject UIAttackPrehab;
    [SerializeField] float instantiatXPos = -8f;
    [SerializeField] GameObject[] UIShake;
    [SerializeField] float shakeInterval = 1f;
    public override void PlaySuperAttack()
    {
        GameObject prehab = Instantiate(UIAttackPrehab, new Vector3(instantiatXPos, transform.position.y + 2f, 0), Quaternion.identity);
        UIAttackArea uIAttackArea = prehab.GetComponent<UIAttackArea>();
        uIAttackArea.MoveArea();
        SmallUIAttack();
        UIShaker();
    }
    async void SmallUIAttack()
    {
        var token = this.GetCancellationTokenOnDestroy();
        for (int i = 0;i <= 2;i++) 
        {
            if (i == 0) continue;
            await UniTask.Delay(TimeSpan.FromSeconds(0.4f), cancellationToken: token);

            GameObject prehab_up = Instantiate(UIAttackPrehab, new Vector3(instantiatXPos, transform.position.y + 2f + 1 * i, 0), Quaternion.identity);
            prehab_up.transform.localScale = new(prehab_up.transform.localScale.x, prehab_up.transform.localScale.y/2, prehab_up.transform.localScale.z);
            UIAttackArea uIAttackArea_up = prehab_up.GetComponent<UIAttackArea>();
            uIAttackArea_up.MoveArea();


            GameObject prehab_down = Instantiate(UIAttackPrehab, new Vector3(instantiatXPos, transform.position.y + 2f - 1 * i, 0), Quaternion.identity);
            prehab_down.transform.localScale = new(prehab_down.transform.localScale.x, prehab_down.transform.localScale.y / 2, prehab_down.transform.localScale.z);
            UIAttackArea uIAttackArea_down = prehab_down.GetComponent<UIAttackArea>();
            uIAttackArea_down.MoveArea();
        }
       
        UIShaker();
    }
    async void UIShaker()
    {
        var token = this.GetCancellationTokenOnDestroy();
        await UniTask.Delay(TimeSpan.FromSeconds(shakeInterval), cancellationToken: token);
        foreach (GameObject UIobj in UIShake)
        {
            UIobj.transform.DOShakePosition(1f, 1f, 10, 1, false, true).SetLink(UIobj);
        }
    }
}
