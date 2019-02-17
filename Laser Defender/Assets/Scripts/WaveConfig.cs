using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject {

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawna = 0.5f;
    [SerializeField] float randomFactor = 0.3f;
    [SerializeField] int numerOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    public GameObject GetEnemyPrefab(){ return enemyPrefab; }
    public List<Transform> GetWayPoints()
    {
        var waveWayPoints = new List<Transform>();
        foreach (Transform point in pathPrefab.transform)
        {
            //Debug.Log(point.position);
            waveWayPoints.Add(point);
        }
        return waveWayPoints;
    }
    public float GetTimeBetweenSpawna(){ return timeBetweenSpawna; }
    public float GetRandomFactor(){ return randomFactor; }
    public int GetNumerOfEnemies(){ return numerOfEnemies; }
    public float mGetMveSpeed(){ return moveSpeed; }
}
