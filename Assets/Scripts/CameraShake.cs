using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.7f;
    public float dampingSpeed = 1.0f;

    // 放大相关的变量
    public float zoomFactor = 2.0f; // 放大倍数
    public float zoomSpeed = 2.0f; // 放大速度
    public float pauseDuration = 1.0f; // 停顿时间
    public float returnSpeed = 2.0f; // 返回原始大小的速度
    public float moveSpeed = 2.0f; // 相机移动到指定位置的速度

    private Vector3 initialPosition;
    private float initialSize;
    private bool isShaking = false;

    void Awake()
    {
        initialPosition = transform.localPosition;
        initialSize = Camera.main.orthographicSize;
    }

    public void TriggerShake(Vector3 collisionPoint)
    {
        if (isShaking) return; // 如果摄像机正在震动，直接返回
        StartCoroutine(ShakeCamera(collisionPoint));
    }

    IEnumerator ShakeCamera(Vector3 targetPosition)
    {
        isShaking = true;
        float elapsed = 0.0f;

        Vector3 targetCamPosition = targetPosition - new Vector3(0, 0, 10); // 保持摄像机的z坐标不变

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = new Vector3(x, y, initialPosition.z);

            elapsed += Time.deltaTime;

            // 移动摄像机到指定位置
            transform.position = Vector3.Lerp(transform.position, targetCamPosition, Time.deltaTime * moveSpeed);

            // 放大摄像机
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, initialSize / zoomFactor, Time.deltaTime * zoomSpeed);

            yield return null;
        }

        // 停顿一段时间
        yield return new WaitForSeconds(pauseDuration);

        // 回到原始位置和大小
        while (Vector3.Distance(transform.position, initialPosition) > 0.01f || Mathf.Abs(Camera.main.orthographicSize - initialSize) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, initialPosition, Time.deltaTime * dampingSpeed);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, initialSize, Time.deltaTime * returnSpeed);
            yield return null;
        }

        transform.position = initialPosition;
        Camera.main.orthographicSize = initialSize;
        isShaking = false;
    }
}
