using UnityEngine;
using UnityEngine.InputSystem;

public class Raycaster
{
	private Camera camera;
	private int groundLayer;

	public void SetCamera(in Camera camera)
	{
		this.camera = camera;
	}

	public void SetGroundLayer(int groundLayer)
	{
		this.groundLayer = groundLayer;
	}

	public Vector3 GetCursorPosition()
	{
		Vector3 mousePos = Mouse.current.position.ReadValue();
		mousePos.z = camera.nearClipPlane;
		Ray rayFromCursor = camera.ScreenPointToRay(mousePos);
		RaycastHit raycastHit;
		int layerMask = 1 << groundLayer;
		Physics.Raycast(rayFromCursor, out raycastHit, int.MaxValue, layerMask);
		return raycastHit.point;
	}
}
