using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Zavod : MonoBehaviour
{
    public Product reproduceProduct;
    public Bar barPrefab;
    public Transform barPosition;
    public Transform spawner;
    public TriggerZavoda tr;

    public int maxAmount = 5;
    
    private IEnumerator Start()
    {
        if (maxAmount > 0)
        {
            Bar bar = Instantiate(barPrefab, transform, true);
            bar.transform.position = barPosition.position;
            bar.sizeBar = 0;

            DOTween.To(() => bar.sizeBar, x => bar.sizeBar = x, 1, Player.zavodSpeed).SetEase(Ease.Linear).OnComplete(
                () =>
                {
                    Product product = Instantiate(reproduceProduct, spawner.position, Random.rotation);
                    tr.readyProducts.Add(product);
                    Destroy(bar.gameObject);
                    maxAmount--;
                    StartCoroutine(Start());
                });
        }
        else
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(Start());
        }
    }
}
