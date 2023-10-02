using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BlockSpawner : MonoBehaviour
{
    
    [SerializeField] private float blockSize;
    [SerializeField] private float spacing;
    [Inject] private BlockNumber blockPrefab;
    [SerializeField] private RectTransform container;
    public int fieldSize = 3;
    public BlockNumber[,] field;


    private void CreateField()
    {
        field = new BlockNumber[fieldSize, fieldSize];
        float fieldWidth = fieldSize * (blockSize + spacing) + spacing;
        container.sizeDelta = new Vector2(fieldWidth, fieldWidth);

        float startX = -(fieldWidth / 2) + (blockSize / 2) + spacing;
        float startY = (fieldWidth / 2) - (blockSize / 2) - spacing;

        for (int x = 0; x < fieldSize; x++)
        {
            for (int y = 0; y < fieldSize; y++)
            {
                var block = Instantiate(blockPrefab, container, false);
                var blockPosition = new Vector2(startX + (x * (blockSize + spacing)), startY - (y * (blockSize + spacing)));
                block.transform.localPosition = blockPosition;

                field[x, y] = block;
                block.SetValue(x, y, 0);
            }
        }
    }

    public void SpawnField()
    {
        if (field == null)
        {
            CreateField();
        }

        for (int x = 0; x < fieldSize; x++)
        {
            for (int y = 0; y < fieldSize; y++)
            {
                field[x, y].SetValue(x, y, 0);
            }
        }

        int numberOfBlocksToSpawn = Random.Range(2, 4);
        for (int i = 0; i < numberOfBlocksToSpawn; i++)
        {
            SpawnRandomBlock();
        }
    }

    public void SpawnRandomBlock()
    {
        var emptyBlocks = new List<BlockNumber>();

        for (int x = 0; x < fieldSize; x++)
        {
            for (int y = 0; y < fieldSize; y++)
            {
                if (field[x, y].IsEmpty)
                {
                    emptyBlocks.Add(field[x, y]);
                }
            }
        }

        if (emptyBlocks.Count == 0)
        {
            Debug.Log("EndGame");
        }
        else
        {
            var block = emptyBlocks[Random.Range(0, emptyBlocks.Count)];
            block.SetValue(block.CoordX, block.CoordY, 1);
        }
    }

    public void ResetAllMerge()
    {
        for(int x = 0; x < fieldSize; x++)
        {
            for(var y = 0;y < fieldSize; y++)
            {
                field[x,y].ResetMearge();
            }
        }
    }
}