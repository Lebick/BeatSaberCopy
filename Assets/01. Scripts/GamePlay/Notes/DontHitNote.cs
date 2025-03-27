using UnityEngine;

public class DontHitNote : Note
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        progress += Time.deltaTime;
        transform.position = startPos + (endPos - startPos) * progress;

        if (progress > 1.2f)
        {
            Destroy(gameObject);
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttack") && Mathf.Abs(1 - progress) <= 0.2f)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);

            GamePlayManager.instance.combo = 0;
        }
    }
}
