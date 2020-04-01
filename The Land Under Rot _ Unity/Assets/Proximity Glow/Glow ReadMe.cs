// *To create new proximity glowing prefab* 
// 1) Replace sphere mesh and renderer on proximityGlowSample with intended mesh and renderer. 
// 2) Make sure look at constraint on HaloEffect has the camera assigned to it and is active.
// 3) Make sure "Halo Fade" script on HaloEffect and "Proximity Glow" script on parent has the player 
//    assigned to the player variable and the light assigned to the light variable.
// 4) Set "Glow Base Intensity" variable on "Proximity Glow" script on parent to the intended maximum light
//    intensity. The Light will ramp up from 0 to this value as the player approaches the object.
// 5) *To change color of glow* Change color of light on parent and then change color of material base map,
//    specular map, and emission map on HaloEffect to match light.
// 6) Rename parent object and create a new prefab in the project folder and presto, you've done it :^)
// 
