using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlantStates;

public class Flower : Plant
{
    public Rigidbody rb = null;
    public GameObject target = null;
    public float minScale = 0.1f;
    public float maxScale = 1.0f;
    public float repeatSpan = 1f;
    public float currentTime = 0f;
    public float growRate = 0.1f;

    public override void Grow()
    {
        /* Please think about merit/demerit of bellow if statement and Mathf.Min,
         * https://docs.unity3d.com/ScriptReference/Mathf.Min.html
        if (this.transform.localScale.x < maxScale)
        {
            this.transform.localScale += Vector3.one * growRate;
        }
        */
        float nextScale = Mathf.Min(this.transform.localScale.x + growRate, maxScale);
        this.transform.localScale = Vector3.one * nextScale;
    }

    public override void ConsumeCalorie()
    {
        throw new System.NotImplementedException();
    }

    public override void ConsumeWater()
    {
        throw new System.NotImplementedException();
    }

    public override void InTakeCalorie()
    {
        this.calorie = Mathf.Min(this.calorie + this.stats.PRODUCE_CALORIE, this.stats.MAX_CALORIE);
    }

    public override void InTakeWater()
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
        currentTime += Time.deltaTime * TimeManager.instance.getCurrentGameSpeedValue();
        if (currentTime > repeatSpan)
        {
            currentTime = 0f;
            Grow();
            InTakeCalorie();
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void Initialize()
    {
        // You have to place stats.asset files into "Resources" folder
        this.stats = Resources.Load<PlantStats>("FlowerStats");
        this.age = 0;
        this.isAdult = false;
        this.health = stats.MAX_HEALTH;
        this.calorie = stats.BASE_CALORIE;
        this.water = stats.BASE_WATER;
        //this.gene = gameObject.AddComponent<PlantGene>();
        // TODO: Add Genus
        this.state = gameObject.AddComponent<Normal>();
        this.rb = this.GetComponent<Rigidbody>();

        float initialScale = Random.Range(minScale, maxScale);
        this.transform.localScale = Vector3.one * initialScale;
    }

}
