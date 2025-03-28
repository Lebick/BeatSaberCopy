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
            if (isLeft)
            {
                //noteSpawn.leftPullingNote.Add(gameObject);
                noteSpawn.GetLeftEffect(transform.position);
            }
            else
            {
                //noteSpawn.rightPullingNote.Add(gameObject);
                noteSpawn.GetRightEffect(transform.position);
            }
            //gameObject.SetActive(false);

            //юс╫ц
            Destroy(gameObject);
            GamePlayManager.instance.combo = 0;
        }
    }
}
