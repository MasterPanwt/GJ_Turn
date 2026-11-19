ENGINE FILES

texturepacker/ - TexturePacker JSON-Hash, referencing spritesheets/ PNGs.
For Phaser, PixiJS, Defold, Godot via importer, and Unity via TexturePacker importer.
Frames use alpha > 0 bounds. Empty cells are skipped.
The pivot is bottom-centre (0.5, 1.0) of the original cell, before trimming.

tilesets/ - Packed PNGs, tile index lists, Tiled 1.10 .tsx, Godot 4.x .tres.
Tiles are sorted by file name; groups and sizes stay separate.
Tilesets have no autotile/terrain rules.

animations/ - Horizontal strips, Aseprite JSON-Array, Godot SpriteFrames .tres.
Aseprite JSON supports Aseprite import and many engines.
Existing animal strips are referenced; one resource per animal/type clip.
Walkers use one shared bottom-centred cell and down/left/right/up tags.
Animal fps comes from animations.json; walkers use characters.json fps, else 8 fps.
JSON frame duration is 1000/fps milliseconds. Godot animations loop.

Godot resources work in Godot 4.3 and newer (tested in 4.7).
Drag the pack folders into your Godot 4.x project, then use the .tres resources.
Keep original PNGs and engine files together so relative paths resolve.
Use nearest-neighbour texture filtering for crisp pixel art.
Optional palette files are in ../palette/.
