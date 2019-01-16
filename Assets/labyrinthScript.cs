using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;

public class labyrinthScript : MonoBehaviour
{
    public KMBombInfo Bomb;
    public KMAudio Audio;
    public KMSelectable moveLeft;
    public KMSelectable moveRight;
    public KMSelectable moveUp;
    public KMSelectable moveDown;
    public GameObject[] controllerButtons;
    public GameObject wallOutlines;

    public int currentStage = 1;

    public Level1MazeIndicators level1Info;
    public Level1MazeIndicators level2Info;
    public Level1MazeIndicators level3Info;
    public Level1MazeIndicators level4Info;
    public Level1MazeIndicators level5Info;

    public Renderer[] exteriorWalls;
    public Material[] exteriorMaterials;

    private Renderer currentActive;
    private bool arrived;
    public int positionIndexTransfer = 0;
    private bool flashing;
    private Level1MazeIndicators currentLevelInfo;

    //Logging
    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    void Awake()
    {
        moduleId = moduleIdCounter++;
        moveLeft.OnInteract += delegate () { PressLeft(); return false; };
        moveRight.OnInteract += delegate () { PressRight(); return false; };
        moveUp.OnInteract += delegate () { PressUp(); return false; };
        moveDown.OnInteract += delegate () { PressDown(); return false; };
    }

    void Start()
    {
        foreach(Renderer wall in exteriorWalls)
        {
            wall.material = exteriorMaterials[0];
        }
        currentLevelInfo = level1Info;
        foreach(GameObject button in controllerButtons)
        {
            button.GetComponent<Renderer>().material = exteriorMaterials[currentLevelInfo.level-1];
        }
    }

    public void LogMazeInfo()
    {
        if(currentStage == 1)
        {
            Debug.LogFormat("[The Labyrinth #{0}] Layer {1}: your portals are at {2} & {3}. You are starting at {4}.", moduleId, currentStage, currentLevelInfo.level1Indicators[currentLevelInfo.target1].name, currentLevelInfo.level1Indicators[currentLevelInfo.target2].name, currentLevelInfo.level1Indicators[currentLevelInfo.startIndex].name);
        }
        else
        {
            Debug.LogFormat("[The Labyrinth #{0}] Layer {1}: your portals are at {2} & {3}. You are starting at {4}.", moduleId, currentStage, currentLevelInfo.level1Indicators[currentLevelInfo.target1].name, currentLevelInfo.level1Indicators[currentLevelInfo.target2].name, currentLevelInfo.level1Indicators[positionIndexTransfer].name);
        }

    }

