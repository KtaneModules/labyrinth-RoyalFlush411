using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1MazeIndicators : MonoBehaviour
{
		public Renderer[] level1Indicators;
		public Renderer[] leftSideIndicators;
		public Renderer[] rightSideIndicators;
		public Material targetMat;
		public Material activeMat;
		public int target1 = 0;
		public int target2 = 0;
		public int startIndex = 0;
		public Renderer[] interiorWalls;
		public int level = 0;
		public labyrinthScript mainScript;

		void Start()
		{
				foreach(Renderer wall in interiorWalls)
				{
						wall.enabled = false;
				}
				StartCoroutine(delay());
		}

		IEnumerator delay()
		{
				yield return new WaitForSeconds(0.1f);
				GetLocations();
		}

		public void GetLocations()
		{
				if(mainScript.currentStage == level)
				{
						target1 = UnityEngine.Random.Range(0,leftSideIndicators.Length);
						while(leftSideIndicators[target1].GetComponent<PositionRules>().isActive)
						{
								target1 = UnityEngine.Random.Range(0,leftSideIndicators.Length);
						}
						leftSideIndicators[target1].enabled = true;
						leftSideIndicators[target1].material = targetMat;
						leftSideIndicators[target1].GetComponent<PositionRules>().isTarget = true;
						target1 = leftSideIndicators[target1].GetComponent<PositionRules>().positionIndex;

						target2 = UnityEngine.Random.Range(0,rightSideIndicators.Length);
						while(rightSideIndicators[target2].GetComponent<PositionRules>().isActive)
						{
								target2 = UnityEngine.Random.Range(0,rightSideIndicators.Length);
						}
						rightSideIndicators[target2].enabled = true;
						rightSideIndicators[target2].material = targetMat;
						rightSideIndicators[target2].GetComponent<PositionRules>().isTarget = true;
						target2 = rightSideIndicators[target2].GetComponent<PositionRules>().positionIndex;

						if(level == 1)
						{
								startIndex = UnityEngine.Random.Range(0,41);
								while(startIndex == target1 || startIndex == target2)
								{
										startIndex = UnityEngine.Random.Range(0,41);
								}
								level1Indicators[startIndex].enabled = true;
								level1Indicators[startIndex].material = activeMat;
								level1Indicators[startIndex].GetComponent<PositionRules>().isActive = true;
						}
						else
						{
								level1Indicators[mainScript.positionIndexTransfer].enabled = true;
								level1Indicators[mainScript.positionIndexTransfer].material = activeMat;
								level1Indicators[mainScript.positionIndexTransfer].GetComponent<PositionRules>().isActive = true;
						}
				}

				if(mainScript.currentStage >= 6)
				{
						level1Indicators[mainScript.positionIndexTransfer].enabled = true;
						level1Indicators[mainScript.positionIndexTransfer].material = activeMat;
						level1Indicators[mainScript.positionIndexTransfer].GetComponent<PositionRules>().isActive = true;
				}

				foreach(Renderer wall in interiorWalls)
				{
						wall.enabled = false;
				}
				if(mainScript.currentStage == level)
				{
						mainScript.LogMazeInfo();
				}
		}
}
