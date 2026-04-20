<div align="center">

# Maze AR Project

### An Augmented Reality Maze Game built with Unity & Vuforia

[![Unity](https://img.shields.io/badge/Unity-2021.x-black?logo=unity)](https://unity.com/)
[![Vuforia](https://img.shields.io/badge/Vuforia-Engine-blue)](https://developer.vuforia.com/)
[![Platform](https://img.shields.io/badge/Platform-Android%20%7C%20iOS-green)]()
[![Language](https://img.shields.io/badge/Language-C%23-purple)]()

</div>

---

## Overview

**Maze AR Project** is a mobile Augmented Reality game where a 3D ball physically rolls through a maze overlaid on the real world using the device camera. The experience starts with **barcode/QR code scanning** — once a code is detected, Vuforia switches to **image target tracking**, anchoring the 3D maze environment onto a printed image. The player tilts the device to guide the ball to the exit.

This project was developed as part of a **Master's degree program** in Robotics Engineering.

---

## Demo

<div align="center">

![Maze AR Demo](demo.gif)

*AR maze overlaid on a real-world image target — ball rolls via device tilt*

</div>

---

## Implementation

### System Architecture

```
┌─────────────────────────────────────────────────────┐
│                  Mobile Device (Camera)              │
│                                                     │
│  ┌──────────────┐      ┌──────────────────────────┐ │
│  │  Barcode     │ ───▶ │  Vuforia Image Target    │ │
│  │  Scanner     │      │  Tracking (Maze.jpg)     │ │
│  └──────────────┘      └──────────────────────────┘ │
│         │                          │                │
│         ▼                          ▼                │
│  ┌──────────────┐      ┌──────────────────────────┐ │
│  │ SimpleBarcodeScanner.cs │   │  3D Maze Scene   │ │
│  │  (Vuforia BarcodeBehaviour) │  (SampleScene)   │ │
│  └──────────────┘      └──────────────────────────┘ │
│                                    │                │
│                                    ▼                │
│                         ┌──────────────────────┐   │
│                         │  ballScript.cs       │   │
│                         │  (Physics + Respawn) │   │
│                         └──────────────────────┘   │
└─────────────────────────────────────────────────────┘
```

### Interaction Flow

```
App Launch
    │
    ▼
Barcode Scanner Active
    │  (scan QR / barcode)
    ▼
Barcode Detected → Display Text (TextMesh Pro)
    │
    ▼
Disable Barcode Scanner Object
Enable Image Target Object
    │
    ▼
Point Camera at Maze Image Target
    │
    ▼
3D Maze Appears Anchored to Target ──▶ Ball Rolls (device tilt / physics)
                                            │
                                     Ball falls off?
                                            │ Yes
                                            ▼
                                     Auto-Respawn at Start
```

---

## Features

| Feature | Description |
|---|---|
| Barcode / QR Scanning | Uses `Vuforia.BarcodeBehaviour` to trigger AR scene |
| Image Target Tracking | Vuforia anchors the 3D maze to a printed image |
| 3D Ball Physics | Unity Rigidbody physics rolls the ball through the maze |
| Auto-Respawn | Ball teleports back to spawn point if it falls off |
| TextMesh Pro UI | Displays scanned barcode text as in-game overlay |
| Scene Switching | Seamless swap from scanner to AR maze on detection |

---

## Tech Stack

| Technology | Version | Role |
|---|---|---|
| **Unity** | 2021.x | Game engine & scene management |
| **Vuforia Engine** | Latest | AR tracking, barcode scanning |
| **C#** | — | Game scripting |
| **TextMesh Pro** | — | UI text rendering |
| **Android / iOS** | — | Target deployment platform |

---

## Project Structure

```
Maze_AR_Project/
├── README.md
├── demo.gif                          # Animated demo preview
├── Maze_Report.pdf                   # Full project report
└── Mazee/
    └── Mazee/
        ├── Assets/
        │   ├── ballScript.cs         # Ball physics & respawn logic
        │   ├── SimpleBarcodeScanner.cs  # Barcode detection & AR switch
        │   ├── Maze.jpg              # Vuforia image target
        │   ├── Plane.mat             # Maze plane material
        │   ├── Sphere.mat            # Ball material
        │   ├── Resources/
        │   │   └── VuforiaConfiguration.asset  # Vuforia license & settings
        │   └── Scenes/
        │       └── SampleScene.unity # Main AR scene
        ├── Packages/
        │   └── manifest.json         # Unity package dependencies
        └── ProjectSettings/
```

---

## Scripts

### `ballScript.cs`

Monitors the ball's world Y-position every frame. If the ball drops more than 10 units below the maze plane (i.e., falls off the edge), it is teleported back to the designated spawn point.

```csharp
public class ballScript : MonoBehaviour
{
    public GameObject plane;
    public GameObject spawnPoint;

    void Update()
    {
        if (transform.position.y < plane.transform.position.y - 10)
        {
            transform.position = spawnPoint.transform.position;
        }
    }
}
```

### `SimpleBarcodeScanner.cs`

Uses Vuforia's `BarcodeBehaviour` component to poll for a scanned barcode each frame. On successful detection:
1. Writes the barcode string to a `TextMeshProUGUI` element.
2. Disables the barcode scanner GameObject.
3. Enables the image target GameObject to begin AR maze tracking.

```csharp
void Update()
{
    if (mBarcodeBehaviour != null && mBarcodeBehaviour.InstanceData != null)
    {
        barcodeAsText.text = mBarcodeBehaviour.InstanceData.Text;

        barcodeScannerObject.SetActive(false);   // hide scanner
        imageTargetObject.SetActive(true);        // show AR maze
    }
    else
    {
        barcodeAsText.text = "";
    }
}
```

---

## Setup & Running

### Requirements

- **Unity Hub** with Unity **2021.x** installed
- **Vuforia Engine** package (already in `PackageCache`)
- A **Vuforia Developer License Key** (free at [developer.vuforia.com](https://developer.vuforia.com/))
- **Android** device (API 24+) or **iOS** device (iOS 12+) with a camera

### Steps

1. Clone this repository:
   ```bash
   git clone https://github.com/behnoudshafizadeh/Maze_AR_Project.git
   ```
2. Open **Unity Hub** → **Open Project** → select the `Mazee/Mazee` folder.
3. In Unity, go to **File → Build Settings** and choose **Android** or **iOS**.
4. Click **Add Open Scenes** to include `SampleScene`.
5. Open `Assets/Resources/VuforiaConfiguration.asset` and paste your **Vuforia License Key**.
6. Connect your device and click **Build and Run**.

---

## Report

The full technical project report is available here: [Maze_Report.pdf](Maze_Report.pdf)

---

## Author

**Behnoud Shafizadeh**
Master's degree program — Robotics Engineering

---

## License

This project was developed as part of a Master's degree program. All rights reserved.
