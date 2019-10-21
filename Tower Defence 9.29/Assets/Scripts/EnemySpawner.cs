using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static int CountEnemyAlive = 0;//Improve founction 01, when before enemy been destory or arrive end point, spwan next wave.
    public Wave[] waves;
    public WaveRight[] rightwaves;
    public Transform START;
    public Transform RIGHTSTART;
    public float waveRate = 0.3f;
    public float waveRateRight = 1f;


    void Start()
    {
        StartCoroutine(WaitPlayer());
    }

    /*public void Stop()
    {
        StopCoroutine(WaitPlayer());
    }*/

    public IEnumerator WaitPlayer()
    {
        Time.timeScale = 1;
        yield return new WaitForSecondsRealtime(13);
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnEnemyRight());
    }

    IEnumerator SpawnEnemy()
    {
        foreach (Wave wave in waves)
        {
            for (int i = 0; i < wave.count; i++)
            {
                GameObject.Instantiate(wave.enemyPrefab, START.position, Quaternion.identity);
                //GameObject.Instantiate(wave.enemyPrefab, RIGHTSTART.position,Quaternion.identity);
                CountEnemyAlive++;
                if (i != wave.count - 1)
                    yield return new WaitForSeconds(wave.rate);
            }
            while (CountEnemyAlive > 1)//If anyone enemy in the map,new enemy will not spawn.
            {
                yield return 0;
            }
            yield return new WaitForSeconds(waveRate);

            /*while (CountEnemyAlive > 0)
            {
                yield return 0;
            }*/

        }
    }

    IEnumerator SpawnEnemyRight()
    {
        foreach (WaveRight wave in rightwaves)
        {
            for (int i = 0; i < wave.count; i++)
            {
                //GameObject.Instantiate(wave.enemyPrefab, START.position, Quaternion.identity);
                GameObject.Instantiate(wave.enemyRightPrefab, RIGHTSTART.position, Quaternion.identity);
                CountEnemyAlive++;
                if (i != wave.count - 1)
                    yield return new WaitForSeconds(wave.rate);
            }
            /* while (CountEnemyAlive > 1)//If anyone enemy in the map,new enemy will not spawn.
             {
                 yield return 0;
             }*/
            yield return new WaitForSeconds(waveRateRight);
        }
        GameManager.Instance.Win();
    }
}

