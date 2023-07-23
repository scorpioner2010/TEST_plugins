using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class TriggerZavoda : MonoBehaviour
{
    public List<Product> readyProducts = new();
    public bool isSend;
    private void OnTriggerStay(Collider other)
    {
        Player player = other.transform.GetComponent<Player>();

        if (player == null)
        {
            return;
        }
        
        if (isSend == false && player.IsFull())
        {
            TryGet(player);
        }
    }

    private void TryGet(Player player)
    {
        if (readyProducts.Count > 0)
        {
            isSend = true;
            Product product = readyProducts.First();

            product.GetComponent<Rigidbody>().isKinematic = true;
            product.GetComponent<Collider>().enabled = false;

            var tweener = product.transform.DOMove(player.bag.position, 150);
            
            tweener.SetSpeedBased(true);
            
            tweener.OnUpdate(() =>
            {
                tweener.ChangeEndValue(player.bag.position, true);
                
                if (Vector3.Distance(player.bag.position, product.transform.position) < 0.05)
                {
                    readyProducts.Remove(product);
                    player.AddProduct(product);
                    isSend = false;
                    tweener.Kill();
                }
                
            });
        }
    }
}
