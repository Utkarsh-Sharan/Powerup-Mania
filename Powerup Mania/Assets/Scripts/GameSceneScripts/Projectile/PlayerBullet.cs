using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private float _bulletSpeed = 10f; // Bullet speed

    void Start()
    {
        Destroy(this.gameObject, 5f);
    }

    void Update()
    {
        transform.Translate(Vector3.up * _bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Tr01Enemy>())
        {
            LevelManager.Instance.DestroyEnemy(Tr01Enemy.GetEnemyID());

            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.GetComponent<MagnetPowerup>())
        {
            LevelManager.Instance.CollectPowerup(MagnetPowerup.GetPowerupID());

            other.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.GetComponent<LevelEndPowerup>())
        {
            LevelManager.Instance.CollectPowerup(LevelEndPowerup.GetPowerupID());

            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
