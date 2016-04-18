using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pick_One.BasicClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pick_One.Levels
{
    static class LevelFactory
    {

        #region Public Methods

        public static List<Tile> GenerateLevel(ContentManager content, Texture2D texture)
        {
            Color[] mapArray = new Color[texture.Width * texture.Height];
            texture.GetData(mapArray); // Places the colors into the array

            var listPosition = new List<Tile>();
            for (int y = 0; y < texture.Height; y++)
            {
                for (int x = 0; x < texture.Width; x++)
                {
                    var color = mapArray[(texture.Width * y) + x];
                    if (color == LevelColorMap.StartPosition)
                    {
                        listPosition.Add(new Tile(content, Tile.TileTypes.StartPosition, x, y));
                    }
                    else if (color == LevelColorMap.EndPosition)
                    {
                        listPosition.Add(new Tile(content, Tile.TileTypes.EndPosition, x, y));
                    }
                    else if (color == LevelColorMap.FloorColor)
                    {
                        listPosition.Add(new Tile(content, Tile.TileTypes.Floor, x, y));
                    }
                    else
                    {
                        if (color == LevelColorMap.UnclimbableWall)
                        {
                            listPosition.Add(new Tile(content, Tile.TileTypes.Unclimbable, x, y));
                        }
                        else
                        {
                            if (color == LevelColorMap.SlowFloor)
                            {
                                listPosition.Add(new Tile(content, Tile.TileTypes.Slow, x, y));
                            }
                            else
                            {
                                // Empty won't be added to the list
                                //listPosition.Add(new Tile(content, Tile.Type.Empty, 0, 0));
                            }
                        }
                    }
                }
            }

            // Need to add a boundary around the entire thing.
            for (int y = -1; y < texture.Height+2; y++)
            {
                for (int x = -1; x < texture.Width+2; x++)
                {
                    if ((x < 0 || x > texture.Width) || (y < 0 || y > texture.Height))
                        listPosition.Add(new Tile(content, Tile.TileTypes.Bounds, x, y));
                }
            }

            return listPosition;
        }

        #endregion

    }
}