    void CheckForTarget()
    {
        if(currentActive.GetComponent<PositionRules>().isActive && currentActive.GetComponent<PositionRules>().isTarget)
        {
            currentStage++;
            arrived = true;
            Audio.PlaySoundAtTransform("portal", transform);
        }
        if(arrived)
        {
            if(currentStage == 2 || currentStage == 8)
            {
                for(int i = 0; i <= 40; i++)
                {
                    level1Info.level1Indicators[i].enabled = false;
                    level3Info.level1Indicators[i].enabled = false;
                }
                foreach(Renderer wall in exteriorWalls)
                {
                    wall.material = exteriorMaterials[1];
                }
                Debug.LogFormat("[The Labyrinth #{0}] Portal entered. Arriving at layer 2.", moduleId);
                positionIndexTransfer = currentActive.GetComponent<PositionRules>().positionIndex;
                level2Info.level1Indicators[positionIndexTransfer].GetComponent<PositionRules>().isActive = true;
                currentLevelInfo = level2Info;
                level2Info.GetLocations();
                currentActive.GetComponent<PositionRules>().isActive = false;
                arrived = false;
            }
            else if(currentStage == 3 || currentStage == 7)
            {
                for(int i = 0; i <= 40; i++)
                {
                    level2Info.level1Indicators[i].enabled = false;
                    level4Info.level1Indicators[i].enabled = false;
                }
                foreach(Renderer wall in exteriorWalls)
                {
                    wall.material = exteriorMaterials[2];
                }
                Debug.LogFormat("[The Labyrinth #{0}] Portal entered. Arriving at layer 3.", moduleId);
                positionIndexTransfer = currentActive.GetComponent<PositionRules>().positionIndex;
                level3Info.level1Indicators[positionIndexTransfer].GetComponent<PositionRules>().isActive = true;
                currentLevelInfo = level3Info;
                level3Info.GetLocations();
                currentActive.GetComponent<PositionRules>().isActive = false;
                arrived = false;
            }
            else if(currentStage == 4 || currentStage == 6)
            {
                for(int i = 0; i <= 40; i++)
                {
                    level3Info.level1Indicators[i].enabled = false;
                    level5Info.level1Indicators[i].enabled = false;
                }
                foreach(Renderer wall in exteriorWalls)
                {
                    wall.material = exteriorMaterials[3];
                }
                Debug.LogFormat("[The Labyrinth #{0}] Portal entered. Arriving at layer 4.", moduleId);
                positionIndexTransfer = currentActive.GetComponent<PositionRules>().positionIndex;
                level4Info.level1Indicators[positionIndexTransfer].GetComponent<PositionRules>().isActive = true;
                currentLevelInfo = level4Info;
                level4Info.GetLocations();
                currentActive.GetComponent<PositionRules>().isActive = false;
                arrived = false;
            }
            else if(currentStage == 5)
            {
                for(int i = 0; i <= 40; i++)
                {
                    level4Info.level1Indicators[i].enabled = false;
                }
                foreach(Renderer wall in exteriorWalls)
                {
                    wall.material = exteriorMaterials[4];
                }
                Debug.LogFormat("[The Labyrinth #{0}] Portal entered. Arriving at layer 5.", moduleId);
                positionIndexTransfer = currentActive.GetComponent<PositionRules>().positionIndex;
                level5Info.level1Indicators[positionIndexTransfer].GetComponent<PositionRules>().isActive = true;
                currentLevelInfo = level5Info;
                level5Info.GetLocations();
                currentActive.GetComponent<PositionRules>().isActive = false;
                arrived = false;
            }
            else if(currentStage == 9)
            {
                for(int i = 0; i <= 40; i++)
                {
                    level2Info.level1Indicators[i].enabled = false;
                }
                foreach(Renderer wall in exteriorWalls)
                {
                    wall.material = exteriorMaterials[0];
                }
                Debug.LogFormat("[The Labyrinth #{0}] Portal entered. Arriving at layer 1.", moduleId);
                positionIndexTransfer = currentActive.GetComponent<PositionRules>().positionIndex;
                level1Info.level1Indicators[positionIndexTransfer].GetComponent<PositionRules>().isActive = true;
                currentLevelInfo = level1Info;
                level1Info.GetLocations();
                currentActive.GetComponent<PositionRules>().isActive = false;
                arrived = false;
            }
            else if(currentStage == 10)
            {
                for(int i = 0; i <= 40; i++)
                {
                    level1Info.level1Indicators[i].enabled = false;
                }
                foreach(Renderer wall in exteriorWalls)
                {
                    wall.enabled = false;
                }
                wallOutlines.SetActive(false);
                moduleSolved = true;
                GetComponent<KMBombModule>().HandlePass();
                Debug.LogFormat("[The Labyrinth #{0}] Labyrinth solved. Module disarmed.", moduleId);
                foreach(GameObject button in controllerButtons)
                {
                    button.SetActive(false);
                }
                return;
            }
        }
        foreach(GameObject button in controllerButtons)
        {
            button.GetComponent<Renderer>().material = exteriorMaterials[currentLevelInfo.level-1];
        }
    }

