using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using AnimalStates;

namespace AnimalStates
{
    public class Hunger : MonoBehaviour, IAnimalState
    {
        // Singleton
        public static Hunger instance { get; private set; }

        public float rayScale = 1.0f;
        public float minXRotation = -15.0f;
        public float maxXRotation = 15.0f;
        public Vector3 targetPos;
        RaycastHit hit;

        public GameObject FindTarget(Animal mover)
        {
            // You can also use onCllision() method
            int targetLayer = LayerMask.GetMask(Layers.Name.Plants.ToString());
            Collider[] colliders = Physics.OverlapSphere(mover.transform.position, 5f * mover.gene.sense, targetLayer);
            GameObject nearestObj = null;
            Vector3 nearestObjPos = Vector3.one * 999;
            foreach (Collider hit in colliders)
            {
                if ((hit.gameObject.transform.position - mover.transform.position).sqrMagnitude < nearestObjPos.sqrMagnitude)
                {
                    nearestObjPos = hit.transform.position;
                    nearestObj = hit.gameObject;
                }
            }
            return nearestObj;
        }

        public void TryToMove(Animal mover, GameObject target)
        {
            if (mover.isMovable)
            {
                bool isStraightRayTouchSomething = CheckStraight(mover);
                bool isDiagnallyDownwardRayTouchWater = CheckDiagnallyDownward(mover);

                if (isStraightRayTouchSomething || isDiagnallyDownwardRayTouchWater)
                {
                    mover.transform.rotation = Quaternion.Euler(Mathf.Clamp(mover.transform.rotation.x, minXRotation, maxXRotation), mover.transform.rotation.y, 0.0f);
                    mover.transform.Rotate(Vector3.up, Random.Range(-60.0f, 60.0f));
                }
                else if (!isStraightRayTouchSomething && !isDiagnallyDownwardRayTouchWater)
                {
                    mover.isMovable = false;
                    if (mover.momUid == null)
                    {
                        // Follow the mother
                        Move(mover, target);
                    }
                    else
                    {
                        Move(mover, target);
                    }
                }
            }

        }

        public async void Move(Animal mover, GameObject target)
        {
            Rigidbody rb = mover.GetComponent<Rigidbody>();

            if (target == null)
            {
                rb.transform.Rotate(-10.0f, 0.0f, 0.0f);
                rb.velocity = rb.transform.forward + rb.transform.up;
                await Task.Delay(TimeManager.instance.getCurrentGameSpeedIntervalA());
                mover.isMovable = true;
            }
            else if ((target.transform.position - mover.transform.position).sqrMagnitude < mover.transform.localScale.sqrMagnitude)
            {
                CalcUtils.LookTarget(mover, target);
                // Do nothing
            }
            else
            {
                CalcUtils.LookTarget(mover, target);
                rb.velocity = rb.transform.forward + rb.transform.up;
                await Task.Delay(TimeManager.instance.getCurrentGameSpeedIntervalA());
                mover.isMovable = true;
            }
        }

        public void Action()
        {
            // Output "Meee" every few seconds;
            Debug.Log("Meee");
        }

        private bool CheckStraight(Animal mover)
        {
            Ray straightRay = new Ray(mover.GetComponent<Renderer>().bounds.center, mover.transform.forward);
            // Debug.DrawRay(straightRay.origin, straightRay.direction * mover.transform.localScale.z * rayScale, Color.green, 2);
            return Physics.Raycast(straightRay, out hit, mover.transform.localScale.z * rayScale);
        }

        private bool CheckDiagnallyDownward(Animal mover)
        {
            Ray diagnallyDownwardRay = new Ray(mover.GetComponent<Renderer>().bounds.center, mover.transform.forward - mover.transform.up / 2);
            // Debug.DrawRay(diagnallyDownwardRay.origin, diagnallyDownwardRay.direction * mover.transform.localScale.z * rayScale, Color.red, 2);
            int waterLayerMask = LayerMask.GetMask(Layers.Name.Water.ToString());
            return Physics.Raycast(diagnallyDownwardRay, out hit, mover.transform.localScale.z * rayScale, waterLayerMask);
        }

        private void RecoverBalance(/* */)
        {
            // Do something
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
