using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionSwitch : MonoBehaviour
{
    //[SerializeField]
    //private string _previewKey = "Fire2"; //temporaneo

    private GameObject[] _dimensions;
    [SerializeField]
    private GameObject _startDimension;

    [SerializeField]
    private GameObject _spaceBarDimA;
    [SerializeField]
    private GameObject _spaceBarDimB;
    private GameObject _activeSpaceBarDim;

    [SerializeField]
    private float _alphaPrev = 0.5f;
    private float _alphaOrigin = 1f;
    private List<GameObject> _previewDim = new List<GameObject>();


    void Start()
    {
        // collect al object tag dimension
        _dimensions = GameObject.FindGameObjectsWithTag("Dimension");

        // set active start dimension  
        ActiveDimension(_startDimension);
       _activeSpaceBarDim = _startDimension; //first yump fix
    }

    void Update()
    {
        // check presure of a key
        // switch dim: active asocieted dimension and deactive al other dimension
        string input = Input.inputString;

        //ignore null input to avoid unnecessary computation
        if (!string.IsNullOrEmpty(input))
        {
            switch (input) //sistemare che sia push
            {
                case " ":
                    if (_spaceBarDimA != null & _spaceBarDimB != null)
                    {
                        if (_activeSpaceBarDim == _spaceBarDimA) _activeSpaceBarDim = _spaceBarDimB;
                        else _activeSpaceBarDim = _spaceBarDimA;

                        ActiveDimension(_activeSpaceBarDim);
                    }
                    break;           

            }
        }

        // check press of preview 
        // expose preview 
        // active all dimension child"shadow"
        Preview();
    }

    void ActiveDimension(GameObject targetDimension)
    {
        //deactive all
        foreach(GameObject dimension  in _dimensions)
        {
            dimension.SetActive(false);
        }

        //active target
        targetDimension.SetActive(true);
    }

    void Preview()
    {

        if (Input.GetButtonDown("Fire2")) //
        {
            // check what active            
            foreach (GameObject dimension in _dimensions)
            {
                if (!dimension.activeSelf)
                {
                    //set active non active whit alpha 0

                    dimension.SetActive(true);

                    foreach (Transform child in dimension.transform)
                    {
                        child.GetComponentInChildren<BoxCollider2D>().enabled = false;
                        SpriteRenderer col = child.GetComponent<SpriteRenderer>();

                        _alphaOrigin = col.material.color.a;
                        Color newColor = new Color(col.material.color.r, col.material.color.g, col.material.color.b, _alphaPrev);


                        col.material.color = newColor;
                    }
                    

                    _previewDim.Add(dimension);
                }
            }
        }

        if (Input.GetButtonUp("Fire2")) //Input.GetButtonUp("Fire2")
        {
            //col.a = 1;
            // deactive dimension with preview satus
            foreach ( GameObject dimension in _previewDim)
            {
                dimension.SetActive(false);
               

                foreach (Transform child in dimension.transform)
                {
                    child.GetComponentInChildren<BoxCollider2D>().enabled = true;
                    SpriteRenderer col = child.GetComponent<SpriteRenderer>();

                    
                    Color newColor = new Color(col.material.color.r, col.material.color.g, col.material.color.b, _alphaOrigin); ///serve una lista di apa origni

                    col.material.color = newColor;
                }
            }

            _previewDim.Clear();
        }

    }


}
