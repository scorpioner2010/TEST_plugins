using System;
using DG.Tweening;
using UnityEngine;

public class Geter : MonoBehaviour
{
    public Transform getre;
    public Zavod zavod;
    public bool isSend;

    public event Action OnComplete;

    public virtual void OnTriggerStay(Collider other)
    {
        Player player = other.transform.GetComponent<Player>();
        
        if (player == null)
        {
            return;
        }
        
        Product product = player.GetProduct<Product1>();
        
        if (isSend == false && player.elementsCount > 0 && product != null)
        {
            Send(player, product);
        }
    }
    
    protected void Send(Player player, Product product)
    {
        isSend = true;
        
        var tweener = product.transform.DOMove(getre.position, 150);
        tweener.SetSpeedBased(true);
        
        tweener.OnUpdate(() =>
        {
            tweener.ChangeEndValue(getre.position, true);
                
            if (Vector3.Distance(getre.position, product.transform.position) < 0.05)
            {
                player.RemoveProduct(product);
                Destroy(product.gameObject);
                isSend = false;
                OnComplete?.Invoke();
                
                if (zavod != null)
                {
                    zavod.maxAmount++;
                }
                
                tweener.Kill();
            }
        });
    }
}
