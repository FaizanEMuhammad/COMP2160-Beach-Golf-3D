# 🏖️ Isometric Beachball Golf

A 3D physics-based golf puzzle game developed in **Unity**. In this game, players navigate varied terrain to line up the perfect shot, aiming to kick a bouncing beachball through a goal ring in as few attempts as possible. 

This project was built collaboratively to demonstrate core gameplay programming, physics interactions, and professional software engineering practices including QA testing, Data Management Planning, and Git feature-branching.

![Project Banner / Gameplay GIF Placeholder]()
*(Recommend adding a GIF here showing the trajectory line and the ball being kicked into the goal)*

---

## 🛠️ Tech Stack
* **Engine:** Unity (2021.3.17f1)
* **Language:** C#
* **Architecture:** Prefab-based scene construction, Entity Relationship mapping

---

## 🚀 Core Features & Systems Implemented

### 🏃 Custom Character Controller & Animation
* **Physics-Aware Movement:** The player avatar smoothly rotates to face movement direction, remains grounded, navigates up/down ramps, and smoothly collides with obstacles without falling off map edges.
* **State-Driven Animation:** Integrated a humanoid rig with seamless state transitions between Idle, Walk, Run (Sprint modifier), Interact (Grabbing), and Cheer (Goal completion).

### ⚽ Physics-Based Kicking & Trajectory System
* **Grab & Aim Mechanics:** Players enter a stationary "grab mode" to lock onto the ball and use WSAD to control the pitch and yaw of the kick.
* **Parabolic Projection:** A dynamically calculated dotted trajectory line visualizes the expected path of the ball prior to shooting.
* **Physics Application:** Releasing the action key applies a calculated physical impulse to the Rigidbody beachball, allowing it to bounce and roll realistically across the environment.

### 🎥 Dynamic Camera Management
* **Isometric Perspective:** The orthographic camera operates at a 45° angle on a 16:10 aspect ratio.
* **Cinematic Transitions:** The camera smoothly interpolates between a player-following state during traversal and an extended "Look-Ahead" state while aiming a kick.

### 📊 Level Design & UI Flow
* **Grid-Based Maps:** Implemented 3 distinct levels featuring varying heights (low, medium, high) and environmental obstacles.
* **Game Loop UI:** Integrated custom fonts for Level Start dialogs (displaying par requirements) and End dialogs (calculating kicks taken vs. par, and overall score).

---

## 📋 Software Engineering & Production Practices
Beyond gameplay programming, this project heavily emphasized industry-standard development workflows:
* **Version Control:** Utilized a Git feature-branching workflow (`main` and `develop` branches) with `.gitignore` and Git LFS. Scene merging conflicts were mitigated via strict prefab-based construction.
* **Quality Assurance (QA):** Developed a comprehensive QA plan tracking bug severity, reproduction steps, and expected vs. actual outcomes.
* **Data Management Plan (DMP):** Designed an ethical telemetry framework to analyze player retention, difficulty spikes, and positional heatmaps while adhering to GDPR and ACS privacy standards.

---

## 🎮 How to Play
1. Clone the repository.
2. Open the project in Unity `2021.3.17f1`.
3. Open `Level 1` in the Scenes folder and press **Play**.
4. **Controls:**
   * `W` `A` `S` `D` to Move.
   * `Shift` to Sprint.
   * Hold `Space` near the ball to enter Grab/Aim mode.
   * Use `W` `A` `S` `D` while aiming to adjust kick trajectory.
   * Release `Space` to Kick.

---

## 👥 Credits & Licensing
* **Developers:** Faizan E Muhammad & Samuel Constantine
* **Assets Used:**
  * [KayKit: Mini-Game Variety Pack](https://kaylousberg.itch.io/kay-kit-mini-game-variety-pack) (CC0)
  * [KayKit: Character Animations](https://kaylousberg.itch.io/kaykit-animations) (CC0)
  * [Toony UI Icons Asset Pack](https://josie-makes-stuff.itch.io/toony-ui-icons-asset-pack) by Josie Wood (CC-BY-SA 4.0)
  * ModularBlocks by Chris Lee (CC0)
