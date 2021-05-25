using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AnimalStates;

public class Sheep : Herbivore
{
    public Rigidbody rb = null;
    public GameObject target = null;
    public float minScale = 0.1f;
    public float maxScale = 1.0f;
    public float growSpan = 30f;
    public float soundSpan = 7f;
    public float currentTime = 0f;
    public float growRate = 0.1f;
    public Slider slider;

    public override void Grow()
    {
        float nextScale = Mathf.Min(this.transform.localScale.x + growRate, maxScale);
        this.transform.localScale = Vector3.one * nextScale;
        this.age++;
    }

    public override void ConsumeCalorie()
    {
        this.calorie = Mathf.Max(this.calorie - this.stats.CONSUME_CALORIE * Time.deltaTime, this.stats.MIN_CALORIE);
        if (this.calorie == this.stats.MIN_CALORIE)
        {
            // Damage
            this.health = Mathf.Max(this.health - Time.deltaTime, this.stats.MIN_HEALTH);
        } 
        else
        {
            // Heal
            this.health = Mathf.Min(this.health + Time.deltaTime, this.stats.MAX_HEALTH);

        }
    }

    public override void ConsumeWater()
    {
        throw new System.NotImplementedException();
    }

    public override void InTakeCalorie()
    {
        throw new System.NotImplementedException();
    }

    public override void InTakeWater()
    {
        throw new System.NotImplementedException();
    }

    public override void Ruminant()
    {
        throw new System.NotImplementedException();
    }

    public override LivingEntity Bleed(Gene momGene, Gene dadGene)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        ConsumeCalorie();

        if (this.health <= this.stats.MIN_HEALTH)
        {
            Die();
        }
        else
        {
            slider.value = this.health;
        }

        if (this.state.GetType() != Eating.instance.GetType())
        {
            if (this.calorie < this.stats.BASE_CALORIE)
            {
                ChangeState(Hunger.instance);
            }
        }

        currentTime += Time.deltaTime * TimeManager.instance.getCurrentGameSpeedValue();
        if (currentTime > growSpan)
        {
            currentTime = 0f;
            Grow();
        }
        if ((int)currentTime % (int)soundSpan == 0)
        {
            MakeSound();
        }

        target = state.FindTarget(this);
        state.Action(this, target);
    }

    private void FixedUpdate()
    {
        state.TryToMove(this, target);
    }

    private void Initialize()
    {
        // You have to place stats.asset files into "Resources" folder
        this.stats = Resources.Load<AnimalStats>("SheepStats");
        this.age = 0;
        this.isAdult = false;
        this.health = stats.MAX_HEALTH;
        this.calorie = stats.BASE_CALORIE;
        this.water = stats.BASE_WATER;
        this.ui = Instantiate(UIManager.instance.animalUI, this.transform);
        this.ui.SetActive(true);
        this.slider = this.ui.transform.Find(UI.Name.Slider_HP.ToString()).GetComponent<Slider>();
        this.slider.minValue = this.stats.MIN_HEALTH;
        this.slider.maxValue = this.stats.MAX_HEALTH;
        this.gene = GeneManager.instance.AnimalGeneInit();
        // TODO: Add Genus
        this.isMovable = true;
        this.state = Normal.instance;
        this.sound = null;
        this.rb = this.GetComponent<Rigidbody>();
        this.stats.DIET = new Species.Type[] { Species.Type.Flower, Species.Type.Grass };
    }

}
