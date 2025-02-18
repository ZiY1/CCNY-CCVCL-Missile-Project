using System;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers.SampleRandomizers.Tags;
using UnityEngine.Perception.Randomization.Samplers;

namespace UnityEngine.Perception.Randomization.Randomizers.SampleRandomizers
{
    /// <summary>
    /// Randomizes the rotation of objects tagged with a RotationRandomizerTag
    /// </summary>
    [Serializable]
    [AddRandomizerMenu("Perception/Custom Rotation Randomizer")]
    public class CustomRotationRandomizer : Randomizer
    {
        /// <summary>
        /// The range of random rotations to assign to target objects
        /// </summary>
        [Tooltip("The range of random rotations to assign to target objects.")]
        public Vector3Parameter rotation = new Vector3Parameter
        {
            x = new UniformSampler(-5, 5),
            y = new UniformSampler(-5, 5),
            z = new UniformSampler(-5, 5)
        };

        public GameObject missileParent;
        [SerializeField] Vector3 initRocketRotation; //takes initial rotation of rocket

        /// <summary>
        /// Get initial rotation settings for rocket so randomize rotation does not rotate too much
        /// </summary>
        protected override void OnScenarioStart()
        {
            base.OnScenarioStart();
            //get init rotation for x;
            //Quaternion rot = rocket.transform.rotation;
            //Debug.Log("InitRocketRotataion: " + initRocketRotation);

            //initRocketRotation = missileParent.transform.eulerAngles;
        }

        /// <summary>
        /// Randomizes the rotation of tagged objects at the start of each scenario iteration
        /// Note: changing the rockets rotation does not change the rockets course.
        /// </summary>
        protected override void OnIterationStart()
        {
            var tags = tagManager.Query<RotationRandomizerTag>();
            foreach (var tag in tags)
            {
                tag.transform.rotation = Quaternion.Euler(rotation.Sample() + missileParent.transform.eulerAngles);

                //tag.transform.rotation = Quaternion.Euler(rotation.Sample());
            }
        }
    }
}
