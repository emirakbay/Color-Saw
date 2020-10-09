using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Color color;
	public int playerBaseBlockCount;
	public Dictionary<Vector3, Block> blocks = new Dictionary<Vector3, Block>();
    public static Player Instance;
    private void Awake()
	{
		if (Instance !=null && Instance !=this)
			Destroy(this.gameObject);

		Instance = this;
	}

    // Start is called before the first frame update
    void Start()
    {
        PlayerBase();
        RefreshBlocks();
    }

	public void PlayerBase()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			var child = transform.GetChild(i);
			var childBlock = child.GetComponent<Block>();
			blocks.Add(child.localPosition, childBlock);

			if (!childBlock.playerBase)
				child.GetComponent<MeshRenderer>().material.color = color;
			else
				playerBaseBlockCount++;
		}
	}

	public void RefreshBlocks()
	{
		foreach (var block in blocks.Values)
		{
			block.neighbor = false;
		}

		foreach (var block in blocks.Values)
		{
			if (block.playerBase)
			{
				block.CheckNeighbor();
			}
		}
	
		List<Block> notNeighborBlocks = new List<Block>();
		foreach (var block in blocks.Values)
		{
			if (!block.neighbor)
				notNeighborBlocks.Add(block);
		}

		foreach (var block in notNeighborBlocks)
		{
			block.DestroyBlock(false);
		}

		if (playerBaseBlockCount == blocks.Count)
			GameManager.Instance.Advance();
	}

	public Block BlockByKey(Vector3 key)
	{
		return blocks.ContainsKey(key) ? blocks[key] : null;
	}
}
