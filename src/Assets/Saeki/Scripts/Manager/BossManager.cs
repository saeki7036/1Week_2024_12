using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField]
    BossEnemyLeader bossEnemy;

    [SerializeField]
    GameObject bossEnemyObject;

    [SerializeField]
    BossEnemySub[] bossSub;

    [SerializeField]
    float downParameter = -11f;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.DOMove(new Vector3(transform.position.x, transform.position.y +downParameter, 0f), 5f);
        SetUpBossLeader();
        bossDeadCheck();
    }

    async void SetUpBossLeader()
    {
        for(int i = 0; i < bossSub.Length;i++)
        await UniTask.WaitUntil(() => bossSub[i].IsDestroyed());

        bossEnemyObject.layer = LayerMask.NameToLayer("Enemy");
    }

    async void bossDeadCheck()
    {
        await UniTask.WaitUntil(() => bossEnemyObject.activeSelf == false);
        GameClear();
    }
    
    void GameClear()
    {
        GameManager manager = GameObject.FindWithTag("Manager").GetComponent<GameManager>();
        if (manager != null)
            manager.BosskillFlag();
    }
}
