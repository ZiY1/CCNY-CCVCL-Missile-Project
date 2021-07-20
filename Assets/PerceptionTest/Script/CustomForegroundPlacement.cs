using System;
using System.Linq;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers.Utilities;
using UnityEngine.Perception.Randomization.Samplers;

namespace UnityEngine.Perception.Randomization.Randomizers.SampleRandomizers
{
    /// <summary>
    /// Creates a 2D layer of of evenly spaced GameObjects from a given list of prefabs
    /// </summary>
    [Serializable]
    [AddRandomizerMenu("Perception/Custom Foreground Placement")]
    public class CustomForegroundPlacement : Randomizer
    {
        /// <summary>
        /// The Z offset component applied to the generated layer of GameObjects
        /// </summary>
        [Tooltip("The Z offset applied to positions of all placed objects.")]
        public float depth;

        /// <summary>
        /// The minimum distance between all placed objects
        /// </summary>
        [Tooltip("The minimum distance between the centers of the placed objects.")]
        public float separationDistance = 2f;

        /// <summary>
        /// The size of the 2D area designated for object placement
        /// </summary>
        [Tooltip("The width and height of the area in which objects will be placed. These should be positive numbers and sufficiently large in relation with the Separation Distance specified.")]
        public Vector2 placementArea;

        /// <summary>
        /// The list of prefabs sample and randomly place
        /// </summary>
        [Tooltip("The list of Prefabs to be placed by this Randomizer.")]
        public GameObjectParameter prefabs;

        int i = 0;

        GameObject m_Container;
        GameObjectOneWayCache m_GameObjectOneWayCache;

        /// <inheritdoc/>
        protected override void OnAwake()
        {
            //Debug.Log("Category Count: " + prefabs.GetCategoryCount());
            //Debug.Log("Category: " + prefabs.GetCategory(prefabs.GetCategoryCount() - 1).name);

            m_Container = new GameObject("Foreground Objects");
            m_Container.transform.parent = scenario.transform;
            prefabs.GetCategory(0).transform.parent = m_Container.transform;
            m_GameObjectOneWayCache = new GameObjectOneWayCache(
                m_Container.transform, prefabs.categories.Select(element => element.Item1).ToArray());
        }

        /// <summary>
        /// Generates a foreground layer of objects at the start of each scenario iteration
        /// </summary>
        protected override void OnIterationStart()
        {
            var seed = SamplerState.NextRandomState();
            var placementSamples = PoissonDiskSampling.GenerateSamples(
                placementArea.x, placementArea.y, separationDistance, seed);
            var offset = new Vector3(placementArea.x, placementArea.y, 0f) * -0.5f;
            //foreach (var sample in placementSamples)
            //{
            //    var instance = m_GameObjectOneWayCache.GetOrInstantiate(prefabs.Sample());
            //    instance.transform.position = new Vector3(sample.x, sample.y, depth) + offset;
            //}
            //int LENGTH = placementSamples.Length; //output 1623
            //int index = Random.Range(0, LENGTH);
            //instance.transform.position = new Vector3(placementSamples[index].x, placementSamples[index].y, depth) + offset;

            //controls where the missle will be placed. This depends on the Placement Area set in Randomizer in Inspector.
            if (i == 5)
                i = 0;
            //var instance = m_GameObjectOneWayCache.GetOrInstantiate(prefabs.Sample());
            prefabs.GetCategory(0).transform.position = new Vector3(i, i++, depth) + offset; //uses existing gameobject
            placementSamples.Dispose();
        }

        /// <summary>
        /// Deletes generated foreground objects after each scenario iteration is complete
        /// </summary>
        protected override void OnIterationEnd()
        {
            m_GameObjectOneWayCache.ResetAllObjects();
        }
    }
}
