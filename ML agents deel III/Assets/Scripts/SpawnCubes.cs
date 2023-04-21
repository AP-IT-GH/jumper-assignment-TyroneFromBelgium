using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCubes : MonoBehaviour
{
    public GameObject prefab;
    public float minSpeed = 5f;
    public float maxSpeed = 10f;
    public float minScale = 0.5f;
    public float maxScale = 2f;
    public float spawnCooldown = 5f;
    public float rotater = 90f;
    private float timer = 0f;

    void Update()
    {
        // Tel de tijd sinds de laatste spawn
        timer += Time.deltaTime;

        // Als de cooldown voorbij is, spawn een prefab en reset de timer
        if (timer >= spawnCooldown)
        {
            SpawnPrefab();
            timer = 0f;
        }
    }

    void SpawnPrefab()
    {
        // Maak een kopie van de prefab op de locatie van de spawner
        GameObject spawnedPrefab = Instantiate(prefab, transform.position, Quaternion.identity);

        // Geef de prefab een willekeurige snelheid en scale
        float speed = Random.Range(minSpeed, maxSpeed);
        float scale = Random.Range(minScale, maxScale);

        Rigidbody rb = spawnedPrefab.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        spawnedPrefab.transform.localScale = new Vector3(spawnedPrefab.transform.localScale.x, scale, spawnedPrefab.transform.localScale.z);

        // Voeg rotatie toe
        spawnedPrefab.transform.rotation = Quaternion.Euler(90f, rotater, 0f);

        // Vernietig de prefab na 5 seconden
        Destroy(spawnedPrefab, 5f);
    }
}
