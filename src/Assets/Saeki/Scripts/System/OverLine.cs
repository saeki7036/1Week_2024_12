using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //��O�ɐڐG�����I�u�W�F�N�g���폜
        Destroy(other.gameObject);
    }
}
