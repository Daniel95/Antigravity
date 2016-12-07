using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ActivateWeapon : MonoBehaviour, IEventSystemHandler {

    [SerializeField]
    private LayerMask rayLayers;
    
    [SerializeField]
    private int maxRaycastDistance = 40;

    [SerializeField]
    private GameObject gun;

    [SerializeField]
    private Transform spawnTransform;

    private PlayerInputs playerInputs;

    private LookAt gunLookAt;

    private List<IWeapon> weapons = new List<IWeapon>();

    private int weaponIndex;

    void Start()
    {
        playerInputs = GetComponent<PlayerInputs>();

        foreach (IWeapon weapon in GetComponents<IWeapon>())
        {
            weapons.Add(weapon);
        }

        gunLookAt = gun.GetComponent<LookAt>();
    }

    //when given the right input, activate the current weapon
    private void Update()
    {
        Vector2 dir = playerInputs.CheckShootInput();

        if (dir != Vector2.zero) {
            gunLookAt.UpdateLookAt(dir + (Vector2)transform.position);

            //Vector2 dir = (clickPos - (Vector2)spawnTransform.position).normalized;

            Vector2 targetPos = dir * maxRaycastDistance;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetPos, maxRaycastDistance, rayLayers);

            if (hit.collider != null)
            {
                targetPos = hit.point;
            }

            //fire the current weapon
            weapons[weaponIndex].Fire(dir, targetPos, spawnTransform.position);
        }
        /*
        if (Input.GetKeyDown(shootInput))
        {
            if (!MouseDetect.IsPointerOverUIObject()) {
                Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                gunLookAt.UpdateLookAt(clickPos);

                Vector2 dir = (clickPos - (Vector2)spawnTransform.position).normalized;

                Vector2 targetPos = dir * maxRaycastDistance;

                RaycastHit2D hit = Physics2D.Raycast(transform.position, targetPos, maxRaycastDistance, rayLayers);

                if (hit.collider != null) {
                    targetPos = hit.point;
                }

                //fire the current weapon
                weapons[weaponIndex].Fire(dir, targetPos, spawnTransform.position);
            }
        }*/
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
