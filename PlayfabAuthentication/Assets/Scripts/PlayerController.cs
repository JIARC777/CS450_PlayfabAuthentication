using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerController : MonoBehaviour
{

    public float speed;
    private Rigidbody rig;

    private float startTime;
    private float timeTaken;

    private int collectablesPicked;
    public int maxCollectables = 10;

    public GameObject playButton;
    public TextMeshProUGUI curTextTime;

    private bool isPlaying;
    // Start is called before the first frame update
    void Awake()
    {
        rig = GetComponent<Rigidbody>();    
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying)
            return;
        float x = Input.GetAxis("Horizontal") * speed;
        float z = Input.GetAxis("Vertical") * speed;
        rig.velocity = new Vector3(x, rig.velocity.y, z);
        curTextTime.text = (Time.time - startTime).ToString("F2");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            collectablesPicked++;
            Destroy(other.gameObject);
            if (collectablesPicked == maxCollectables)
                End();
        }
    }

    public void Begin()
    {
        playButton.SetActive(true);
        startTime = Time.time;
        isPlaying = true;
    }

    void End()
    {
        timeTaken = Time.time - startTime;
        isPlaying = false;
        LeaderBoard.instance.SetLeaderboardEntry(-Mathf.RoundToInt(timeTaken * 1000f));
        Debug.Log(-Mathf.RoundToInt(timeTaken * 1000f));
        
    }
}
