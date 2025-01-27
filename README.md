# The Nest
### Corner of the internet for seagulls to rest

![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/larssonludvig/TheNest/main.yml)
[![License](https://img.shields.io/badge/License-Apache_2.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)
![issues](https://img.shields.io/github/issues-raw/larssonludvig/TheNest)

**The Nest** is a website written in Microsoft Blazor webassembly. It is meant to serve as a collection of features that can be of use to different games and communities.

---

## Features
Currently, the site contains a collection of tools that can be used for the game [**The Finals**](https://www.reachthefinals.com/).

|Game|Tool|Descritpion|
|-|-|-|
|The Finals|Loadouts|Allows planning of loadouts with the ability to share loadouts.|
||Randomizer|Loadout randomizer. Allows the user to randomize loadouts for the game fully.|
||Randomizer options|Allows the user to exclude items and gadgets from the randomizer. Options are stored locally in the browser.|
||Leaderboards|Allows the user to view the different leaderboards that are publicly available from [*Embark*](https://www.embark-studios.com/).|
||Maps|__Under Development__. Detailed maps about from the game.

## Planned Changes
* Merge repositories for frontend and backend
* Move backend API from Spring boot to ASP.NET
* Change so the backend can be run as a docker container
* Unify location for items between frontend and backend. Possibly by creating API endpoint
* Create a home page that is not the About page
* Move the site from Github pages to a domain
* General refactorization on the codebase
* Create a database for longterm storage
  * Store min rank of Ruby over time
  * Store history of leaderboard and players

## Licensing
This project, and all code it contains, are licensed under the [*Apache License*](https://www.apache.org/licenses/LICENSE-2.0).
