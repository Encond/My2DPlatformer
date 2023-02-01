using UnityEngine;
using UnityEngine.Playables;

namespace Timelines
{
    public class TimelineManager : MonoBehaviour
    {
        [SerializeField] private PlayableDirector _startGameTimeline;

        private void Start()
        {
            _startGameTimeline.playOnAwake = true;
            _startGameTimeline.gameObject.SetActive(true);
        }
    }
}
