using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace PDollarGestureRecognizer
{
	public class GestureController : MonoBehaviour
	{
		[Header ("Gesture Prefab")]
		[SerializeField] private Transform gestureOnScreenPrefab;

		[Header ("Public Gesture Results")]
		public string finalGestureResult;
		public float finalGestureScore;
		
		[Header ("Gesture State")]
		public bool isDrawModeOn;
		public bool isCheckingResult;
		public float waitResultTime;

		[Header ("Overlay State")]
		[SerializeField] private GameObject pauseOverlay;
		[SerializeField] private float pauseFadeMultiplier;

		private PlayerController playerController;
		private List<Gesture> trainingSet = new List<Gesture>();
		private List<Point> points = new List<Point>();
		private int strokeId = -1;
		private Vector3 virtualKeyPosition = Vector2.zero;
		private Rect drawArea;
		private int vertexCount = 0;
		private List<LineRenderer> gestureLinesRenderer = new List<LineRenderer>();
		private LineRenderer currentGestureLineRenderer;
		private bool isRecognized;
		private Image pauseOverlayImage;
		

		void Start()
		{
			playerController = GetComponent<PlayerController>();
			pauseOverlayImage = pauseOverlay.GetComponent<Image>();

			drawArea = new Rect(0, 0, Screen.width, Screen.height);

			TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/");
			foreach (TextAsset gestureXml in gesturesXml)
			trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));
		}

		void Update()
		{
			isDrawModeOn = playerController.drawMode;

			if (isDrawModeOn)
			{
				FadeIn();
				GestureRecognizer();
			}
		}

		private void GestureRecognizer()
		{
			if (playerController.primaryMouseAction.IsPressed())
			{
				virtualKeyPosition = Pointer.current.position.ReadValue();
			}

			if (drawArea.Contains(virtualKeyPosition))
			{
				if (playerController.primaryMouseAction.WasPressedThisFrame())
				{
					if (isRecognized)
					{
						isRecognized = false;
						strokeId = -1;
					}

					++strokeId;

					Transform tmpGesture = Instantiate(gestureOnScreenPrefab, transform.position, transform.rotation) as Transform;
					currentGestureLineRenderer = tmpGesture.GetComponent<LineRenderer>();

					gestureLinesRenderer.Add(currentGestureLineRenderer);

					vertexCount = 0;
				}

				if (playerController.primaryMouseAction.IsPressed())
				{
					points.Add(new Point(virtualKeyPosition.x, -virtualKeyPosition.y, strokeId));

					currentGestureLineRenderer.positionCount = ++vertexCount;
					currentGestureLineRenderer.SetPosition(vertexCount - 1, Camera.main.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
				}

				if (playerController.secondaryMouseAction.WasPressedThisFrame())
				{	
					isCheckingResult = true;
					isRecognized = true;

					Gesture candidate = new Gesture(points.ToArray());
					Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

					Debug.Log(gestureResult.GestureClass + " " + gestureResult.Score);

					finalGestureResult = gestureResult.GestureClass;
					finalGestureScore = gestureResult.Score;

					points.Clear();

						foreach (LineRenderer lineRenderer in gestureLinesRenderer)
						{
							lineRenderer.positionCount = 0;
							Destroy(lineRenderer.gameObject);
						}

					gestureLinesRenderer.Clear();
					FadeOut();

					playerController.DisableDrawMode();
					StartCoroutine("FinalResultTimer");
				}
			}
		}

		private IEnumerator FinalResultTimer()
		{
			yield return new WaitForSeconds(waitResultTime);
			isCheckingResult = false;
		}

		private void FadeIn()
		{
			if (pauseOverlayImage.color.a < 0.35f)
			{
				pauseOverlayImage.color = new Color (pauseOverlayImage.color.r, pauseOverlayImage.color.g, pauseOverlayImage.color.b, pauseOverlayImage.color.a + 0.1f * pauseFadeMultiplier * Time.unscaledDeltaTime);
			}
		}

		private void FadeOut()
		{
			if (pauseOverlayImage.color.a > 0f)
			{
				pauseOverlayImage.color = new Color (pauseOverlayImage.color.r, pauseOverlayImage.color.g, pauseOverlayImage.color.b, pauseOverlayImage.color.a - 0.1f * pauseFadeMultiplier * Time.unscaledDeltaTime);
			}
		}

	}
}