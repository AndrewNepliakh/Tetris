using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetraminoLineChecker
{
    public void CheckForLines()
    {
        for (int i = TetraminoController.Height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
                EventManager.TriggerEvent<OnCompleteLineEvent>();
            }
        }
    }

    private bool HasLine(int i)
    {
        for (int j = 0; j < TetraminoController.Width; j++)
        {
            if (TetraminoController.Grid[j, i] == null) return false;
        }

        return true;
    }

    private void DeleteLine(int i)
    {
        for (int j = 0; j < TetraminoController.Width; j++)
        {
            InjectBox.Get<PoolManager>().GetPool<Cube>().Deactivate(TetraminoController.Grid[j, i]);
            EventManager.TriggerEvent<OnScoreGainedEvent>();
            TetraminoController.Grid[j, i] = null;
        }
    }

    private void RowDown(int i)
    {
        for (int k = i; k < TetraminoController.Height; k++)
        {
            for (int j = 0; j < TetraminoController.Width; j++)
            {
                if (TetraminoController.Grid[j, k] != null)
                {
                    TetraminoController.Grid[j, k - 1] = TetraminoController.Grid[j, k];
                    TetraminoController.Grid[j, k] = null;
                    TetraminoController.Grid[j, k - 1].transform.position -= Vector3.up;
                }
            }
        }
    }
}