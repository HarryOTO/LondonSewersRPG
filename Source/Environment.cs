using Cocos2D;
using CocosDenshion;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LondonSewersRPG.Source
{
    class Environment
    {
        public Rectangle[,] rectangles;
        public double[,] actualXValues;
        public double[,] actualYValues;
        public int[,] collisionMap;
        public int[,] textureMap;
        public int startX;
        public int startY;
        public int size;
        public Texture2D[] textures;

        public Environment(
            int[,] collisionMap,
            int[,] textureMap,
            int startX,
            int startY,
            int size,
            Texture2D[] textures)
        {

            this.collisionMap = collisionMap;
            this.textureMap = textureMap;
            this.startX = startX;
            this.startY = startY;
            this.size = size;
            this.textures = textures;
            rectangles = new Rectangle[
                this.textureMap.GetLength(0),
                this.textureMap.GetLength(1)
                ];


            actualXValues = new double[
                this.textureMap.GetLength(0),
                this.textureMap.GetLength(1)
                ];


            actualYValues = new double[
                this.textureMap.GetLength(0),
                this.textureMap.GetLength(1)
                ];

            for (int row = 0; row < textureMap.GetLength(0); row++)
            {
                for (int col = 0; col < textureMap.GetLength(1); col++)
                {
                    rectangles[row, col] = new Rectangle(startX + size * col, startY + size * row, size, size);

                    actualXValues[row, col] = rectangles[row, col].X;
                    actualYValues[row, col] = rectangles[row, col].Y;
                }
            }
        }


        public int[] collides(Rectangle gameObject)
        {
            for (int row = 0; row < textureMap.GetLength(0); row++)
            {
                for (int col = 0; col < textureMap.GetLength(1); col++)
                {
                    if (gameObject.Intersects(rectangles[row, col]) && collisionMap[row, col] == 1)
                    {
                        return new int[] { row, col };
                    }
                }
            }
            return new int[] { -1, -1 };
        }

        public void tp(int x, int y)
        {
            for (int row = 0; row < textureMap.GetLength(0); row++)
            {
                for (int col = 0; col < textureMap.GetLength(1); col++)
                {
                    rectangles[row, col].X = x + size * col;
                    rectangles[row, col].Y = y + size * row;
                }
            }
        }

        public void move(double x, double y)
        {
            for (int row = 0; row < textureMap.GetLength(0); row++)
            {
                for (int col = 0; col < textureMap.GetLength(1); col++)
                {
                    actualXValues[row, col] += x;
                    actualYValues[row, col] += y;
                    rectangles[row, col].X = (int)actualXValues[row, col];
                    rectangles[row, col].Y = (int)actualYValues[row, col];
                }
            }
        }


        public virtual void draw(SpriteBatch spriteBatch)
        {
            for (int row = 0; row < textureMap.GetLength(0); row++)
            {
                for (int col = 0; col < textureMap.GetLength(1); col++)
                {
                    spriteBatch.Draw(
                        textures[textureMap[row, col]],
                        rectangles[row, col],
                        Color.White
                        );
                }
            }
        }



    }
}

