using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SalaryShowing : MonoBehaviour
{

    float timealive = 0;
    Vector2 deltaposition;

    Canvas objcanvas;
    Camera objcam;
    GameObject obj;
    int type;
    Text textUi;

    // Start is called before the first frame update
    void Start()
    {

    }
    public void setTextAndPosition(Canvas canvas, Camera cam, GameObject m_obj, string text, int m_type) {

        textUi = GetComponent<Text>();
        textUi.text = text;

        objcam = cam;
        objcanvas = canvas;
        obj = m_obj;
        type = m_type;

        if (type == 0)
        {
            textUi.color = new Color(0,180,255);
            deltaposition.x = 50.0f;
            deltaposition.y = 50.0f;
        }
        if (type == 1)
        {
            textUi.color = new Color(255,255,0);
            deltaposition.x = -20.0f;
            deltaposition.y = -20.0f;
        }

        Destroy(gameObject , 2.0f);
    }
    // Update is called once per frame
    void Update()
    {
        timealive += Time.deltaTime;

        if (type == 0)
        {
            deltaposition.y += (Time.deltaTime * 50.0f) * (timealive / 2.0f);
            deltaposition.x += (Time.deltaTime * 15.0f);
        }
        else if (type == 1)
        {
            deltaposition.y -= (Time.deltaTime * 50.0f) * (timealive / 2.0f);
            deltaposition.x -= (Time.deltaTime * 15.0f);
        }
        {
            Color c = textUi.color;
            c.a = 1.0f - (timealive / 2.0f);
            textUi.color = c;
        }


        if (objcam == null)
        {
            RectTransform rt = obj.GetComponent<RectTransform>();
            Vector2 ifelementposition = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y);
            RectTransform recttransform = GetComponent<RectTransform>();
            recttransform.anchoredPosition = ifelementposition + deltaposition;
        }
        else
        {
            RectTransform CanvasRect = objcanvas.GetComponent<RectTransform>();
            Vector2 ViewportPosition = objcam.WorldToViewportPoint(obj.transform.position);

            Vector2 WorldObject_ScreenPosition = new Vector2(
                ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
                ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

            RectTransform recttransform = GetComponent<RectTransform>();
            recttransform.anchoredPosition = WorldObject_ScreenPosition + deltaposition;
        }

        //RectTransform recttransform = GetComponent<RectTransform>();
        //Vector2 currentAnchored = recttransform.anchoredPosition;
        //recttransform.anchoredPosition = currentAnchored;
    }
}
