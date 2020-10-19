using DG.Tweening;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float force = 5.0f;
    [SerializeField] float durationAnim = 0.33f;

    private new Rigidbody rigidbody;
    private new Renderer renderer;
    private bool isInitialized = false;

    private GameManager gameMgr;

    public void Init(GameManager gameMgr)
    {
        this.gameMgr = gameMgr;

        rigidbody.isKinematic = false;
        //rigidbody.velocity = Vector3.up * force * Time.deltaTime;
        rigidbody.velocity = new Vector3(0, force, 0);

        isInitialized = true;
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        rigidbody.isKinematic = true;
        renderer.sharedMaterial.DOFade(1.0f, 0.0f);
    }

    private void Update()
    {
        if (isInitialized && gameMgr.IsGameStarted)
            Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isInitialized && gameMgr.IsGameStarted)
        {
            if (collision.gameObject.GetComponent<Tile>())
            {
                Jump();
                gameMgr.AddPoints();
            }
            else if (collision.gameObject.CompareTag("Respawn"))
            {
                renderer.sharedMaterial.DOFade(0.0f, durationAnim).OnComplete(Delete);
                gameMgr.EndGame();
            }
        }
    }

    private void Move()
    {
        if (Input.GetMouseButton(0))
        {
            float inputValue = Input.GetAxis("Mouse X");
            if (inputValue > 0)
            {
                // Mouse moved left.
                rigidbody.AddForce(new Vector3(1, 0, 0));
            }
            else if (inputValue < 0)
            {
                // Mouse moved right.
                rigidbody.AddForce(new Vector3(-1, 0, 0));
            }
        }
    }

    private void Jump()
    {
        // Get the gravity value.
        var magnitude = Physics.gravity.magnitude;
        // Calculete the vertical speed;
        var speed = Mathf.Sqrt(2 * force * magnitude);
        // Jump.
        rigidbody.velocity = new Vector3(0, speed * 0.5f, 0);
    }

    private void Delete()
    {
        GetComponent<Collider>().enabled = false;
        //Destroy(gameObject);
    }
}
