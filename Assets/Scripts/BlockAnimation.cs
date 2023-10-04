using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlockAnimation : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] TextMeshProUGUI points;

    private float moveTime = 0.1f;
    private float appearTime = 0.1f;

    private Sequence sequence;
    public void Move(BlockNumber from, BlockNumber to, bool isMerging)
    {
        from.DestroyAnimation();
        to.SetAnimation(this);

        image.color = from.blockColors[from.BlockValue];
        points.text = from.Point.ToString();
        transform.position = from.transform.position;

        sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(to.transform.position, moveTime));

        if (isMerging)
        {
            sequence.AppendCallback(() =>
            {
                image.color = to.blockColors[to.BlockValue];
                points.text = to.Point.ToString();
                transform.position = to.transform.position;
            });

            sequence.Append(transform.DOScale(1.2f, appearTime));
            sequence.Append(transform.DOScale(1f, appearTime));
        }

        sequence.AppendCallback(() =>
        {
            to.UpdateBlock();
            Destroy();
        });
    }

    public void Appear(BlockNumber block)
    {
        block.DestroyAnimation();
        block.SetAnimation(this);

        image.color = block.blockColors[block.BlockValue];
        points.text = block.Point.ToString();
        transform.position = block.transform.position;

        transform.localScale = new Vector2(0, 0);

        sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(1.2f, appearTime));
        sequence.Append(transform.DOScale(1f, appearTime));
        sequence.AppendCallback(() =>
        {
            block.UpdateBlock();
            Destroy();
        });
    }
    public void Destroy()
    {
        sequence.Kill();
        Destroy(gameObject);
    }


}
