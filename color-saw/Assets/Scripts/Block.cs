using UnityEngine;

public class Block : MonoBehaviour
{
	public bool playerBase;
	public bool neighbor;
	private Vector3 blockPosition;
	private void Awake()
	{
		blockPosition = transform.localPosition;
		if (playerBase)
			neighbor = true;
	}

	public void DestroyBlock(bool refresh = true)
	{
		Player.Instance.blocks.Remove(blockPosition);
		
		if (transform.childCount > 0)
		{
			var particles = transform.GetChild(0);
			particles.SetParent(null);
			particles.gameObject.SetActive(true);
		}

		if (playerBase)
			GameManager.Instance.GameOver();

		else
		{
			if (refresh)
				Player.Instance.RefreshBlocks();

			Destroy(gameObject);
		}
	}

	public void CheckNeighbor()
	{
		neighbor = true;

		var rightBlock = Player.Instance.BlockByKey(blockPosition + new Vector3(1, 0, 0));
		var leftBlock = Player.Instance.BlockByKey(blockPosition + new Vector3(-1, 0, 0));
		var topBlock = Player.Instance.BlockByKey(blockPosition + new Vector3(0, 0, 1));
		var bottomBlock = Player.Instance.BlockByKey(blockPosition + new Vector3(0, 0, -1));

		if (rightBlock && !rightBlock.neighbor)
			rightBlock.CheckNeighbor();
	
		if (leftBlock && !leftBlock.neighbor)
			leftBlock.CheckNeighbor();
	
		if (topBlock && !topBlock.neighbor)
			topBlock.CheckNeighbor();

		if (bottomBlock && !bottomBlock.neighbor)
			bottomBlock.CheckNeighbor();
	}
}