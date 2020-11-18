This is the Light version of ProDrawCallOptimizer for you to check and see if
it works on your project.

This Light version supports Diffuse and Bumped Diffuse shaders.
No custom shaders.

To get the full version just get the package from:
https://www.assetstore.unity3d.com/#/content/16538


ProDrawcall optimizer is an editor extension that will help you reduce
the number of draw calls on your project with just a single clicks.

This package lets you remap automatically each of your UV maps on your
meshes to a single atlas, hence having one material with an atlas for all
the meshes that share the same shader.

Have specific / custom shaders?, dont worry, the tool is flexible enough to accept any kind of shader, just add them in the custom shaders tab.

NOTES:
- Easy, learn after watching one of the videos.
- No scripting required.
- Meshes automatically set up and adjusted.
- Full lightmapping support.
- Your source assets will not be touched.
- Full multiple material per game object support
- Examples included for you to see and play.
- Supports tiled materials

/**************************** TUTORIAL ****************************/
For more detailed info on how this package works, you can check this
video on how the tool works:
https://www.youtube.com/watch?v=BFl9o6DsUts

Or if you are more interested in checking how the custom shaders work with
the tool, you can also see this video:
https://www.youtube.com/watch?v=SqCf2AaW98Y&feature=youtu.be

If you already have the tool, then you can check this video to correctly set
your objects to get the most of it:
https://www.youtube.com/watch?v=NBaXorFya8E&feature=youtu.be

Finally if you are having problems with textures looking weird, you can also check
this video on what causes this issue:
https://www.youtube.com/watch?v=SK9NLz6k2D0&feature=youtu.be
/*******************************************************************/

/************* SUPPORT ***************/
Any comments / suggestions / bugs?, drop me a line at:
support@pencilsquaregames.com
/*************************************/

Tips to optimize drawcalls:
- Try to use similar texture sizes for each atlas.
- If your shader uses more than one texture try to make textures have the
same size, else there will be resizing to the main texture.
- Adjust the generated atlas size to the closest generated size in order to
not lose quality on your meshes.
- Avoid Shadows realtime shadows when possible.

