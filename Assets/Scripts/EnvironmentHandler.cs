using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentHandler : MonoBehaviour
{
    public GameObject creaturePrefab;
    public GameObject foodPrefab;
    private Transform creaturesTransform;
    private Transform foodsTransform;
    public float foodSpawnDelay = 10f;
    public float creatureSpawnDelay = 10f;
    private float foodSpawnTime;
    private float creatureSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        creaturesTransform = this.transform.GetChild(0);
        foodsTransform = this.transform.GetChild(1);
        this.populateFood();
        this.populateCreatures();
        this.foodSpawnTime = foodSpawnDelay;
        this.creatureSpawnTime = creatureSpawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")) {
            GameObject creature = Instantiate(creaturePrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            creature.transform.parent=creaturesTransform;
        }

        this.foodSpawnTime -= Time.deltaTime;
        this.creatureSpawnTime -= Time.deltaTime;

        if (foodSpawnTime < 0f) {
            this.spawnFood();
            this.foodSpawnTime = this.foodSpawnDelay;
        } 

        if (creatureSpawnTime < 0f) {
            this.spawnCreature();
            this.creatureSpawnTime = this.creatureSpawnDelay;
        }

    }

    private void populateCreatures() {
        for (int i=0; i<5; i++) {
            this.spawnCreature();
        }
    }

    private void spawnCreature() {
        GameObject food = Instantiate(creaturePrefab, new Vector2(Random.Range(-10f, 10f),  Random.Range(-5f, 5f)), Quaternion.identity);
        food.transform.parent = creaturesTransform;
    }

    private void populateFood() {
        for (int i=0; i<20; i++) {
            this.spawnFood();
        }
    }

    private void spawnFood() {
        GameObject food = Instantiate(foodPrefab, new Vector2(Random.Range(-10f, 10f),  Random.Range(-5f, 5f)), Quaternion.identity);
        food.transform.parent = foodsTransform;
    }
}
