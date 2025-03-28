using UnityEngine;

public class PullingEffect : MonoBehaviour
{
    public NoteSpawn noteSpawn;

    public bool isLeft;

    private void OnParticleSystemStopped()
    {
        if (isLeft)
        {
            // GameObject�� Ǯ�� ��ȯ�Ϸ��� Enqueue�� �߰�
            noteSpawn.leftPullingEffect.Enqueue(gameObject);
        }
        else
        {
            noteSpawn.rightPullingEffect.Enqueue(gameObject);
        }
        gameObject.SetActive(false); // ��Ȱ��ȭ �� Ǯ�� ��ȯ
    }
}