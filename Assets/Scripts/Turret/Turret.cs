using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform Player;

    private float _respawnTime = 0f;
    private bool _isShoot = false;
    private float _minRange = -sqrt(3) / 2.0f;
    private const float _maxRange = 0f;

    private static int sqrt(int v)
    {
        throw new NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        _respawnTime = 1.5f;
    }

    private float timer = 0f;
    private Vector3 playerPosition;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (_isShoot && FindRange())
        {
            Debug.Log("¸ñÇ¥ Æ÷Âø");
            playerPosition = new Vector3(Player.position.x, transform.position.y, Player.position.z);

            if (timer >= _respawnTime)
            {
                GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
                bullet.transform.LookAt(Player);

                timer = 0f;
            }
        }
    }

    private bool FindRange()
    {
        Vector3 distanceVector = Player.position - transform.position;
        Debug.Log(Vector3.Dot(transform.forward, distanceVector.normalized));

        if (Vector3.Dot(transform.forward, distanceVector) > _minRange && Vector3.Dot(transform.forward, distanceVector) < _maxRange)
        {
            return true;
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {   
            _isShoot = true;
        }
    }   

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _isShoot = false;
        }
    }
}
