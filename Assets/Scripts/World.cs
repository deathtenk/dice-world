using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {
	public byte[,,] data;
	public int worldX=16;
	public int worldY=16;
	public int worldZ=16;


	public int chunkSize = 16;
	public GameObject chunk;
	public Chunk[,,] chunks;

	void Start()
	{
		//Contents of data array
		data = new byte[worldX, worldY, worldZ];

		for (int x=0; x<worldX; x++) {
			for (int z=0; z<worldZ; z++) {
				int stone = PerlinNoise (x, 0, z, 10, 3, 1.2f);
				stone += PerlinNoise (x, 300, z, 20, 4, 0) + 10;
				int red = PerlinNoise (x, 100, z, 50, 2, 0) + 1;//Added +1 to make sure minimum grass height is 1
				for (int y=0; y<worldY; y++) {
						if (y<=stone)
           {
            data[x,y,z]=1;
           }
          else if (y<=red+stone)
          {
            data[x,y,z]=2;
          }
				}
			}
		}

    Debug.Log(new Vector3 (Mathf.FloorToInt(worldX/chunkSize),Mathf.FloorToInt(worldY/chunkSize),Mathf.FloorToInt(worldZ/chunkSize)));

    // Gives size of the 3 dimensional array for each one of my chunk objects
		chunks=new Chunk[Mathf.FloorToInt(worldX/chunkSize),Mathf.FloorToInt(worldY/chunkSize),Mathf.FloorToInt(worldZ/chunkSize)];

		/*for (int x=0; x<chunks.GetLength(0); x++){
			for (int y=0; y<chunks.GetLength(1); y++){
				for (int z=0; z<chunks.GetLength(2); z++){
	      GameObject newChunk = Instantiate(chunk,
          new Vector3(x*chunkSize - 0.5f,y*chunkSize + 0.5f,z*chunkSize - 0.5f),
          new Quaternion(0,0,0,0)) as GameObject;
		    
        chunks[x,y,z] = newChunk.GetComponent("Chunk") as Chunk;
        chunks[x,y,z].worldGO=gameObject;
        chunks[x,y,z].chunkSize=chunkSize;
        chunks[x,y,z].chunkX=x*chunkSize;
        chunks[x,y,z].chunkY=y*chunkSize;
        chunks[x,y,z].chunkZ=z*chunkSize;
      
        }
      }
    }*/	
	}



  public void GenColumn (int x, int z) {
    // chunks
  for (int y=0; y<chunks.GetLength(1); y++){
    GameObject newChunk = Instantiate(chunk,
                                      new Vector3(x*chunkSize - 0.5f,y*chunkSize + 0.5f,z*chunkSize - 0.5f),
                                      new Quaternion(0,0,0,0)) as GameObject;
          
    chunks[x,y,z] = newChunk.GetComponent<Chunk>();
    chunks[x,y,z].worldGO=gameObject;
    chunks[x,y,z].chunkSize=chunkSize;
    chunks[x,y,z].chunkX=x*chunkSize;
    chunks[x,y,z].chunkY=y*chunkSize;
    chunks[x,y,z].chunkZ=z*chunkSize;
          
    }
  }

  public void GenColumnToXML(int x, int z) {
  }

  public void UnloadColumn(int x, int z) {
    for (int y = 0; y<chunks.GetLength(1); y++)
    {
      Object.Destroy (chunks [x,y,z].gameObject);
    }
  }

	public byte Block(int x, int y, int z)
	{
		if(x >= worldX || x < 0 || y >= worldY || y < 0 || z >= worldZ || z < 0)
		{
			return (byte) 0;
		}
		return data[x, y, z];
	}

	int PerlinNoise(int x, int y, int z, float scale, float height, float power)
	{
		float rValue;
		rValue=Noise.Noise.GetNoise(((double)x) / scale, ((double)y)/ scale, ((double)z) / scale);
		rValue*=height;

		if(power!=0){
			rValue=Mathf.Pow( rValue, power);
		}
		return (int) rValue;
	}



	
}
