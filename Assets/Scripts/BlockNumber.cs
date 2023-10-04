using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Zenject;

public class BlockNumber : MonoBehaviour
{
    public int CoordX { get; private set; }
    public int CoordY { get; private set; }

    public int BlockValue { get; private set; }
    public int MaxValue = 11;
    public bool IsEmpty => BlockValue == 0;
    public bool IsMerged { get; private set; }
    public int Point => IsEmpty ? 0 : (int)Math.Pow(2, BlockValue);
    [SerializeField] private Image blockImage;
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] public Color[] blockColors;
    private BlockAnimation currentAnimation;




   public void SetValue(int x, int y, int value, bool updateUI = true)
   {
      CoordX = x;
      CoordY = y;
      BlockValue = value;
      if(updateUI)
        {
            UpdateBlock();
        }
      
   }
    public void SetAnimation(BlockAnimation animation)
    {
        currentAnimation = animation;
    }

   public void UpdateBlock()
   {
       if (!IsEmpty)
      {
        pointsText.text = Point.ToString();
      }
      else
      {
            pointsText.text = "";
      }

      blockImage.color = blockColors[BlockValue];
      
   }




    public void IncrementValue()
    { 
        BlockValue++;
        Debug.Log(BlockValue);
        IsMerged = true;
    }

    public void ResetMearge()
    {
        IsMerged = false;
    }

    public void MeargeBlocks(BlockNumber block, BlockAnimationController blockAnimationController)
    {
        if (block != null)
        {
            blockAnimationController.Transition(this, block, false);
            block.IncrementValue();
            SetValue(CoordX, CoordY, 0);
            
        }
        else
        {
            Debug.LogError("Block is null.");
        }
    }

    public void MoveToBlock(BlockNumber block, BlockAnimationController blockAnimationController)
    {
        blockAnimationController.Transition(this, block, false );
        block.SetValue(block.CoordX, block.CoordY, BlockValue, false);
        SetValue(CoordX, CoordY, 0);
        UpdateBlock();
    }
    public void DestroyAnimation()
    {
        if(currentAnimation != null)
        {
            currentAnimation.Destroy();
        }
    }
}
