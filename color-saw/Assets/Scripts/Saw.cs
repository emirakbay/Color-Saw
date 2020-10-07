using UnityEngine;
using System.Collections;

public class Saw : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(new Vector3(0f, 100f, 0f) * Time.deltaTime);
    }
}
