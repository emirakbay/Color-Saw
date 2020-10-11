using System.Collections;
using UnityEngine;

public class Saw : MonoBehaviour
{
	public Vector3 rotation;

	public Transform model;

	private void Start()
	{
		StartCoroutine(RotationCoroutine());
	}

	private void OnTriggerEnter(Collider other)
	{
		var block = other.GetComponent<Block>();
        if (block)
            block.DestroyBlock();
	}

	private IEnumerator RotationCoroutine()
	{
		while (true)
		{
			model.Rotate(rotation, rotation.magnitude * Time.deltaTime);
			yield return null;
		}
	}
}