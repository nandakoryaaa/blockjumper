using UnityEngine;

public class Block
{
    public int offset;
    public int width;

    private GameObject _gmo;
	private Vector3[] _vertices;
	private Vector3 _v = new Vector3(0, 0, 0);
    private Rigidbody _rigidBody;

    public Block(int offset, float z, int h, int w, Color32 color)
    {
        this.offset = offset;
        this.width = w;
        _gmo = new GameObject();
		
		_v.z = z;
        _gmo.transform.position = _v;

        var meshRenderer = _gmo.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Standard"));
        meshRenderer.material.SetColor("_Color", color);
        
        var meshFilter = _gmo.AddComponent<MeshFilter>();
        meshFilter.mesh = new Mesh();

        _vertices = new Vector3[]
        {
            
            new Vector3(0, h, 0), new Vector3(0, h, h), new Vector3(w, h, h), new Vector3(w, h, 0),
            new Vector3(0, 0, 0), new Vector3(0, h, 0), new Vector3(w, h, 0), new Vector3(w, 0, 0),
            new Vector3(w, 0, 0), new Vector3(w, h, 0), new Vector3(w, h, h), new Vector3(w, 0, h),
        };

		meshFilter.mesh.vertices = _vertices;
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

        _rigidBody = _gmo.AddComponent<Rigidbody>();
        _rigidBody.mass = 1;
        this.DisablePhysics();
    }

    public void EnablePhysics(bool withGravity = false)
    {
        _rigidBody.detectCollisions = true;
        _rigidBody.useGravity = withGravity;
    }

    public void DisablePhysics()
    {
        _rigidBody.detectCollisions = false;
        _rigidBody.useGravity = false;
    }

    public void Update(int offset, int w, Color32 color)
    {
        this.offset = offset;
        this.width = w;

        _vertices[2][0] = w;
        _vertices[3][0] = w;
        _vertices[6][0] = w;
        _vertices[7][0] = w;
        _vertices[8][0] = w;
        _vertices[9][0] = w;
        _vertices[10][0] = w;
        _vertices[11][0] = w;

		var meshFilter = _gmo.GetComponent<MeshFilter>();
 		meshFilter.mesh.vertices = _vertices;
		meshFilter.mesh.RecalculateBounds();

        _gmo.GetComponent<MeshRenderer>()
        	.material.SetColor("_Color", color);
    }

    public void Activate()
    {
        _gmo.SetActive(true);
    }
    
    public void Deactivate()
    {
        _gmo.SetActive(false);
    }

	public bool IsActive() {
		return _gmo.activeSelf;
	}

	public void SetPosition(Vector3 pos) {
        _gmo.transform.position = pos;
	}

	public Vector3 GetPosition() {
		return _gmo.transform.position;
	}

    public void SetX(float x)
    {
		_v = _gmo.transform.position;
		_v.x = x;
        _gmo.transform.position = _v;
    }

    public void SetY(float y)
    {
		_v = _gmo.transform.position;
		_v.y = y;
        _gmo.transform.position = _v;
    }

    public float GetX()
    {
        return _gmo.transform.position.x;
    }

    public float GetY()
    {
        return _gmo.transform.position.y;
    }

    public void MoveX(float dist)
    {
		_v = _gmo.transform.position;
		_v.x += dist;
        _gmo.transform.position = _v;
    }
}
