using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using AnimalStates;

namespace AnimalStates
{
    public class Eating : MonoBehaviour, IAnimalState
    {
        // Singleton
        public static Eating instance { get; private set; }

        public GameObject FindTarget(Animal mover)
        {
            return Hunger.instance.FindTarget(mover);
        }

        public void TryToMove(Animal mover, GameObject target)
        {
            if (mover.isMovable)
            {
                mover.isMovable = false;
                Move(mover, target);
            }
        }

        public async void Move(Animal mover, GameObject target)
        {
            Rigidbody rb = mover.GetComponent<Rigidbody>();

            if (target == null)
            {
                mover.isMovable = true;
            }
            else if ((target.transform.position - mover.transform.position).sqrMagnitude < mover.transform.localScale.sqrMagnitude)
            {
                rb.transform.Rotate(-10.0f, 0.0f, 0.0f);
                await Task.Delay(TimeManager.instance.getCurrentGameSpeedIntervalA());
                mover.isMovable = true;
            }
            else
            {
                mover.isMovable = true;
            }
        }

        public void Action(Animal mover, GameObject target)
        {
            foreach (Species.Type diet in mover.stats.DIET)
            {
                if (target.CompareTag(diet.ToString()))
                {
                    Debug.Log("EATING");
                }
            }
            //(Flower)target.
            //mover.calorie += 100f;
            //Debug.Log("EATING");
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

    }
}
