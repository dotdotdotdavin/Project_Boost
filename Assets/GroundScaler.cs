
using UnityEngine;

public class GroundScaler : MonoBehaviour
{
    Renderer rend;
    [SerializeField] float scaleX = 0.5f;
    [SerializeField] float scaleY = 1f;
    [SerializeField] float scaleZ = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
      rend = GetComponent<Renderer> ();
      rend.material.SetTextureScale("_MainTex", new Vector3(scaleX, scaleY, scaleZ));

    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
