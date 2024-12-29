using DG.Tweening;
using UnityEngine;

public class UIShake : MonoBehaviour
{
    [SerializeField]
    GameObject[] ShakeObjects;

     public void OneShake()
    {
        foreach (GameObject obj in ShakeObjects)
        {
            obj.transform.DOShakePosition(1f, 1f, 10, 1, false, true).SetLink(obj);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U)) { OneShake(); }
    }
}
