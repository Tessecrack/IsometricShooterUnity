public class UserInput
{
    private Controls controls;
    public bool IsStartFire { get; private set; }
    public bool IsStopFire { get; private set; }
    public bool IsDash => controls.Main.Dash.WasPerformedThisFrame();
    public int SelectedWeapon { get; private set; }

	public static UserInput CreateUserInput()
	{
		var userInput = new UserInput();
		userInput.Init();
		return userInput;
	}

	public void Init()
	{
		controls = new Controls();

		controls.Main.Fire.performed += context => StartFire();
		controls.Main.Fire.canceled += context => StopFire();

		controls.Main.FirstWeapon.performed += context => SelectWeapon(0);
		controls.Main.SecondWeapon.performed += context => SelectWeapon(1);
		controls.Main.ThirdWeapon.performed += context => SelectWeapon(2);
	}

	public void Enable()
	{
		controls.Enable();
	}

	public void Disable()
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
