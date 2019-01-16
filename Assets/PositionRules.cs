using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRules : MonoBehaviour
{
		public Renderer location;
		public bool isActive;
		public bool isTarget;
		public bool canMoveUp;
		public bool canMoveLeft;
		public bool canMoveRight;
		public bool canMoveDown;
		public Renderer toUp;
		public Renderer toLeft;
		public Renderer toRight;
		public Renderer toDown;
		public int positionIndex;
		public Level1MazeIndicators mazeData;

		void Start()
		{
				for(int i = 0; i <= 40; i++)
				{
						mazeData.level1Indicators[i].GetComponent<PositionRules>().positionIndex = i;
						mazeData.level1Indicators[i].enabled = false;
				}
				location = GetComponent<Renderer>();
				if(!canMoveUp)
				{
						toUp = location;
				}
				else if(positionIndex >= 5 && positionIndex < 10)
				{
						toUp = mazeData.level1Indicators[positionIndex - 5];
				}
				else
				{
						toUp = mazeData.level1Indicators[positionIndex - 6];
				}

				if(!canMoveLeft)
				{
						toLeft = location;
				}
				else
				{
						toLeft = mazeData.level1Indicators[positionIndex - 1];
				}

				if(!canMoveRight)
				{
						toRight = location;
				}
				else
				{
						toRight = mazeData.level1Indicators[positionIndex + 1];
				}

				if(!canMoveDown)
				{
						toDown = location;
				}
				else if(positionIndex >= 0 && positionIndex < 5)
				{
						toDown = mazeData.level1Indicators[positionIndex + 5];
				}
				else
				{
						toDown = mazeData.level1Indicators[positionIndex + 6];
				}
		}
}
