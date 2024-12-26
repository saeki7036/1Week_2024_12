using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody2D rb2D;

    [SerializeField]
    protected float speed = 0.01f;

    protected Vector3 vector;

    public void SetVector(Vector3 vector3) => vector = vector3;
    public void SetScale()
    {
        Vector3 Scale = this.transform.localScale;
        Scale.x--;
        Scale.y--;
        this.transform.localScale = Scale;
        if (Scale == Vector3.forward)
            Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<PlayerDamageArea>(out PlayerDamageArea player))
        {
            player.TakeDamage(1);
            Debug.Log("Damage");
            Destroy (this.gameObject);
        }
        Debug.Log("Hit");
    }
}
