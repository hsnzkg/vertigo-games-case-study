using UnityEngine;

namespace Project.Scripts.Utility
{
    public static class VectorUtility
    {
        /// <summary>
        /// Vector axis mapping options for Vector2 to Vector3 conversion
        /// </summary>
        public enum VectorAxisMapping
        {
            XY, XZ, YX, YZ, ZX, ZY
        }

        /// <summary>
        /// Converts Vector2 to Vector3 using specified axis mapping with optional custom z value
        /// </summary>
        public static Vector3 ToVector3(this Vector2 vector2, VectorAxisMapping mapping = VectorAxisMapping.XY, float z = 0f)
        {
            return mapping switch
            {
                VectorAxisMapping.XY => new Vector3(vector2.x, vector2.y, z),
                VectorAxisMapping.XZ => new Vector3(vector2.x, z, vector2.y),
                VectorAxisMapping.YX => new Vector3(vector2.y, vector2.x, z),
                VectorAxisMapping.YZ => new Vector3(z, vector2.x, vector2.y),
                VectorAxisMapping.ZX => new Vector3(vector2.y, z, vector2.x),
                VectorAxisMapping.ZY => new Vector3(z, vector2.y, vector2.x),
                _ => new Vector3(vector2.x, vector2.y, z)
            };
        }

        /// <summary>
        /// Converts Vector2 to Vector3 using XY axes (z = 0)
        /// </summary>
        public static Vector3 ToVector3XY(this Vector2 vector2) => vector2.ToVector3();

        /// <summary>
        /// Converts Vector2 to Vector3 using XZ axes (y = 0)
        /// </summary>
        public static Vector3 ToVector3XZ(this Vector2 vector2) => vector2.ToVector3(VectorAxisMapping.XZ);

        /// <summary>
        /// Converts Vector2 to Vector3 using YX axes (z = 0)
        /// </summary>
        public static Vector3 ToVector3YX(this Vector2 vector2) => vector2.ToVector3(VectorAxisMapping.YX);

        /// <summary>
        /// Converts Vector2 to Vector3 using YZ axes (x = 0)
        /// </summary>
        public static Vector3 ToVector3YZ(this Vector2 vector2) => vector2.ToVector3(VectorAxisMapping.YZ);

        /// <summary>
        /// Converts Vector2 to Vector3 using ZX axes (y = 0)
        /// </summary>
        public static Vector3 ToVector3ZX(this Vector2 vector2) => vector2.ToVector3(VectorAxisMapping.ZX);

        /// <summary>
        /// Converts Vector2 to Vector3 using ZY axes (x = 0)
        /// </summary>
        public static Vector3 ToVector3ZY(this Vector2 vector2) => vector2.ToVector3(VectorAxisMapping.ZY);

        public static Vector3 QuantizeToSign(this ref Vector3 vector)
        {
            vector.x = vector.x > 0f ? 1f : vector.x < 0f ? -1f : 0f;
            vector.y = vector.y > 0f ? 1f : vector.y < 0f ? -1f : 0f;
            vector.z = vector.z > 0f ? 1f : vector.z < 0f ? -1f : 0f;
            return vector;
        }
    }
}
