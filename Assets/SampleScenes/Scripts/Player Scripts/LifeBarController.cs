using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LifeBarController : MonoBehaviour
{
    public float m_StartingLife = 0f;               // The amount of Life Player starts with
    public Slider m_Slider;                         // The slider to represent how much Life the Player currently has.
    public Image m_FillImage;                       // The image component of the slider.
    public Color m_FullLifeColor = Color.red;       // The color the Life bar will be when on full life.
    public Color m_ZeroLifeColor = Color.green;     // The color the Life bar will be when on no life.

    public GameObject FairyLife;

    public static float m_CurrentLife;              // How much Life the Player currently has.
    private bool m_Reborn;                          // Has the Player been healed to full Life yet?

    Animator anim;
    public static AudioSource audioSrc;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();
    }


    private void OnEnable()
    {
        // When the Player is enabled, reset the player's Life and whether or not it's Reborn.
        m_CurrentLife = m_StartingLife;
        m_Reborn = false;

        // Update the Life's slider's value and color.
        SetLifeUI();
    }


    public void TakeLife(float amount)
    {

        //audioSrc.clip = Resources.Load<AudioClip>(" ");
        //audioSrc.Play();

        // Increase current Life by the amount of Life given.
        m_CurrentLife += amount;

        // Change the UI elements appropriately.
        SetLifeUI();

        // If the current health is at or below zero and it has not yet been registered, call OnDeath.
        if (m_CurrentLife >= 100f && !m_Reborn)
        {
            OnRebirth();
        }
    }


    private void SetLifeUI()
    {
        // Set the slider's value appropriately.
        m_Slider.value = m_CurrentLife;

        // Interpolate the color of the bar between the choosen colors based on the current percentage of the starting Life.
        m_FillImage.color = Color.Lerp(m_ZeroLifeColor, m_FullLifeColor, m_CurrentLife / 100f);
    }


    private void OnRebirth()
    {
        // Set the flag so that this function is only called once.
        m_Reborn = true;

        //audioSrc.clip = Resources.Load<AudioClip>(" ");
        //audioSrc.Play();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fairy"))
        {
            TakeLife(20);
            other.gameObject.SetActive(false);
            //Destroy(FairyLife);
        }
    }
}