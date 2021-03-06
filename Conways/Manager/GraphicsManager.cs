﻿namespace Conways.Manager
{
    public class GraphicsManager
    {
        private static GraphicsManager _instance;

        public GraphicsManager(int graphicsWidth, int graphicsHeight)
        {
            GraphicsWidth = graphicsWidth;
            GraphicsHeight = graphicsHeight;
        }

        public static GraphicsManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GraphicsManager(800, 600);
                }
                return _instance;
            }
        }

        public int GraphicsWidth { get; set; }
        public int GraphicsHeight { get; set; }
    }
}