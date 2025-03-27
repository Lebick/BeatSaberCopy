using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Note : MonoBehaviour
{
    public float progress = 0f;

    public GameObject destroyEffect;

    protected Vector3 startPos, endPos;

    protected virtual void Start()
    {
        startPos = transform.position;
        endPos = GamePlayManager.instance.correctPos.position;

        endPos.x = startPos.x;
        endPos.y = startPos.y;
    }

    protected virtual void Update()
    {
        progress += Time.deltaTime;
        transform.position = startPos + (endPos - startPos) * progress;

        if(progress > 1.2f)
        {
            Destroy(gameObject);
            GamePlayManager.instance.combo = 0;
        }
    }


    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttack") && Mathf.Abs(1 - progress) <= 0.2f)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);

            GamePlayManager.instance.combo++;
        }
    }
}
