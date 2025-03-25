using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Note : MonoBehaviour
{
    public float progress = 0f;

    public float moveTime;

    private void Start()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        Vector3 startPos = transform.position;

        Vector3 endPos = GamePlayManager.instance.correctPos.position;
        endPos.x = startPos.x;
        endPos.y = startPos.y;

        while(true)
        {
            progress += Time.deltaTime;
            transform.position = startPos + (endPos - startPos) * progress / moveTime;

            if (progress >= 4f)
                progress = 0f;

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print(Mathf.Abs(1 - progress));
        if (other.CompareTag("PlayerAttack") || Mathf.Abs(1 - progress) <= 0.2f)
        {
            print("ÀÌÈ÷Èþ");
        }
    }
}
