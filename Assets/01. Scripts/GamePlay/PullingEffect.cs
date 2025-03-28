using UnityEngine;

public class PullingEffect : MonoBehaviour
{
    public NoteSpawn noteSpawn;

    public bool isLeft;

    private void OnParticleSystemStopped()
    {
        if (isLeft)
        {
            // GameObject를 풀로 반환하려면 Enqueue로 추가
            noteSpawn.leftPullingEffect.Enqueue(gameObject);
        }
        else
        {
            noteSpawn.rightPullingEffect.Enqueue(gameObject);
        }
        gameObject.SetActive(false); // 비활성화 후 풀에 반환
    }
}