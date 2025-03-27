using UnityEngine;

public class Rainbow : MonoBehaviour
{
    public Material material;
    public Gradient color;

    private float progress = 0f;

    public float time;

    private void Update()
    {
        progress += Time.deltaTime / time;

        if (progress >= 1)
            progress -= 1;

        material.color = color.Evaluate(progress);
    }
}
