using System.Threading;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject pipePrefab;
    private float timer;
    private float timeMax = 1f ;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if (timer>timeMax)
        {
            SpawnPipe();
            timer = 0;
        }
            
        
    }
    public void SpawnPipe()
    {
        Vector3 SpawnPosition = transform.position + new Vector3(Random.Range(1, 18),Random.Range(1, 18),0)*Time.deltaTime;
       
        GameObject newPipe;

        newPipe = Instantiate(pipePrefab,SpawnPosition,Quaternion.identity);

        Destroy(newPipe,5f);

    }
}
