using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public AudioSource titanTransformation;

    int i;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(Sound), 2f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > i){
            i +=5;
            Spawn();
        }
    }

    void Spawn(){
        //Random.Range(406, 473), 
        Vector3 randomSpawnPosition = new Vector3(Random.Range(100, 190), 6.5f, Random.Range(20, 100));
        Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity);
        // Debug.Log("Spawn");
    }

    void Sound(){
        titanTransformation.Play();
    }
}
