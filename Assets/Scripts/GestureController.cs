using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace PDollarGestureRecognizer
{
	public class GestureController : MonoBehaviour
	{
		[Header ("Gesture Prefab")]
		public Transform gestureOnScreenPrefab;

		[Header ("Player Controller")]
		public PlayerController playerController;

		private List<Gesture> trainingSet = new List<Gesture>();
		private List<Point> points = new List<Point>();
		private int strokeId = -1;
		private Vector3 virtualKeyPosition = Vector2.zero;
		private Rect drawArea;
		private int vertexCount = 0;
		private List<LineRenderer> gestureLinesRenderer = new List<LineRenderer>();
		private LineRenderer currentGestureLineRenderer;
		private bool recognized;

		void Start()
		{
			drawArea = new Rect(0, 0, Screen.width, Screen.height);

			TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/10-stylus-MEDIUM/");
			foreach (TextAsset gestureXml in gesturesXml)
			trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));

		}

		void Update()
		{
			if (playerController.drawMode == true)
			{
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
					if (recognized)
					{
						recognized = false;
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
					recognized = true;

					Gesture candidate = new Gesture(points.ToArray());
					Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

					Debug.Log(gestureResult.GestureClass + " " + gestureResult.Score);

					points.Clear();

						foreach (LineRenderer lineRenderer in gestureLinesRenderer)
						{
							lineRenderer.positionCount = 0;
							Destroy(lineRenderer.gameObject);
						}

					gestureLinesRenderer.Clear();

					playerController.drawMode = false;
				}
			}
		}
	}
}