using UnityEngine;

public class Player : MonoBehaviour
{
    public int hitPoints;
    public Projectile projectile;
    public float speed;
    public float fireRate;

    private float timer;
    private Projectile prj;
    private Rigidbody2D prjRB;

    void Update()
    {
        if (Input.GetMouseButton(0))
            Fire();
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 v3 = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f);
        transform.position += speed * v3.normalized * Time.deltaTime;
    }

    private void Fire()
    {
        if (timer >= fireRate)
        {
            timer = 0;

            prj = Projectile.Instantiate<Projectile>(projectile);
            prj.transform.position = transform.position;
            prjRB = prj.GetComponent<Rigidbody2D>();

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            Vector3 directionVector = (mousePosition - prj.transform.position).normalized;

            prjRB.velocity = prj.speed * directionVector;

        }
        timer += Time.deltaTime;
    }
}
