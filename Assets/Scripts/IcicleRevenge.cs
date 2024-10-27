using System.Collections;

using UnityEngine;

namespace Basics
{
    public class IcicleRevenge : MonoBehaviour
    {
        public float speed = 10f;
        public float fireDelay = 1f;
        public float jitter = 0.1f;
        public Transform particleLocation;
        public GameObject particlePrefab;
        public FMODUnity.EventReference flyingEvent;

        private Rigidbody2D _rb;
        private Vector2 startPos;

        IEnumerator Start()
        {
            startPos = transform.position;
            _rb = GetComponent<Rigidbody2D>();
            var player = FindObjectOfType<PlayerController>();

            Vector2 direction = Vector2.right;
            float start = Time.time;
            while(Time.time < start + fireDelay)
            {
                transform.position = startPos + Random.insideUnitCircle*jitter;
                direction = player.transform.position - transform.position;
                float angle = Vector2.SignedAngle(Vector2.down, direction);
                transform.rotation = Quaternion.Euler(0f, 0f, angle);
                yield return null;
            }

            FMODUnity.RuntimeManager.PlayOneShot(flyingEvent, transform.position);
            _rb.velocity = direction.normalized*speed;
        }

        public void DestroyBullet()
        {
            var particle = Instantiate(particlePrefab, null);
            particle.transform.position = particleLocation.position;
            particle.transform.rotation = transform.rotation;

            Destroy(gameObject);
        }
    }
}
