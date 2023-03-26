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
        int num = number - 1;
        if (num < 0 || num > listOfWeapons.Count)
        {
            return;
        }
        SpawnWeapon(listOfWeapons[num]);
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
