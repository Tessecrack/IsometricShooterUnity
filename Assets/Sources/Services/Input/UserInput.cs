using UnityEngine;

public class UserInput : MonoBehaviour
{
    private Controls controls;
    public bool IsStartFire { get; private set; }
    public bool IsStopFire { get; private set; }
    public bool IsDash { get; private set; }

	private void Awake()
	{
        controls = new Controls();

        controls.Main.Fire.performed += context => StartFire();
        controls.Main.Fire.canceled += context => StopFire();

        controls.Main.Dash.performed += context => StartDash();
		controls.Main.Dash.canceled += context => StopDash();
	}

    private void StartFire()
    {
        IsStartFire = true;
        IsStopFire = false;
    }

    private void StopFire()
    {
        IsStartFire = false;
        IsStopFire = true;
    }

    private void StartDash()
    {
        IsDash = true;
    }

	private void StopDash()
	{
		IsDash = false;
	}

    public float GetValueHorizontal()
    {
        return controls.Main.MoveLeftRight.ReadValue<float>();
    }

	public float GetValueVertical()
	{
		return controls.Main.MoveForwardBack.ReadValue<float>();
	}

    public int GetSelectedWeapon()
    {
        return controls.Main.SelectWeapon.ReadValue<int>();
    }
}
