# Random Block Master

**Random Block Master** is an archived Android puzzle/strategy game that I developed as a solo hobby project with Unity and C# and previously released on Google Play.

The project combines grid-based puzzle mechanics with randomized blocks, abilities, progression and score-driven game modes. It started as my first larger personal Android game project and became an opportunity to work across **game design, gameplay programming, UI/UX, cloud services and mobile release workflows**.

> **Portfolio note:** This repository is a curated source-code showcase of the original project. It is intentionally **not a complete buildable copy** of the historical Unity project. Third-party assets, production credentials and backend configuration are excluded.

## Project Highlights

- Solo-developed mobile game from concept to Google Play release
- Grid-based puzzle gameplay with multiple game modes
- Blocks with different rarities, stats and gameplay abilities
- Combo, scoring, progression and upgrade systems
- Boss/wave mechanics for the more combat-oriented game mode
- ScriptableObject-based game and content data
- Event-driven communication through a ScriptableObject game channel
- Cloud save, authentication and online leaderboards
- Firebase Realtime Database integration
- Android release and monetization experience
- Experimental multiplayer/netcode work during development

## Game Design

The original concept was inspired by the accessibility of grid-based puzzle games and the variety/progression of randomized ability systems.

Two of the main modes were:

- **Zen** — score-focused puzzle gameplay
- **Takedown** — a more progression- and combat-oriented mode with block power, bosses and waves

A central part of the design was the block system. Blocks could have different rarities and abilities, allowing the puzzle layer to interact with progression and combat mechanics rather than relying only on shape placement.

## My Role

This was a **solo personal project**, so I worked across the full development cycle:

- Game concept and game design
- Gameplay and systems programming
- UI/UX implementation
- Visual and audio integration
- Progression, scoring and balancing
- Backend/cloud integration
- Android builds and Google Play release
- Ads/monetization integration
- Testing, iteration and maintenance

## Tech Stack

| Area | Technologies |
| --- | --- |
| Engine | Unity 2021.3 LTS |
| Language | C# |
| Architecture | ScriptableObjects, event-driven game channel, manager-based gameplay systems |
| Online Services | Unity Authentication, Cloud Save, Leaderboards |
| Backend | Firebase Realtime Database |
| Mobile | Android / Google Play |
| Additional | Unity Ads / IAP, URP, experimental Unity Netcode |

## Curated Source Code

The files under **source/** are selected parts of the original project that illustrate its architecture and gameplay systems.

| Area | Example |
| --- | --- |
| Event architecture | GameChannel.cs |
| Data-driven design | BlockData.cs, ShapeData.cs, PlayerData.cs |
| Game systems | ComboManager.cs, BlockPowerManager.cs, BossManager.cs |
| Online features | CloudSaveManager.cs, LeaderboardInfo.cs |

The code is preserved primarily as a **historical portfolio sample**. It reflects the state of the project at the time rather than a modernized rewrite.

## Screenshots & Gameplay

Play Store screenshots and gameplay media will be added here from the archived release material.

## Archived Android Build

An archived APK may be provided through GitHub Releases.

Backend-dependent functionality may no longer be available because the original online services are no longer maintained.

## Project Status

**Archived / no longer maintained.**

The game was previously available on Google Play for a limited period. I later stopped active development and maintenance, but kept the project as a personal milestone and technical learning project.

## Repository Scope

This public repository intentionally excludes:

- Firebase/production configuration
- API/backend credentials
- Signing keys and keystores
- Third-party music, sound effects, UI packs and other licensed assets
- Large binary project assets
- The complete historical Unity project

This keeps the repository focused on the parts that are most relevant for a technical portfolio.