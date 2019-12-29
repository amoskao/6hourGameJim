using UnityEngine;

public class InfinityS : MonoBehaviour
{
    private Material material;
    public Vector2 offset;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }
    /*public float length, startpos;
    public GameObject cam;
    public float Effect;

    
    private void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;

    }

    private void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - Effect));
        float floor = (cam.transform.position.x * Effect);
        transform.position = new Vector3(startpos + floor, transform.position.y, transform.position.z);

        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }*/

    void Update()
    {
        //Vector2 offset = new Vector2(Time.time * speed, 0);
        //GetComponent<Renderer>().material.mainTextureOffset = offset;
        material.mainTextureOffset += offset * Time.deltaTime;
    }
}
