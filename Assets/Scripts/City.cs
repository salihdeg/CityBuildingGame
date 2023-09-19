using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class City : MonoBehaviour
{
    public static City Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _statsText;

    [Header("Stats")]
    public int money;
    public int day;
    public int currentPopulation;
    public int currentJob;
    public int currentFood;

    public int maxPopulation;
    public int maxJobs;

    public int incomePerJob;

    public List<Building> buildings = new List<Building>();

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        UpdateStatsText();
    }

    public void OnPlaceBuilding(Building building)
    {
        money -= building.buildingPreset_SO.cost;
        maxPopulation += building.buildingPreset_SO.population;
        maxJobs += building.buildingPreset_SO.jobs;
        currentFood += building.buildingPreset_SO.food;
        buildings.Add(building);

        UpdateStatsText();
    }

    public void OnRemoveBuilding(Building building)
    {
        maxPopulation -= building.buildingPreset_SO.population;
        maxJobs -= building.buildingPreset_SO.jobs;
        currentFood -= building.buildingPreset_SO.food;
        buildings.Remove(building);

        Destroy(building.gameObject);

        UpdateStatsText();
    }

    public void EndTurn()
    {
        CalculateMoney();
        CalculatePopulation();
        CalculateJobs();
        CalculateFood();

        day++;
        UpdateStatsText();
    }

    private void CalculateFood()
    {
        currentJob = 0;
        foreach (Building item in buildings)
        {
            currentJob += item.buildingPreset_SO.food;
        }
    }

    private void CalculateJobs()
    {
        currentJob = (int)MathF.Min(currentPopulation, maxJobs);
    }

    private void CalculatePopulation()
    {
        if (currentFood >= currentPopulation && currentPopulation < maxPopulation)
        {
            currentFood -= currentPopulation / 4;
            currentPopulation = (int)MathF.Min(currentPopulation + (currentFood / 4), maxPopulation);
        }
        else if (currentFood < currentPopulation)
        {
            currentPopulation = currentFood;
        }
    }

    private void CalculateMoney()
    {
        money += currentJob * incomePerJob;
        foreach (Building item in buildings)
        {
            money -= item.buildingPreset_SO.costPerTurn;
        }
    }

    private void UpdateStatsText()
    {
        string stats = $"Day: {day} Money: {money}$\nPop: {currentPopulation}/{maxPopulation} Job: {currentJob}/{maxJobs} Food: {currentFood}";
        _statsText.text = stats;
    }
}
