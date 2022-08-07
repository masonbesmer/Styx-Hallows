using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WoodGatherBarController : MonoBehaviour
{
    public float m_StartingWood = 0f;               // The amount of Wood Player starts with
    public Slider m_Slider;                         // The slider to represent how much Wood the Player currently has.
    public Image m_FillImage;                       // The image component of the slider.
    public Color m_FullWoodColor = Color.yellow;    // The color the Wood bar will be when on full Wood.
    public Color m_ZeroWoodColor = Color.blue;      // The color the Wood bar will be when on no Wood.

    public GameObject WoodPile;

    public static float m_CurrentWood;              // How much Wood the Player currently has.

    Animator anim;
    public static AudioSource audioSrc;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();
    }


    private void OnEnable()
    {
        // When the Player is enabled, reset the player's WoodGather.
        m_CurrentWood = m_StartingWood;

        // Update the WoodGather's slider's value and color.
        SetWoodGatherUI();
    }


    public void GetWood(float amount)
    {

        //audioSrc.clip = Resources.Load<AudioClip>(" ");
        //audioSrc.Play();

        // Increase current WoodGather amount by the amount of Wood given.
        m_CurrentWood += amount;

        // Change the UI elements appropriately.
        SetWoodGatherUI();
    }


    public void GiveWood(float amount)
    {

        //audioSrc.clip = Resources.Load<AudioClip>(" ");
        //audioSrc.Play();

        // Decrease current WoodGather amount by the amount of Wood given.
        m_CurrentWood -= amount;

        // Change the UI elements appropriately.
        SetWoodGatherUI();
    }


    private void SetWoodGatherUI()
    {
        // Set the slider's value appropriately.
        m_Slider.value = m_CurrentWood;

        // Interpolate the color of the bar between the choosen colors based on the max amount of gathered Wood: 100.
        m_FillImage.color = Color.Lerp(m_ZeroWoodColor, m_FullWoodColor, m_CurrentWood / 100f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wood"))
        {
            GetWood(10);
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Pyre") && m_CurrentWood >= 20)
        {
            GiveWood(20);
            other.gameObject.SetActive(false);
        }
    }
}
