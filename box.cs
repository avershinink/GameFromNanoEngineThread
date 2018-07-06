using UnityEngine;

public class box : MonoBehaviour {

    private AudioSource ASrc;
    void Start()
    {
        ASrc = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name != "Player")
        {
            Destroy(col.gameObject);
            ASrc.Play();
        }
    }
}
