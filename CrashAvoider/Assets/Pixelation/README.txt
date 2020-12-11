Pixelation Camera v1.0 by Tristan Weber

With this asset a second camera can be added to the scene to
pixelize 3D objects. Two ready to drop Pixelation Camera prefabs
are part of the core files ('Pixelation/Core/').
The PixelationCameraFullscreen applies a pixelization effect to
all objects in the scene. The PixelationCamera applies the same
effect only to objects on the Pixelation render layer.
The size of the pixels and the bits per color channel are
controlled via the PixelationPost script directly on the
pixelation cameras.

To enable the pixelation camera, make sure your project has 
a Pixelation render layer. To do so go to 'Edit/Project Settings/
Tags and Layers' and add a new layer named Pixelation to the User
Layers.
Then make sure your main camera has it's Culling Mask set to
everything but the Pixelation layer and your PixelCamera has the
Culling Mask set to nothing but the Pixelation layer.
If unsure consult the PDF manual or contact the author via the
Asset Store.

If you want multiple pixelation effects at the same time (e.g.
with different pixel sizes or different color reductions) you
have to add a separate render layer for each of these.

The pixelation effect does not work in deferred rendering.