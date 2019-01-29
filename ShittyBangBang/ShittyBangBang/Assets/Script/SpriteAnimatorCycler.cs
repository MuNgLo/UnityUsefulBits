using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimatorCycler : MonoBehaviour
{
    public Sprite[] sprites;
    public float animationSpeed = 10.0f;
    private SpriteRenderer image;
    private float lastChange;
    private int lastIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<SpriteRenderer>();
        image.sprite = sprites[0];
        lastChange = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > lastChange + 1.0f / animationSpeed)
        {
            lastIndex++;
            lastChange = Time.time;
            if(lastIndex < sprites.Length)
            {
                image.sprite = sprites[lastIndex];
            }
            else
            {
                lastIndex = 0;
                image.sprite = sprites[lastIndex];
            }
        }
    }
}
