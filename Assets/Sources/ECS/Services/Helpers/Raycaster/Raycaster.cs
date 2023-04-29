using UnityEngine;

public class Raycaster
{
	private Camera camera;
	private int groundLayer;

	public void SetCamera(Camera camera)
	{
		this.camera = camera;
	}

	public void SetGroundLayer(int groundLayer)
	{
		this.groundLayer = groundLayer;
	}

	public Vector3 GetCursorPosition()
	{
		Ray rayFromCursor = camera.ScreenPointToRay(Input.mousePosition);
		RaycastHit raycastHit;
		int layerMask = 1 << groundLayer;
		Physics.Raycast(rayFromCursor, out raycastHit, int.MaxValue, layerMask);
		return raycastHit.point;
	}
}
