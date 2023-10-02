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
   public bool IsMerged {  get; private set; }
   private int Point => IsEmpty ? 0 : (int)Math.Pow(2, BlockValue);
   [SerializeField] private Image blockImage;
   [SerializeField] private TextMeshProUGUI pointsText;
   [SerializeField] private Color[] blockColors;




   public void SetValue(int x, int y, int value)
   {
      CoordX = x;
      CoordY = y;
      BlockValue = value;
      UpdateBlock();
   }

   private void UpdateBlock()
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
        UpdateBlock();
    }

    public void ResetMearge()
    {
        IsMerged = false;
    }

    public void MeargeBlocks(BlockNumber block)
    {
        if (block != null)
        {
            block.IncrementValue();
            SetValue(CoordX, CoordY, 0);
            UpdateBlock();
        }
        else
        {
            Debug.LogError("Block is null.");
        }
    }

    public void MoveToBlock(BlockNumber block)
    {
        block.SetValue(block.CoordX, block.CoordY, BlockValue);
        SetValue(CoordX, CoordY, 0);
        UpdateBlock();
    }
}
