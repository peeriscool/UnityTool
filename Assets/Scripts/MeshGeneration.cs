using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGeneration : MonoBehaviour
{
	[Range(0.1f, 5)]
	[SerializeField] float RadiusIn;

	[Range(0.1f, 10)]
	[SerializeField] float Thickness;

	[Range(3, 256)]
	[SerializeField] int Segments = 3;
	static float Dotsize { get { return 0.1f; } }
	float RadiusOut { get { return RadiusIn + Thickness; } }
	const float TAU = 6.283185307179586f; //full turn in radius 2*pi

	private void OnDrawGizmosSelected()
    {
		//Gizmos.DrawWireSphere(transform.position, RadiusIn);
		//Gizmos.DrawWireSphere(transform.position, RadiusOut);
		DrawCircle(transform.position,transform.rotation, RadiusIn, Segments);
		DrawCircle(transform.position, transform.rotation, RadiusOut, Segments);

	}

	public static void DrawCircle(Vector3 pos,Quaternion rot,float radius,int detail=32)
    {
		Vector3[] points3D = new Vector3[detail];
		//Vector2[] points2D = new Vector2[detail];
        for (int i = 0; i < detail; i++)
        {
			float t = i /(float)detail;

			float angleRadian = t * TAU; //angle in radius

			Vector2 points2D = new Vector2(Mathf.Cos(angleRadian),Mathf.Sin(angleRadian));
			points2D *= radius;
			points3D[i] = pos + rot * points2D;
		}
		//display Gizmos as dots
        for (int i = 0; i < detail; i++)
        {
			Gizmos.DrawSphere(points3D[i], Dotsize);
        }
		//display gizmos as lines
		for (int i = 0; i < detail-1; i++)
		{
			Gizmos.DrawLine(points3D[i], points3D[i+1]);
		}
		Gizmos.DrawLine(points3D[detail-1], points3D[0]); //complete the circle
	}
}
/*
 * 	// MeshData
	private Mesh Gmesh;
	Vector3[] NewVertices;
	Vector2[] newUV;
	int[] newTriangles;
	[Range(0.1f, 10f)]
	public float slidervalue;
	public bool updateToggle;
	public Material newMat;
	void Awake()
	{
		Gmesh = new Mesh();
		if (this.GetComponent<MeshFilter>() != null)
		{
			assignMesh(quad(), normals(), new Vector2[0], triIndices);
			this.gameObject.GetComponent<MeshRenderer>().material = newMat;
		}
		else
		{
			Debug.Log("No MeshFilter Found");
		}
	}
	void Update()
	{
		if (updateToggle == true)
		{
			assignMesh(quad(), normals(), new Vector2[0], triIndices);
			updateToggle = false;
		}

	}
	void assignMesh(List<Vector3> points, List<Vector3> normals, Vector2[] UV, int[] Triangles)
	{
		Gmesh.SetVertices(points);
		Gmesh.SetNormals(normals);
		Gmesh.SetUVs(0, Quad_uvs());
		Gmesh.triangles = Triangles;
		Gmesh.RecalculateNormals();
		GetComponent<MeshFilter>().sharedMesh = Gmesh;
	}
	void assignMesh() //assign empty mesh
	{
		Gmesh.vertices = NewVertices;
		Gmesh.uv = newUV;
		Gmesh.triangles = newTriangles;
		Gmesh.name = "Gmesh_";
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

	private int[] triIndices = new int[] //quad TriIndices
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
			points[i] = Vector3.Scale(points[i], new Vector3(slidervalue, slidervalue, slidervalue));
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
	};*/