using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharges : MonoBehaviour
{
    [SerializeField]
    private float maxCharge = 300;

    [SerializeField]
    private float startCharge = 100;

    [SerializeField]
    private float chargeIncrementSpeed = 0.01f;

    [SerializeField]
    private int rechargeDelay = 45;

    private float _chargeValue;

    private int _fullCharges;

    private Frames _frames;

    private ScaleBar _scaleBar;
    private ImageColor _imageColor;
    private ChargeBorders _chargeBorders;

    private Coroutine _rechargeCoroutine;

    public static Action NotEnoughCharges;

    public enum ChargeAbleAction
    {
        ReverseSpeed = 100,
    }

    private void Start()
    {
        GameObject barGO = GameObject.FindGameObjectWithTag(Tags.ChargeBar);

        if (barGO == null)
            return;

        _scaleBar = barGO.GetComponent<ScaleBar>();
        _imageColor = barGO.GetComponentInChildren<ImageColor>();
        _chargeBorders = barGO.GetComponentInChildren<ChargeBorders>();

        _frames = GetComponent<Frames>();

        _chargeValue = startCharge;

        StartRecharge();
    }

    /// <summary>
    /// Returns whether we have enough charge required for the action, if we have enough decrement it from chargeValue, and start recharging
    /// </summary>
    /// <param name="actionToCharge"></param>
    /// <returns></returns>
    public bool UseCharge(ChargeAbleAction actionToCharge)
    {
        int actionCost = (int)actionToCharge;

        if (actionCost > _chargeValue)
        {
            if (NotEnoughCharges != null)
                NotEnoughCharges();

            return false;
        }

        _chargeValue -= actionCost;

        AdjustBar();
        UpdateCharges(Mathf.FloorToInt(_chargeValue / (int)ChargeAbleAction.ReverseSpeed));

        StopCoroutine(_rechargeCoroutine);
        _frames.StopExecuteAfterDelay();
        _frames.ExecuteAfterDelay(rechargeDelay, StartRecharge);

        return true;
    }

    public void StartRecharge()
    {
        _rechargeCoroutine = StartCoroutine(Recharge());
    }

    /// <summary>
    /// Recharges the players charges, and scales the bar.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Recharge()
    {
        var fixedUpdate = new WaitForFixedUpdate();

        while (_chargeValue < maxCharge)
        {
            _chargeValue += chargeIncrementSpeed;
            AdjustBar();
            yield return fixedUpdate;
        } 
    }

    private void AdjustBar()
    {
        float proportion = _chargeValue / maxCharge;
        _scaleBar.ScaleWidthPropotion(proportion);

        if (Mathf.Floor(_chargeValue / (int)ChargeAbleAction.ReverseSpeed) > _fullCharges)
        {
            UpdateCharges(_fullCharges + 1);
        }
    }

    private void UpdateCharges(int charges)
    {
        _fullCharges = charges;
        _imageColor.SetColor(charges);
        _chargeBorders.SetBorderAmount(charges);
    }
}
