using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LifeBarController : MonoBehaviour
{
    public float m_StartingEss = 0f;               // The amount of Life Player starts with
    public static float m_MaxEss = 100f;           // The max amount of essence
    public Slider m_Slider;                        // The slider to represent how much Life the Player currently has.
    public Image m_FillImage;                      // The image component of the slider.
    public Color m_FullEssColor = Color.green;    // The color the Life bar will be when on full life.
    public Color m_ZeroEssColor = Color.red;       // The color the Life bar will be when on no life.

    public static float m_CurrentEss;              // How much Life the Player currently has.

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
        m_CurrentEss = m_StartingEss;

        // Update the Life's slider's value and color.
        SetEssUI();
    }


    public void TakeEss(float amount)
    {
        //audioSrc.clip = Resources.Load<AudioClip>(" ");
        //audioSrc.Play();

        // If the current health would be reduced to 0 call OnRebirth(), else just set the UI
        if(m_CurrentEss - amount < 0f)
        {
            m_CurrentEss -= amount;
            SetEssUI();

            OnRebirth();
        }
        else
        {
            m_CurrentEss -= amount;
            SetEssUI();
        }
    }


    public void GiveEss(float amount)
    {
        //audioSrc.clip = Resources.Load<AudioClip>(" ");
        //audioSrc.Play();

        //Give Player amount of essence upon collection
        m_CurrentEss += amount;
        SetEssUI();
        
    }


    private void SetEssUI()
    {
        // Set the slider's value appropriately.
        m_Slider.value = m_CurrentEss;

        // Interpolate the color of the bar between the choosen colors based on the current percentage of Essence
        m_FillImage.color = Color.Lerp(m_ZeroEssColor, m_FullEssColor, m_CurrentEss / m_MaxEss);
    }


    private void OnRebirth()
    {
        // Set the flag so that this function is only called once.

        //audioSrc.clip = Resources.Load<AudioClip>(" ");
        //audioSrc.Play();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fairy"))
        {
            TakeEss(20);
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Essence"))
        {
            GiveEss(20);
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Pyre") && m_CurrentEss >= 100)
        {
            TakeEss(100);
            other.gameObject.SetActive(false);
        }
    }
}