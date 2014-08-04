using UnityEngine;
using System.Collections;

public class ModifyTerrain : MonoBehaviour {

  World world;
  GameObject cameraGO;
  public double loadTimer = 1.0f;
  private double curTimer = 0.0f;

  public void Start()
  {
    world = gameObject.GetComponent ("World") as World;
    cameraGO = GameObject.FindGameObjectWithTag("MainCamera");
  }

  public void Update()
  {
    if(Input.GetMouseButtonDown(0))
      ReplaceBlockCursor (0);
    if(Input.GetMouseButtonDown(1))
      AddBlockCursor (1);

    curTimer -= Time.deltaTime;
    if (curTimer <= 0.0f) {
      LoadChunks(GameObject.FindGameObjectWithTag("MainCamera").transform.position,48,64);
      curTimer = loadTimer;
    }
  }

  public void LoadChunks(Vector3 playerPos, float distToLoad, float distToUnload)
  {
    for(int x=0; x<world.chunks.GetLength(0);x++)
    {
      for(int z=0; z<world.chunks.GetLength(2);z++)
      {
        float dist=Vector2.Distance(new Vector2(x*world.chunkSize, z*world.chunkSize), 
                                    new Vector2(playerPos.x,playerPos.y));

        if(dist<distToLoad){
          if(world.chunks[x,0,z]==null){
            world.GenColumn(x,z);
          }
        } 
        else if(dist>distToUnload){
          if(world.chunks[x,0,z]!=null){
            world.UnloadColumn(x,z);
          }
        }
      }
    }
  }

	public void ReplaceBlockCenter(float range, byte block){
    //replaces block directly infront of the player
    Ray ray = new Ray(cameraGO.transform.position, cameraGO.transform.forward);
    RaycastHit hit;

    if (Physics.Raycast (ray, out hit))
    {
      if(hit.distance<range) 
      {
        ReplaceBlockAt(hit, block);
      }
    }
  }

  public void AddBlockCenter(float range, byte block){
    //adds the blocks specified directly in front of the player
    Ray ray = new Ray(cameraGO.transform.position, cameraGO.transform.forward);
    RaycastHit hit;

    if (Physics.Raycast (ray, out hit))
    {
      if(hit.distance<range)
      {
        AddBlockAt(hit,block);
      }
      Debug.DrawLine(ray.origin,ray.origin+(ray.direction*hit.distance),Color.green,2);
    }
  }

  public void ReplaceBlockCursor(byte block) {
    //replaces block at area where cursor is pointing
    Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
    RaycastHit hit;

    if (Physics.Raycast (ray, out hit))
    {
      ReplaceBlockAt (hit, block);
      Debug.DrawLine (ray.origin,ray.origin+ (ray.direction*hit.distance), Color.green, 2);
    }
  }

  public void AddBlockCursor(byte block) {
    //adds blocks to where the cursor is pointing
    Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
    RaycastHit hit;

    if (Physics.Raycast (ray, out hit))
    {
      AddBlockAt(hit, block);
      Debug.DrawLine (ray.origin,ray.origin+ (ray.direction*hit.distance), Color.green, 2);
    }
  }

  public void ReplaceBlockAt(RaycastHit hit, byte block) {
    //remove a block at these impact coordinate, you can raycast against the terrain and call
    Vector3 position = hit.point;
    position+=(hit.normal* -0.5f);

    SetBlockAt (position, block);
  }

  public void AddBlockAt(RaycastHit hit, byte block) {
    //adds the specified block at these impact coordinates, you can raycast against the terrain
    Vector3 position = hit.point;
    position+=(hit.normal*0.5f);
    
    SetBlockAt(position,block);
  }

  public void SetBlockAt(Vector3 position, byte block) {
    //sets the specified block to these coordinates
    int x= Mathf.RoundToInt (position.x);
    int y= Mathf.RoundToInt (position.y);
    int z= Mathf.RoundToInt (position.z);

    SetBlockAt(x,y,z,block);
  }

  public void SetBlockAt(int x, int y, int z, byte block) {
    //adds the specified block at these coordinates
    //print ("Adding: " + x + ", " + y + ", " + z);

    world.data[x,y,z]=block;
    UpdateChunkAt(x,y,z);
  }

  public void UpdateChunkAt(int x, int y, int z) {
    //Updates the chunk containing this block
    int updateX = Mathf.FloorToInt ( x/world.chunkSize);
    int updateY = Mathf.FloorToInt ( y/world.chunkSize);
    int updateZ = Mathf.FloorToInt ( z/world.chunkSize);

    //print ("Updating: " + updateX + ", " + updateY + ", " + updateZ);
    world.chunks[updateX,updateY, updateZ].update=true;
    if(x-(world.chunkSize*updateX)==0 && updateX!=0){
      world.chunks[updateX-1,updateY, updateZ].update=true;
    }
    
    if(x-(world.chunkSize*updateX)==15 && updateX!=world.chunks.GetLength(0)-1){
      world.chunks[updateX+1,updateY, updateZ].update=true;
    }
    
    if(y-(world.chunkSize*updateY)==0 && updateY!=0){
      world.chunks[updateX,updateY-1, updateZ].update=true;
    }
    
    if(y-(world.chunkSize*updateY)==15 && updateY!=world.chunks.GetLength(1)-1){
      world.chunks[updateX,updateY+1, updateZ].update=true;
    }
    
    if(z-(world.chunkSize*updateZ)==0 && updateZ!=0){
      world.chunks[updateX,updateY, updateZ-1].update=true;
    }
        
    if(z-(world.chunkSize*updateZ)==15 && updateZ!=world.chunks.GetLength(2)-1){
      world.chunks[updateX,updateY, updateZ+1].update=true;
    }
  }

}
