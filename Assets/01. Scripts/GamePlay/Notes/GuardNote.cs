using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class GuardNote : Note
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
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
                //Instantiate(destroyEffect, transform.position, Quaternion.identity);
                print("막았다!!!!!!!!!!!!!!");
                Destroy(gameObject);

                GamePlayManager.instance.combo++;
            }
        }
    }

    private IEnumerator WaitReleaseTime(Collider other)
    {
        yield return new WaitForSeconds(0.2f);

        if (other.GetComponent<PlayerAttack>().btnReleaseTime != 0)
        {
            //Instantiate(destroyEffect, transform.position, Quaternion.identity);
            print("막았다!!!!!!!!!!!!!!");
            Destroy(gameObject);

            GamePlayManager.instance.combo++;
        }
    }
}
