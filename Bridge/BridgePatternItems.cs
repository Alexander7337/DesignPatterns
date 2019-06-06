namespace Bridge
{
    public interface IRenderer
    {
        string WhatToRenderAs { get; }
    }

    public abstract class Shape
    {
        private IRenderer rendering;

        protected Shape(IRenderer rendering)
        {
            this.rendering = rendering;
        }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"Drawing {Name} as {rendering.WhatToRenderAs}";
        }
    }

    public class VectorRenderer : IRenderer
    {
        public string WhatToRenderAs { get { return "pixels"; } }
    }

    public class RasterRenderer : IRenderer
    {
        public string WhatToRenderAs { get { return "lines"; } }
    }

    public class Circle : Shape
    {
        public Circle() : base(new VectorRenderer())
        {
            Name = Name = this.GetType().Name;
        }

        public Circle(IRenderer rendering) : base(rendering)
        {
            Name = Name = this.GetType().Name;
        }
    }

    public class Square : Shape
    {
        public Square() : base(new RasterRenderer())
        {
            Name = this.GetType().Name;
        }

        public Square(IRenderer rendering) : base(rendering)
        {
            Name = this.GetType().Name;
        }
    }
}
