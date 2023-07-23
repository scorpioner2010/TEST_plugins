using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static float zavodSpeed = 1f;
    public Transform bag;
    public int maxAmount = 3;
    private List<Product> products = new();
    public TMP_Text textCoin;
    public int coins;
    public int elementsCount => products.Count;
    
    public bool IsFull()
    {
        if (products.Count == maxAmount)
        {
            return false;
        }

        return true;
    }

    public Product GetProduct<T>()
    {
        foreach (Product element in products)
        {
            bool isHas  = element is T;
            
            if (isHas)
            {
                return element;
            }
        }

        return default;
    }
    
    public void AddProduct(Product product)
    {
        products.Add(product);
        product.transform.parent = bag;
        product.transform.localPosition = Vector3.zero;
        RepositionProducts();
    }
    
    public void RemoveProduct(Product product)
    {
        products.Remove(product);
        RepositionProducts();
    }

    private void RepositionProducts()
    {
        for (int index = 0; index < products.Count; index++)
        {
            Product product = products[index];
            product.transform.localPosition = new Vector3(0, index, 0);
        }
    }
}
