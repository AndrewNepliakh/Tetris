using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetraminoController
{
   private const float HEIGHT = 20;
   private const float WIDTH = 10;
   private const float X_POS = (WIDTH / 2) - 1;
   private const float Y_POS = HEIGHT;
   private const float Z_POS = 0.0f;

   private TetraminoSpawner _tetraminoSpawner;
   
   private static readonly Vector3 _spawnPosition = new Vector3(X_POS , Y_POS, Z_POS);

   public TetraminoController()
   {
      _tetraminoSpawner = new TetraminoSpawner();
   }

   public static float Height => HEIGHT;
   public static float Width => WIDTH;
   public static Vector3 SpawnPosition => _spawnPosition;

   public void SpawnTetramino()
   {
      _tetraminoSpawner.Spawn();
   }

}
