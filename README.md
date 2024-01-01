# HackerRankTopStories
## Usage
navigate to _{baseUrl}/TopStories?number={numberOfStoriesToShow}_ ie: __localhost:5000/TopStories?number20__ to show 20 tom stories from HackerRank
## What to update
- to make API more responsive Stories can go to cache every 60 seconds - this will reduce waiting for stories to load
- cache stories in some sort of database preferably NoSQL like Redis instead of memory
- move getting stories from hackerrank to its individual class and reference it in StoryProvider
- in future when Api will get bigger move from minimal API to Controllers
