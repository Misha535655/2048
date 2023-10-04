
using UnityEngine;
using DG.Tweening;
using Zenject;

public class BlockAnimationController : MonoBehaviour
{
    [Inject] BlockAnimation animationPrefab;
    private void Awake()
    {
        DOTween.Init();
    }

    public void Transition(BlockNumber from, BlockNumber to, bool isMerging)
    {
        Instantiate(animationPrefab, transform, false).Move(from, to, isMerging);
    }

    public void Appear(BlockNumber block) 
    {
        Instantiate(animationPrefab, transform, false).Appear(block);
    }
}
