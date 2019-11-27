﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VisualLineManager : MonoBehaviour
{
    public GameObject template;
    public static float width = .2f;
    private List<VisualLine> lines;

    // Start is called before the first frame update
    void Start()
    {
        lines = new List<VisualLine>();

        if(template == null)
        {
            const int capVertices = 10;

            template = new GameObject("default line template");
            LineRenderer renderer = template.AddComponent<LineRenderer>();

            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, Color.white);

            Material material = new Material(Shader.Find("UI/Unlit/Text"));
            material.SetTexture("_MainTex", texture);
            renderer.material = material;

            // Renderer settings
            renderer.startWidth = width;
            renderer.endWidth = width;
            renderer.numCapVertices = capVertices;
        }

        template.SetActive(false);
    }

    public VisualLine CreateLine(DataPoint start, DataPoint end, Color? color = null, double thickness = .2)
    {
        GameObject line = Instantiate(template);
        line.name = "line";
        line.transform.SetParent(this.transform);
        line.SetActive(true);


        LineRenderer renderer = line.GetComponent<LineRenderer>();
        if(color != null)
        {
            renderer.material.SetColor("_EmissionColor", color.Value);
            renderer.material.GetColor("_EmissionColor");
        }

        renderer.sortingOrder = lines.Count + 1;

        VisualLine visualLine = new VisualLine(this, renderer, start, end, renderer.material.GetColor("_EmissionColor"), (float)thickness);
        lines.Add(visualLine);

        return visualLine;
    }

    public void StopTrackingLine(VisualLine line)
    {
        lines.Remove(line);
    }
}
