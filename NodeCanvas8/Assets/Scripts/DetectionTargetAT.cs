using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions
{

    public class DetectionTargetAT : ActionTask
    {
        public BBParameter<LayerMask> thiefLayerMask;
        public BBParameter<float> detectionRadius;
        public BBParameter<Transform> currentTarget;
        public BBParameter<Transform> detectionTarget;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            return null;
        }

        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute()
        {
            // Running this on update would hold the behaviour tree

            Transform bestTarget = null;
            float bestValue = Mathf.Infinity;

            Collider[] detectedThiefColliders = Physics.OverlapSphere(agent.transform.position, detectionRadius.value, thiefLayerMask.value);
            foreach (Collider detectedThief in detectedThiefColliders)
            {
                float distanceToThief = Vector3.Distance(detectedThief.transform.position, agent.transform.position);

                if (bestValue > distanceToThief)
                {
                    bestTarget = detectedThief.transform;
                    bestValue = distanceToThief;
                }

            }

            detectionTarget.value = bestTarget;

            bool hasTarget = bestTarget != null;
            if (detectedThiefColliders.Length > 0)
            {
                Debug.DrawLine(agent.transform.position, bestTarget.position);
            }

            EndAction(true);
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
        {


        }

        //Called when the task is disabled.
        protected override void OnStop()
        {

        }

        //Called when the task is paused.
        protected override void OnPause()
        {

        }
    }
}