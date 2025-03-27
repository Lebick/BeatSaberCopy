using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour
{
    public GameObject test;
    public GameObject test1;
    public GameObject test2;

    private void Start()
    {
        StartCoroutine(Testing());
    }

    private IEnumerator Testing()
    {
        while (true)
        {
            Instantiate(test);
            yield return new WaitForSeconds(1f);
            Instantiate(test1);
            yield return new WaitForSeconds(1f);
            Instantiate(test2);
            yield return new WaitForSeconds(1f);
        }
    }
}
