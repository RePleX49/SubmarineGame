using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjDataSeaSix
{
    public Vector3 pos;
    public Vector3 scale;
    public Quaternion rot;

    public Matrix4x4 matrix
    {
        get
        {
            return Matrix4x4.TRS(pos, rot, scale);

        }
    }
    public ObjDataSeaSix(Vector3 pos, Vector3 scale, Quaternion rot)
    {
        this.pos = pos;
        this.scale = scale;
        this.rot = rot;
    }
}

public class GPUInstanceSeagrass : MonoBehaviour
{
    public Vector3[] positions;
    public Vector3[] scales;
    public Quaternion[] rotations;
    //public GameObject[] gameObjects;
    public Mesh objMesh;
    public Material objMat;
    public int batchIndexMax = 942;

    private List<List<ObjDataSeaSix>> batches = new List<List<ObjDataSeaSix>>();
    void Start()
    {
        //for (int i = 0; i < gameObjects.Length; i++)
        //{
        //    Vector3 posMult = new Vector3(0, -1, 0);
        //    positions[i] = gameObjects[i].transform.position + posMult;
        //    //Quaternion rotationAngle = new Quaternion(0, 1, 0, Mathf.PI / 2);
        //    rotations[i] = gameObjects[i].transform.rotation;
        //    rotations[i] *= Quaternion.Euler(-90, 0, 0);
        //    Vector3 scaleMult = new Vector3(25, 25, 75);
        //    scales[i] = (gameObjects[i].transform.lossyScale  + scaleMult);
        //    gameObjects[i].SetActive(false);
        //}

        int batchIndexNum = 0;
        List<ObjDataSeaSix> currBatch = new List<ObjDataSeaSix>();
        for (int i = 0; i < positions.Length; i++)
        {
            AddObj(currBatch, i);
            batchIndexNum++;
            if (batchIndexNum >= batchIndexMax)
            {
                batches.Add(currBatch);
                currBatch = BuildNewBatch();
                batchIndexNum = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        RenderBatches();
    }

    private void AddObj(List<ObjDataSeaSix> currBatch, int i)
    {
        currBatch.Add(new ObjDataSeaSix(positions[i], scales[i], rotations[i]));
        Debug.Log("Added Mesh Seagrass");
    }
    private List<ObjDataSeaSix> BuildNewBatch()
    {
        return new List<ObjDataSeaSix>();
    }
    private void RenderBatches()
    {
        foreach (var batch in batches)
        {
            Graphics.DrawMeshInstanced(objMesh, 0, objMat, batch.Select((a) => a.matrix).ToList());
        }
    }
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.Linq;

//public class ObjData
//{
//    public Vector3 pos;
//    public Vector3 scale;
//    public Quaternion rot;

//    public Matrix4x4 matrix
//    {
//        get
//        {
//            return Matrix4x4.TRS(pos, rot, scale);

//        }
//    }
//    public ObjData(Vector3 pos, Vector3 scale, Quaternion rot)
//    {
//        this.pos = pos;
//        this.scale = scale;
//        this.rot = rot;
//    }
//}
//public class GPUSpawner : MonoBehaviour
//{
//    public GameObject[] gameObjects;
//    public List<Vector3> positions = new List<Vector3>();
//    public Vector3 maxPos;
//    public Mesh objMesh;
//    public Material objMat;

//    private List<List<ObjData>> batches = new List<List<ObjData>>();
//    void Start()
//    {
//        //positions.Length = gameObjects.Length;
//        for (int i = 0; i < gameObjects.Length; i++)
//        {
//            positions.Add(gameObjects[i].transform.position);
//            //gameObjects[i].SetActive(false);
//        }

//        int batchIndexNum = 0;
//        List<ObjData> currBatch = new List<ObjData>();
//        for (int i = 0; i < 300; i++)
//        {
//            AddObj(currBatch, i);
//            batchIndexNum++;
//            if (batchIndexNum >= 1000)
//            {
//                batches.Add(currBatch);
//                currBatch = BuildNewBatch();
//                batchIndexNum = 0;
//            }
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        RenderBatches();
//    }

//    private void AddObj(List<ObjData> currBatch, int i)

//    {
//        Vector3 position = new Vector3(Random.Range(-maxPos.x, maxPos.x), Random.Range(-maxPos.y, maxPos.y), Random.Range(-maxPos.z, maxPos.z));
//        currBatch.Add(new ObjData(position, new Vector3(2, 2, 2), Quaternion.identity));
//    }
//    private List<ObjData> BuildNewBatch()
//    {
//        return new List<ObjData>();
//    }
//    private void RenderBatches()
//    {
//        foreach (var batch in batches)
//        {
//            Graphics.DrawMeshInstanced(objMesh, 0, objMat, batch.Select((a) => a.matrix).ToList());
//        }
//    }
//}





//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.Linq;

//public class ObjData
//{
//    public Vector3 pos;
//    public Vector3 scale;
//    public Quaternion rot;

//    public Matrix4x4 matrix
//    {
//        get
//        {
//            return Matrix4x4.TRS(pos, rot, scale);

//        }
//    }
//    public ObjData(Vector3 pos, Vector3 scale, Quaternion rot)
//    {
//        this.pos = pos;
//        this.scale = scale;
//        this.rot = rot;
//    }
//}
//public class GPUSpawner : MonoBehaviour
//{
//    public int instances;

//    public GameObject[] rocks;
//    public Vector3 maxPos;
//    public Mesh objMesh;
//    public Material objMat;

//    private List<List<ObjData>> batches = new List<List<ObjData>>();
//    void Start()
//    {
//        int batchIndexNum = 0;
//        instances = rocks.Length;
//        List<ObjData> currBatch = new List<ObjData>();
//        for (int i = 0; i < instances; i++)
//        {
//            AddObj(currBatch, i);
//            batchIndexNum++;
//            if (batchIndexNum >= 300)
//            {
//                batches.Add(currBatch);
//                currBatch = BuildNewBatch();
//                batchIndexNum = 0;
//            }
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        RenderBatches();
//    }

//    private void AddObj(List<ObjData> currBatch, int i)

//    {
//        Vector3 position = new Vector3(Random.Range(-maxPos.x, maxPos.x), Random.Range(-maxPos.y, maxPos.y), Random.Range(-maxPos.z, maxPos.z));
//        currBatch.Add(new ObjData(position, new Vector3(2, 2, 2), Quaternion.identity));
//    }
//    private List<ObjData> BuildNewBatch()
//    {
//        return new List<ObjData>();
//    }
//    private void RenderBatches()
//    {
//        foreach (var batch in batches)
//        {
//            Graphics.DrawMeshInstanced(objMesh, 0, objMat, batch.Select((a) => a.matrix).ToList());
//        }
//    }
//}