using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanPoint : MonoBehaviour
{
    public GameObject Enemy;
    public float SpwanPointOffset, SpwanRateMin, SpwanRateMax;

    private float SpwanTimer;

    // Start is called before the first frame update
    void Start()
    {
        SpwanTimer = Random.Range(SpwanRateMin, SpwanRateMax);
    }

    // Update is called once per frame
    void Update()
    {
        SpwanTimer -= Time.deltaTime;
        if (SpwanTimer < 0)
        {
            Instantiate(Enemy,
                new Vector2(transform.position.x, transform.position.y
                + Random.Range(-SpwanPointOffset, SpwanPointOffset))
                , Quaternion.identity);

            SpwanTimer = Random.Range(SpwanRateMin, SpwanRateMax);
        }
    }
}
