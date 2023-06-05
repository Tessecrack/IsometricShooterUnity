using UnityEngine;

public class UserInput : MonoBehaviour
{
    private Controls controls;
    public bool IsStartFire { get; private set; }
    public bool IsStopFire { get; private set; }
    public bool IsDash { get; private set; }

    public int SelectedWeapon { get; private set; }

	private void Awake()
	{
        controls = new Controls();

        controls.Main.Fire.performed += context => StartFire();
        controls.Main.Fire.canceled += context => StopFire();

        controls.Main.Dash.performed += context => StartDash();
		controls.Main.Dash.canceled += context => StopDash();

        controls.Main.FirstWeapon.performed += context => SelectWeapon(0);
        controls.Main.SecondWeapon.performed += context => SelectWeapon(1);
		controls.Main.ThirdWeapon.performed += context => SelectWeapon(2);
	}

	private void OnEnable()
	{
        controls.Enable();
	}

	private void OnDisable()
	{
		controls.Disable();
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

    private void SelectWeapon(int number)
    {
		SelectedWeapon = number;
    }

    public float GetValueHorizontal()
    {
		return controls.Main.MoveLeftRight.ReadValue<float>();
    }

	public float GetValueVertical()
	{
		return controls.Main.MoveForwardBack.ReadValue<float>();
	}
}
