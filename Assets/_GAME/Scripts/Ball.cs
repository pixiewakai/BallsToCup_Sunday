using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour {
    public List<Color> Colors;

    private MeshRenderer _meshRenderer;
    
    private void Awake() {
        _meshRenderer = GetComponent<MeshRenderer>();
        var mat = new Material(_meshRenderer.material) {
            color = Colors[Random.Range(0, Colors.Count)]
        };
        _meshRenderer.material = mat;
    }
}
