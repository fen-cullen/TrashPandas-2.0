using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashUIController : MonoBehaviour
{
    private Slider slider;
    private PlayerController playerController;
    private Vector3 normalPosition;
    private Vector3 hidePosition;
    // Start is called before the first frame update
    void Start()
    {
        this.slider = GetComponent<Slider>();
        this.playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        this.slider.value = 0;
        this.normalPosition = this.transform.position;
        this.hidePosition = new Vector3(this.transform.position.x, this.transform.position.y - Mathf.Abs(this.transform.position.y), this.transform.position.z);
        this.transform.position = this.hidePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.playerController.dashState == PlayerController.DashState.Ready || this.playerController.dashState == PlayerController.DashState.Dashing)
        {
            this.slider.value = 0;
            this.transform.position = this.hidePosition;
        } 
        else
        {
            this.slider.value = this.playerController.dashCooldown;
            this.transform.position = this.normalPosition;
        }
    }
}
