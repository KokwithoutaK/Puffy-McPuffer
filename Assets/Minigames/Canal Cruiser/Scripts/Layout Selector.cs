using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutSelector : MonoBehaviour
{
    [SerializeField]
    int levelIndex;

    [SerializeField]
    GameObject animationObject;
    GameObject Puffy;

    float puffyStart = -17;
    float moveSpeed = 0.05f;

    [SerializeField, Range(0.1f, 2)]
    float CinimaticSeconds;

    GameObject CinamaticBars;

    enum startAnimationState { DriveOnScreen, JumpBack, GameStart }
    [SerializeField]startAnimationState Animation;

    private void Start()
    {

        Physics2D.gravity = new Vector2(-9.81f, 0); // Left (behind player)
        for (int i = 0; i < transform.childCount; i++) transform.GetChild(i).gameObject.SetActive(false);
        transform.GetChild(levelIndex).gameObject.SetActive(true);


        // animation
        if (levelIndex != 0) this.enabled = false;

        Instantiate(animationObject, new Vector2(-9,0), Quaternion.Euler(0, 0, -90));

        Puffy = GameObject.Find("Player");
        Puffy.GetComponent<PlayerScript>().enabled = false;
        Puffy.transform.position = new Vector2(puffyStart, 0);

        GameObject Cargo = GameObject.Find("Cargo Objects").transform.GetChild(0).gameObject; 
        Cargo.transform.position = new Vector2(puffyStart - 2, 0); // moves cargo behind puffy

        GameObject.Find("Game Manager").GetComponent<ScoreScript>().enabled = false;

        CinamaticBars = GameObject.Find("Cinematic Bars");
    }

    private void FixedUpdate()
    {
        switch (Animation)
        {
            case startAnimationState.DriveOnScreen:
                {
                    if (Puffy.transform.position.x > -10f)
                    {
                        GameObject.Find("Game Manager").GetComponent<ScoreScript>().score--;
                        Animation = startAnimationState.JumpBack;
                    }
                    else Puffy.transform.Translate(new Vector2(0, moveSpeed));

                }
                break;
            case startAnimationState.JumpBack:
                {
                    if (Puffy.transform.position.x < -11) Animation = startAnimationState.GameStart;
                    else Puffy.transform.Translate(new Vector2(0, -moveSpeed * 2));
                }
                break;
            case startAnimationState.GameStart:
                {
                    Puffy.transform.position = new Vector2(-11, 0);
                    GameObject.Find("Game Manager").GetComponent<ScoreScript>().enabled = true;
                    Puffy.GetComponent<PlayerScript>().enabled = true;

                    StartCoroutine(FadeOutBars());

                    this.enabled = false;
                }
                break;
        }
    }

    IEnumerator FadeOutBars()
    {
        float smootheness = 100;
        for (int i = 0; i < smootheness; i++)
        {
            yield return new WaitForSeconds(CinimaticSeconds / smootheness);
            float scale = Mathf.Lerp(0, 2 / CinamaticBars.transform.GetChild(0).GetComponent<RectTransform>().rect.height, i / smootheness);

            CinamaticBars.transform.localScale += new Vector3(0, scale, 0);
        }
    }
}
