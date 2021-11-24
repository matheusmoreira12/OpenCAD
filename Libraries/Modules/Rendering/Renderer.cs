using System;
using System.Windows.Media;

namespace Rendering
{
    public abstract class Renderer<T> where T : class
    {
        public Renderer(T target)
        {
            Target = target;
        }

        public abstract void Render(DrawingContext context, Brush fill, Pen stroke);

        public T Target { get; }
    }
}
