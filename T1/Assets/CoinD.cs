using DG.Tweening;
using UnityEngine;

public class CoinD : MonoBehaviour
{
    public void Fly(Player player)
    {
        var tweener = transform.DOJump(player.textCoin.transform.parent.position, 1,3,.8f).SetEase(Ease.OutExpo);
        tweener.OnComplete(() =>
        {
            player.coins++;
            player.textCoin.text = player.coins.ToString();
            player.textCoin.transform.parent.DOPunchScale(Vector3.one/3, 0.3f, 2);
            Destroy(gameObject);
        });
    }
}
