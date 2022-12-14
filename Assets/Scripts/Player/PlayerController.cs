using UnityEngine;


[RequireComponent(typeof(Ship))]
[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour
{

    private Ship _ship;
    private bool _isEnabledController = true;

    private void Awake()
    {
        _ship = GetComponent<Ship>(); // ???????? ???????? ???????        
    }

    private void Update()
    {
        // ??????, ???? ??? ?????
        if (_isEnabledController)
        {
            MovementInput(); // Move player ship
            FireInput(); // Fire player ship
        }
    }

    /// <summary>
    /// Movement Player Ship
    /// </summary>
    private void MovementInput()
    {
        // Get movement input
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        // Vector2 direction movement by player
        Vector2 direction = new Vector2(horizontalMovement, verticalMovement);

        // trigger Move
        _ship.movementEvent.CallMoveEvent(direction, _ship._currentShipDetails.speedShip);

    }

    /// <summary>
    /// Fire Input ship
    /// </summary>
    private void FireInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _ship.fireWeaponEvent.CallOnFireWeaponEvent(_ship.weaponDetails, Vector2.one);
        }
    }

    /// <summary>
    /// ???????? ?????????? ??????
    /// </summary>
    public void EnablePLayerContoller()
    {
        _isEnabledController = true;
    }

    /// <summary>
    /// ????????? ?????????? ??????
    /// </summary>
    public void DisablePLayerContoller()
    {
        _isEnabledController = false;
    }

}
