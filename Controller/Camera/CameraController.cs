using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform parent;            // Sobre el que orbita y depende la posicion de la camara
    public Transform lookAt;            // Adonde mira la camara
    public float distanceFromParent;
    private float fixedDist;            // Distancia corregida de la camara respecto al parent en caso de colision u oclusion


    public float sensitivityWheel;      // Sensibilidad del zoom (rueda del raton)
    private float inputScrollWheel;

    // Use this for initialization
    void Start () {
        SetParent(parent);
        transform.position = parent.position - parent.forward * distanceFromParent;
    }

    // Update is called once per frame
    void Update () {
        inputScrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if (inputScrollWheel != 0)
        {
            distanceFromParent += inputScrollWheel * sensitivityWheel;      // Zoom
            distanceFromParent = Mathf.Clamp(distanceFromParent, 1f, 3f);   // Limita la distancia (zoom) para el rango [2,6]
        }

        // Calculamos la distancia corregida
        fixedDist = FixDistance();                     

        transform.position = Vector3.Lerp(transform.position, parent.position - parent.forward * fixedDist, 25f * Time.deltaTime);
        transform.LookAt(lookAt);
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }

    // Corrige la distancia ante colisiones u oclusiones de la camara
    float FixDistance()
    {
        RaycastHit hit;                 // Guarda la informacion de la colision
        LayerMask layerMask = 1 << 8;   // Asignamos el layer 8 que es el del player
        layerMask = ~layerMask;         // Invierte el layer para que colisione con cualquier layer excepto este

        /* Emite un Raycast desde el parent a la camara
         colisionara con el primer collider que encuentre (punto mas cercano al parent)
         colisionara solo con el layer pasado por parametro */
        if (Physics.Raycast(parent.position, -parent.forward, out hit, distanceFromParent, layerMask))
        {
            return hit.distance;        // Si colisiona se devuelve la distancia corregida
        }
        return distanceFromParent;       // Si no colisiona se devuelve la distancia original
    }
}
