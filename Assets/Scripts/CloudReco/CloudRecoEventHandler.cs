using UnityEngine;
using System.Collections;

namespace Vuforia
{
	public class CloudRecoEventHandler : MonoBehaviour, ICloudRecoEventHandler
	{
		public static GameObject ImageTargetTemplate;
        public static Vector2 m_vSizeTarget;
        public float m_fTimeScan;

		#region PRIVATE_MEMBER_VARIABLES
		private static ObjectTracker mImageTracker;

		#endregion

		#region UNITY_MONOBEHAVIOUR_METHODS
		void Awake()
		{
			ImageTargetTemplate = FindObjectOfType<ImageTargetBehaviour>().gameObject;
		}
		// Use this for initialization
		void Start()
		{
			CloudRecoBehaviour cloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
			if (cloudRecoBehaviour)
			{
				cloudRecoBehaviour.RegisterEventHandler(this);
			}
		}

        public void StartReco()
        {
            StopAllCoroutines();
			GetComponent<CloudRecoBehaviour>().enabled = true;
			StartCoroutine(WaitUntilSomething());
        }

        void StopReco()
        {
			GetComponent<CloudRecoBehaviour>().enabled = false;
		}

        IEnumerator ScanAndStop()
        {
            yield return new WaitForSeconds(m_fTimeScan);
            StopReco();
        }

        IEnumerator WaitUntilSomething ()
        {
            yield return new WaitUntil(() => mImageTracker.TargetFinder.Deinit());
            mImageTracker.TargetFinder.StartInit(GetComponent<CloudRecoBehaviour>().AccessKey, GetComponent<CloudRecoBehaviour>().SecretKey);

				yield return new WaitUntil(() => mImageTracker.TargetFinder.GetInitState() == TargetFinder.InitState.INIT_SUCCESS);
			//mImageTracker.TargetFinder.ClearTrackables(false);

            Debug.Log("tadaaaa");
			mImageTracker.TargetFinder.StartRecognition();
            StartCoroutine(ScanAndStop());
        }

		#endregion

		#region ICloudRecoEventHandler_IMPLEMENTATION

		public void OnInitialized()
		{
			ObjectTracker imageTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
			if (imageTracker != null) mImageTracker = imageTracker;
            GetComponent<CloudRecoBehaviour>().enabled = false;

		}

		/// Visualize initialization errors
		public void OnInitError(TargetFinder.InitState state)
		{ }

		/// Visualize update errors
		public void OnUpdateError(TargetFinder.UpdateState state)
		{ }

		/// when we start scanning, unregister Trackable from the ImageTargetTemplate, then delete all trackables
		public void OnStateChanged(bool isChanged)
		{
            if (isChanged)
                Debug.Log("new result or again the same"); 
                // :: stopcoroutine from the waiting of the time to stop the reco
		}

		///  Handles new search results
		public void OnNewSearchResult(TargetFinder.TargetSearchResult result)
		{
			// :: stop continuous check internet
			// Instantiate the gameobject Canvas / Reco
			mImageTracker.TargetFinder.ClearTrackables(false);

			// :: Set the trackable to the new gameobject
			ImageTargetBehaviour imageTargetBehaviour =
				(ImageTargetBehaviour)mImageTracker.TargetFinder.EnableTracking(result, ImageTargetTemplate);

            m_vSizeTarget = imageTargetBehaviour.ImageTarget.GetSize();

			if (result.MetaData != null)
			{
                // :: Create menu according to the xml/metadata
			}
		}
		#endregion
	}
}
