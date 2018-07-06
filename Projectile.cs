using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed;
    public int damage;
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag != "Player")
            Destroy(gameObject);
    }
}
