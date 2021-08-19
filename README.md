# BTD6 (Extremely) Raw Tower Stats

See the releases for a zip file for every version from now on (hopefully)

Mod.cs is based from [Timotheeee's model dumper](https://github.com/Timotheeee/btd6_mods/tree/master/model_dumper)

Circular references are caused by a Behaviour having a mutator that references the original Behaviour. The code just gets rid of the mutator's reference to the original Behaviour. [Based of epicalex's method of removing circular references](https://github.com/epicalex4444/btd6_tower_data/tree/main/towerDataCollector)