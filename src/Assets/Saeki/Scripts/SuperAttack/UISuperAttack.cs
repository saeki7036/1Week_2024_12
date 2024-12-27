using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISuperAttack : SuperAttackBase
{
    [SerializeField] GameObject UIAttackPrehab;
    [SerializeField] float instantiatXPos = -8f;
    public override void PlaySuperAttack()
    {
        GameObject prehab = Instantiate(UIAttackPrehab, new Vector3(instantiatXPos, transform.position.y + 2f, 0), Quaternion.identity);
        UIAttackArea uIAttackArea = prehab.GetComponent<UIAttackArea>();
        uIAttackArea.MoveArea();
    }
}
