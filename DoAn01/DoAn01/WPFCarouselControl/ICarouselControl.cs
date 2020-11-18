using System.Windows;

namespace DoAn01.WPFCarouselControl
{
    public interface ICarouselControl
    {
        double Rotate(double startXInScreenCoordinates, double endXInScreenCoordinates);
        void RotateIncrement(int increment);
        void RotateRight();
        void RotateLeft();
        bool ShowRotation { get; set; }
    }
}
