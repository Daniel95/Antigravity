using UnityEngine;
using System.Collections.Generic;

public class ActivateWeapon : MonoBehaviour {

    [SerializeField]
    private LayerMask layers;

    [SerializeField]
    private KeyCode shootInput = KeyCode.Mouse0;

    [SerializeField]
    private int maxRaycastDistance = 40;

    [SerializeField]
    private GameObject gun;

    [SerializeField]
    private Transform spawnTransform;

    private LookAt gunLookAt;

    private List<IWeapon> weapons = new List<IWeapon>();

    private int weaponIndex;

    void Start()
    {
        foreach (IWeapon weapon in GetComponents<IWeapon>())
        {
            weapons.Add(weapon);
        }

        gunLookAt = gun.GetComponent<LookAt>();
    }

    //when given the right input, activate the current weapon
    private void Update()
    {
        if (Input.GetKeyDown(shootInput))
        {
            Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            gunLookAt.UpdateLookAt(clickPos);

            Vector2 dir = (clickPos - (Vector2)spawnTransform.position).normalized;

            Vector2 targetPos = dir * maxRaycastDistance;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetPos, maxRaycastDistance, layers);

            if (hit.collider != null) {
                targetPos = hit.point;
            }

            //fire the current weapon
            weapons[weaponIndex].Fire(dir, targetPos, spawnTransform.position);
        }
    }

    void ChangeWeapon(int _change)
    {
        weaponIndex += _change;

        if (weaponIndex < 0)
            weaponIndex = weapons.Count - 1;

        else if (weaponIndex > weapons.Count - 1)
            weaponIndex = 0;
    }
}
