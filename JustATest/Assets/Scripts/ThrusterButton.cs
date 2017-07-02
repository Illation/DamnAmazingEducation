using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterButton : MonoBehaviour
{
    [SerializeField]
    bool IsLeft = true;

    private WallController _wall;

    [SerializeField]
    GameObject Projector;
    float _projectorRotation;

    [SerializeField]
    GameObject TextMesh;
    private Renderer _rend = null;
    private Vector2 _offset = Vector2.zero;

    // Use this for initialization
    void Start()
    {
        _wall = (GameObject.Find("Wall")).GetComponent<WallController>();

        _rend = TextMesh.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var thrusters = IsLeft ? _wall.LeftThrusters : _wall.RightThrusters;
        bool allLoaded = true;
        foreach (var thruster in thrusters)
        {
            if (!(thruster.IsLoaded) || thruster.IsActivated) allLoaded = false;
        }
        if(allLoaded)
        {
            _offset = new Vector2(0, 0.5f);
            _rend.material.SetColor("_EmissionColor", new Color(255, 255, 255));
            Projector.SetActive(true);
        }
        else
        {
            Projector.SetActive(false);
            _offset = Vector2.zero;
            _rend.material.SetColor("_EmissionColor", new Color(0, 0, 0));
        }
        _projectorRotation += Time.deltaTime * 360;
        Projector.transform.rotation = Quaternion.Euler(90, _projectorRotation, 90);
        _rend.material.SetTextureOffset("_MainTex", _offset);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GlobalSoundManager.instance.PlayClip(GlobalSounds.ButtonPress, IsLeft ? SourcePosition.Left : SourcePosition.Right, 1);
            var thrusters = IsLeft ? _wall.LeftThrusters : _wall.RightThrusters;
            bool allLoaded = true;
            foreach (var thruster in thrusters)
            {
                if (!(thruster.IsLoaded)) allLoaded = false;
            }
            if (allLoaded)
            {
                foreach (var thruster in thrusters)
                {
                    thruster.Activate();
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GlobalSoundManager.instance.PlayClip(GlobalSounds.ButtonPress, IsLeft ? SourcePosition.Left : SourcePosition.Right, 1);
        }
    }
}
