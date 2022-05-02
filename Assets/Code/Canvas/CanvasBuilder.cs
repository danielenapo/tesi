using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

/*
 * fill the canvas with different types of interfaces (for example button, data, slider, ...)
 * depending on the values obtained by the CoAP discovery
 */
public class CanvasBuilder : MonoBehaviour
{
	private Dictionary<string, GameObject> interfaces = new Dictionary<string, GameObject>();
	private ArrayList elements = new ArrayList();
	private float sizeX;
	private Dictionary<string, string> discoveryDict;
	private CoapProxy coapProxy;
	
	public GameObject data, button, slider;
	public Text labelText;
	public float offsetSize;
	public string ip;


	//CI STAREBBE FARE UN TEMPLATE/FACADE PATTERN
	/*	void Awake()
	{
		//sizeX = 0.142f;
		offsetSize /= 5;
		getInterfaces();
		setLabelText();
		instantiateInterfaces();
		this.gameObject.transform.Rotate(88, 0, 0);
		//imageManager=Camera.main.GetComponent<ARTrackedImageManager>();
		//resize();

	}
	public void initialize(float sizeX) //testing
	{ }*/
	private void Awake()
	{
		coapProxy = this.gameObject.GetComponent<CoapProxy>();
		coapProxy.setIp(ip);
	}

	public void initialize(float sizeX)
	{
		this.sizeX = sizeX;
		offsetSize /= 5;
		addInterfaces();
		setLabelText();
		instantiateInterfaces();
		resize();
	}
	// function to retrieve interfaces associated to the image (available CoAP resources), and fills the interfaces list
	// after doing a coap discovery, it associates to each actuator a button.
	public void addInterfaces()
	{
		discoveryDict = coapProxy.discover();
		elements.Add(slider);
		//Debug.Log("added interfaces [deb]");
	}

	//sets labelText valuem and changes font size according to the string length
	public void setLabelText()
	{
		labelText.text = "Coffee Machine";
		//labelText.fontSize = 11;
	}

	//function that instantiates prefabs
	//TODO: togliere elements e istanziare direttamente da discoveryDict
	public void instantiateInterfaces()
	{
		Vector3 thisPosition = this.gameObject.transform.position;
		//thisPosition.y += offsetSize;
		//float offset = offsetSize;
		foreach (GameObject element in elements)
		{
			
		}

		foreach (KeyValuePair<string, string> entry in discoveryDict)
		{
			if (entry.Value == "core.a")
			{
				GameObject newButton = Instantiate(button, thisPosition, this.gameObject.transform.rotation);
				newButton.transform.localScale = new Vector3(0.5f, 0.5f, 1) / 100;
				newButton.transform.parent = this.gameObject.transform;
				newButton.GetComponent<ButtonValue>().setUri(entry.Key.Substring(1));
				Debug.Log("SUBSTRING RESULT --> from " + entry.Key + " to " + entry.Key.Substring(1));
				thisPosition.y -= offsetSize;
			}
		}
	}

	//rerizes canvas according to the image size
	public void resize()
	{
		this.gameObject.transform.Rotate(90, 0, 0);
		//Debug.Log("(DEB) width value: " + sizeX);
		this.gameObject.transform.localScale=Vector3.Scale(this.gameObject.transform.localScale,new Vector3(sizeX, sizeX, sizeX));
		//Debug.Log("(DEB) resized! -> scale: " + this.gameObject.transform.localScale);

	}


}
