using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://www.youtube.com/watch?v=6xs0Saff940
public class PlaneGenerator : MonoBehaviour
{
	// MeshData
	private Mesh Gmesh;
	Vector3[] NewVertices;
	Vector2[] newUV;
	int[] newTriangles;
	[Range(0.1f,10f)]
	public float slidervalue;
	public bool updateToggle;
	public Material newMat;
	void Awake()
    {
	    Gmesh = new Mesh();
	    if(this.GetComponent<MeshFilter>() != null)
	    {
		    //assignMesh();
			assignMesh(quad(),normals(), new Vector2[0], triIndices);
			//assignMesh(squad(), new Vector2[0], squadIndices);
			//newMat = Resources.Load("NormalDebugMaterial", typeof(Material)) as Material;
			//this.GetComponent<Renderer>().material = newMat;
			this.gameObject.GetComponent<MeshRenderer>().material = newMat;
		}
	    else
	    {
		    // GetComponent<MeshFilter>().mesh = Gmesh;
		    Debug.Log("No MeshFilter Found");
	    }
    }
	void Update()
	{
		if(updateToggle ==true)
        {
			assignMesh(quad(), normals(), new Vector2[0], triIndices);
			updateToggle = false;
		}
		
	}
    void assignMesh(List<Vector3> points, List<Vector3> normals, Vector2[] UV, int[] Triangles )
	{
		Gmesh.SetVertices(points);
		Gmesh.SetNormals(normals);
		Gmesh.SetUVs(0,Quad_uvs());
		//Gmesh.uv = UV;
		Gmesh.triangles = Triangles;
		//Gmesh.name = "Gmesh_";
		Gmesh.RecalculateNormals();
		GetComponent<MeshFilter>().sharedMesh = Gmesh;
	}
	void assignMesh() //assign empty mesh
	{
		Gmesh.vertices = NewVertices;
		Gmesh.uv = newUV;
		Gmesh.triangles = newTriangles;
		Gmesh.name="Gmesh_";
		GetComponent<MeshFilter>().sharedMesh = Gmesh;
	}

	//Mesh Generation
	private List<Vector3> quad()
	{
		List<Vector3> points = new List<Vector3>()
		{
			new Vector3(-1,1),
			new Vector3(1,1),
			new Vector3(-1,-1),
			new Vector3(1,-1)
		};
        for (int i = 0; i < points.Count; i++)
        {
            points[i] = Vector3.Scale(points[i], new Vector3(slidervalue, slidervalue, slidervalue));
        }
        return points;
	}

	private int[]triIndices = new int[] //quad TriIndices
	{
		1,0,2,
		3,1,2
	};
	private List<Vector2> Quad_uvs()
    {
		List<Vector2> uvs = new List<Vector2>
		{
			new Vector2(1,1),
			new Vector2(0,1),
			new Vector2(1,0),
			new Vector2(0,0)
		};
		
		return uvs;
    }
	private List<Vector3> normals() //quad normals
	{
		List<Vector3> normals = new List<Vector3>
		{
		new Vector3(0,0,1),
		new Vector3(0,0,1),
		new Vector3(0,0,1),
		new Vector3(0,0,1)
		};
		return normals;
	}
	//--
	private List<Vector3> squad()
	{
		List<Vector3> points = new List<Vector3>()
		{
			new Vector3(-1,1),
			new Vector3(1,1),
			new Vector3(-1,-1),
			new Vector3(1,-1)
			,
			new Vector3(0,1),
			new Vector3(1,1),
			new Vector3(0,0),
			new Vector3(1,0)
		};
        for (int i = 0; i < points.Count; i++)
        {
			points[i] = Vector3.Scale(points[i] ,new Vector3(slidervalue, slidervalue, slidervalue));
        }
		return points;
	}
	private int[] squadIndices = new int[] //quad TriIndices
	{
		1,0,2,
		3,1,2
		,
		6,5,7,
		7,6,8
	};
}
