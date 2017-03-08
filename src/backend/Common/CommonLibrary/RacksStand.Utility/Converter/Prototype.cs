using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace RacksStand.Utility.Converter
{
    /// <summary>
    /// This class is used to create a duplicate object or clone of the current object to enhance performance. 
    /// This pattern is used when creation of object is costly or complex.
    /// </summary>
    /// <typeparam name="T">type of class</typeparam>
    [Serializable]
    public abstract class Prototype<T> where T : class, new()
    {
        /// <summary>
        /// create clone of class without field value.
        /// </summary>
        /// <returns></returns>
        public T Clone()
        {
            return new T();
        }
        /// <summary>
        /// Shallow copy.It copies the values of all fields and any references, and returns a reference to this copy. 
        /// </summary>
        /// <returns></returns>
        public T Copy()
        {
            return (T)this.MemberwiseClone();
        }

        /// <summary>
        /// Deep Copy- The implemented class and there reference type propertyies classes must me Serializable.
        /// </summary>
        /// <returns></returns>
        public T DeepCopy()
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Seek(0, SeekOrigin.Begin);
            T copy = (T)formatter.Deserialize(stream);
            stream.Close();
            return copy;
        }
    }
}
