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

    public void ChangeWeapon(int number)
    {
        if (listOfWeapons.Count <= 0)
        {
            return;
        }
        if (CurrentWeaponNumber == number)
        {
            return;
        }
        CurrentWeaponNumber = number;
        if (number < 0 || number >= listOfWeapons.Count)
        {
            return;
        }
        SpawnWeapon(listOfWeapons[number]);
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
        for (int i = 0; i < listOfWeapons.Count; ++i)
        {
            Debug.Log("Arsenal " + listOfWeapons[i].nameWeapon + " " + listOfWeapons[i].CurrentTypeWeapon);
        }
        currentWeapon = Instantiate(weapon, pointSpawnWeapon, false);
	}
}
