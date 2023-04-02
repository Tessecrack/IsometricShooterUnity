using System.Collections.Generic;
using UnityEngine;

public class Arsenal : MonoBehaviour
{
    [SerializeField] private List<Weapon> listOfWeapons;

    [SerializeField] private Transform pointSpawnWeapon;

    private Weapon currentWeapon;
    public int CurrentWeaponNumber { get; private set; }

    void Start()
    {
        ChangeWeapon(1);        
    }

    public bool ChangeWeapon(int number)
    {
        if (listOfWeapons.Count <= 0 
            || CurrentWeaponNumber == number
            || number < 0 
            || number >= listOfWeapons.Count)
        {
            return false;
        }
        CurrentWeaponNumber = number;
        SpawnWeapon(listOfWeapons[number]);
        return true;
    }

    public Weapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    private void SpawnWeapon(Weapon weapon)
    { 
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }
        currentWeapon = Instantiate(weapon, pointSpawnWeapon, false);
	}
}
