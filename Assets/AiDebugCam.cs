using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDebugCam : MonoBehaviour {

    public Material[] mat;


    private void OnPreRender()
    {
        for (int i = 0; i < mat.Length; ++i)
        {
            mat[i].SetOverrideTag("RenderType", "Ennemy");
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
