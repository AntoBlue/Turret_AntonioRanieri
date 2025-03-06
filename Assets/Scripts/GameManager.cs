using NUnit.Framework.Constraints;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject youWin;
    [SerializeField] GameObject gameOver;


    #region Turrets

    [SerializeField] GameObject[] turretsPrefab;
    [SerializeField] int numberOfTurrets = 5;
    private GameObject[] _turrets;

    [SerializeField][Range(0.1f, 50f)] private float minDistanceX = 10;
    [SerializeField][Range(0.1f, 50f)] private float minDistanceZ = 10;

    [SerializeField][Range(0.1f, 100f)] private float deltaX = 30f;
    [SerializeField][Range(0.1f, 100f)] private float deltaZ = 30f;

    [SerializeField][Range(0.5f, 5f)] private float minFireRate = 0.5f;
    [SerializeField][Range(0.5f, 5f)] private float maxFireRate = 2f;

    [SerializeField][Range(1f, 50f)] private float minFireDistance = 10f;
    [SerializeField][Range(1f, 50f)] private float maxFireDistance = 20f;
    #endregion

    #region Walls

    private int _wallsAvaible = 0;

    [SerializeField] private GameObject[] wallsPrefab;

    [SerializeField][Range(0.1f, 50f)] private float minDistanceWallX = 1;
    [SerializeField][Range(0.1f, 50f)] private float minDistanceWallZ = 5;

    [SerializeField][Range(0.1f, 100f)] private float deltaWallX = 1f;
    [SerializeField][Range(0.1f, 100f)] private float deltaWallZ = 1f;

    #endregion

    void Start()
    {
        if (numberOfTurrets == 0)
        {
            Debug.LogWarning("No turrets detected");
            return;
        }

        _wallsAvaible = numberOfTurrets;

        _turrets = new GameObject[numberOfTurrets];

        for (int i = 0; i < numberOfTurrets; i++)
        {
            GameObject turret = Instantiate(turretsPrefab[Random.Range(0, turretsPrefab.Length)]);
            _turrets[i] = turret;

            int tries = 5;

            bool intersect = false;
            do
            {
                turret.transform.position = new Vector3(minDistanceX + Random.Range(-1f, 1f) * deltaX, 0,
                    minDistanceZ + Random.Range(-1f, 1f) * deltaZ);
                turret.transform.Rotate(Vector3.up, Random.Range(-1f, 1f) * deltaZ);

                foreach (var addedTurret in _turrets)
                {
                    if (addedTurret == turret || addedTurret == null) continue;

                    if (addedTurret.GetComponent<Collider>().bounds.Intersects(turret.GetComponent<Collider>().bounds))
                    {
                        intersect = true;
                        break;
                    }
                }

                tries--;
            }
            while(intersect && tries > 0);

            FireBulletsAtTarget turretScripts = turret.GetComponent<FireBulletsAtTarget>();
            turretScripts.Configure(Random.Range(minFireRate, maxFireRate), Random.Range(minFireDistance, maxFireDistance),
                transform);

            //Place wall around turret

            GameObject wall = Instantiate(wallsPrefab[Random.Range(0, wallsPrefab.Length)]);

            wall.transform.position = turret.transform.position + new Vector3(
                minDistanceWallX + Random.Range(-1f, 1f) * deltaWallX,
                wall.transform.localScale.y * 0.5f,
                minDistanceWallZ + Random.Range(-1f, 1f) * deltaWallZ);

            //rotate around turret randomly
            wall.transform.RotateAround(turret.transform.position, Vector3.up, Random.Range(0.3f, 360f));

            //rotate around itself randomly locally
            wall.transform.Rotate(Vector3.up, Random.Range(-45f, 45f), Space.Self);

            //notify
            DestroyOnMultipleHit destroyOnMultipleHit = wall.GetComponent<DestroyOnMultipleHit>();
            destroyOnMultipleHit.GameManager = this;
        }
    }

    public void GameOver()
    {
        DestroyAllTurrets();

        Debug.Log($"Game Over - Play Time: {Time.time}");

        gameOver.SetActive(true);
    }

    private void DestroyAllTurrets()
    {
        throw new System.NotImplementedException();
    }

    public void DidDestroyWall()
    {
        _wallsAvaible--;

        if(_wallsAvaible <= 0)
        {
           DestroyAllTurrets();

            Debug.Log($"Game Over - You Win! Play Time: {Time.time}");
 

            youWin.SetActive(true);
        }
    }
    
    private void DestoyAllTurrets()
    {
           foreach(var turret in _turrets)
           {
               Destroy(turret);
           }
    }
}
