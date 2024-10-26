using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    [SerializeField]
    private GameObject fireball;
    public float force = 10;

    public float damage = 20f;

    void Start()
    {
        Camera mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        Vector3 scale = transform.localScale;
        float angle = Vector2.SignedAngle(Vector2.right, rotation);
        if(angle > 90f || angle < -90f)
        {
            scale.x = -scale.x;
            angle += 180f;
        }

        transform.localRotation = Quaternion.Euler(0f, 0f, angle);
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Terrain"))
        {
            DestroyBullet();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Terrain"))
        {
            DestroyBullet();
        }
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
