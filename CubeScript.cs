using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeScript : MonoBehaviour
{

    public string suit;
    public string value; 
    private Font font;
    private float characterSize = 0.1f;
    private Vector3 offset = new Vector3(-0.5f, 0f, -0.25f);
    // Start is called before the first frame update
    void Start()
    {
        // Create a new game object and attach a TextMesh component to it
        GameObject textObject = new GameObject("Text");
        TextMesh textMesh = textObject.AddComponent<TextMesh>();
        // Set the text to render

        if (suit == null || value == "0") {
            textMesh.text = "";
        }
        else {textMesh.text = value + suit;
        }
        
    
        // Set the font
        textMesh.font = font;

        // Set the font size to match the game object size
        Renderer objectRenderer = GetComponent<Renderer>();
        Bounds bounds = objectRenderer.bounds;
        // float fontSize = Mathf.Min(bounds.size.x, bounds.size.y, bounds.size.z) / characterSize;
        textMesh.characterSize = 0.03f;
        textMesh.fontSize = 255;

        // Set the font color to black
        textMesh.color = Color.black;

        // Set the position and rotation of the text object
        textObject.transform.position = transform.position + Vector3.up * bounds.extents.y + offset;
        textObject.transform.rotation = Quaternion.LookRotation(Vector3.up);

        // Set the text rendering mode to Smooth
        textMesh.font.material.mainTexture.filterMode = FilterMode.Trilinear;

        // Make the text object a child of the current game object
        textObject.transform.parent = transform;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
}

