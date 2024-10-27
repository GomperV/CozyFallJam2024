using System.Collections;
using System.Collections.Generic;

using TriInspector;

using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject head;
    public List<GameObject> segments;
    public float moveSpeed = 5f;
    public float segmentDistance = 3f;
    public GameObject segmentPrefab;
    public Transform playerRoot;
    public Sprite normalSegmentSprite;
    public Sprite brokenSegmentSprite;
    public float invulnerabilityDuration = 1f;
    public int startingHealth = 5;
    public float desiredDistanceToMouse = 1f;
    public GameObject iceVignette;

    [Header("Speed boost")]
    public float accelerationDuration = 1f;
    public float boostAmount = 2f;
    public ParticleSystem speedTrail;

    [Header("Game UI")]
    public UIManager ui;

    [Header("Upgrades")]
    public List<string> upgradesOwned;

    [Header("Attacks")]
    public GameObject flamethrowerHitbox;
    public ParticleSystem flameParticles;
    [InfoBox("Multiplier to player speed while they are using the flamethrower")]
    public float flamethrowerMovementMultiplier = 0.9f;

    [Header("Audio")]
    public FMODUnity.EventReference flameEvent;
    private FMOD.Studio.EventInstance flameEventInstance;
    private const string FIRE_THROW_STOPS = "fire_throw_stops";
    private const string WHICH_FIRE_LOOPS = "which_fire_loops";
    private const int NUM_FIRE_LOOPS_ONE_INDEXED = 3;

    private bool _boostUnlocked;
    private float _lastHit = -999f;
    private float _movementTimer = -999f;
    private List<SpriteRenderer> _segmentSprites;
    private int _health;
    private Rigidbody2D _rb;


    private bool facingRight = true;
    private void Awake()
    {
        upgradesOwned = new();
        _rb = GetComponent<Rigidbody2D>();
        flameEventInstance = FMODUnity.RuntimeManager.CreateInstance(flameEvent);
    }

    void Start()
    {
        _segmentSprites = new List<SpriteRenderer>();
        _health = 0;
        for(int i = 0; i < startingHealth; i++)
        {
            AddSegment();
        }
    }

    public void DamagePlayer()
    {
        if(Time.time > _lastHit + invulnerabilityDuration)
        {
            _lastHit = Time.time;
            _health = Mathf.Clamp(_health - 1, 0, 99);
            SetHealthDisplay();

            if(_health <= 1 && _health > 0)
            {
                print("hp < 1");
                iceVignette.SetActive(true);
                StartCoroutine(IceVignetteEffect());
            } else if(_health <= 0)
            {
                ui.GameLost();
            }
        }
    }

    IEnumerator IceVignetteEffect()
    {
        iceVignette.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
        while (_health <= 1)
        {
            for (float i = 0; i < 0.5f; i+= 0.01f)
            {
                //print(i);
                yield return new WaitForSeconds(0.01f);
                iceVignette.GetComponent<Image>().color = new Color(1f, 1f, 1f, i);
            }
            for (float i = 0.5f; i > 0; i -= 0.01f)
            {
                //print(i);
                yield return new WaitForSeconds(0.01f);
                iceVignette.GetComponent<Image>().color = new Color(1f, 1f, 1f, i);
            }
        }
        yield return new WaitForSeconds(0.01f);
        iceVignette.SetActive(false);
    }

    public void SetHealthDisplay()
    {
        int missingHealth = segments.Count - _health;
        for(int i = 0; i < segments.Count; i++)
        {
            _segmentSprites[i].sprite = i < missingHealth ? brokenSegmentSprite : normalSegmentSprite;
        }
    }

    public void AddSegment()
    {

        GameObject segment = Instantiate(segmentPrefab, playerRoot);
        segments.Add(segment);
        _segmentSprites.Add(segment.GetComponentInChildren<SpriteRenderer>());
        segment.transform.localScale = Vector3.one*((8f - segments.Count)/15f + 7f/15f);
        var wobble = segment.GetComponentInChildren<SegmentWobble>();
        wobble.offset = segments.Count*0.1f;
        _health++;
        SetHealthDisplay();
    }

    void Update()
    {
        if (_rb.velocity.sqrMagnitude == 0f)
        {
            ResetSpeedBoost();
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        float speed = GetMoveSpeed();

        if(Input.GetButton("Fire2"))
        {
            if(!flamethrowerHitbox.activeSelf)
            {
                flameParticles.Play();
                flameEventInstance.setParameterByName(FIRE_THROW_STOPS, 0f);
                flameEventInstance.setParameterByName(WHICH_FIRE_LOOPS, Random.Range(1, NUM_FIRE_LOOPS_ONE_INDEXED + 1));
                flameEventInstance.start();
            }

            //use flamethrower without moving  - bad version for now
            float angle = Vector2.SignedAngle(Vector2.up, mousePos - head.transform.position);
            head.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            flamethrowerHitbox.SetActive(true);
            speed *= flamethrowerMovementMultiplier;
            ResetSpeedBoost();
        }
        else
        {
            flameEventInstance.setParameterByName(FIRE_THROW_STOPS, 1f);
            flameParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            flamethrowerHitbox.SetActive(false);
        }

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        if (difference.x >= 0 && !facingRight && (Input.GetButton("Fire1") || Input.GetButton("Fire2")))
        { // mouse is on right side of player
            head.GetComponentInChildren<SpriteRenderer>().flipY = false;
            facingRight = true;
        }
        if (difference.x < 0 && facingRight && (Input.GetButton("Fire1") || Input.GetButton("Fire2")))
        { // mouse is on left side
            head.GetComponentInChildren<SpriteRenderer>().flipY = true;
            facingRight = false;
        }
        // Only move and rotate the character when its far enough away to prevent jitter
        if (Input.GetButton("Fire1") && Vector3.Distance(mousePos, head.transform.position) > desiredDistanceToMouse)
        {
            float angle = Vector2.SignedAngle(Vector2.up, mousePos - head.transform.position);
            _rb.velocity = (mousePos - head.transform.position).normalized*speed;
            //head.transform.position = Vector3.MoveTowards(head.transform.position, mousePos, speed*Time.deltaTime);
            head.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        else
        {
            _rb.velocity = Vector3.zero;
        }

        if(segments.Count > 0)
        {
            MoveSegmentTowards(segments[0].transform, head.transform, speed);
        }

        for(int i = 1; i < segments.Count; i++)
        {
            MoveSegmentTowards(segments[i].transform, segments[i - 1].transform, speed);
        }
    }

    private void ResetSpeedBoost()
    {
        _movementTimer = Time.time;
        speedTrail.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    public float GetMoveSpeed()
    {
        float speed = moveSpeed;
        if(_boostUnlocked)
        {
            float t = (Time.time - _movementTimer)/accelerationDuration;
            float boost = Mathf.Lerp(0f, boostAmount, t);
            speed += boost;

            if(t >= 1f && !speedTrail.isPlaying)
            {
                speedTrail.Play();
            }
        }
        return speed;
    }

    private void MoveSegmentTowards(Transform segment, Transform target, float speed)
    {
        Vector3 delta = target.position - segment.position;
        float distance = delta.magnitude;
        Vector2 direction = delta.normalized;
        if(distance > segmentDistance)
        {
            Vector3 targetWithMargin = target.position - (Vector3)direction*segmentDistance;
            segment.position = Vector3.MoveTowards(segment.position, targetWithMargin, speed);
        }
    }

    public void ApplyUpgrade(UpgradeData data)
    {
        if(data.id == "healing")
        {
            _health = segments.Count;
            SetHealthDisplay();
            // healing is a repeating upgrade, don't add it to the list
            return;
        }

        if(data.id == "health")
        {
            AddSegment();
            AddSegment();
        }

        if(data.id == "fireball")
        {
            GetComponent<PlayerCombat>().enabled = true;
        }

        if(data.id == "speedboost")
        {
            _boostUnlocked = true;
        }

        upgradesOwned.Add(data.id);
    }
}
