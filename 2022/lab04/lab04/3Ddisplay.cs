using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace lab04
{
    class Display3D
    {
        public float fieldOfView { get; set; }
        float aspectRatio = 1;
        float nearPlaneDistance = 1;
        float farPlaneDistance = 100;
        Vector3 cameraPosition = new Vector3(3, 0.5f, 0.5f);
        Vector3 cameraTarget = new Vector3(0, 0.5f, 0.5f);
        Vector3 cameraUpVector = new Vector3(0, 0, 1);

        public Display3D(float fieldOfView)
        {
            this.fieldOfView = fieldOfView;       
        }

        public (double, double) GetScreenCoordinates(Vector4 pl, float angle = 0)
        {
            
            Matrix4x4 rotationMatrix = Matrix4x4.CreateRotationX(angle);
            Matrix4x4 viewMatrix = Matrix4x4.CreateLookAt(cameraPosition, cameraTarget, cameraUpVector);
            Matrix4x4 projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearPlaneDistance, farPlaneDistance);
            Vector4 pe = Vector4.Transform(pl, (projectionMatrix * viewMatrix * rotationMatrix));
 
            return (pe.X / pe.W, pe.Y / pe.W);
        }
        
    }
}