    public void PressLeft()
    {
        if(moduleSolved || flashing)
        {
            return;
        }
        moveLeft.AddInteractionPunch();
        if(currentStage == 1 || currentStage == 9)
        {
            currentLevelInfo = level1Info;
            foreach(Renderer indicator in level1Info.level1Indicators)
            {
                if(indicator.GetComponent<PositionRules>().isActive)
                {
                    currentActive = indicator;
                }
            }
        }
        else if(currentStage == 2 || currentStage == 8)
        {
            currentLevelInfo = level2Info;
            foreach(Renderer indicator in level2Info.level1Indicators)
            {
                if(indicator.GetComponent<PositionRules>().isActive)
                {
                    currentActive = indicator;
                }
            }
        }
        else if(currentStage == 3 || currentStage == 7)
        {
            currentLevelInfo = level3Info;
            foreach(Renderer indicator in level3Info.level1Indicators)
            {
                if(indicator.GetComponent<PositionRules>().isActive)
                {
                    currentActive = indicator;
                }
            }
        }
        else if(currentStage == 4 || currentStage == 6)
        {
            currentLevelInfo = level4Info;
            foreach(Renderer indicator in level4Info.level1Indicators)
            {
                if(indicator.GetComponent<PositionRules>().isActive)
                {
                    currentActive = indicator;
                }
            }
        }
        else if(currentStage == 5)
        {
            currentLevelInfo = level5Info;
            foreach(Renderer indicator in level5Info.level1Indicators)
            {
                if(indicator.GetComponent<PositionRules>().isActive)
                {
                    currentActive = indicator;
                }
            }
        }

        if(!currentActive.GetComponent<PositionRules>().canMoveLeft)
        {
            GetComponent<KMBombModule>().HandleStrike();
            Debug.LogFormat("[The Labyrinth #{0}] Strike! You tried to move left from position {1} in layer {2}. That is not allowed.", moduleId, currentActive.name, currentLevelInfo.level);
            flashing = true;
            if(currentStage <= 5)
            {
                StartCoroutine(strikeFlash());
            }
            else
            {
                StartCoroutine(portalFlash());
            }
        }
        else
        {
            Audio.PlaySoundAtTransform("move", transform);
            currentActive.GetComponent<PositionRules>().toLeft.enabled = true;
            currentActive.GetComponent<PositionRules>().toLeft.GetComponent<PositionRules>().isActive = true;
            currentActive.GetComponent<PositionRules>().isActive = false;
            currentActive.enabled = false;
            currentActive = currentActive.GetComponent<PositionRules>().toLeft;
            currentActive.material = level1Info.activeMat;
        }
        CheckForTarget();

    }

    public void PressRight()
    {
        if(moduleSolved || flashing)
        {
            return;
        }
        moveRight.AddInteractionPunch();
        if(currentStage == 1 || currentStage == 9)
        {
            currentLevelInfo = level1Info;
            foreach(Renderer indicator in level1Info.level1Indicators)
            {
                if(indicator.GetComponent<PositionRules>().isActive)
                {
                    currentActive = indicator;
                }
            }
        }
        else if(currentStage == 2 || currentStage == 8)
        {
            currentLevelInfo = level2Info;
            foreach(Renderer indicator in level2Info.level1Indicators)
            {
                if(indicator.GetComponent<PositionRules>().isActive)
                {
                    currentActive = indicator;
                }
            }
        }
        else if(currentStage == 3 || currentStage == 7)
        {
            currentLevelInfo = level3Info;
            foreach(Renderer indicator in level3Info.level1Indicators)
            {
                if(indicator.GetComponent<PositionRules>().isActive)
                {
                    currentActive = indicator;
                }
            }
        }
        else if(currentStage == 4 || currentStage == 6)
        {
            currentLevelInfo = level4Info;
            foreach(Renderer indicator in level4Info.level1Indicators)
            {
                if(indicator.GetComponent<PositionRules>().isActive)
                {
                    currentActive = indicator;
                }
            }
        }
        else if(currentStage == 5)
        {
            currentLevelInfo = level5Info;
            foreach(Renderer indicator in level5Info.level1Indicators)
            {
                if(indicator.GetComponent<PositionRules>().isActive)
                {
                    currentActive = indicator;
                }
            }
        }

        if(!currentActive.GetComponent<PositionRules>().canMoveRight)
        {
            GetComponent<KMBombModule>().HandleStrike();
            Debug.LogFormat("[The Labyrinth #{0}] Strike! You tried to move right from position {1} in layer {2}. That is not allowed.", moduleId, currentActive.name, currentLevelInfo.level);
            flashing = true;
            if(currentStage <= 5)
            {
                StartCoroutine(strikeFlash());
            }
            else
            {
                StartCoroutine(portalFlash());
            }
        }
        else
        {
            Audio.PlaySoundAtTransform("move", transform);
            currentActive.GetComponent<PositionRules>().toRight.enabled = true;
            currentActive.GetComponent<PositionRules>().toRight.GetComponent<PositionRules>().isActive = true;
            currentActive.GetComponent<PositionRules>().isActive = false;
            currentActive.enabled = false;
            currentActive = currentActive.GetComponent<PositionRules>().toRight;
            currentActive.material = level1Info.activeMat;
        }
        CheckForTarget();
    }

