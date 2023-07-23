using System;
using UnityEngine;

public class Market : Geter
{
    public CoinD coinDPrefab;
    public Transform canvas;
    
    public Player player;
    public override void OnTriggerStay(Collider other)
    {
        player = other.transform.GetComponent<Player>();
        
        if (player == null)
        {
            return;
        }
        
        Product product = player.GetElement<Product2>();
        
        if (isSend == false && player.ElementsCount > 0 && product != null)
        {
            Send(player, product);
        }
    }
    
    private void Awake()
    {
        OnComplete += () =>
        {
            Vector3 screenViewport1 = Camera.main.WorldToScreenPoint(getre.position);
            CoinD c = Instantiate(coinDPrefab, canvas);
            c.transform.position = screenViewport1;
            c.Fly(player);
        };
    }
}
