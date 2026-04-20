using UnityEngine;
using Vuforia;

public class SimpleBarcodeScanner : MonoBehaviour
{
    public TMPro.TextMeshProUGUI barcodeAsText;
    public GameObject barcodeScannerObject;
    public GameObject imageTargetObject;

    BarcodeBehaviour mBarcodeBehaviour;

    void Start()
    {
        mBarcodeBehaviour = GetComponent<BarcodeBehaviour>();
        if (mBarcodeBehaviour == null)
        {
        }
        else
        {
        }

        // Initially disable the image target
        if (imageTargetObject == null)
        {
        }
        else
        {
            imageTargetObject.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update method called"); // Add this line
        if (mBarcodeBehaviour != null && mBarcodeBehaviour.InstanceData != null)
        {
            barcodeAsText.text = mBarcodeBehaviour.InstanceData.Text;
            Debug.Log("Barcode detected"); // Add this line

            // Disable the barcode scanner object
            barcodeScannerObject.SetActive(false);

            // Enable the image target object
            imageTargetObject.SetActive(true);
        }
        else
        {
            Debug.Log("Barcode not detected"); // Add this line
            barcodeAsText.text = "";
        }
    }

}
