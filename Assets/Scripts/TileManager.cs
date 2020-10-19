using System.Collections;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] Tile tilePrefab = null;
    [SerializeField] Transform tileParent = null;
    [SerializeField] float speedOfTiles = 15.0f;
    [SerializeField] float lifeOfTiles = 5.0f;
    [SerializeField] float spawnDelay = 1.0f;

    [SerializeField] int xRange = 3;

    private GameManager gameMgr;

    public void Init(GameManager gameMgr)
    {
        this.gameMgr = gameMgr;

        InitStartTiles();
        StartCoroutine(Spawner());
    }

    public void StartSpawn()
    {
        StartCoroutine(Spawner());
    }

    private void InitStartTiles()
    {
        for (int i = 0; i < tileParent.childCount; i++)
        {
            var child = tileParent.GetChild(i);
            if (child.GetComponent<Tile>())
                child.GetComponent<Tile>().Init(this, speedOfTiles, lifeOfTiles);
        }
    }

    private IEnumerator Spawner()
    {
        while (true)
        {
            var xPos = Random.Range(-xRange, xRange);
            var tile = Instantiate(tilePrefab, new Vector3(xPos, 3, 42), Quaternion.identity);
            tile.Init(this, speedOfTiles, lifeOfTiles);
            tile.transform.SetParent(tileParent);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