    public void PressUp()
    {
        if(moduleSolved || flashing)
        {
            return;
        }
        moveUp.AddInteractionPunch();
        if(currentStage == 1 || currentStage == 9)
        {
            currentLevelInfo = level1Info;
            foreach(Renderer indicator in level1Info.level1Indicators)
            {
                if(indicator.GetComponent<PositionRules>().isActive)
                {
                    currentActive = indicator;
                }
            }
        }
        else if(currentStage == 2 || currentStage == 8)
        {
            currentLevelInfo = level2Info;
            foreach(Renderer indicator in level2Info.level1Indicators)
            {
                if(indicator.GetComponent<PositionRules>().isActive)
                {
                    currentActive = indicator;
                }
            }
        }
        else if(currentStage == 3 || currentStage == 7)
        {
            currentLevelInfo = level3Info;
            foreach(Renderer indicator in level3Info.level1Indicators)
            {
                if(indicator.GetComponent<PositionRules>().isActive)
                {
                    currentActive = indicator;
                }
            }
        }
        else if(currentStage == 4 || currentStage == 6)
        {
            currentLevelInfo = level4Info;
            foreach(Renderer indicator in level4Info.level1Indicators)
            {
                if(indicator.GetComponent<PositionRules>().isActive)
                {
                    currentActive = indicator;
                }
            }
        }
        else if(currentStage == 5)
        {
            currentLevelInfo = level5Info;
            foreach(Renderer indicator in level5Info.level1Indicators)
            {
                if(indicator.GetComponent<PositionRules>().isActive)
                {
                    currentActive = indicator;
                }
            }
        }

        if(!currentActive.GetComponent<PositionRules>().canMoveUp)
        {
            GetComponent<KMBombModule>().HandleStrike();
            Debug.LogFormat("[The Labyrinth #{0}] Strike! You tried to move up from position {1} in layer {2}. That is not allowed.", moduleId, currentActive.name, currentLevelInfo.level);
            flashing = true;
            if(currentStage <= 5)
            {
                StartCoroutine(strikeFlash());
            }
            else
            {
                StartCoroutine(portalFlash());
            }
        }
        else
        {
            Audio.PlaySoundAtTransform("move", transform);
            currentActive.GetComponent<PositionRules>().toUp.enabled = true;
            currentActive.GetComponent<PositionRules>().toUp.GetComponent<PositionRules>().isActive = true;
            currentActive.GetComponent<PositionRules>().isActive = false;
            currentActive.enabled = false;
            currentActive = currentActive.GetComponent<PositionRules>().toUp;
            currentActive.material = level1Info.activeMat;
        }
        CheckForTarget();
    }

