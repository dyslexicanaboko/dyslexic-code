using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace ServerOps
{
    public static class Serialization
    {
        //EHH - 03/27/2012 - Added this method
        /// <summary>
        /// Serialize any object that is xml serializable into an XML string
        /// </summary>
        /// <typeparam name="T">any type</typeparam>
        /// <param name="anyObject">any object to be xml serialized</param>
        /// <returns>xml string of the object</returns>
        /// <remarks>This method will not work with the Exception or SoapException classes since they use IDictionary
        /// for their Data property.</remarks>
        public static string SerializeToXmlString<T>(this T anyObject)
        {
            string strXML = string.Empty;

            XmlSerializer ser = new XmlSerializer(typeof(T));

            using (StringWriter sw = new StringWriter())
            {
                ser.Serialize(sw, anyObject);

                strXML = sw.ToString();
            }

            return strXML;
        }

        //EHH - 03/27/2012 - Added this method
        /// <summary>
        /// Take any object that has been xml serialized into a string and deserialize it back into an
        /// object of that type.
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="anyObject">The object of type T</param>
        /// <param name="serializedXmlObject">The string containing the XML of the serialized object</param>
        /// <returns>A new deserialized object</returns>
        public static T DeserializeFromXmlString<T>(this T anyObject, string serializedXmlObject)
        {
            T deserializedObject;

            XmlSerializer ser = new XmlSerializer(typeof(T));

            using (StringReader sr = new StringReader(serializedXmlObject))
            {
                deserializedObject = (T)ser.Deserialize(sr);
            }

            return deserializedObject;
        }
    }

    /// <summary>
    /// Make an exact clone of an object using a serialization method with a binary formatter. This method may be more
    /// expensive to use, so avoid using it in a loop.
    /// </summary>
    /// <typeparam name="T">Any object</typeparam>
    /// <param name="original">The original object to clone</param>
    /// <returns>A exact copy of the original object</returns>
    public static class Cloning
    {
        public static T DeepClone<T>(this T source)
        {
            T clone;

            IFormatter formatter = new BinaryFormatter();

            using (MemoryStream ms = new MemoryStream())
            {
                formatter.Serialize(ms, source);

                ms.Seek(0, SeekOrigin.Begin);

                clone = (T)formatter.Deserialize(ms);
            }

            return clone;
        }
    }
}
