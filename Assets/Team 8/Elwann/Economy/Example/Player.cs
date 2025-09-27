using UnityEngine;
using Core.Economy;

public class Player : MonoBehaviour
{
    [Header("Player Currencies")]
    [SerializeField] private CurrencyData _currency;

    private Wallet _wallet;

    void Start()
    {
        _wallet = new Wallet();

        if (_currency != null)
            _wallet.Add(_currency, 100);

    }

    public Wallet GetWallet()
    {
        return _wallet;
    }
}
