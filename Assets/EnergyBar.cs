using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class EnergyBar : MonoBehaviour
{
    public static EnergyBar Instance { get; private set; }
    public GameObject EnergyBarIcon;
    private int Energy = 0;
    private int MaxEnergy;

    private Animator animator;

    #region Start Animation
    private bool StartAnimated = false;
    private int StartAnimationDelay = 10;
    private int StartAnimationDelayCounter = 10;
    #endregion

    public AudioClip EnergyDownSound;

    public Dictionary<int, GameObject> EnergyBarIcons = new Dictionary<int, GameObject>();

    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        MaxEnergy = Leni.MaxEnergy;
        animator.SetInteger("Energy", MaxEnergy);
    }

    private void FixedUpdate()
    {
        // Cascading animation when this energy bar is instantiated.
        if (!StartAnimated)
        {
            StartAnimationDelayCounter++;
            if (StartAnimationDelayCounter < StartAnimationDelay)
            {
                return;
            }
            StartAnimationDelayCounter = 0;

            if (Energy < MaxEnergy)
            {
                var energyBarIcon = Instantiate(EnergyBarIcon, transform);
                energyBarIcon.transform.localPosition = energyBarIcon.transform.localPosition + new Vector3(36f * Energy, 0f, 0f);
                EnergyBarIcons.Add(Energy, energyBarIcon);
                Energy++;
            }
            else 
            {
                StartAnimated = true;
            }
        }
    }

    public void SetEnergy(int energy)
    {
        while (EnergyBarIcons.Count != energy)
        {
            if (EnergyBarIcons.Count > energy)
            {
                RemoveEnergy();
            }

            if (EnergyBarIcons.Count < energy)
            {
                AddEnergy();
            }

            animator.SetInteger("Energy", EnergyBarIcons.Count);
        };
    }

    private void AddEnergy()
    {
        var energyBarIcon = Instantiate(EnergyBarIcon, transform);
        energyBarIcon.transform.localPosition = energyBarIcon.transform.localPosition + new Vector3(36f * (EnergyBarIcons.Count), 0f, 0f);
        EnergyBarIcons.Add(EnergyBarIcons.Count, energyBarIcon);
    }

    private void RemoveEnergy()
    {
        var index = EnergyBarIcons.Count - 1;

        var energyBarIcon = EnergyBarIcons[index];
        EnergyBarIcons.Remove(index);
        var energyBarIconAnimator = energyBarIcon.GetComponent<Animator>();
        energyBarIconAnimator.SetBool("Remove", true);
        AudioManager.Play("EnergyDown", EnergyDownSound, 1, false, 1);
    }
}
