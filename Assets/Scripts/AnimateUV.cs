using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimateUV : MonoBehaviour
{
    public int materialIndex = 0;
    public Vector2 uvAnimationRate = new Vector2(1.0f, 0.0f);
    public string textureName = "_MainTex";
    //Renderer renderer = this.gameObject.GetComponent<MeshRenderer>();

    Vector2 uvOffset = Vector2.zero;
    void LateUpdate()
    {
        uvOffset += (uvAnimationRate * Time.deltaTime);
        if (this.GetComponent<Renderer>().enabled)
        {
            this.GetComponent<Renderer>().materials[materialIndex].SetTextureOffset(textureName, uvOffset);
        }
    }

}