using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EnnemyCam : MonoBehaviour {

    public Camera ennemyCam;
    public Shader testShader;

    GameObject parent;

    Camera cam;

    AiData data;

    Texture2D image;

    int imgWidth;
    int imgHeight;

    Rect area;

    Color[] pixels;
    byte[] bytes;

    // Use this for initialization
    void Start () {
        cam = ennemyCam.GetComponent<Camera>();
        cam.SetReplacementShader(testShader, "RenderType");
        //cam.targetTexture = renderTex;


        imgWidth = cam.targetTexture.width;
        imgHeight = cam.targetTexture.height;

        image = new Texture2D(imgWidth, imgHeight);

        area = new Rect(0, 0, imgWidth, imgHeight);

        data = GetComponentInParent<AiController>().data;
    }

    private void OnPostRender()
    {

        RenderTexture.active = cam.targetTexture;

        //int imgWidth = RenderTexture.active.width;
        //int imgHeight = RenderTexture.active.height;

        //image = new Texture2D(imgWidth, imgHeight);
        image.ReadPixels(area, 0, 0);
        image.Apply();

        data.spotting = RedPixelProportion(image) * 1000000;

        data.playerIsDetected = data.spotting > 100;

        //DestroyImmediate(currentRT);

        //Debug.Log("done");        

        //byte[] bytes = image.EncodeToPNG();
        //File.WriteAllBytes(@"C:\Users\Simon\Documents\GitHub\Last_Semester_Projecto\SavedScreen.png", bytes);
    }

    float RedPixelProportion (Texture2D image)
    {

        bytes = image.GetRawTextureData();

        int length = bytes.Length;

        int redCount = 0;

        for (int i = 0; i < length; i += 4)
        {
            if (bytes[i] == 255)
                redCount++;
        }


        return redCount / (float)(length / 4);

    }


    // Update is called once per frame
    void Update () {
		
	}
}
