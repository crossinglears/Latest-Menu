# Tool Documentation

## Tool Name
**Latest Menu Manager**

## Overview
Latest Menu Manager is a **UI menu management system** for Unity that ensures only one menu panel is active in a category (branch) at a time. It provides an efficient way to manage UI panels dynamically while optionally hiding the main UI.

## Features
- **Only one active panel per branch at a time**
- **Main UI auto-toggle option**
- **Lightweight dictionary-based management**

## Requirements
- Unity **2021.3 or newer**
- **Canvas-based UI system**
- **EventSystem** in the scene for UI interaction

## Inspector Fields

### LatestMenuManager (Singleton)
Manages the active UI menus and ensures only one per branch remains open.

- **Main GUI** (`GameObject`)  
  The main UI element that can be disabled when a menu opens.

### AutoLatestMenu (Attached to Individual UI Panels)
Controls each UI panel that should be managed.

- **Branch** (`string`)  
  The unique identifier for a menu category. Only one panel per branch can be active.

- **Close GUI** (`bool`)  
  If enabled, the **Main GUI** will be hidden when this panel is opened.

## How to Use

### 1. Setup
1. Add `LatestMenuManager` to an empty GameObject in the scene.
2. Assign a **Main GUI** reference if needed.

### 2. Creating Managed Panels
1. Attach `AutoLatestMenu` to any UI panel to be managed.
2. Set a **Branch name** to define its category.
3. Enable **Close GUI** if opening the panel should hide the main UI.

### 3. Controlling Menus via Code
You can programmatically open or close menus:

// Open a panel in a specific branch
LatestMenuManager.Instance.SetCurrentActive("Settings", settingsPanel, true);

// Close a branch and restore the main GUI if no other panels are open
LatestMenuManager.Instance.CloseBranch("Settings", true);

## Editor Tools
Auto Cleanup: Detects and removes duplicate LatestMenuManager instances.
Automatic Initialization: Ensures singleton setup is correct.

## Notes
Only one panel per branch can be active at a time.
Closing a panel restores the Main UI if no other panels remain open.
No need to manually toggle panels—it's handled automatically.

## Contact
For support, contact the publisher.
