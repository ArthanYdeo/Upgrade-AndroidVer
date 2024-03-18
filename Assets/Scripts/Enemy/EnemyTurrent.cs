using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurrent : MonoBehaviour
{
    public GameObject LaserBullet;
    public Transform LaserBulletPos;

    private float timer;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector2.Distance(transform.position, player.transform.position);


        if (distance > 7)
        {
            timer += Time.deltaTime;

            if (timer > 4)
            {
                timer = 0;
                shoot();
            }
        }


    }

    void shoot()
    {
        Instantiate(LaserBullet, LaserBulletPos.position, Quaternion.identity);
    }
}
