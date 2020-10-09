using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
	public const float posIncreamentByGrid = 1F;
	public const float min_X = -15.6F;
	public const float max_X = 15.6F;
	public const float min_Z = -27.6F;
	public const float max_Z = 27.6F;
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.W))
		{
			transform.position += Vector3.forward * -posIncreamentByGrid;
			ClampMovement();
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			transform.position += Vector3.back * -posIncreamentByGrid;
			ClampMovement();
		}
			
		if (Input.GetKeyDown(KeyCode.A))
		{
			transform.position += Vector3.right * posIncreamentByGrid;
			ClampMovement();
		}

		if (Input.GetKeyDown(KeyCode.D))
		{
			transform.position += Vector3.right * -posIncreamentByGrid;
			ClampMovement();
		}
	}
	public void ClampMovement()
	{
		Vector3 playerPos = transform.position;
		
		float x_Pos = playerPos.x;
		float z_Pos = playerPos.z;
		
		float xPos = Mathf.Clamp(x_Pos, min_X, max_X);
		float zPos = Mathf.Clamp(z_Pos, min_Z, max_Z);

		transform.position = new Vector3(xPos, playerPos.y, zPos);
	}
}
