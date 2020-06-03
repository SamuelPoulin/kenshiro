using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FPS : MonoBehaviour {

    float average;
    float i;
    float counter;

    Text text;

	void Start () {
        counter = 0;
        average = 0;
        i = 0;

        text = GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {
        if(counter >= 0.1f)
        {
            text.text = Mathf.Round(average/i).ToString();
            counter = 0;
            i = 0;
            average = 0;
        }
        average += (1 / Time.deltaTime);
        counter += Time.deltaTime;
        i++;

        
	}
}
