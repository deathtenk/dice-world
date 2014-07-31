using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PolygonGenerator : MonoBehaviour {

	public List<Vector3> newVertices = new List<Vector3>();
	public List<int> newTriangles = new List<int>();
	public List<Vector2> newUV = new List<Vector2>();
	public bool update=false;

	public byte[,] blocks;

	private Mesh mesh;

	private float tUnit = 0.5f;
	private Vector2 tStone = new Vector2(0, 0);
	private Vector2 tRed = new Vector2(0, 1);

	private int squareCount;

	public List<Vector3> colVertices = new List<Vector3>();
	public List<int> colTriangles = new List<int>();
	private int colCount;

	private MeshCollider col;

	void Update() {
		if (update) {
			BuildMesh();
			UpdateMesh();
			update=false;
		}
	}

	void Start ()
	{
		mesh = GetComponent<MeshFilter> ().mesh;
		col = GetComponent<MeshCollider> ();
		GenTerrain();
		BuildMesh();
		UpdateMesh();
	}

	byte Block (int x, int y)
	{
		if(x==-1 || x==blocks.GetLength(0) ||   y==-1 || y==blocks.GetLength(1)){
			return (byte)0;
		}
		return blocks[x,y];
	}

	void GenCollider(int x, int y)
	{
		Debug.Log( Block (x, y+1));
		// top
		if (Block (x, y+1) == 0) {
		colVertices.Add( new Vector3 (x  , y  , 1));
		colVertices.Add( new Vector3 (x + 1 , y  , 1));
		colVertices.Add( new Vector3 (x + 1 , y  , 0 ));
		colVertices.Add( new Vector3 (x  , y  , 0 ));
		
		ColliderTriangles();

		colCount++;
		}

		// bot
		if (Block (x, y-1) == 0) {
		colVertices.Add( new Vector3 (x  , y - 1, 0));
		colVertices.Add( new Vector3 (x + 1 , y - 1, 0));
		colVertices.Add( new Vector3 (x + 1 , y - 1, 1 ));
		colVertices.Add( new Vector3 (x  , y - 1, 1 ));

		ColliderTriangles();
		
		colCount++;
		}

		// left
		if (Block (x-1, y) == 0) {
		colVertices.Add( new Vector3 (x  , y - 1,  1));
		colVertices.Add( new Vector3 (x , y  , 1));
		colVertices.Add( new Vector3 (x , y  , 0 ));
		colVertices.Add( new Vector3 (x  , y - 1, 0 ));

		ColliderTriangles();
		
		colCount++;
		}

		//right
		if (Block (x+1, y) == 0) {
		colVertices.Add( new Vector3 (x + 1, y  , 1));
		colVertices.Add( new Vector3 (x + 1 , y - 1  , 1));
		colVertices.Add( new Vector3 (x + 1 , y - 1  , 0 ));
		colVertices.Add( new Vector3 (x + 1  , y  , 0 ));

		ColliderTriangles();
		
		colCount++;
		}
	}
	

	void ColliderTriangles()
	{
		colTriangles.Add(colCount*4);
		colTriangles.Add((colCount*4)+1);
		colTriangles.Add((colCount*4)+3);
		colTriangles.Add((colCount*4)+1);
		colTriangles.Add((colCount*4)+2);
		colTriangles.Add((colCount*4)+3);
	}

	void GenSquare(int x, int y, Vector2 texture) {
		newVertices.Add (new Vector3 (x, y, 0));
		newVertices.Add (new Vector3 (x + 1, y, 0));
		newVertices.Add (new Vector3 (x + 1, y - 1, 0));
		newVertices.Add (new Vector3 (x, y - 1, 0));
		
		newTriangles.Add (squareCount*4);
		newTriangles.Add ((squareCount*4)+1);
		newTriangles.Add ((squareCount*4)+3);
		newTriangles.Add ((squareCount*4)+1);
		newTriangles.Add ((squareCount*4)+2);
		newTriangles.Add ((squareCount*4)+3);
		
		newUV.Add(new Vector2 (tUnit * texture.x, tUnit * texture.y + tUnit));
		newUV.Add(new Vector2 (tUnit * texture.x + tUnit, tUnit * texture.y + tUnit));
		newUV.Add(new Vector2 (tUnit * texture.x + tUnit, tUnit * texture.y));
		newUV.Add(new Vector2 (tUnit * texture.x, tUnit * texture.y));

		squareCount++;
	}

	void UpdateMesh() {
		mesh.Clear ();
		mesh.vertices = newVertices.ToArray ();
		mesh.triangles = newTriangles.ToArray ();
		mesh.uv = newUV.ToArray ();
		mesh.Optimize ();
		mesh.RecalculateNormals ();

		Mesh newMesh = new Mesh();
		newMesh.vertices = colVertices.ToArray();
		newMesh.triangles = colTriangles.ToArray();

		col.sharedMesh = newMesh;

		colVertices.Clear ();
		colTriangles.Clear ();
		colCount = 0;

		squareCount = 0;
		newVertices.Clear();
		newTriangles.Clear();
		newUV.Clear();

	}

	void GenTerrain() {
		blocks = new byte[96, 128];
		for (int px=0; px<blocks.GetLength (0); px++)
		{
			int stone = Noise (px,0,80,15,1);
			stone+=Noise (px,0,50,30,1);
			stone+=Noise (px,0,10,10,1);
			stone+=75;

			int red = Noise(px, 0, 100f,35,1);
			red+= Noise (px,100,50,30,1);
			red+=75;

			for (int py = 0; py<blocks.GetLength (1); py++)
			{
				if (py<stone) {
					blocks [px, py] = 1; //for stone spawning

					if(Noise (px,py,12,16,1)> 10) {
						blocks[px, py] = 2; //dirt spawn
					}

					if(Noise(px,py*2, 16, 14, 1) > 10) {
						blocks[px, py] = 0; // cave spawn
					}
				}
				else if (py<red) {
						blocks[px,py] = 2;
					}
			}
		}
	}

	int Noise(int x, int y, float scale, float mag, float exp) {
		return (int) (Mathf.Pow ((Mathf.PerlinNoise(x/scale, y/scale)*mag), (exp) ));
	}

	void BuildMesh()
	{
	for (int px=0; px<blocks.GetLength(0); px++) {
		for (int py=0; py<blocks.GetLength(1); py++) {
				if (blocks[px, py] != 0)
				{
					GenCollider(px,py);
					if(blocks[px,py] == 1) 
					{
						GenSquare(px,py,tStone);
					}
					else if (blocks[px, py] == 2)
					{
						GenSquare(px,py,tRed);
					}
				}
			}
		}
	}
}
