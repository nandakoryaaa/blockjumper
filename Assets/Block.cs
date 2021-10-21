using UnityEngine;

public class Block
{
    public int offset;
    public int width;
    public GameObject gmo;
	public Vector3[] vertices;
	private Vector3 v = new Vector3(0, 0, 0);

    public Block(int offset, float z, int h, int w, Color32 color)
    {
        this.offset = offset;
        this.width = w;
        this.gmo = new GameObject();
		
		this.v.z = z;
        this.gmo.transform.position = this.v;

        MeshRenderer meshRenderer = this.gmo.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Standard"));
        meshRenderer.material.SetColor("_Color", color);
        
        MeshFilter meshFilter = this.gmo.AddComponent<MeshFilter>();
        meshFilter.mesh = new Mesh();

        this.vertices = new Vector3[]
        {
            new Vector3(0, h, 0), new Vector3(0, h, h), new Vector3(w, h, h), new Vector3(w, h, 0),
            new Vector3(0, 0, 0), new Vector3(0, h, 0), new Vector3(w, h, 0), new Vector3(w, 0, 0),
            new Vector3(w, 0, 0), new Vector3(w, h, 0), new Vector3(w, h, h), new Vector3(w, 0, h),
        };

		meshFilter.mesh.vertices = this.vertices;
        meshFilter.mesh.triangles = new int[]
        {
            0, 1, 2, 0, 2, 3,
            4, 5, 6, 4, 6, 7,
            8, 9, 10, 8, 10, 11
        };

        meshFilter.mesh.normals = new Vector3[]
        {
            Vector3.up, Vector3.up, Vector3.up, Vector3.up,
            Vector3.back, Vector3.back, Vector3.back, Vector3.back,
            Vector3.right, Vector3.right, Vector3.right, Vector3.right
        };
    }

    public void update(int offset, int w, Color32 color)
    {
        this.offset = offset;
        this.width = w;

        this.vertices[2][0] = w;
        this.vertices[3][0] = w;
        this.vertices[6][0] = w;
        this.vertices[7][0] = w;
        this.vertices[8][0] = w;
        this.vertices[9][0] = w;
        this.vertices[10][0] = w;
        this.vertices[11][0] = w;

		MeshFilter meshFilter = this.gmo.GetComponent<MeshFilter>();
 		meshFilter.mesh.vertices = this.vertices;
		meshFilter.mesh.RecalculateBounds();

        MeshRenderer meshRenderer = this.gmo.GetComponent<MeshRenderer>();
        meshRenderer.material.SetColor("_Color", color);
    }

    public void activate()
    {
        this.gmo.SetActive(true);
    }
    
    public void deactivate()
    {
        this.gmo.SetActive(false);
    }

	public bool isActive() {
		return this.gmo.activeSelf;
	}

	public void setPosition(Vector3 pos) {
        this.gmo.transform.position = pos;
	}

	public Vector3 getPosition() {
		return this.gmo.transform.position;
	}

    public void setX(float x)
    {
		this.v = this.gmo.transform.position;
		this.v.x = x;
        this.gmo.transform.position = this.v;
    }

    public void setY(float y)
    {
		this.v = this.gmo.transform.position;
		this.v.y = y;
        this.gmo.transform.position = this.v;
    }

    public float getX()
    {
        return this.gmo.transform.position.x;
    }

    public float getY()
    {
        return this.gmo.transform.position.y;
    }

    public void moveX(float dist)
    {
		this.v = this.gmo.transform.position;
		this.v.x += dist;
        this.gmo.transform.position = v;
    }
}
