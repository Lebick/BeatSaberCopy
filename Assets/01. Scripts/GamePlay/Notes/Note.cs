using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Note : MonoBehaviour
{
    public float progress = 0f;

    public Vector3 startPos, endPos;

    public bool isLeft;

    public NoteSpawn noteSpawn;

    public void initialize()
    {
        progress = 0f;
        startPos = transform.position;
        startPos.z = 17;
        endPos = GamePlayManager.instance.correctPos.position;

        endPos.x = startPos.x;
        endPos.y = startPos.y;
    }

    protected virtual void Start()
    {
        startPos = transform.position;
        startPos.z = 17;
        endPos = GamePlayManager.instance.correctPos.position;

        endPos.x = startPos.x;
        endPos.y = startPos.y;
    }

    protected virtual void Update()
    {
        progress += Time.deltaTime;
        transform.position = startPos + (endPos - startPos) * progress;

        if (progress > 1.2f)
        {
            // Enqueue�� ������Ʈ Ǯ�� �߰�
            if (isLeft)
            {
                noteSpawn.leftPullingNote.Enqueue(gameObject); // ���� Add()�� Enqueue()�� ����
            }
            else
            {
                noteSpawn.rightPullingNote.Enqueue(gameObject); // ���� Add()�� Enqueue()�� ����
            }
            gameObject.SetActive(false);
            GamePlayManager.instance.combo = 0;
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerAttack player) && player.isLeft == isLeft && player.distance >= 0.1f && Mathf.Abs(1 - progress) <= 0.2f)
        {
            GamePlayManager.instance.combo++;

            // Enqueue�� ������Ʈ Ǯ�� �߰�
            if (isLeft)
            {
                noteSpawn.leftPullingNote.Enqueue(gameObject); // ���� Add()�� Enqueue()�� ����
                noteSpawn.GetLeftEffect(transform.position);
            }
            else
            {
                noteSpawn.rightPullingNote.Enqueue(gameObject); // ���� Add()�� Enqueue()�� ����
                noteSpawn.GetRightEffect(transform.position);
            }
            gameObject.SetActive(false);
        }
    }
}