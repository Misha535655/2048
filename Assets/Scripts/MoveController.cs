using ModestTree;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using static Unity.Collections.AllocatorManager;

public class MoveController : MonoBehaviour
{
    [Inject] GameController gameController;
    [Inject] BlockSpawner spawner;
    [Inject] private ScoreController _scoreController;
    [Inject] BlockNumber block;

    private bool anyBlockMoved;
    private void Start()
    {
        SwipeDetector.SwipeEvent += OnInput;
    }

    private void OnInput(Vector2 direction)
    {
        if (!gameController.GameStarted) 
        {
            return;
        }
        
        anyBlockMoved = false;
        spawner.ResetAllMerge();
        Move(direction);
        if (anyBlockMoved)
        {
            spawner.SpawnRandomBlock();
            GameChecker();
        }
    }

    private void Move(Vector2 direction)
    {
        int startXY;
        int dir;

        if (direction.x > 0 || direction.y < 0)
        {
            startXY = spawner.fieldSize - 1;
        }
        else
        {
            startXY = 0;
        }
        if(direction.x != 0)
        {
            dir = (int)direction.x;
        }
        else
        {
            dir = -(int)direction.y;
        }

        for(int i = 0; i < spawner.fieldSize; i++)
        {
            for(int j = startXY; j>= 0 && j < spawner.fieldSize; j -= dir)
            {
                var block = direction.x != 0 ? spawner.field[j, i] : spawner.field[i, j];
           
                if (block.IsEmpty)
                    continue;

                var blockToMerge = FindBlockToMerge(block, direction);
                if (blockToMerge != null)
                {
                    block.MeargeBlocks(blockToMerge);
                    _scoreController.AddPoints();
                    anyBlockMoved = true;
                    continue;
                }

                var emptyBlock = FindEmptyBlock(block, direction);
                if(emptyBlock != null)
                {
                    block.MoveToBlock(emptyBlock);
                    anyBlockMoved = true;
                }
            }
        }

    }

    private BlockNumber FindBlockToMerge(BlockNumber block, Vector2 direction)
    {
        int startX = block.CoordX + (int)direction.x;
        int startY = block.CoordY - (int)direction.y;
        

        for (int x = startX, y = startY;
            x >= 0 && x < spawner.fieldSize && y >= 0 && y < spawner.fieldSize;
            x += (int)direction.x, y -= (int)direction.y)
        {
            if (spawner.field[x, y] == null || spawner.field[x, y].IsEmpty) continue;
            if (spawner.field[x, y].BlockValue == block.BlockValue && !spawner.field[x, y].IsMerged)
            {
                Debug.Log(spawner.field[x, y]);
                return spawner.field[x, y];
            }
            break;
        }
        return null;
    }
    private BlockNumber FindEmptyBlock(BlockNumber block, Vector2 direction)
    {
        BlockNumber emptyBlock = null;
        int startX = block.CoordX + (int)direction.x;
        int startY = block.CoordY - (int)direction.y;

        for (int x = startX, y = startY;
            x >= 0 && x < spawner.fieldSize && y >= 0 && y < spawner.fieldSize;
            x += (int)direction.x, y -= (int)direction.y)
        {
            if (spawner.field[x, y].IsEmpty)
                emptyBlock = spawner.field[x, y];
            else
                break;
                
        }
        return emptyBlock;
    }

    private void GameChecker()
    {
        bool lose = true;

        for (int x = 0; x < spawner.fieldSize; x++)
        {
            for (int y = 0; y < spawner.fieldSize; y++)
            {
                if (spawner.field[x, y].BlockValue == block.MaxValue)
                {
                    gameController.Win();
                }

                if (lose && spawner.field[x, y].IsEmpty ||
                    FindBlockToMerge(spawner.field[x, y], Vector2.up) ||
                    FindBlockToMerge(spawner.field[x, y], Vector2.down) ||
                    FindBlockToMerge(spawner.field[x,y], Vector2.left) ||
                    FindBlockToMerge(spawner.field[x,y], Vector2.right)
                    )
                {
                    lose = false; 
                }
            }
        }

        if (lose)
        {
            gameController.Lose();
        }
    }
}
