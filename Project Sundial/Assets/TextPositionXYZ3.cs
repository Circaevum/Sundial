using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

public class TextPositionXYZ3 : MonoBehaviour
{
    public TextAsset jsonFile; // The JSON file to parse

    private float currentZ = 0f; // Track the current Z position for each Feature
    private float currentY = 0f; // Track the current Y position for each line of text

    private float levelOffset = 0.3f;

    void Start()
    {
        // Load the JSON file
        string json = jsonFile.text;

        // Parse the JSON
        JToken token = JToken.Parse(json);

        // Create a modular structure based on the shape of the data
        GameObject root = new GameObject("Root"); // Create a root object for the modular structure
        CreateModularStructure(token, root.transform); // Create the modular structure recursively
    }

    // Create a modular structure recursively
    void CreateModularStructure(JToken token, Transform parent)
    {
        switch (token.Type)
        {
            case JTokenType.Object:
                // Check if this is a Feature with a "type" property set to "feature"
                if (token["type"]?.ToString() == "Feature")
                {
                    // Bring the current Y position back to 0
                    currentY = 0;
                    // Increment the current Z position for each new Feature
                    currentZ += 1f;
                }

                // Create a GameObject for each property in the object
                foreach (JProperty property in token)
                {
                    GameObject obj = new GameObject(property.Name);
                    obj.transform.SetParent(parent);

                    // Set the Z position for this GameObject based on the current Z position
                    obj.transform.localPosition = new Vector3(0f, currentY, currentZ);

                    // Increment the current Y position for each new line of text
                    currentY -= levelOffset;

                    // Recursively create the modular structure for this property
                    CreateModularStructure(property.Value, obj.transform);
                }
                break;
            case JTokenType.Array:
                // Create a GameObject for each item in the array
                for (int i = 0; i < token.Count(); i++)
                {
                    GameObject obj = new GameObject(i.ToString());
                    obj.transform.SetParent(parent);

                    // Set the Z position for this GameObject based on the current Z position
                    obj.transform.localPosition = new Vector3(0f, currentY, currentZ);

                    // Increment the current Y position for each new line of text
                    currentY -= levelOffset;

                    // Recursively create the modular structure for this item
                    CreateModularStructure(token[i], obj.transform);
                }
                break;
            case JTokenType.String:
            case JTokenType.Integer:
            case JTokenType.Float:
                // Create a TextMesh for the value
                GameObject textObject = new GameObject("Text");
                textObject.transform.SetParent(parent);
                TextMesh textMesh = textObject.AddComponent<TextMesh>();
                textMesh.text = token.Type.ToString() +": "+ token.ToString();

                // Set the Z position for this GameObject based on the current Z position
                textObject.transform.localPosition = new Vector3(0f, currentY, currentZ);

                // Increment the current Y position for each new line of text
                currentY -= levelOffset;

                // Set the color of the TextMesh based on the JSON object type
                switch (token.Type)
                {
                    case JTokenType.String:
                        textMesh.color = Color.red;
                        break;
                    case JTokenType.Integer:
                        textMesh.color = Color.green;
                        break;
                    case JTokenType.Float:
                        textMesh.color = Color.blue;
                        break;
                }
                break;
                break;
            default:
                // Do nothing for other types
                break;
        }
    }
}
