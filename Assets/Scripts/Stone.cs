using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private Camera mainCamera;
    public GameObject hitPlayerEffect;
    public GameObject hitWallEffect;


    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);
        if (IsOutside(viewportPosition))
        {
            Camera.main.GetComponent<CameraShake>().TriggerShake(transform.position);
            Instantiate(hitWallEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private bool IsOutside(Vector3 viewportPosition)
    {
        return viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Camera.main.GetComponent<CameraShake>().TriggerShake(transform.position);
            Wall wall = collision.gameObject.GetComponent<Wall>();
            wall.TakeDamage(1);
            Instantiate(hitWallEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Camera.main.GetComponent<CameraShake>().TriggerShake(transform.position);
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.HitByStone();
            Instantiate(hitPlayerEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Floor"))
        {
            Camera.main.GetComponent<CameraShake>().TriggerShake(transform.position);
            Instantiate(hitWallEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