    public void PressDown()
    {
        if(moduleSolved || flashing)
        {
            return;
        }
        moveDown.AddInteractionPunch();
        if(currentStage == 1 || currentStage == 9)
        {
            currentLevelInfo = level1Info;
            foreach(Renderer indicator in level1Info.level1Indicators)
            {
                if(indicator.GetComponent<PositionRules>().isActive)
                {
                    currentActive = indicator;
                }
            }
        }
        else if(currentStage == 2 || currentStage == 8)
        {
            currentLevelInfo = level2Info;
            foreach(Renderer indicator in level2Info.level1Indicators)
            {
                if(indicator.GetComponent<PositionRules>().isActive)
                {
                    currentActive = indicator;
                }
            }
        }
        else if(currentStage == 3 || currentStage == 7)
        {
            currentLevelInfo = level3Info;
            foreach(Renderer indicator in level3Info.level1Indicators)
            {
                if(indicator.GetComponent<PositionRules>().isActive)
                {
                    currentActive = indicator;
                }
            }
        }
        else if(currentStage == 4 || currentStage == 6)
        {
            currentLevelInfo = level4Info;
            foreach(Renderer indicator in level4Info.level1Indicators)
            {
                if(indicator.GetComponent<PositionRules>().isActive)
                {
                    currentActive = indicator;
                }
            }
        }
        else if(currentStage == 5)
        {
            currentLevelInfo = level5Info;
            foreach(Renderer indicator in level5Info.level1Indicators)
            {
                if(indicator.GetComponent<PositionRules>().isActive)
                {
                    currentActive = indicator;
                }
            }
        }

        if(!currentActive.GetComponent<PositionRules>().canMoveDown)
        {
            GetComponent<KMBombModule>().HandleStrike();
            Debug.LogFormat("[The Labyrinth #{0}] Strike! You tried to move down from position {1} in layer {2}. That is not allowed.", moduleId, currentActive.name, currentLevelInfo.level);
            flashing = true;
            if(currentStage <= 5)
            {
                StartCoroutine(strikeFlash());
            }
            else
            {
                StartCoroutine(portalFlash());
            }
        }
        else
        {
            Audio.PlaySoundAtTransform("move", transform);
            currentActive.GetComponent<PositionRules>().toDown.enabled = true;
            currentActive.GetComponent<PositionRules>().toDown.GetComponent<PositionRules>().isActive = true;
            currentActive.GetComponent<PositionRules>().isActive = false;
            currentActive.enabled = false;
            currentActive = currentActive.GetComponent<PositionRules>().toDown;
            currentActive.material = level1Info.activeMat;
        }
        CheckForTarget();
    }

    IEnumerator strikeFlash()
    {
        Audio.PlaySoundAtTransform("strike", transform);
        foreach(GameObject button in controllerButtons)
        {
            button.SetActive(false);
        }
        int flashCounter = 0;
        while(flashCounter < 15)
        {
            foreach(Renderer wall in currentLevelInfo.interiorWalls)
            {
                wall.material = exteriorMaterials[currentLevelInfo.level-1];
                wall.enabled = true;
            }
            yield return new WaitForSeconds(0.05f);

            foreach(Renderer wall in currentLevelInfo.interiorWalls)
            {
                wall.enabled = false;
            }
            yield return new WaitForSeconds(0.05f);
            flashCounter++;
        }
        foreach(GameObject button in controllerButtons)
        {
            button.SetActive(true);
        }
        flashing = false;
    }

    IEnumerator portalFlash()
    {
        Audio.PlaySoundAtTransform("strike", transform);
        foreach(GameObject button in controllerButtons)
        {
            button.SetActive(false);
        }
        int flashCounter = 0;
        while(flashCounter < 15)
        {
            currentLevelInfo.level1Indicators[currentLevelInfo.target1].material = currentLevelInfo.targetMat;
            currentLevelInfo.level1Indicators[currentLevelInfo.target1].enabled = true;
            currentLevelInfo.level1Indicators[currentLevelInfo.target2].material = currentLevelInfo.targetMat;
            currentLevelInfo.level1Indicators[currentLevelInfo.target2].enabled = true;
            foreach(Renderer wall in currentLevelInfo.interiorWalls)
            {
                wall.material = exteriorMaterials[currentLevelInfo.level-1];
                wall.enabled = true;
            }
            yield return new WaitForSeconds(0.05f);

            currentLevelInfo.level1Indicators[currentLevelInfo.target1].enabled = false;
            currentLevelInfo.level1Indicators[currentLevelInfo.target2].enabled = false;
            foreach(Renderer wall in currentLevelInfo.interiorWalls)
            {
                wall.enabled = false;
            }
            yield return new WaitForSeconds(0.05f);
            flashCounter++;
        }
        foreach(GameObject button in controllerButtons)
        {
            button.SetActive(true);
        }
        flashing = false;
    }
}
