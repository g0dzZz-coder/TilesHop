using DG.Tweening;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Material[] materials = null;
    [SerializeField] Material[] lerpMaterials = null;
    [SerializeField] Material transparentMaterial = null;
    [SerializeField] float durationAnim = 0.33f;

    private new Rigidbody rigidbody = null;
    private new Renderer renderer = null;
    private float speed;
    private bool isActive = false;

    private TileManager tileMgr;

    public void Init(TileManager tileMgr, float speed, float lifeTime)
    {
        this.tileMgr = tileMgr;
        this.speed = speed;

        isActive = true;

        Invoke(nameof(Delete), lifeTime);
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        renderer.sharedMaterial = materials[Random.Range(0, materials.Length)];
    }

    private void Update()
    {
        if (isActive)
            rigidbody.velocity = new Vector3(0, 0, -speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Ball>())
            //renderer.material
            renderer.material.Lerp(renderer.sharedMaterial, transparentMaterial, durationAnim);
    }

    private void Delete()
    {
        Destroy(gameObject);
    }
}
