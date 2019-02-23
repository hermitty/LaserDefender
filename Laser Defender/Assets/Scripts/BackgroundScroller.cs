using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

    [SerializeField] float backgroundScrollSpeed = 0.02f;
    Material myMaterial;
    Vector2 offset;

    // Use this for initialization
    private void Awake()
    {
        SetUpSingleton();
    }
    void Start () {
        myMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(0f, backgroundScrollSpeed);
	}
    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
        myMaterial.mainTextureOffset += offset * Time.deltaTime;
	}
}
