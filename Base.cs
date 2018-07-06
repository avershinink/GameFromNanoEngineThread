using UnityEngine;

public class Base : MonoBehaviour {
    public int MaxHP;
    public int HP;
    void Start()
    {
        HP = MaxHP; 
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy") HP--;
    }
}
