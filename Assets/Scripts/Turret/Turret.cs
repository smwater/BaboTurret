using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform Player;

    private float _respawnTime = 0f;
    private bool _isShoot = false;
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

        if (_isShoot)
        {
            Debug.Log("¸ñÇ¥ Æ÷Âø");
            playerPosition = new Vector3(Player.position.x, transform.position.y, Player.position.z);
            gameObject.transform.LookAt(playerPosition);

            if (timer >= _respawnTime)
            {
                GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
                bullet.transform.LookAt(Player);

                timer = 0f;
            }
        }
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
