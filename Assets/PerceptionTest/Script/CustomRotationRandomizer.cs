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
        //[Tooltip("The range of random rotations to assign to target objects.")]
        //public Vector3Parameter rotation = new Vector3Parameter
        //{
        //    x = new UniformSampler(0, 360),
        //    y = new UniformSampler(0, 360),
        //    z = new UniformSampler(0, 360)
        //};

        int i = 0;
        Vector3 rotation;

        /// <summary>
        /// Randomizes the rotation of tagged objects at the start of each scenario iteration
        /// </summary>
        /// 
        protected override void OnIterationStart()
        {
            //controls the rotation of the missle
            rotation = new Vector3(i, i, i++);
            var tags = tagManager.Query<RotationRandomizerTag>();
            foreach (var tag in tags)
                tag.transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
