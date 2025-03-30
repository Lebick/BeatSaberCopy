using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class GuardNote : Note
{
    public override void initialize()
    {
        base.initialize();
    }

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
            if (isLeft)
            {
                noteSpawn.leftSpecialPullingNote.Enqueue(gameObject);
            }
            else
            {
                noteSpawn.rightSpecialPullingNote.Enqueue(gameObject);
            }
            gameObject.SetActive(false);
            GamePlayManager.instance.combo = 0;
        }
    }


    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttack") && Mathf.Abs(1 - progress) <= 0.2f &&
            Mathf.Abs(Mathf.Abs(other.transform.parent.transform.eulerAngles.y - 180) - 90) <= 30f)
        {
            if(other.GetComponent<PlayerAttack>().btnReleaseTime == 0)
            {
                StartCoroutine(WaitReleaseTime(other));
            }
            else if (other.GetComponent<PlayerAttack>().btnReleaseTime <= 0.2f)
            {
                GamePlayManager.instance.combo++;
                if (isLeft)
                {
                    noteSpawn.leftSpecialPullingNote.Enqueue(gameObject);
                    noteSpawn.GetLeftEffect(transform.position);
                }
                else
                {
                    noteSpawn.rightSpecialPullingNote.Enqueue(gameObject);
                    noteSpawn.GetRightEffect(transform.position);
                }
                gameObject.SetActive(false);

            }
        }
    }

    private IEnumerator WaitReleaseTime(Collider other)
    {
        yield return new WaitForSeconds(0.2f);

        if (other.GetComponent<PlayerAttack>().btnReleaseTime != 0)
        {
            GamePlayManager.instance.combo++;

            if (isLeft)
            {
                noteSpawn.leftSpecialPullingNote.Enqueue(gameObject);
                noteSpawn.GetLeftEffect(transform.position);
            }
            else
            {
                noteSpawn.rightSpecialPullingNote.Enqueue(gameObject);
                noteSpawn.GetRightEffect(transform.position);
            }
            gameObject.SetActive(false);
        }
    }
}
