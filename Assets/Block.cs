using UnityEngine;

public class Block
{
    public int offset;
    public int width;
    public GameObject gameObject;

    private Vector3[] _vertices;
    private Vector3 _v = new Vector3(0, 0, 0);
    private BoxCollider _collider;
    private MeshRenderer _renderer;
    private bool _renderPending = true;

    public Block(int offset, float z, int h, int w, Color32 color)
    {
        this.offset = offset;
        this.width = w;
        this.gameObject = new GameObject();
        
        _v.z = z;
        this.SetPosition(_v);

        _renderer = this.gameObject.AddComponent<MeshRenderer>();
        _renderer.material = new Material(Shader.Find("Standard"));
        _renderer.material.SetColor("_Color", color);

        var meshFilter = this.gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = new Mesh();

        Vector3 v0 = new Vector3(0, 0, 0); //     v5----w----v6
        Vector3 v1 = new Vector3(0, 0, h); //     /|         /|
        Vector3 v2 = new Vector3(w, 0, h); //    / |        / |
        Vector3 v3 = new Vector3(w, 0, 0); //   v4-+------v7  |
        Vector3 v4 = new Vector3(0, h, 0); //   |  v1------+--v2    Y  Z
        Vector3 v5 = new Vector3(0, h, h); //   h /        | h      | /
        Vector3 v6 = new Vector3(w, h, h); //   |/         |/       |/
        Vector3 v7 = new Vector3(w, h, 0); //   v0--------v3        0------ X
    
        _vertices = new Vector3[]
        {
            v0, v1, v5, v4, // left
            v0, v3, v2, v1, // bottom
            v3, v7, v6, v2, // right
            v4, v5, v6, v7, // top
            v0, v4, v7, v3 // front
        };

        meshFilter.mesh.vertices = _vertices;
        meshFilter.mesh.triangles = new int[]
        {
             0,  1,  2,  0,  2,  3,
             4,  5,  6,  4,  6,  7,
             8,  9, 10,  8, 10, 11,
            12, 13, 14, 12, 14, 15,
            16, 17, 18, 16, 18, 19 
        };

        meshFilter.mesh.normals = new Vector3[]
        {
            Vector3.left, Vector3.left, Vector3.left, Vector3.left,
            Vector3.down, Vector3.down, Vector3.down, Vector3.down,
            Vector3.right, Vector3.right, Vector3.right, Vector3.right,
            Vector3.up, Vector3.up, Vector3.up, Vector3.up,
            Vector3.back, Vector3.back, Vector3.back, Vector3.back,
        };

        _collider = this.gameObject.AddComponent<BoxCollider>();
    }

    public void Reshape(int offset, int w, Color32 color)
    {
        this.offset = offset;

        if (this.width != w)
        {
            this.width = w;

            _vertices[5].x = w;
            _vertices[6].x = w;
            _vertices[8].x = w;
            _vertices[9].x = w;
            _vertices[10].x = w;
            _vertices[11].x = w;
            _vertices[14].x = w;
            _vertices[15].x = w;
            _vertices[18].x = w;
            _vertices[19].x = w;

            var meshFilter = this.gameObject.GetComponent<MeshFilter>();
             meshFilter.mesh.vertices = _vertices;
            meshFilter.mesh.RecalculateBounds();
    
            _v = _collider.size;
            _v.x = w;
            _collider.size = _v;
            _collider.center = _v / 2;
        }

        _renderer.material.SetColor("_Color", color);
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
        _renderPending = true;
    }
    
    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public bool IsActive()
    {
        return this.gameObject.activeSelf;
    }

    public void SetPosition(Vector3 pos)
    {
        this.gameObject.transform.position = pos;
    }

    public Vector3 GetPosition()
    {
        return this.gameObject.transform.position;
    }

    public void SetX(float x)
    {
        _v = this.gameObject.transform.position;
        _v.x = x;
        this.gameObject.transform.position = _v;
    }

    public void SetY(float y)
    {
        _v = this.gameObject.transform.position;
        _v.y = y;
        this.gameObject.transform.position = _v;
    }

    public float GetX()
    {
        return this.gameObject.transform.position.x;
    }

    public float GetY()
    {
        return this.gameObject.transform.position.y;
    }

    public bool IsVisible()
    {
        bool result = _renderer.isVisible || _renderPending;
        _renderPending = false;

        return result;
    }

    public void SetColor(Color32 color)
    {
        _renderer.material.SetColor("_Color", color);
    }

    // public void debug() {
    //     Debug.Log("x: " + this.GetX() + " w: " + this.width + " bounds: " + _renderer.bounds);
    // }

}
