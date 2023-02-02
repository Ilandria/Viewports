# How to run
* Editor) Download/clone the repo and open the root in Unity 2020.3.0f1.
* WebGL) [A WebGL build](https://ilandria.github.io/Viewports/) is live on GitHub Pages - see the running environments from the main page of the repository to access it.
* Standalone) Download/clone the repository and the latest Standalone build can be found & run from the "Build" folder in the root of the repository.

# Controls
* Controls are on-screen in the application.
* Alt-F4 to exit if you're using the Standalone app.

# Notes
* Most of the assessment-related code is in the /Source/OrthoViewport folder and in the root of /Source. Code found in Misc is mostly fun extra stuff.

# Differences from assessment specs
* The viewports are on the right side of the screen to not interfere with the how-to-use instructions.
* The main view doesn't support click & drag because implementing a second ray-tracer for a perspective camera would have been almost entirely duplicate work. The ortho raycaster is using a trick to function quickly: we don't need to know the depth of the object from the viewport location since it's on a 2d plane. However, for the main view to function a separate implementation that does not use this trick would be required. Instead of writing effectively duplicate code, I instead focused on showcasing other uses of the Unity engine such as audio, physics, baked lighting, culling masks, layer masks, etc.
* There is one GetComponent<RectTransform>() being called multiple times instead of once on initialization. This is because I implemented it quickly, then forgot to move it into initialization (Start or Awake) as I was focusing on other, more important parts of the assessment. Of course this would definitely be done only once in a real environment!

# Known issues
* There is a secret feature that does not function in the WebGL build, but this does not interfere with the project itself. If you'd like a surprise, run the Standalone version. The function doesn't work in WebGL simply due to web hosting/pricing/etc. - it's not a big deal to fix if one had access to an AWS/GCP/Azure account.
* You can clip objects out-of-bounds by moving them right to the edge using the viewports. This is because viewport movement does not use physics. If I had more time, I would implement the movement using a physics joint attached between the object and the player's grab point. For now, you just have to restart the application if this happens.
