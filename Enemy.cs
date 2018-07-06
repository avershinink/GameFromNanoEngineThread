using UnityEngine;

public class Enemy : MonoBehaviour {

    public int HP;
    public float speed;
    public Transform[] MovingPath;
    public Sprite EnemyStdSprite;
    public Sprite EnemyDmgSprite;
    public float showInjureForTime;

    private const float allowedDistanceGap = 0.2f;
    private bool isDead;
    private SpriteRenderer EnemySprRnd;
    private AudioSource HitSound;
    private AudioSource DeathSound;
    private Transform NextPos;
    private int i;
    private float injureTime;

    void Start()
    {
        EnemySprRnd = gameObject.GetComponent<SpriteRenderer>();
        HitSound = this.transform.Find("Sounds/Hit").GetComponent<AudioSource>();
        DeathSound = this.transform.Find("Sounds/Death").GetComponent<AudioSource>();
        i = 0;
    }

    void Update ()
    {
        // Enemy moving
		if(i < MovingPath.Length)
        {
            if(NextPos == null)
                NextPos = MovingPath[i];
            transform.position = Vector2.MoveTowards(transform.position, NextPos.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, NextPos.position) <= allowedDistanceGap)
            {
                NextPos = null;
                i++;
            }
        }

        // enemy suffer
        if (injureTime > 0)
        {
            if (EnemySprRnd.sprite != EnemyDmgSprite)
                EnemySprRnd.sprite = EnemyDmgSprite;
            injureTime -= Time.deltaTime;
        }
        else
            EnemySprRnd.sprite = EnemyStdSprite;
        
        if (isDead && injureTime <= 0)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(!isDead)
            if (col.tag == "projectile")
            {
                HitSound.Play();
                HP -= col.gameObject.GetComponent<Projectile>().damage;
                injureTime = showInjureForTime;
                if (HP == 0)
                    Die();
            }
            else if (col.name == "Base")
                Die(true);
    }

   
    public void Die(bool isKamikaze = false)
    {
        DeathSound.Play();
        if (isKamikaze)
            injureTime = DeathSound.clip.length;
        else
            GameWorker.score++;
        isDead = true;
    }
}
