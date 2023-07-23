using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static float speedAnimation = 0.5f;
    public static float zavodSpeed = 0.2f;
    public Rigidbody rb;
    public float speedMove = 10;
    public Animator anim;
    public Transform bag;
    public string lastAnim = "Idle";
    public Vector3 lastDirectionRotate;
    public int maxAmount = 3;
    private List<Product> products = new();
    public TMP_Text textCoin;
    public int coins;
    public int ElementsCount => products.Count;
    
    public bool IsGet()
    {
        if (products.Count == maxAmount)
        {
            return false;
        }

        return true;
    }

    public Product GetElement<T>()
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
    
    public void Add(Product product)
    {
        products.Add(product);
        product.transform.parent = bag;
        product.transform.localPosition = Vector3.zero;
        RepositionProducts();
    }
    
    public void Remove(Product product)
    {
        products.Remove(product);
        RepositionProducts();
    }

    private Vector3 _dir;

    private void RepositionProducts()
    {
        for (int index = 0; index < products.Count; index++)
        {
            Product product = products[index];
            product.transform.localPosition = new Vector3(0, index, 0);
        }
    }
    
    private void Update()
    {
        _dir = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W))
        {
            _dir += Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _dir += Vector3.back;
        }
        
        
        if (Input.GetKey(KeyCode.A))
        {
            _dir += Vector3.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _dir += Vector3.right;
        }
    }

    private void SetAnimation(string nameAnim)
    {
        if (nameAnim != lastAnim)
        {
            anim.SetTrigger(nameAnim);
            lastAnim = nameAnim;
        }
    }
    
    private void FixedUpdate()
    {
        _dir = _dir.normalized * Time.fixedDeltaTime * speedMove;
        rb.velocity = new Vector3(_dir.x, rb.velocity.y, _dir.z);
        
        if (rb.velocity.magnitude > 1f)
        {
            SetAnimation("Run");
            lastDirectionRotate = _dir;
            anim.transform.LookAt(lastDirectionRotate + transform.position);
        }
        else
        {
            SetAnimation("Idle");
        }
    }
}
