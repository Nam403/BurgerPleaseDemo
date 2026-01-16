using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] GameObject foodPrefab;
    [SerializeField] GameObject foodSpawner;
    [SerializeField] Vector3 rotateVector = new Vector3(0f, 0f, 0f);
    [SerializeField] float distanceForSpawning = 0.5f;
    [SerializeField] float countDownTime = 0.25f;
    float countDownSpawn;

    void Start()
    {
        countDownSpawn = countDownTime;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = CharacterCarry.Instance.transform.position - foodSpawner.transform.position;
        if(distance.magnitude <= distanceForSpawning)
        {
            if (countDownSpawn <= 0)
            {
                SpawnFood();
                countDownSpawn = countDownTime;
            }
            else
            {
                countDownSpawn -= Time.deltaTime;
            }
        }
        else
        {
            countDownSpawn = countDownTime;
        }
    }

    void SpawnFood()
    {
        if(!CharacterCarry.Instance.CanCarry())
        {
            return;
        }
        GameObject food = Instantiate(foodPrefab, foodSpawner.transform.position, Quaternion.identity);
        food.transform.rotation = Quaternion.Euler(rotateVector);
        CharacterCarry.Instance.GetCarriedObject(food);
    }
}
